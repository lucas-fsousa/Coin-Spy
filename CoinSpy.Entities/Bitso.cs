using CoinCheck.Utils;
using System;

namespace CoinCheck.Entities {
  public class Bitso: BaseEntity {
    protected override IdentityAPI Key => IdentityAPI.Bitso;

    public override void Change() {
      try {
        Log.Add("\n\t BITSO - START SET DETAILS SYMBOL\n", "BITSO");

        ExecDetails resultExec = ExecuteChange.Run(Key);

        Log.Add($"BITSO - DETAILS FOR SYMBOL # SUCCESS # {resultExec.CountNews} NEWS ADDED - {resultExec.CountUpdated} UPDATED # ", "BITSO");
      } catch(Exception ex) {
        Log.Add($"BITSO - DETAILS FOR SYMBOL # ERRO # {ex.Message}", "BITSO");
      }
    }

  }
}
