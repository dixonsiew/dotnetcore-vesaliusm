using System.Text.Json.Serialization;

namespace vesalius_m.Models
{
    public class ApplicationUserFamily
    {
        [JsonPropertyName("auf_id")]
        public long AufId { get; set; }
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
        public string? PatientPrn { get; set; }
        public long NokRefNumber { get; set; }
        public bool IsPatient { get; set; }
        public string? FullName { get; set; }
        public string? Relationship { get; set; }
        public string? Prn { get; set; }
        [JsonPropertyName("nricPassport")]
        public string? NricPassport { get; set; }
        public string? DocNumber { get; set; }
        public string? Dob { get; set; }
        public string? Gender { get; set; }
        public string? Nationality { get; set; }
        public required string ContactNumber { get; set; }
        public required string Address { get; set; }
        public bool IsActive { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Email { get; set; }
        public bool IsKidsExplorer { get; set; }
        public bool IsGoldenPearl { get; set; }

        public static List<ApplicationUserFamily> List(IEnumerable<dynamic> q)
        {
            return q.Select(FromRs).ToList();
        }

        public static ApplicationUserFamily FromRs(dynamic r)
        {
            return new ApplicationUserFamily
            {
                AufId = r.AUF_ID,
                UserId = r.USER_ID,
                PatientPrn = r.PATIENT_PRN,
                NokRefNumber = r.NOK_REF_NUMBER,
                IsPatient = r.IS_PATIENT == "Y" ? true : false,
                FullName = r.FULLNAME,
                Relationship = r.RELATIONSHIP ?? "-",
                Prn = r.NOK_PRN ?? "-",
                NricPassport = r.NRIC_PASSPORT ?? "-",
                DocNumber = r.DOC_NUMBER,
                Dob = r.DOB ?? "-",
                Gender = r.GENDER,
                Nationality = r.NATIONALITY ?? "-",
                ContactNumber = r.CONTACT_NUMBER ?? "-",
                Address = r.ADDRESS ?? "-",
                IsActive = r.IS_ACTIVE == "Y" ? true : false,
                MaritalStatus = r.MARITAL_STATUS ?? "-",
                Email = r.EMAIL ?? "-",
                IsKidsExplorer = r.IS_KIDS_EXPLORER == "Y" ? true : false,
                IsGoldenPearl = r.IS_GOLDEN_PEARL == "Y" ? true : false,
            };
        }
    }
}
