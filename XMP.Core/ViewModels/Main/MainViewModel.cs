using System;
using FlexiMvvm.ViewModels;
using XMP.Core.Navigation;
using System.Windows.Input;
using FlexiMvvm.Commands;
using System.Threading.Tasks;
using System.Collections.Generic;
using XMP.Core.ViewModels.Main.Items;
using FlexiMvvm.Collections;
using XMP.Core.Models;
using System.Linq;
using XMP.Core.Services.Abstract;
using Acr.UserDialogs;
using System.Threading;
using Xamarin.Essentials;
using XMP.Core.Helpers;
using FlexiMvvm.Weak.Subscriptions;

namespace XMP.Core.ViewModels.Main
{
    public class MainViewModel : LifecycleViewModel
    {
        public MainViewModel(IVacationRequestsManagerService vacationRequestsManagerService, IVacationRequestsFilterService vacationRequestsFilterService, ISessionService sessionService, IUserDialogs userDialogs, INavigationService navigationService)
        {
            NavigationService = navigationService;

            VacationRequestsManagerService = vacationRequestsManagerService;

            UserDialogs = userDialogs;

            VacationRequestsFilterService = vacationRequestsFilterService;

            SessionService = sessionService;

            ResetItems();
        }

        private EventHandlerWeakEventSubscription<IVacationRequestsManagerService> vacationsChangedSubcription;
        private EventHandlerWeakEventSubscription<IVacationRequestsManagerService, VacantionRequest> vacationChangedSubcription;
        private EventHandlerWeakEventSubscription<IVacationRequestsManagerService, VacantionRequest> vacationAddedSubcription;

        private CancellationTokenSource resetItemsCTS;

        private object requestItemsSyncRoot = new object();

        private VacantionRequestFilterType CurrentFilter { get; set; }

        protected INavigationService NavigationService { get; }

        protected IVacationRequestsManagerService VacationRequestsManagerService { get; }

        protected IVacationRequestsFilterService VacationRequestsFilterService { get; }

        protected IUserDialogs UserDialogs { get; }

        protected ISessionService SessionService { get; }

        public ICommand ShowDetailsCmd => CommandProvider.Get<VacationRequestItemVM>(OnShowDetails);

        public ICommand AddCmd => CommandProvider.Get(OnAdd);

        public ICommand FilterCmd => CommandProvider.Get<FilterItemVM>(OnFilter);

        private IEnumerable<FilterItemVM> filterItems;
        public IEnumerable<FilterItemVM> FilterItems
        {
            get => filterItems;
            private set => SetValue(ref filterItems, value, nameof(FilterItems));
        }

        private ObservableCollection<VacationRequestItemVM> requestItems;
        public ObservableCollection<VacationRequestItemVM> RequestItems
        {
            get => requestItems;
            private set => SetValue(ref requestItems, value, nameof(RequestItems));
        }

        public Interaction CloseMenuInteraction { get; } = new Interaction();

        private string userName;
        public string UserName
        {
            get => userName;
            private set => SetValue(ref userName, value, nameof(UserName));
        }

        public string AddButtonTitle => "New";

        public string ScreenTitle => "All Requests";

        public override async Task InitializeAsync(bool recreated)
        {
            SetFilterItems();

            vacationsChangedSubcription = new EventHandlerWeakEventSubscription<IVacationRequestsManagerService>(VacationRequestsManagerService, (source, handler) => source.VacationsChanged += handler, (source, handler) => source.VacationsChanged -= handler, VacationsChangedEventHandler);

            vacationChangedSubcription = new EventHandlerWeakEventSubscription<IVacationRequestsManagerService, VacantionRequest>(VacationRequestsManagerService, (source, handler) => source.VacationChanged += handler, (source, handler) => source.VacationChanged -= handler, VacationChangedEventHandler);

            vacationAddedSubcription = new EventHandlerWeakEventSubscription<IVacationRequestsManagerService, VacantionRequest>(VacationRequestsManagerService, (source, handler) => source.VacationAdded += handler, (source, handler) => source.VacationAdded -= handler, VacationAddedEventHandler);

            var loading = UserDialogs.Loading();

            UserName = SessionService.User?.FullName;

            await base.InitializeAsync(recreated);

            await VacationRequestsManagerService.Sync();

            loading.Dispose();
        }

        private void VacationAddedEventHandler(object sender, VacantionRequest e)
        => AddVacationRequestIfNeeded(e);

        private void AddVacationRequestIfNeeded(VacantionRequest e)
        {
            if (VacationRequestsFilterService.Filter(e, CurrentFilter))
            {
                var itemVM = SetupRequestItemVM(e);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    lock (requestItemsSyncRoot)
                        RequestItems.Add(itemVM);
                });
            }
        }

        private void VacationChangedEventHandler(object sender, VacantionRequest e)
        {
            var shoulBeVisible = VacationRequestsFilterService.Filter(e, CurrentFilter);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                lock (requestItemsSyncRoot)
                {
                    var item = RequestItems.FirstOrDefault(t => t.Model.LocalId == e.LocalId);

                    if (item != null)
                    {
                        item.SetModel(e);

                        if (!shoulBeVisible)
                            RequestItems.Remove(item);
                    }
                    else if (shoulBeVisible)
                    {
                        var itemVM = SetupRequestItemVM(e);

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

            lock (requestItemsSyncRoot)
            {
                resetItemsCTS?.Cancel();

                resetItemsCTS?.Dispose();

                resetItemsCTS = new CancellationTokenSource();

                cancellationToken = resetItemsCTS.Token;
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

                lock (requestItemsSyncRoot)
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
