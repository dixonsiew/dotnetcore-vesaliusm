namespace vesalius_m.Models
{
    public class HospitalProfile
    {
        public required string ProfileDesc { get; set; }
        public required string ProfileValue { get; set; }

        public static IEnumerable<HospitalProfile> GetQ(IEnumerable<dynamic> q)
        {
            return q.Select(o => new HospitalProfile
            {
                ProfileDesc = o.PROFILE_DESC,
                ProfileValue = o.PROFILE_VALUE,
            });
        }
    }
}
