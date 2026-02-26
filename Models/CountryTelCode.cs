namespace vesalius_m.Models
{
    public class CountryTelCode
    {
        public required string CountryName { get; set; }
        public required string TelCode { get; set; }

        public static List<CountryTelCode> List(IEnumerable<dynamic> q)
        {
            return q.Select(FromRs).ToList();
        }

        public static CountryTelCode FromRs(dynamic r)
        {
            return new CountryTelCode
            {
                CountryName = r.COUNTRY_NAME,
                TelCode = r.TEL_CODE,
            };
        }
    }
}
