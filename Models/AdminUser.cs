using System.Text.Json.Serialization;

namespace vesalius_m.Models
{
    public class AdminUser
    {
        [JsonPropertyName("admin_id")]
        public int AdminId { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        [JsonIgnore]
        public required string Password { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Resident { get; set; }
        public string? Dob { get; set; }
        public string? Sex { get; set; }
        public string? Address { get; set; }
        public string? ContactNumber { get; set; }
        public string? Passport { get; set; }
        public string? Nationality { get; set; }
        public string? Role { get; set; }
        [JsonPropertyName("userGroupId")]
        public int UserGroupId { get; set; }
        public string? UserGroupName { get; set; }
        public List<AssignBranch>? AdminBranches { get; set; }

        public static AdminUser FromRs(dynamic r)
        {
            return new AdminUser
            {
                AdminId = Convert.ToInt32(r.ADMIN_ID),
                Username = r.USERNAME,
                Email = r.EMAIL,
                Password = r.PASSWORD,
                Title = r.TITLE,
                FirstName = r.FIRST_NAME,
                MiddleName = r.MIDDLE_NAME,
                LastName = r.LAST_NAME,
                Resident = r.RESIDENT,
                Dob = r.DOB,
                Sex = r.SEX,
                Address = r.ADDRESS,
                ContactNumber = r.CONTACT_NUMBER,
                Passport = r.PASSPORT,
                Nationality = r.NATIONALITY,
                Role = r.ROLE,
                UserGroupId = Convert.ToInt32(r.USER_GROUP_ID),
                UserGroupName = r.USER_GROUP_NAME,
            };
        }
    }
}
