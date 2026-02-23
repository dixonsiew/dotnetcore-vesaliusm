using System.Text.Json.Serialization;

namespace vesalius_m.Models
{
    public class ApplicationUser
    {
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public bool IsKidsExplorer { get; set; }
        public bool IsGoldenPearl { get; set; }
        public required string Password { get; set; }
        public required string Title { get; set; }
        public required string FirstName { get; set; }
        public required string MiddleName { get; set; }
        public required string LastName { get; set; }
        public string? FullName { get; set; }
        public required string Resident { get; set; }
        public required string Dob { get; set; }
        public required string Sex { get; set; }
        public required string Race { get; set; }
        public required string Address { get; set; }
        public required string Address1 { get; set; }
        public required string Address2 { get; set; }
        public required string Address3 { get; set; }
        public required string CityState { get; set; }
        public required string PostalCode { get; set; }
        public required string Country { get; set; }
        public required string ContactNumber { get; set; }
        public required string Passport { get; set; }
        public required string Nationality { get; set; }
        public required string VerificationCode { get; set; }
        public bool FirstTimeLogin { get; set; }
        public bool FirstTimeBiometric { get; set; }
        public required string Role { get; set; }
        public required string MasterPrn { get; set; }
        public required string PlayerId { get; set; }
        public required string MachineId { get; set; }
        public required string RegisterDate { get; set; }
        public required string Inactive { get; set; }
        public int IsLoggedIn { get; set; }
        public string? DateLoggedIn { get; set; }
        public int SignInType { get; set; }
        public string? DocNoSignUp { get; set; }
        public string? FullNameSignUp { get; set; }
        //public List<AssignBranch>? UserBranches { get; set; }
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
            return q.Select(o => new ApplicationUser
            {
                UserId = o.USER_ID,
                Username = o.USERNAME,
                Email = o.EMAIL == null ? "-" : o.EMAIL,
                Password = o.PASSWORD,
                Title = o.TITLE,
                FirstName = o.FIRST_NAME,
                MiddleName = o.MIDDLE_NAME,
                LastName = o.LAST_NAME,
                Resident = o.RESIDENT,
                Dob = o.DOB,
                Sex = o.SEX,
                Race = o.RACE == null ? "-" : o.RACE,
                Address = o.ADDRESS,
                Address1 = o.ADDRESS_1,
                Address2 = o.ADDRESS_2,
                Address3 = o.ADDRESS_3,
                CityState = o.CITYSTATE,
                PostalCode = o.POSTCODE,
                Country = o.COUNTRY,
                ContactNumber = o.CONTACT_NUMBER == null ? "-" : o.CONTACT_NUMBER,
                Passport = o.PASSPORT,
                Nationality = o.NATIONALITY,
                VerificationCode = o.VERIFICATION_CODE,
                FirstTimeLogin = o.FIRST_TIME_LOGIN == 1 ? true : false,
                FirstTimeBiometric = o.FIRST_TIME_BIOMETRIC == 1 ? true : false,
                Role = o.ROLE,
                MasterPrn = o.MASTER_PRN,
                PlayerId = o.PLAYER_ID,
                MachineId = o.MACHINE_ID,
                RegisterDate = o.REGISTRATION_DATE_TIME,
                RegisterDateExcel = o.REGISTRATION_DATE_TIME,
                Inactive = o.INACTIVE_FLAG,
                SessionId = o.SESSION_ID,
                SignInType = o.SIGN_IN_TYPE,
            }).ToList();
        }


    }
}
