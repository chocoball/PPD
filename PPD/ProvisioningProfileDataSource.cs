using System;
using AppKit;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace PPD
{
    public class ProvisioningProfileDataSource : NSTableViewDataSource
    {
        public List<ProvisioningProfileData> datas = new List<ProvisioningProfileData>();

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
        }
    }
}
