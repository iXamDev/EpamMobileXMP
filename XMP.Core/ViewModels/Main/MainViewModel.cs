using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using FlexiMvvm.Collections;
using FlexiMvvm.Commands;
using FlexiMvvm.ViewModels;
using FlexiMvvm.Weak.Subscriptions;
using Xamarin.Essentials;
using XMP.Core.Helpers;
using XMP.Core.Models;
using XMP.Core.Navigation;
using XMP.Core.Services.Abstract;
using XMP.Core.ViewModels.Main.Items;

namespace XMP.Core.ViewModels.Main
{
    public class MainViewModel : LifecycleViewModel
    {
        private EventHandlerWeakEventSubscription<IVacationRequestsManagerService> _vacationsChangedSubcription;

        private EventHandlerWeakEventSubscription<IVacationRequestsManagerService, EventArgs<VacantionRequest>> _vacationChangedSubcription;

        private EventHandlerWeakEventSubscription<IVacationRequestsManagerService, EventArgs<VacantionRequest>> _vacationAddedSubcription;

        private CancellationTokenSource _resetItemsCTS;

        private object _requestItemsSyncRoot = new object();

        private string _userName;

        private ObservableCollection<VacationRequestItemVM> _requestItems;

        private IEnumerable<FilterItemVM> _filterItems;

        public MainViewModel(IVacationRequestsManagerService vacationRequestsManagerService, IVacationRequestsFilterService vacationRequestsFilterService, ISessionService sessionService, IUserDialogs userDialogs, INavigationService navigationService)
        {
            NavigationService = navigationService;

            VacationRequestsManagerService = vacationRequestsManagerService;

            UserDialogs = userDialogs;

            VacationRequestsFilterService = vacationRequestsFilterService;

            SessionService = sessionService;

            ResetItems();
        }

        public ICommand ShowDetailsCmd => CommandProvider.Get<VacationRequestItemVM>(OnShowDetails);

        public ICommand AddCmd => CommandProvider.Get(OnAdd);

        public ICommand FilterCmd => CommandProvider.Get<FilterItemVM>(OnFilter);

        public IEnumerable<FilterItemVM> FilterItems
        {
            get => _filterItems;
            private set => SetValue(ref _filterItems, value, nameof(FilterItems));
        }

        public ObservableCollection<VacationRequestItemVM> RequestItems
        {
            get => _requestItems;
            private set => SetValue(ref _requestItems, value, nameof(RequestItems));
        }

        public Interaction CloseMenuInteraction { get; } = new Interaction();

        public string UserName
        {
            get => _userName;
            private set => SetValue(ref _userName, value, nameof(UserName));
        }

        public string AddButtonTitle => "New";

        public string ScreenTitle => "All Requests";

        protected INavigationService NavigationService { get; }

        protected IVacationRequestsManagerService VacationRequestsManagerService { get; }

        protected IVacationRequestsFilterService VacationRequestsFilterService { get; }

        protected IUserDialogs UserDialogs { get; }

        protected ISessionService SessionService { get; }

        private VacantionRequestFilterType CurrentFilter { get; set; }

        public override async Task InitializeAsync(bool recreated)
        {
            SetFilterItems();

            _vacationsChangedSubcription = new EventHandlerWeakEventSubscription<IVacationRequestsManagerService>(VacationRequestsManagerService, (source, handler) => source.VacationsChanged += handler, (source, handler) => source.VacationsChanged -= handler, VacationsChangedEventHandler);

            _vacationChangedSubcription = new EventHandlerWeakEventSubscription<IVacationRequestsManagerService, EventArgs<VacantionRequest>>(VacationRequestsManagerService, (source, handler) => source.VacationChanged += handler, (source, handler) => source.VacationChanged -= handler, VacationChangedEventHandler);

            _vacationAddedSubcription = new EventHandlerWeakEventSubscription<IVacationRequestsManagerService, EventArgs<VacantionRequest>>(VacationRequestsManagerService, (source, handler) => source.VacationAdded += handler, (source, handler) => source.VacationAdded -= handler, VacationAddedEventHandler);

            var loading = UserDialogs.Loading();

            UserName = SessionService.User?.FullName;

            await base.InitializeAsync(recreated);

            await VacationRequestsManagerService.Sync();

            loading.Dispose();
        }

        private void VacationAddedEventHandler(object sender, EventArgs<VacantionRequest> e)
        => AddVacationRequestIfNeeded(e.Value);

        private void AddVacationRequestIfNeeded(VacantionRequest e)
        {
            if (VacationRequestsFilterService.Filter(e, CurrentFilter))
            {
                var itemVM = SetupRequestItemVM(e);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    lock (_requestItemsSyncRoot)
                        RequestItems.Add(itemVM);
                });
            }
        }

        private void VacationChangedEventHandler(object sender, EventArgs<VacantionRequest> e)
        {
            var shoulBeVisible = VacationRequestsFilterService.Filter(e.Value, CurrentFilter);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                lock (_requestItemsSyncRoot)
                {
                    var item = RequestItems.FirstOrDefault(t => t.Model.LocalId == e.Value.LocalId);

                    if (item != null)
                    {
                        item.SetModel(e.Value);

                        if (!shoulBeVisible)
                            RequestItems.Remove(item);
                    }
                    else if (shoulBeVisible)
                    {
                        var itemVM = SetupRequestItemVM(e.Value);

                        RequestItems.Add(itemVM);
                    }
                }
            });
        }

        private void VacationsChangedEventHandler(object sender, EventArgs e)
        => ResetItems();

        private void OnAdd()
        {
            CloseMenuInteraction.RaiseRequested();

            NavigationService.NavigateToDetails(this, new Details.DetailsParameters());
        }

        private void OnFilter(FilterItemVM filterVM)
        {
            CloseMenuInteraction.RaiseRequested();

            if (CurrentFilter != filterVM.Type)
            {
                CurrentFilter = filterVM.Type;

                ResetItems();
            }
        }

        private void ResetItems()
        {
            CancellationToken cancellationToken;

            lock (_requestItemsSyncRoot)
            {
                _resetItemsCTS?.Cancel();

                _resetItemsCTS?.Dispose();

                _resetItemsCTS = new CancellationTokenSource();

                cancellationToken = _resetItemsCTS.Token;
            }

            Task.Run(() => ResetItems(cancellationToken));
        }

        private async Task ResetItems(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            var allModels = VacationRequestsManagerService.GetVacantionRequests();

            var filterModels = VacationRequestsFilterService.Filter(allModels, CurrentFilter);

            var newItems = filterModels?.Select(SetupRequestItemVM)?.ToList() ?? new List<VacationRequestItemVM>();

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                lock (_requestItemsSyncRoot)
                {
                    RequestItems = new ObservableCollection<VacationRequestItemVM>(newItems);
                }
            });
        }

        private void OnShowDetails(VacationRequestItemVM itemVM)
        {
            CloseMenuInteraction.RaiseRequested();

            NavigationService.NavigateToDetails(this, new Details.DetailsParameters(itemVM.Model.LocalId));
        }

        private VacationRequestItemVM SetupRequestItemVM(VacantionRequest model)
        {
            var itemVM = new VacationRequestItemVM();

            itemVM.SetModel(model);

            return itemVM;
        }

        private FilterItemVM SetupFilterItemVM(VacantionRequestFilterType arg)
        => new FilterItemVM(arg);

        private void SetFilterItems()
        {
            FilterItems = EnumHelper.GetEnumStates<VacantionRequestFilterType>()
                .Select(SetupFilterItemVM)
                .ToArray();

            CurrentFilter = FilterItems.First().Type;
        }
    }
}
