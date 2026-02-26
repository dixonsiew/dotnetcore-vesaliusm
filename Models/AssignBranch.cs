using System.Text.Json.Serialization;

namespace vesalius_m.Models
{
    public class AssignBranch
    {
        [JsonPropertyName("assignBranchId")]
        public int AssignBranchId { get; set; }
        public string? Prn { get; set; }
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("adminId")]
        public int AdminId { get; set; }
        [JsonPropertyName("branchId")]
        public int BranchId { get; set; }
        public Branch? Branch { get; set; }

        public static AssignBranch FromRs(dynamic r)
        {
            return new AssignBranch
            {
                AssignBranchId = r.ASSIGN_BRANCH_ID,
                Prn = r.PRN,
                UserId = r.USER_ID,
                AdminId = r.ADMIN_ID,
                BranchId = r.BRANCH_ID,
            };
        }
    }
}
