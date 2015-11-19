using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace TableViewDemo
{
    public class MultipleItemSelectionWithSwitchesViewController : MultipleItemSelectionViewControllerBase
    {
        private const string ReuseIdentifier = "SwitchReuseIdentifier";

        public MultipleItemSelectionWithSwitchesViewController(IEnumerable<string> items) : base(items)
        {
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(ReuseIdentifier) as SwitchCell;

            if (cell == null)
            {
                cell = new SwitchCell();
                cell.SwitchValueChanged += HandleItemSwitchValueChanged;
            }

            int index = indexPath.Row;

            cell.Index = index;
            cell.Title = GetItemTitle(index);
            cell.SwitchValue = GetItemSelected(index);

            return cell;
        }

        private void HandleItemSwitchValueChanged(object sender, EventArgs e)
        {
            var switchCell = (SwitchCell)sender;

            SetItemSelected(switchCell.Index, switchCell.SwitchValue);
        }

        private class SwitchCell : UITableViewCell
        {
            private UISwitch _switch;

            public event EventHandler SwitchValueChanged;

            public SwitchCell() : base(UITableViewCellStyle.Default, MultipleItemSelectionWithSwitchesViewController.ReuseIdentifier)
            {
                _switch = new UISwitch();

                AccessoryView = _switch;

                SelectionStyle = UITableViewCellSelectionStyle.None;
            }

            public bool SwitchValue
            {
                get { return _switch.On; }
                set { _switch.On = value; }
            }

            public string Title
            {
                get { return TextLabel.Text; }
                set { TextLabel.Text = value; }
            }

            public int Index { get; set; }

            public override void MovedToWindow()
            {
                base.MovedToWindow();

                if (Window != null)
                {
                    _switch.ValueChanged += HandleValueChanged;
                }
                else
                {
                    _switch.ValueChanged -= HandleValueChanged;
                }
            }

            private void HandleValueChanged(object sender, EventArgs e)
            {
                if (SwitchValueChanged != null)
                {
                    SwitchValueChanged(this, EventArgs.Empty);
                }
            }
        }
    }
}

