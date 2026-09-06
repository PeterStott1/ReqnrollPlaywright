namespace ReqnrollPlaywright.Models
{
    public class PlaywrightConfig
    {
        public PlaywrightSettings PlaywrightSettings { get; set; }
        public BrowserSettings Browser { get; set; }
        public ContextSettings Context { get; set; }
        public TimeoutSettings Timeouts { get; set; }
        public TracingSettings Tracing { get; set; }
        public VideoSettings Video { get; set; }
        
    }
}