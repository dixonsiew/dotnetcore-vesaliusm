namespace vesalius_m.Models
{
    public class Country
    {
        public required string CountryName { get; set; }
        public required string TelCode { get; set; }
        public required string CountryCode { get; set; }

        public static List<Country> List(IEnumerable<dynamic> q)
        {
            return q.Select(FromRs).ToList();
        }

        public static Country FromRs(dynamic r)
        {
            return new Country
            {
                CountryName = r.COUNTRY_NAME,
                TelCode = r.TEL_CODE,
                CountryCode = r.COUNTRY_CODE,
            };
        }
    }
}
