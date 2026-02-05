namespace vesalius_m.Models
{
    public class CountryTelCode
    {
        public required string CountryName { get; set; }
        public required string TelCode { get; set; }

        public static IEnumerable<CountryTelCode> GetQ(IEnumerable<dynamic> q)
        {
            return q.Select(o => new CountryTelCode
            {
                CountryName = o.COUNTRY_NAME,
                TelCode = o.TEL_CODE
            });
        }
    }
}
