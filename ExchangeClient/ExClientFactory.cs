using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using ExchangeClient.Objects;
using ExchangeClient.Objects.Interfaces;

namespace ExchangeClient
{
    public class ExClientFactory : BaseRestClient, IExClientFactory
    {
        private readonly ClientOptions _clientOptions;
        private readonly ExClientFactoryOptions _options;

        public ISpotClientGrammar Client { get; }

        public ExClientFactory(ExClientFactoryOptions options) : base(options.Name, ExClientHelper.CreateClientOptions(options)!)
        {
            _options = options;
            _clientOptions = ExClientHelper.CreateClientOptions(options)!;
            Client = new SpotClientGrammar(options.Name, _clientOptions);
        }

    }
}
