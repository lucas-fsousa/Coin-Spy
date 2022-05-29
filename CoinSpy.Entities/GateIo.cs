using CoinCheck.Utils;
using System;

namespace CoinCheck.Entities {
  public class GateIo: BaseEntity {

    protected override IdentityAPI Key => IdentityAPI.GateIO;

    public override void Change() {
      try {
        Log.Add("\n\t GATEIO - START SET DETAILS SYMBOL\n", "GATEIO");

        ExecDetails resultExec = ExecuteChange.Run(Key);

        Log.Add($"GATEIO - DETAILS FOR SYMBOL # SUCCESS # {resultExec.CountNews} NEWS ADDED - {resultExec.CountUpdated} UPDATED # ", "GATEIO");
      } catch(Exception ex) {
        Log.Add($"GATEIO - DETAILS FOR SYMBOL # ERRO # {ex.Message}", "GATEIO");
      }
    }

  }
}
