using Binance.Net.Objects;
using Bitfinex.Net.Objects;
using Bittrex.Net.Objects;
using Bybit.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using ExchangeClient.Objects;
using Huobi.Net.Objects;
using Kucoin.Net.Objects;

namespace ExchangeClient
{
    internal static class ExClientHelper
    {
        public static ClientOptions? CreateClientOptions(ExClientFactoryOptions options)
        {
            switch (options.Name)
            {
                case "Binance":
                    return new BinanceClientOptions()
                    {
                        ApiCredentials = new BinanceApiCredentials(options.ApiKey, options.SecretKey),
                        Proxy = GetProxy(options),
                        SpotApiOptions = new BinanceApiClientOptions()
                        {
                            BaseAddress = options.Test ? BinanceApiAddresses.TestNet.RestClientAddress : BinanceApiAddresses.Default.RestClientAddress
                        }
                    };
                case "Kucoin":
                    return new KucoinClientOptions()
                    {
                        ApiCredentials = new KucoinApiCredentials(options.ApiKey, options.SecretKey, options.ApiPassPhrase!),
                        Proxy = GetProxy(options),
                        SpotApiOptions = new KucoinRestApiClientOptions()
                        {
                            BaseAddress = options.Test ? KucoinApiAddresses.TestNet.SpotAddress : KucoinApiAddresses.Default.SpotAddress
                        }
                    };
                case "Huobi":
                    return new HuobiClientOptions()
                    {
                        ApiCredentials = new ApiCredentials(options.ApiKey, options.SecretKey),
                        Proxy = GetProxy(options)
                    };
                case "Bitfinex":
                    return new BitfinexClientOptions()
                    {
                        ApiCredentials = new ApiCredentials(options.ApiKey, options.SecretKey),
                        Proxy = GetProxy(options)
                    };
                case "Bittrex":
                    return new BittrexClientOptions()
                    {
                        ApiCredentials = new ApiCredentials(options.ApiKey, options.SecretKey),
                        Proxy = GetProxy(options)
                    };
                case "Bybit":
                    return new BybitClientOptions()
                    {
                        ApiCredentials = new ApiCredentials(options.ApiKey, options.SecretKey),
                        Proxy = GetProxy(options),
                        SpotApiOptions = new RestApiClientOptions()
                        {
                            BaseAddress = options.Test ? BybitApiAddresses.TestNet.SpotRestClientAddress : BybitApiAddresses.Default.SpotRestClientAddress
                        }
                    };
                default:
                    return null;
            }
        }

        private static ApiProxy? GetProxy(ExClientFactoryOptions options)
        {
            return !String.IsNullOrWhiteSpace(options.ProxyHost) && options.ProxyPort > 0
                ? new ApiProxy(options.ProxyHost, options.ProxyPort)
                : null;
        }
    }
}
