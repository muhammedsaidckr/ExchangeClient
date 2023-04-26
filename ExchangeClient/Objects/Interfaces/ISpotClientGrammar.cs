using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Objects;

namespace ExchangeClient.Objects.Interfaces
{
    public interface ISpotClientGrammar
    {
        /// <summary>Place an order</summary>
        /// <param name="baseAsset">The base asset of the order</param>
        /// <param name="quoteAsset">The quote asset of the order</param>
        /// <param name="side">The side of the order</param>
        /// <param name="type">The type of the order</param>
        /// <param name="quantity">The quantity of the order</param>
        /// <param name="price">The price of the order, only for limit orders</param>
        /// <param name="accountId">[Optional] The account id to place the order on, required for some exchanges, ignored otherwise</param>
        /// <param name="clientOrderId">[Optional] Client specified id for this order</param>
        /// <param name="ct">[Optional] Cancellation token for cancelling the request</param>
        /// <returns>The id of the resulting order</returns>
        Task<Order> PlaceOrderAsync(
            string baseAsset,
            string quoteAsset,
            CommonOrderSide side,
            CommonOrderType type,
            decimal quantity,
            decimal? price = null,
            string? accountId = null,
            string? clientOrderId = null,
            CancellationToken ct = default(CancellationToken));

        /// <summary>Get asset balance</summary>
        /// <param name="asset">Asset [e.g BTC, LTC, USDT] for balance</param>
        /// <param name="accountId">[Optional] The account id to retrieve balances for, required for some exchanges, ignored otherwise</param>
        /// <param name="ct">[Optional] Cancellation token for cancelling the request</param>
        /// <returns></returns>
        Task<Balance> GetBalanceAsync(
            string asset,
            string? accountId = null,
            CancellationToken ct = default(CancellationToken));

        /// <summary>Get balances </summary>
        /// <param name="accountId">[Optional] The account id to retrieve balances for, required for some exchanges, ignored otherwise</param>
        /// <param name="ct">[Optional] Cancellation token for cancelling the request</param>
        /// <returns></returns>
        Task<IEnumerable<Balance>> GetBalancesAsync(string? accountId, CancellationToken ct);

        /// <summary>Get an order by id</summary>
        /// <param name="orderId">The id</param>
        /// <param name="symbol">[Optional] The symbol the order is on, required for some exchanges, ignored otherwise</param>
        /// <param name="ct">[Optional] Cancellation token for cancelling the request</param>
        /// <returns></returns>
        Task<Order> GetOrderAsync(string orderId, string? symbol = null, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get current price of symbol e.g BTCUSDT
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Task<WebCallResult<BinancePrice>> GetPriceAsync(string symbol);

        /// <summary>
        /// Get converted amount of asset
        /// </summary>
        /// <param name="binancePrice"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        decimal GetPriceAmount(BinancePrice binancePrice, decimal quantity);

        /// <summary>
        /// Convert amount to tradeable amount which is necessary for exchanges.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="qtyLength"></param>
        /// <returns></returns>
        decimal ConvertTradeableAmount(decimal amount, int qtyLength);
    }
}
