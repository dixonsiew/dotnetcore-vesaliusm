using System.Text.Json.Serialization;

namespace vesalius_m.Models
{
    public class AppVersion
    {
        public required string LatestVersion { get; set; }
        [JsonPropertyName("osPlatform")]
        public required string OSPlatform { get; set; }
        public required long Status { get; set; }

        public static List<AppVersion> List(IEnumerable<dynamic> q)
        {
            return q.Select(o => new AppVersion
            {
                LatestVersion = o.LATEST_VERSION,
                OSPlatform = o.OS_PLATFORM,
                Status = o.STATUS,
            }).ToList();
        }
    }
}
