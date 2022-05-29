using CoinCheck.Data;
using CoinCheck.Models;
using CoinCheck.Repository;
using System.Collections.Generic;
using System.Linq;

namespace CoinCheck.Services {
  public class Coin: ICoin {
    public void Clear() {
      new CoinDAL().Clear();
    }

    public List<CoinModel> Get() {
      List<CoinModel> lstCoins = new CoinDAL().Get();

      return lstCoins;
    }
    
    public void Delete(CoinModel model) {
      if(model == null)
        return;

      new CoinDAL().Delete(model);
    }

    public void Insert(CoinModel model) {
      if(model == null)
        return;

      new CoinDAL().Insert(model);
    }

    public void Update(CoinModel model) {
      if(model == null)
        return;

      new CoinDAL().Update(model);
    }

    public CoinModel Get(CoinModel model) {
      return new CoinDAL().Get(model).FirstOrDefault();
    }
  }
}
