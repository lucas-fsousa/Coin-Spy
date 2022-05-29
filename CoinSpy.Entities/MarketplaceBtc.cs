using CoinCheck.Repository;
using CoinCheck.Utils;
using System;

namespace CoinCheck.Entities {
  public class MarketplaceBtc: BaseEntity {
    protected override IdentityAPI Key => IdentityAPI.MarketPlaceBtc;

    public override void Change() {
      try {
        Log.Add("\n\t MARKETPLACEBTC - START SET DETAILS SYMBOL\n", "MARKETPLACEBTC");

        ExecDetails resultExec = ExecuteChange.Run(Key);

        Log.Add($"MARKETPLACEBTC - DETAILS FOR SYMBOL # SUCCESS # {resultExec.CountNews} NEWS ADDED - {resultExec.CountUpdated} UPDATED # ", "MARKETPLACEBTC");
      } catch(Exception ex) {
        Log.Add($"MARKETPLACEBTC - DETAILS FOR SYMBOL # ERRO # {ex.Message}", "MARKETPLACEBTC");
      }
    }

   
  }
}
