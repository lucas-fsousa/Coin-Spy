using CoinCheck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinCheck.Repository {
  public interface ICoin {
    public abstract List<CoinModel> Get();
    public abstract CoinModel Get(CoinModel model);
    public abstract void Clear();
    public abstract void Insert(CoinModel model);
    public abstract void Update(CoinModel model);
    public abstract void Delete(CoinModel model);
  }
}
