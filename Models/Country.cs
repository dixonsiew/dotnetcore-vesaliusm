namespace vesalius_m.Models
{
    public class Country
    {
        public required string CountryName { get; set; }
        public required string TelCode { get; set; }
        public required string CountryCode { get; set; }

        public static List<Country> List(IEnumerable<dynamic> q)
        {
            return q.Select(o => new Country
            {
                CountryName = o.COUNTRY_NAME,
                TelCode = o.TEL_CODE,
                CountryCode = o.COUNTRY_CODE,
            }).ToList();
        }
    }
}
