namespace Andy.X.Runtime.Model.Packages
{
    public class PackageSettings
    {
        public bool AutoRestart { get; set; }
        public bool SkipMessageIfFails { get; set; }

        public PackageSettings()
        {
            AutoRestart = true;
            SkipMessageIfFails = true;
        }
    }
}
