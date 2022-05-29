using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinCheck.Models.MarketplaceBTC {
  public class MarketplaceBtcSymbolsModel {
    public List<string> symbol { get; set; }
    public List<string> description { get; set; }
    public List<string> currency { get; set; }

    //[JsonProperty("base-currency")]
    public List<string> BaseCurrency { get; set; }

    //[JsonProperty("exchange-listed")]
    public List<bool> ExchangeListed { get; set; }

    //[JsonProperty("exchange-traded")]
    public List<bool> ExchangeTraded { get; set; }
    public List<string> minmovement { get; set; }
    public List<int> pricescale { get; set; }
    public List<string> type { get; set; }
    public List<string> timezone { get; set; }

    //[JsonProperty("session-regular")]
    public List<string> SessionRegular { get; set; }

    //[JsonProperty("withdrawal-fee")]
    public List<string> WithdrawalFee { get; set; }
  }
}
