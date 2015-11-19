using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace TableViewDemo
{
    public class ItemSelectedEventArgs : EventArgs
    {
        public int Index { get; private set; }

        public ItemSelectedEventArgs(int index)
        {
            Index = index;
        }
    }

    public class SingleItemSelectionViewController : UITableViewController
    {
        private const string ReuseIdentifier = "StringReuseIdentifier";

        private readonly List<string> _items;

        public event EventHandler<ItemSelectedEventArgs> ItemSelected;

        public SingleItemSelectionViewController(IEnumerable<string> items) : base(UITableViewStyle.Plain)
        {
            _items = new List<string>(items);
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _items.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(ReuseIdentifier);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, ReuseIdentifier);
            }

            cell.TextLabel.Text = _items[indexPath.Row];

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            OnItemSelected(indexPath.Row);

            tableView.DeselectRow(indexPath, animated: true);
        }

        private void OnItemSelected(int index)
        {
            if (ItemSelected != null)
            {
                ItemSelected(this, new ItemSelectedEventArgs(index));
            }
        }
    }
}

