using CoinCheck.Repository;
using CoinCheck.Services;
using CoinCheck.Utils;

namespace CoinCheck.Entities {
  public abstract class BaseEntity {
    protected static IExecuteChange ExecuteChange => new ExecuteChange();
    protected abstract IdentityAPI Key { get; }
    public abstract void Change();
  }
}
