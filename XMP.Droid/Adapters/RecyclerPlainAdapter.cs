using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using Android.Support.V7.Widget;
using Android.Views;
using FlexiMvvm.Collections;
using JetBrains.Annotations;

namespace XMP.Droid.Adapters
{
    public class RecyclerPlainAdapter<THolder> : RecyclerViewObservablePlainAdapter
        where THolder : RecyclerViewObservableViewHolder
    {
        public RecyclerPlainAdapter(RecyclerView recyclerView, int itemLayoutId) : base(recyclerView)
        {
            this.itemLayoutId = itemLayoutId;
        }

        private Type HolderType { get; } = typeof(THolder);

        private readonly int itemLayoutId;

        protected override RecyclerViewObservableViewHolder OnCreateItemViewHolder(ViewGroup parent, int viewType)
        {
            var inflater = LayoutInflater.From(parent.Context);

            var itemView = inflater.Inflate(itemLayoutId, parent, false);

            return Activator.CreateInstance(HolderType, itemView) as RecyclerViewObservableViewHolder;
        }
    }
}
