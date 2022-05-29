using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinCheck.Models {
  public class LogsModel {
    public int Id { get; set; }
    public string PlaceExecution { get; set; }
    public string Description { get; set; }
    public DateTime DateExecution { get; set; }
  }
}
