using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinCheck.Models.Bitso {
  public class Payload {
    public string high { get; set; }
    public string last { get; set; }
    public DateTime created_at { get; set; }
    public string book { get; set; }
    public string volume { get; set; }
    public string vwap { get; set; }
    public string low { get; set; }
    public string ask { get; set; }
    public string bid { get; set; }
    public string change_24 { get; set; }

  }

  public class BitsoCoinModel {
    public bool success { get; set; }
    public IList<Payload> payload { get; set; }

  }



}
