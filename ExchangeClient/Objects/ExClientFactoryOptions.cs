namespace ExchangeClient.Objects
{
    public class ExClientFactoryOptions
    {
        public string Name { get; set; }
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
#nullable enable
        public string? ApiPassPhrase { get; set; }
        public string? AccountId { get; set; }
        public string? ProxyHost { get; set; }
        public int ProxyPort { get; set; } = 0;
#nullable disable
        public bool Test { get; set; } = false;

    }
}
