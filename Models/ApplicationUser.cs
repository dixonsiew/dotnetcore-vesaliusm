using System.Text.Json.Serialization;

namespace vesalius_m.Models
{
    public class ApplicationUser
    {
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public bool IsKidsExplorer { get; set; }
        public bool IsGoldenPearl { get; set; }
        public required string Password { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Resident { get; set; }
        public string? Dob { get; set; }
        public string? Sex { get; set; }
        public string? Race { get; set; }
        public string? Address { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string? CityState { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? ContactNumber { get; set; }
        public string? Passport { get; set; }
        public string? Nationality { get; set; }
        public string? VerificationCode { get; set; }
        public bool FirstTimeLogin { get; set; }
        public bool FirstTimeBiometric { get; set; }
        public string? Role { get; set; }
        public string? MasterPrn { get; set; }
        public string? PlayerId { get; set; }
        public string? MachineId { get; set; }
        public string? RegisterDate { get; set; }
        public string? Inactive { get; set; }
        public int IsLoggedIn { get; set; }
        public string? DateLoggedIn { get; set; }
        public int SignInType { get; set; }
        public string? DocNoSignUp { get; set; }
        public string? FullNameSignUp { get; set; }
        public List<AssignBranch>? UserBranches { get; set; }
        public string? SessionId { get; set; }
        public string? RegisterDateExcel { get; set; }

        public static List<ApplicationUserFamily> ListUserFamily(IEnumerable<dynamic> q)
        {
            return q.Select(o => new ApplicationUserFamily
            {
                AufId = 0,
                UserId = o.USER_ID,
                NokRefNumber = 0,
                IsPatient = true,
                FullName = o.FIRST_NAME,
                Relationship = "Self",
                Prn = o.MASTER_PRN,
                NricPassport = "-",
                DocNumber = "-",
                Dob = "-",
                Gender = o.SEX,
                Nationality = "-",
                ContactNumber = "-",
                Address = "-",
                IsActive = true,
                MaritalStatus = "-",
                Email = "-",
                IsKidsExplorer = o.IS_KIDS_EXPLORER == "Y" ? true : false,
                IsGoldenPearl = o.IS_GOLDEN_PEARL == "Y" ? true : false,
            }).ToList();
        }

        public static List<ApplicationUser> List(IEnumerable<dynamic> q)
        {
            return q.Select(FromRs).ToList();
        }

        public static ApplicationUser FromRs(dynamic r)
        {
            return new ApplicationUser
            {
                UserId = r.USER_ID,
                Username = r.USERNAME,
                Email = r.EMAIL == null ? "-" : r.EMAIL,
                Password = r.PASSWORD,
                Title = r.TITLE,
                FirstName = r.FIRST_NAME,
                MiddleName = r.MIDDLE_NAME,
                LastName = r.LAST_NAME,
                Resident = r.RESIDENT,
                Dob = r.DOB,
                Sex = r.SEX,
                Race = r.RACE == null ? "-" : r.RACE,
                Address = r.ADDRESS,
                Address1 = r.ADDRESS_1,
                Address2 = r.ADDRESS_2,
                Address3 = r.ADDRESS_3,
                CityState = r.CITYSTATE,
                PostalCode = r.POSTCODE,
                Country = r.COUNTRY,
                ContactNumber = r.CONTACT_NUMBER == null ? "-" : r.CONTACT_NUMBER,
                Passport = r.PASSPORT,
                Nationality = r.NATIONALITY,
                VerificationCode = r.VERIFICATION_CODE,
                FirstTimeLogin = r.FIRST_TIME_LOGIN == 1 ? true : false,
                FirstTimeBiometric = r.FIRST_TIME_BIOMETRIC == 1 ? true : false,
                Role = r.ROLE,
                MasterPrn = r.MASTER_PRN,
                PlayerId = r.PLAYER_ID,
                MachineId = r.MACHINE_ID,
                RegisterDate = r.REGISTRATION_DATE_TIME?.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                RegisterDateExcel = r.REGISTRATION_DATE_TIME?.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                Inactive = r.INACTIVE_FLAG,
                SessionId = r.SESSION_ID,
                SignInType = r.SIGN_IN_TYPE,
            };
        }
    }
}
