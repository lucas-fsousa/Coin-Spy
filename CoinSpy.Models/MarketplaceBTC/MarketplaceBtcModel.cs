using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinCheck.Models.MarketplaceBTC {
  public class MarketplaceBtcModel {
    public string pair { get; set; }
    public string high { get; set; }
    public string low { get; set; }
    public string vol { get; set; }
    public string last { get; set; }
    public string buy { get; set; }
    public string sell { get; set; }
    public string open { get; set; }
    public int date { get; set; }
  }
}
