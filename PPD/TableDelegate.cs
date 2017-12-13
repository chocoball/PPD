using System;
using AppKit;

namespace PPD
{
    public class TableDelegate : NSTableViewDelegate
    {
        private const string CellIdentifier = "PPCell";
        private ProvisioningProfileDataSource dataSource;

        public TableDelegate(ProvisioningProfileDataSource source)
        {
            dataSource = source;
        }

        public override NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            var view = (NSTextField)tableView.MakeView(CellIdentifier, this);
            if (view == null)
            {
                view = new NSTextField();
                view.Identifier = CellIdentifier;
                view.BackgroundColor = NSColor.Clear;
                view.Bordered = false;
                view.Selectable = true;
                view.Editable = false;
            }

            switch (tableColumn.Title)
            {
                case "Name":
                    view.StringValue = dataSource.datas[(int)row].Name;
                    break;
                case "CreationDate":
                    view.StringValue = dataSource.datas[(int)row].CreationDate.ToString();
                    break;
                case "ExpirationDate":
                    view.StringValue = dataSource.datas[(int)row].ExpirationDate.ToString();
                    break;
            }

            return view;
        }

        public override bool ShouldSelectRow(NSTableView tableView, nint row)
        {
            return true;
        }
    }
}
