# Exchange Client 

Gather all exchange which is created by JKorf. It is based on JKorf [CryptoExchange](https://github.com/JKorf/CryptoExchange.Net). 

- Common client functions gathered in this package. 
- You can easily create factory via Binance, Bittrex, Bybit, etc. 
- Easily trade through 6 exchange


## Example

```
var exClientOptions = new ExClientFactoryOptions()
{
    SecretKey = "YOUR-SECRET-KEY",
    ApiKey = "YOUR-API-KEY",
    ApiPassPhrase = "",
    Name = "Exchange-Name", // Binance
    Test = true // if it has testnet you can set it the true to trade on testnet
}
```
```
factory = new  new ExClientFactory(exClientOptions);
```

### Trade 
```
factory.Client.PlaceOrderAsync("BTC", "USDT", CommonOrderSide.Buy, CommonOrderType.Market, 0.001M);
```

    