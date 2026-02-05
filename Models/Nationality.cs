using System.Text.Json.Serialization;

namespace vesalius_m.Models
{
    public class Nationality
    {
        [JsonPropertyName("nationality")]
        public required string Nationalityx { get; set; }

        public static IEnumerable<Nationality> GetQ(IEnumerable<dynamic> q)
        {
            return q.Select(o => new Nationality
            {
                Nationalityx = o.NATIONALITY
            });
        }
    }
}
