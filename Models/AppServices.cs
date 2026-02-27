namespace vesalius_m.Models
{
    public class AppServices
    {
        public required string ServiceName { get; set; }
        public required string ServiceImage { get; set; }

        public static List<AppServices> List(IEnumerable<dynamic> q)
        {
            return q.Select(FromRs).ToList();
        }

        public static AppServices FromRs(dynamic r)
        {
            return new AppServices
            {
                ServiceName = r.SERVICE_NAME,
                ServiceImage = r.SERVICE_IMAGE,
            };
        }
    }
}
