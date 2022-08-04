using CoinCheck.Models;
using CoinCheck.Models.Binance;
using CoinCheck.Models.Bitso;
using CoinCheck.Models.GateIO;
using CoinCheck.Models.MarketplaceBTC;
using CoinCheck.Repository;
using CoinCheck.Utils;
using PublicUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CoinCheck.Services {
  public class ExecuteChange: IExecuteChange {
    private readonly ICoin _coin;
    public ExecuteChange() {
      _coin = new Coin();
    }

    public ExecDetails Run(IdentityAPI key) {
      int countNews = 0, countUpdated = 0, countDeleted = 0;
      List<CoinModel> apiResponse = ProccessJsonObject(key);
      double percent = ConfigurationManager.AppSettings["percentChange"].AsDouble();

      if(percent.IsDefault()) {
        percent = 0.05;
        Log.Add("THE DEFAULT IS INVALID. VALUE MODIFIED TO 0.5", "EXECUTECHANGE");
      }

      if(apiResponse == null || apiResponse.Count < 1)
        throw new Exception($"{nameof(apiResponse)} is null or is empty list");

      foreach(CoinModel coinApi in apiResponse) {
        try {
          CoinModel coinExists = _coin.Get(coinApi);

          if(coinExists == null) {
            DateTime now = DateTime.Now;
            countNews++;
            coinExists = coinApi;
            coinExists.CaptureDate = now;
            coinExists.LastUpdate = now;

            _coin.Insert(coinExists);

            Log.Add($"ADD COIN FOR: {coinExists.Symbol}", "COIN DAL");
            continue;
          }

          /* The following calculation is done to identify if there has been a significant change in the values
           * ​​so that they are changed in the database to avoid spamming insignificant updates. The percentage can be changed in App.config.*/

          double baseTempCoinExists = Math.Abs(coinExists.LastPrice.AsDouble());
          double currentMaxPrice = baseTempCoinExists + (baseTempCoinExists * percent);
          double currentMinPrice = baseTempCoinExists - (baseTempCoinExists * percent);

          double apiCurrentPrice = Math.Abs(coinApi.LastPrice.AsDouble());

          if((currentMaxPrice < apiCurrentPrice) || (currentMinPrice > apiCurrentPrice)) {
            countUpdated++;
            coinExists.LastUpdate = DateTime.Now;

            _coin.Update(coinExists);

            Log.Add($"THE {coinApi.Symbol} HAD ITS PRICE CHANGED FROM {coinExists.LastPrice} TO {coinApi.LastPrice}", "EXECUTECHANGE");
            continue;
          }
        } catch(Exception ex) {
          Log.Add($"FAILED WHILE CHECKING ITEM {coinApi.Symbol} - {ex.Message}", "EXECUTECHANGE");
        }



      }

      return new(countNews, countUpdated, countDeleted);
    }

    public List<CoinModel> ProccessJsonObject(IdentityAPI key) {
      List<CoinModel> response = new();
      try {
        

        #region GATEIO CONVERT

        if(IdentityAPI.GateIO == key) {
          string url = Const.GATEIO_TICKER_ALL;

          foreach(GateIoCoinModel gateioCoin in AppAux.GetObjFromJsonAPI<List<GateIoCoinModel>>(url)) {
            string name = gateioCoin.currency_pair.AsString().Split('_')[0];
            string baseSymbol = gateioCoin.currency_pair.AsString().Split('_')[1];
            string symbol = name + baseSymbol;

            CoinModel coinDetailsModel = new();
            coinDetailsModel.Broker = key.AsString();
            coinDetailsModel.ChangePercent = gateioCoin.change_percentage.AsString();
            coinDetailsModel.CloseTime = gateioCoin.etf_pre_timestamp.AsString();
            coinDetailsModel.HighPrice = gateioCoin.high_24h.AsString();
            coinDetailsModel.LowPrice = gateioCoin.low_24h.AsString();
            coinDetailsModel.LastPrice = gateioCoin.last.AsString();
            coinDetailsModel.Name = name;
            coinDetailsModel.OpenPrice = gateioCoin.low_24h.AsString();
            coinDetailsModel.OpenTime = gateioCoin.etf_pre_timestamp.AsString();
            coinDetailsModel.Volume = gateioCoin.quote_volume.AsString();
            coinDetailsModel.Symbol = symbol;
            coinDetailsModel.BaseSymbol = baseSymbol;

            response.Add(coinDetailsModel);
          }

          return response;
        }
        #endregion

        #region BINANCE CONVERT

        if(IdentityAPI.Binance == key) {
          string url = Const.BINANCE_TICKER_ALL;

          foreach(BinanceCoinModel responseAPI in AppAux.GetObjFromJsonAPI<List<BinanceCoinModel>>(url)) {
            CoinModel coin = new();
            coin.Broker = key.AsString();
            coin.ChangePercent = responseAPI.priceChangePercent.AsString();
            coin.CloseTime = responseAPI.closeTime.AsString();
            coin.HighPrice = responseAPI.highPrice.AsString();
            coin.LowPrice = responseAPI.lowPrice.AsString();
            coin.LastPrice = responseAPI.lastPrice.AsString();
            coin.Name = responseAPI.symbol.AsString();
            coin.OpenPrice = responseAPI.openPrice.AsString();
            coin.OpenTime = responseAPI.openTime.AsString();
            coin.Volume = responseAPI.volume.AsString();
            coin.BaseSymbol = responseAPI.symbol.AsString();
            coin.Symbol = responseAPI.symbol.AsString();
            response.Add(coin);
          }

          return response;
        }

        #endregion

        #region BITSO CONVERT

        if(IdentityAPI.Bitso == key) {
          string url = Const.BITSO_TICKER_ALL;

          foreach(Payload responseAPI in AppAux.GetObjFromJsonAPI<BitsoCoinModel>(url).payload) {
            string name = responseAPI.book.Split('_')[0].ToUpper();
            string baseSymbol = responseAPI.book.Split('_')[1].ToUpper();
            string symbol = name + baseSymbol;

            CoinModel coin = new();
            coin.Broker = key.AsString();
            coin.ChangePercent = responseAPI.change_24.AsString();
            coin.CloseTime = "00000";
            coin.HighPrice = responseAPI.high.AsString();
            coin.LowPrice = responseAPI.low.AsString();
            coin.LastPrice = responseAPI.last.AsString();
            coin.Name = name;
            coin.OpenPrice = "00000";
            coin.OpenTime = "00000";
            coin.Volume = responseAPI.volume.AsString();
            coin.BaseSymbol = baseSymbol;
            coin.Symbol = symbol;
            response.Add(coin);
          }

          return response;
        }

        #endregion

        #region MARKETPLACEBTC CONVERT
        /* The bitcoin market does not offer an endpoint capable of providing all currency data at once. 
         * Therefore, it is necessary to obtain the symbols of each currency and then obtain the details of the currencies one by one.*/

        if(IdentityAPI.MarketPlaceBtc == key) {
          string url = Const.MARKETPLACEBTC_SYMBOL_ALL;

          // gets the list of all symbols first and then makes a request for each symbol in order to get the details.
          foreach(string responseAPIsymbol in AppAux.GetObjFromJsonAPI<MarketplaceBtcSymbolsModel>(url).symbol) {
            string name = responseAPIsymbol.Split('-')[0].ToUpper();
            string baseSymbol = responseAPIsymbol.Split("-")[1].ToUpper();
            string symbol = name + baseSymbol;

            url = Const.MARKETPLACEBTC_SYMBOL + responseAPIsymbol;
            MarketplaceBtcModel responseAPI = AppAux.GetObjFromJsonAPI<List<MarketplaceBtcModel>>(url).First();

            CoinModel coin = new();
            coin.Broker = key.AsString();
            coin.ChangePercent = "0";
            coin.CloseTime = "00000";
            coin.HighPrice = responseAPI.high.AsString();
            coin.LowPrice = responseAPI.low.AsString();
            coin.LastPrice = responseAPI.last.AsString();
            coin.Name = name;
            coin.OpenPrice = responseAPI.open.AsString();
            coin.OpenTime = "00000";
            coin.Volume = responseAPI.vol.AsString();
            coin.BaseSymbol = baseSymbol;
            coin.Symbol = symbol;
            response.Add(coin);

          }

          return response;
        }

        #endregion


        return response;
      } catch(Exception ex) {
        throw new Exception(ex.Message);
      }


    }

  }
}
