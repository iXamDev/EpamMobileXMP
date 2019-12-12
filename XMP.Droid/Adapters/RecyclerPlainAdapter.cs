using System;
using Android.Support.V7.Widget;
using Android.Views;
using FlexiMvvm.Collections;

namespace XMP.Droid.Adapters
{
    public class RecyclerPlainAdapter<THolder> : RecyclerViewObservablePlainAdapter
        where THolder : RecyclerViewObservableViewHolder
    {
        private readonly int _itemLayoutId;

        public RecyclerPlainAdapter(RecyclerView recyclerView, int itemLayoutId)
            : base(recyclerView)
        {
            _itemLayoutId = itemLayoutId;
        }

        private Type HolderType { get; } = typeof(THolder);

        protected override RecyclerViewObservableViewHolder OnCreateItemViewHolder(ViewGroup parent, int viewType)
        {
            var inflater = LayoutInflater.From(parent.Context);

            var itemView = inflater.Inflate(_itemLayoutId, parent, false);

            return Activator.CreateInstance(HolderType, itemView) as RecyclerViewObservableViewHolder;
        }
    }
}
