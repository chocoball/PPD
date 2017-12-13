// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PPD
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        AppKit.NSTableColumn creationDate { get; set; }

        [Outlet]
        AppKit.NSTableColumn expirationDate { get; set; }

        [Outlet]
        AppKit.NSTableColumn name { get; set; }

        [Outlet]
        AppKit.NSTableView table { get; set; }

        [Action ("OnClickDelete:")]
        partial void OnClickDelete (Foundation.NSObject sender);

        [Action ("OnClickRefresh:")]
        partial void OnClickRefresh (Foundation.NSObject sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (creationDate != null) {
                creationDate.Dispose ();
                creationDate = null;
            }

            if (expirationDate != null) {
                expirationDate.Dispose ();
                expirationDate = null;
            }

            if (name != null) {
                name.Dispose ();
                name = null;
            }

            if (table != null) {
                table.Dispose ();
                table = null;
            }
        }
    }
}
