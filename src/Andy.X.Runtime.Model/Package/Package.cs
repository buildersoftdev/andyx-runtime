using System.Text.Json.Serialization;

namespace Andy.X.Runtime.Model.Packages
{
    public class Package
    {
        public string? Id { get; set; }
        public string? Version { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PackageType Type { get; set; }
        public string? Key { get; set; }
        public bool? AcceptLicense { get; set; }


        public Package()
        {
            Type = PackageType.Extension;
            AcceptLicense = false;
        }
    }
}
