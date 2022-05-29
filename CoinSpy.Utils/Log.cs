using CoinCheck.Data;
using CoinCheck.Models;
using PublicUtility;
using System;

namespace CoinCheck.Utils {
  public static class Log {
    public static void Add(string message, string place) {
      var model = new LogsModel {
        DateExecution = DateTime.Now,
        Description = message,
        PlaceExecution = place
      };

      new LogDAL().Add(model);
      message.Print();
    }
  }
}
