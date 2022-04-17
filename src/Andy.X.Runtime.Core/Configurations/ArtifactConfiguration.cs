namespace Andy.X.Runtime.Core.Configurations
{
    public class ArtifactConfiguration
    {
        public string? Name { get; set; }
        public string? Source { get; set; }
        public string? Token { get; set; }
        public bool IsInternal { get; set; }

        public ArtifactConfiguration()
        {
            IsInternal = false;
        }
    }
}
