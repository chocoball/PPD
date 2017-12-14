using System;
using AppKit;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Foundation;

namespace PPD
{
    public class ProvisioningProfileDataSource : NSTableViewDataSource
    {
        public List<ProvisioningProfileData> datas = new List<ProvisioningProfileData>();

        private string lastKey;
        private bool lastAscending;

        public ProvisioningProfileDataSource()
        {
        }

        public override nint GetRowCount(NSTableView tableView)
        {
            return datas.Count;
        }


        public void ReadFrom(string dir)
        {
            Console.WriteLine("ReadFrom dir=" + dir);

            datas.Clear();

            if (!Directory.Exists(dir))
            {
                Console.WriteLine("no exist");
                return;
            }

            var files = Directory.GetFiles(dir, "*.mobileprovision");
            if (files.Length <= 0)
            {
                Console.WriteLine("no file .mobileprovision");
                return;
            }

            var regName = new Regex(@"\<key\>Name\</key\>\s*\<string\>(?<name>.+)\</string\>");
            var regCreation = new Regex(@"\<key\>CreationDate\</key\>\s*\<date\>(?<date>.+)\</date\>");
            var regExpiration = new Regex(@"\<key\>ExpirationDate\</key\>\s*\<date\>(?<date>.+)\</date\>");

            foreach (var i in files)
            {
                Console.WriteLine(i);

                var text = File.ReadAllText(i);
                var name = regName.Match(text).Groups["name"].Value;
                var create = regCreation.Match(text).Groups["date"].Value;
                var expiration = regExpiration.Match(text).Groups["date"].Value;

                datas.Add(new ProvisioningProfileData(i, name, create, expiration));
            }

            Sort(lastKey, lastAscending);
        }

        public void Sort(string key, bool ascending)
        {
            lastKey = key;
            lastAscending = ascending;

            switch (key)
            {
                case "Name":
                    if (ascending)
                    {
                        datas.Sort((a, b) => a.Name.CompareTo(b.Name));
                    }
                    else
                    {
                        datas.Sort((a, b) => -1 * a.Name.CompareTo(b.Name));
                    }
                    break;
                case "CreationDate":
                    if (ascending)
                    {
                        datas.Sort((a, b) => a.CreationDate.CompareTo(b.CreationDate));
                    }
                    else
                    {
                        datas.Sort((a, b) => -1 * a.CreationDate.CompareTo(b.CreationDate));
                    }
                    break;
                case "ExpirationDate":
                    if (ascending)
                    {
                        datas.Sort((a, b) => a.ExpirationDate.CompareTo(b.ExpirationDate));
                    }
                    else
                    {
                        datas.Sort((a, b) => -1 * a.ExpirationDate.CompareTo(b.ExpirationDate));
                    }
                    break;
            }
        }

        public override void SortDescriptorsChanged(NSTableView tableView, Foundation.NSSortDescriptor[] oldDescriptors)
        {
            if (oldDescriptors.Length > 0)
            {
                Sort(oldDescriptors[0].Key, oldDescriptors[0].Ascending);
            }
            else
            {
                NSSortDescriptor[] tbSort = tableView.SortDescriptors;
                Sort(tbSort[0].Key, tbSort[0].Ascending);
            }

            tableView.ReloadData();
        }
    }
}
