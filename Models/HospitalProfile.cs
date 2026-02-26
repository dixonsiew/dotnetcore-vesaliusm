namespace vesalius_m.Models
{
    public class HospitalProfile
    {
        public required string ProfileDesc { get; set; }
        public required string ProfileValue { get; set; }

        public static List<HospitalProfile> List(IEnumerable<dynamic> q)
        {
            return q.Select(FromRs).ToList();
        }

        public static HospitalProfile FromRs(dynamic r)
        {
            return new HospitalProfile
            {
                ProfileDesc = r.PROFILE_DESC,
                ProfileValue = r.PROFILE_VALUE,
            };
        }
    }
}
