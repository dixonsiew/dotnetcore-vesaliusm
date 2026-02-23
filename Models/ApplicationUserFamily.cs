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
        public required string FullName { get; set; }
        public required string Relationship { get; set; }
        public required string Prn { get; set; }
        [JsonPropertyName("nricPassport")]
        public required string NricPassport { get; set; }
        public required string DocNumber { get; set; }
        public required string Dob { get; set; }
        public required string Gender { get; set; }
        public required string Nationality { get; set; }
        public required string ContactNumber { get; set; }
        public required string Address { get; set; }
        public bool IsActive { get; set; }
        public required string MaritalStatus { get; set; }
        public required string Email { get; set; }
        public bool IsKidsExplorer { get; set; }
        public bool IsGoldenPearl { get; set; }

        public static List<ApplicationUserFamily> List(IEnumerable<dynamic> q)
        {
            return q.Select(o => new ApplicationUserFamily
            {
                AufId = o.AUF_ID,
                UserId = o.USER_ID,
                PatientPrn = o.PATIENT_PRN,
                NokRefNumber = o.NOK_REF_NUMBER,
                IsPatient = o.IS_PATIENT == "Y" ? true : false,
                FullName = o.FULLNAME,
                Relationship = o.RELATIONSHIP == null ? "-" : o.RELATIONSHIP,
                Prn = o.NOK_PRN == null ? "-" : o.NOK_PRN,
                NricPassport = o.NRIC_PASSPORT == null ? "-" : o.NRIC_PASSPORT,
                DocNumber = o.DOC_NUMBER,
                Dob = o.DOB == null ? "-" : o.DOB,
                Gender = o.GENDER,
                Nationality = o.NATIONALITY == null ? "-" : o.NATIONALITY,
                ContactNumber = o.CONTACT_NUMBER == null ? "-" : o.CONTACT_NUMBER,
                Address = o.ADDRESS == null ? "-" : o.ADDRESS,
                IsActive = o.IS_ACTIVE == "Y" ? true : false,
                MaritalStatus = o.MARITAL_STATUS == null ? "-" : o.MARITAL_STATUS,
                Email = o.EMAIL == null ? "-" : o.EMAIL,
                IsKidsExplorer = o.IS_KIDS_EXPLORER == "Y" ? true : false,
                IsGoldenPearl = o.IS_GOLDEN_PEARL == "Y" ? true : false,
            }).ToList();
        }
    }
}
