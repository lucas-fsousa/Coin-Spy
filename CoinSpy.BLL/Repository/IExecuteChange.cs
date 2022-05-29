using CoinCheck.Models;
using CoinCheck.Utils;
using System.Collections.Generic;

namespace CoinCheck.Repository {
  public interface IExecuteChange {
    public abstract ExecDetails Run(IdentityAPI key);
    public abstract List<CoinModel> ProccessJsonObject(IdentityAPI key);
  }
}
