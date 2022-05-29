using CoinCheck.Utils;
using System;

namespace CoinCheck.Entities {
  public class Binance: BaseEntity {
    protected override IdentityAPI Key => IdentityAPI.Binance;

    public override void Change() {
      try {
        Log.Add("\n\t BINANCE - START SET DETAILS SYMBOL\n", "BINANCE");

        ExecDetails resultExec = ExecuteChange.Run(Key);

        Log.Add($"BINANCE - DETAILS FOR SYMBOL # SUCCESS # {resultExec.CountNews} NEWS ADDED - {resultExec.CountUpdated} UPDATED # ", "BINANCE");
      } catch(Exception ex) {
        Log.Add($"BINANCE - DETAILS FOR SYMBOL # ERRO # {ex.Message}", "BINANCE");
      }
    }
    
    
  }
}
