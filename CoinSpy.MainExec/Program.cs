using CoinCheck.Entities;
using CoinCheck.Utils;
using PublicUtility;
using System;
using System.Linq;
using System.Threading;

namespace CoinCheck.MainExec {

  internal class Program: IDisposable {
    private readonly DateTime _startExecution = DateTime.Now;
    private readonly Binance _binance;
    private readonly GateIo _gateio;
    private readonly MarketplaceBtc _marketplaceBtc;
    private readonly Bitso _bitso;

    public Program() {
      _marketplaceBtc = new MarketplaceBtc();
      _binance = new Binance();
      _gateio = new GateIo();
      _bitso = new Bitso();

      LoadBrokers();
    }

    protected void Run() {
      SetCoins();
    }

    private void LoadBrokers() {
      Log.Add(" LOADING BROKERS MAPPED INTO THE SYSTEM...", "PROGRAM");
      int i = 1;
      foreach(string brokerName in X.EnumToDict<IdentityAPI>().Values.OrderBy(x => x.Length).ToList()) {
        string.Format($"  [{i} - {brokerName}]").Print();
        Thread.Sleep(500);
        i++;
      }
    }

    private void SetCoins() {
      _binance.Change();
      _gateio.Change();
      _bitso.Change();
      _marketplaceBtc.Change();
    }

    static void Main(string[] args) {
      Log.Add($" --------- STARTING PROCCESS - {DateTime.Now} ---------\n", "PROGRAM");

      using(Program app = new())
        app.Run();

      Log.Add($"\n --------- PROCCESS ENDED - {DateTime.Now} ---------", "PROGRAM");
    }

    public void Dispose() {
      new UserNotification(_startExecution).Notify();
      GC.SuppressFinalize(this);
      GC.Collect();
    }
  }
}
