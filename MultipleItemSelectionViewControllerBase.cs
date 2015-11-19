using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace TableViewDemo
{
    public class ItemsSelectedEventArgs : EventArgs
    {
        public IEnumerable<int> SelectedIndexes { get; private set; }

        public ItemsSelectedEventArgs(IEnumerable<int> selectedIndexes)
        {
            SelectedIndexes = selectedIndexes.ToList();
        }
    }

    public abstract class MultipleItemSelectionViewControllerBase : UITableViewController
    {
        private class Item
        {
            public string Title { get; private set; }

            public bool Selected { get; set; }

            public int Index { get; private set; }

            public Item(string title, int index)
            {
                Title = title;
                Index = index;
            }
        }

        private readonly List<Item> _items;

        public event EventHandler<ItemsSelectedEventArgs> ItemsSelected;

        protected MultipleItemSelectionViewControllerBase(IEnumerable<string> items) : base(UITableViewStyle.Plain)
        {
            int index = 0;
            _items = items.Select(item => new Item(item, index++)).ToList();

            NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Done, HandleDoneButtonPressed);
        }

        protected void SetItemSelected(int index, bool selected)
        {
            _items[index].Selected = selected;
        }

        protected void ToggleItemSelected(int index)
        {
            SetItemSelected(index, !GetItemSelected(index));
        }

        protected bool GetItemSelected(int index)
        {
            return _items[index].Selected;
        }

        protected string GetItemTitle(int index)
        {
            return _items[index].Title;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _items.Count;
        }

        private void HandleDoneButtonPressed(object sender, EventArgs e)
        {
            if (ItemsSelected != null)
            {
                ItemsSelected(this, new ItemsSelectedEventArgs(_items.Where(item => item.Selected).Select(item => item.Index)));
            }
        }
    }
}

