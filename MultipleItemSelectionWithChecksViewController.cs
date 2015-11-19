using System.Collections.Generic;
using Foundation;
using UIKit;

namespace TableViewDemo
{
    public class MultipleItemSelectionWithChecksViewController : MultipleItemSelectionViewControllerBase
    {
        private const string ReuseIdentifier = "CheckReuseIdentifier";

        public MultipleItemSelectionWithChecksViewController(IEnumerable<string> items) : base(items)
        {
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(ReuseIdentifier);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, ReuseIdentifier);
            }

            var index = indexPath.Row;

            cell.TextLabel.Text = GetItemTitle(index);

            bool selected = GetItemSelected(index);
            cell.Accessory = selected ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            ToggleItemSelected(indexPath.Row);

            tableView.ReloadRows(new [] { indexPath }, UITableViewRowAnimation.Automatic);

            tableView.DeselectRow(indexPath, animated: true);
        }
    }
}

