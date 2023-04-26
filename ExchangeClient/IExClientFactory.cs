using ExchangeClient.Objects.Interfaces;

namespace ExchangeClient
{
    public interface IExClientFactory
    {
        public ISpotClientGrammar Client { get; }
    }
}