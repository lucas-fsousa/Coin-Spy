using System;

namespace CoinCheck.Models {
  public class CoinModel {
    public int Id { get; set; }
    public string Broker { get; set; }
    public string Name { get; set; }
    public string BaseSymbol { get; set; }
    public string Symbol { get; set; }
    public string HighPrice { get; set; }
    public string LowPrice { get; set; }
    public string Volume { get; set; }
    public string OpenTime { get; set; }
    public string CloseTime { get; set; }
    public string OpenPrice { get; set; }
    public string ChangePercent { get; set; }
    public string LastPrice { get; set; }
    public DateTime LastUpdate { get; set; }
    public DateTime CaptureDate { get; set; }
  }
}
