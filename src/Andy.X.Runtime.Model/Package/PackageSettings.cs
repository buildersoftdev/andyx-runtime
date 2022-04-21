namespace Andy.X.Runtime.Model.Packages
{
    public class PackageSettings
    {
        public bool AutoRestart { get; set; }
        public bool SkipMessageIfFails { get; set; }

        // default is 60 seconds
        public long Timeout { get; set; }

        public PackageSettings()
        {
            AutoRestart = true;
            SkipMessageIfFails = true;
            Timeout = new TimeSpan(0, 1, 0).Seconds;
        }
    }
}
