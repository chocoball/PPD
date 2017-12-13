using System;
using System.Collections.Generic;
using System.IO;

using AppKit;
using Foundation;

namespace PPD
{
    public partial class ViewController : NSViewController
    {
        private string readDir = "/Library/MobileDevice/";

        private ProvisioningProfileDataSource source = new ProvisioningProfileDataSource();

        public ViewController(IntPtr handle) : base(handle)
        {
            var mng = new NSFileManager();

            readDir = mng.GetHomeDirectoryForCurrentUser() + "Library/MobileDevice/Provisioning Profiles";
            readDir = readDir.Replace("file://", "");

            ReadConf();
            source.ReadFrom(readDir);
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            table.DataSource = source;
            table.Delegate = new TableDelegate(source);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
        }

        partial void OnClickDelete(Foundation.NSObject sender)
        {
            Console.WriteLine("OnClickDelete");

            if (table.SelectedRowCount <= 0)
            {
                return;
            }

            using (var alert = new NSAlert())
            {
                alert.InformativeText = "削除する？";
                alert.AddButton("はい");
                alert.AddButton("いいえ");
                var ret = alert.RunSheetModal(NSApplication.SharedApplication.MainWindow);
                if (ret != (int)NSAlertButtonReturn.First)
                {
                    return;
                }
            }

            var datas = new List<ProvisioningProfileData>();
            foreach (var i in table.SelectedRows)
            {
                Console.WriteLine("Selected Row = " + i);
                datas.Add(source.datas[(int)i]);
            }

            foreach (var i in datas)
            {
                if (File.Exists(i.Path))
                {
                    File.Delete(i.Path);
                }
                source.datas.Remove(i);
            }

            table.ReloadData();
        }

        partial void OnClickRefresh(Foundation.NSObject sender)
        {
            Console.WriteLine("OnClickRefresh");

            source.ReadFrom(readDir);
            table.ReloadData();
        }



        private void ReadConf()
        {
            var path = "~/.ppm.conf";
            if (!File.Exists(path))
            {
                return;
            }
            var lines = File.ReadAllLines(path);
            foreach (var i in lines)
            {
                var s = i.Split('=');
                if (s.Length != 2)
                {
                    continue;
                }
                if (s[0].Trim() == "READ_DIR")
                {
                    readDir = s[1].Trim();
                }
            }
        }
    
        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }
    }
}
