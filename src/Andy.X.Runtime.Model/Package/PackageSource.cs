namespace Andy.X.Runtime.Model.Packages
{
    public class PackageSource
    {
        //andyx
        public string? Tenant { get; set; }
        public string? Product { get; set; }
        public string? Component { get; set; }

        //andy x, kafka, pulsar
        public string? Topic { get; set; }

        // this is valid only if Tenant, Product and Component are not filled, the engine will take PhysicalPathTopic
        public string? PhysicalPathTopic { get; set; }


        // andy x
        public string? TenantToken { get; set; }
        public string? ComponentToken { get; set; }

        // kafka, pulsar
        public string? ApiKey { get; set; }
    }
}
