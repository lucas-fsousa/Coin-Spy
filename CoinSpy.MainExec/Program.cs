using CoinCheck.Entities;
using CoinCheck.Utils;
using PublicUtility;
using System;
using System.Linq;
using System.Threading;

namespace CoinCheck.MainExec {

  internal class Program {
    private readonly DateTime _startExecution = DateTime.Now;
    private readonly Binance _binance;
    private readonly GateIo _gateio;
    private readonly UserNotification _userNotification;
    private readonly MarketplaceBtc _marketplaceBtc;
    private readonly Bitso _bitso;

    public Program() {
      _marketplaceBtc = new MarketplaceBtc();
      _binance = new Binance();
      _gateio = new GateIo();
      _bitso = new Bitso();
      _userNotification = new UserNotification(_startExecution);
    }

    protected void Run() {
      LoadBrokers();
      SetCoins(); // 1° to exec
      Notify(); // last to exec
    }

    private void LoadBrokers() {
      Log.Add(" LOADING BROKERS MAPPED INTO THE SYSTEM...", "PROGRAM");
      int i = 1;
      foreach(string brokerName in X.EnumToDict<IdentityAPI>().Values.OrderBy(x => x.Length).ToList()) {
        string.Format($"  [{i} - {brokerName}]").Print();
        i++;
        Thread.Sleep(500);
      }
    }

    private void Notify() {
      _userNotification.Notify();
    }

    private void SetCoins() {
      _binance.Change();
      _gateio.Change();
      _bitso.Change();
      _marketplaceBtc.Change();
    }

    static void Main(string[] args) {
      Log.Add($" --------- STARTING PROCCESS - {DateTime.Now} ---------\n", "PROGRAM");

      new Program().Run();

      Log.Add($"\n --------- PROCCESS ENDED - {DateTime.Now} ---------", "PROGRAM");
    }


  }
}
