using System;
using System.Collections.Specialized;
using FlexiMvvm.Collections;
using Foundation;
using UIKit;
using XMP.IOS.Views.Main.Cells;

namespace XMP.IOS.Views.Main.Source
{
    public class ContentVacationRequestItemsSource : TableViewObservablePlainSource
    {
        public ContentVacationRequestItemsSource(UITableView tableView)
            : base(tableView, vm => ContentVacationRequestItemTableViewCell.CellId)
        {
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = base.GetCell(tableView, indexPath);

            UpdateCellDevider(cell, indexPath, RowsInSection(tableView, DefaultSection));

            return cell;
        }

        protected override void ReloadSection(nint section, NotifyCollectionChangedEventArgs args, UITableViewRowAnimation withRowAnimation = UITableViewRowAnimation.Automatic)
        {
            base.ReloadSection(section, args, withRowAnimation);

            if (TableViewWeakReference.TryGetTarget(out var tableView))
            {
                var totalItemsCount = RowsInSection(tableView, DefaultSection);

                foreach (var cell in tableView.VisibleCells)
                {
                    var indexPath = tableView.IndexPathForCell(cell);

                    UpdateCellDevider(cell, indexPath, totalItemsCount);
                }
            }
        }

        private static void UpdateCellDevider(UITableViewCell cell, NSIndexPath indexPath, nint totalCount)
        {
            if (cell is ContentVacationRequestItemTableViewCell contentVacationRequestItemTableViewCell)
                contentVacationRequestItemTableViewCell.View.LastItemDeviderView.Hidden = indexPath.Row + 1 != totalCount;
        }
    }
}
