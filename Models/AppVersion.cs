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
            return q.Select(FromRs).ToList();
        }

        public static AppVersion FromRs(dynamic r)
        {
            return new AppVersion
            {
                LatestVersion = r.LATEST_VERSION,
                OSPlatform = r.OS_PLATFORM,
                Status = r.STATUS,
            };
        }
    }
}
