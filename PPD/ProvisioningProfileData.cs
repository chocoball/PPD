using System;
namespace PPD
{
    public class ProvisioningProfileData
    {
        public string Path { get; protected set; }
        public string Name { get; protected set; }
        public DateTime CreationDate { get; protected set; }
        public DateTime ExpirationDate { get; protected set; }

        public ProvisioningProfileData()
        {
        }

        public ProvisioningProfileData(string path, string name, string creation, string expiration)
        {
            Path = path;
            Name = name;
            CreationDate = DateTime.Parse(creation);
            ExpirationDate = DateTime.Parse(expiration);
        }
    }
}
