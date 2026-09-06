namespace ReqnrollPlaywright.Models
{

    public class ContextSettings
    {
        public int ViewportWidth { get; set; }
        public int ViewportHeight { get; set; }
        public bool IgnoreHttpsErrors { get; set; }
        public string Locale { get; set; }
        public string TimezoneId { get; set; }
    }
}