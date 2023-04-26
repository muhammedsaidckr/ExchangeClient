using Binance.Net.Clients;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects;
using Binance.Net.Objects.Models.Spot;
using Bitfinex.Net.Clients;
using Bitfinex.Net.Interfaces.Clients;
using Bitfinex.Net.Objects;
using Bittrex.Net.Clients;
using Bittrex.Net.Interfaces.Clients;
using Bittrex.Net.Objects;
using Bybit.Net.Clients;
using Bybit.Net.Interfaces.Clients;
using Bybit.Net.Objects;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Interfaces.CommonClients;
using CryptoExchange.Net.Objects;
using ExchangeClient.Objects.Interfaces;
using Huobi.Net.Clients;
using Huobi.Net.Interfaces.Clients;
using Huobi.Net.Objects;
using Kucoin.Net.Clients;
using Kucoin.Net.Interfaces.Clients;
using Kucoin.Net.Objects;

namespace ExchangeClient.Objects
{
    public class SpotClientGrammar : ISpotClientGrammar
    {
        private string _exchangeName { get; }
        private ISpotClient SpotClient { get; }
        private readonly ClientOptions _clientOptions;


        public SpotClientGrammar(string name, ClientOptions clientOptions)
        {
            _exchangeName = name;
            _clientOptions = clientOptions;
            SpotClient = SpotClientApi();
        }

        private ISpotClient SpotClientApi()
        {
            switch (_exchangeName)
            {
                case "Binance":
                    return (ISpotClient)BinanceClient().SpotApi;
                case "Huobi":
                    return (ISpotClient)HuobiClient().SpotApi;
                case "Kucoin":
                    return (ISpotClient)KucoinClient().SpotApi;
                case "Bitfinex":
                    return (ISpotClient)BitfinexClient().SpotApi;
                case "Bittrex":
                    return (ISpotClient)BittrexClient().SpotApi;
                case "Bybit":
                    return (ISpotClient)BybitClient().SpotApiV1;
                default:
                    throw new InvalidOperationException("Unexpected client");
            }
        }

        private IBinanceClient BinanceClient()
        {
            return new BinanceClient((BinanceClientOptions)_clientOptions);
        }
        private IKucoinClient KucoinClient()
        {
            return new KucoinClient((KucoinClientOptions)_clientOptions);
        }
        private IHuobiClient HuobiClient()
        {
            return new HuobiClient((HuobiClientOptions)_clientOptions);
        }

        private IBitfinexClient BitfinexClient()
        {
            return new BitfinexClient((BitfinexClientOptions)_clientOptions);
        }

        private IBittrexClient BittrexClient()
        {
            return new BittrexClient((BittrexClientOptions)_clientOptions);
        }

        private IBybitClient BybitClient()
        {
            return new BybitClient((BybitClientOptions)_clientOptions);
        }

        public async Task<Order> PlaceOrderAsync(string baseAsset, string quoteAsset, CommonOrderSide side, CommonOrderType type, decimal quantity, decimal? price, string? accountId, string? clientOrderId, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(baseAsset) && string.IsNullOrWhiteSpace(quoteAsset))
                throw new ArgumentException(nameof(baseAsset) + " required for Binance " + nameof(ISpotClient.PlaceOrderAsync), nameof(baseAsset));

            var symbol = SpotClient.GetSymbolName(baseAsset, quoteAsset);
            var order = await SpotClient.PlaceOrderAsync(symbol, side, type, quantity, price, accountId: accountId, clientOrderId: clientOrderId, ct).ConfigureAwait(false);
            if (!order)
                return new Order();

            var tradeInfo = await GetOrderAsync(order.Data.Id, symbol, ct);

            return tradeInfo;
        }

        public async Task<Balance> GetBalanceAsync(
            string asset,
            string? accountId = null,
            CancellationToken ct = default(CancellationToken))
        {
            var balances = await SpotClient.GetBalancesAsync(accountId, ct).ConfigureAwait(false);

            var balance = balances.Data.FirstOrDefault(t => t.Asset == "USDT")!;

            return balance;
        }

        public async Task<IEnumerable<Balance>> GetBalancesAsync(string? accountId, CancellationToken ct)
        {
            var balances = await SpotClient.GetBalancesAsync(accountId, ct).ConfigureAwait(false);

            return balances.Data;
        }

        public async Task<Order> GetOrderAsync(string orderId, string? symbol, CancellationToken ct)
        {
            var order = await SpotClient.GetOrderAsync(orderId, symbol!, ct: ct).ConfigureAwait(false);

            return order.Data;
        }

        public async Task<WebCallResult<BinancePrice>> GetPriceAsync(string symbol)
        {
            if (_exchangeName != "Binance")
            {
                throw new InvalidOperationException("Exchange must be binance to get price");
            }
            var binancePrice = await BinanceClient().SpotApi.ExchangeData.GetPriceAsync(symbol).ConfigureAwait(false);

            return binancePrice.As(new BinancePrice()
            {
                Price = binancePrice.Data.Price,
                Symbol = binancePrice.Data.Symbol,
                Timestamp = binancePrice.Data.Timestamp
            });
        }

        public decimal GetPriceAmount(BinancePrice binancePrice, decimal quantity)
        {
            return binancePrice.Price * quantity;
        }

        public decimal ConvertTradeableAmount(decimal amount, int qtyLength)
        {
            return Math.Floor(amount * Convert.ToInt64(Math.Pow(10, qtyLength))) / Convert.ToInt64(Math.Pow(10, qtyLength));
        }
    }
}
