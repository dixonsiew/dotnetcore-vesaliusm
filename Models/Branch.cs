using System.Text.Json.Serialization;

namespace vesalius_m.Models
{
    public class Branch
    {
        public int BranchId { get; set; }
        [JsonIgnore]
        public required string Url { get; set; }
        [JsonIgnore]
        public required string Passcode { get; set; }
        public required string BranchName { get; set; }

        public static Branch FromRs(dynamic r)
        {
            return new Branch
            {
                BranchId = r.BRANCH_ID,
                Url = r.URL,
                Passcode = r.PASSCODE,
                BranchName = r.BRANCH_NAME,
            };
        }
    }
}
