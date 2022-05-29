namespace CoinCheck.Utils {
  public static class Const {

    #region BINANCE
    
    public const string BINANCE_API = "https://api.binance.com";
    public const string BINANCE_TICKER_ALL = "https://api.binance.com/api/v3/ticker/24hr";
    public const string BINANCE_TICKER_SYMBOL = "https://api.binance.com/api/v3/ticker/24hr?symbol=";

    #endregion

    #region GATEIO

    public const string GATEIO_API = "https://api.gateio.ws";
    public const string GATEIO_TICKER_ALL = "https://api.gateio.ws/api/v4/spot/tickers?";
    public const string GATEIO_TICKER_SYMBOL = "https://api.gateio.ws/api/v4/spot/tickers?currency_pair=";

    #endregion

    #region BITSO

    public const string BITSO_API = "https://api.bitso.com";
    public const string BITSO_TICKER_ALL = "https://api.bitso.com/v3/ticker/";
    public const string BITSO_TICKER_SYMBOL = "https://api.bitso.com/v3/ticker/?book=";

    #endregion

    #region MERCADO BITCOIN

    public const string MARKETPLACEBTC_API = "https://api.mercadobitcoin.net";
    //public const string MARKETPLACEBTC_TICKER_ALL = "https://api.mercadobitcoin.net/api/v4/tickers";
    public const string MARKETPLACEBTC_SYMBOL_ALL = "https://api.mercadobitcoin.net/api/v4/symbols";
    public const string MARKETPLACEBTC_SYMBOL = "https://api.mercadobitcoin.net/api/v4/tickers?symbols=";

    #endregion


  }
}
