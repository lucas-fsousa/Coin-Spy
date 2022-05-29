using CoinCheck.Models;
using CoinCheck.Repository;
using CoinCheck.Services;
using CoinCheck.Utils;
using PublicUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace CoinCheck.Entities {
  public class UserNotification {
    private readonly ICoin _coin;
    private readonly DateTime _startExecution;

    public UserNotification(DateTime startProgram) {
      _coin = new Coin();
      _startExecution = startProgram;
    }

    private static string GetBody(int countNews, int countUpdates) {
      StringBuilder sb = new();

      if(countNews > 0) {
        sb.Append($"<p>NEWS - COINS ADDEDS 24h - <strong>{countNews}</strong></p>");
      }

      if(countUpdates > 0) {
        sb.Append($"<p>UPDATE - COINS UPDATEDS 24h - <strong>{countUpdates}</strong></p>");
      }

      return sb.ToString();
    }

    private static Attachment GetAttachment(List<CoinModel> lstCoins, string fileName) {
      string filePath = @$"C:\Users\Public\Documents\{fileName}.xlsx";
      XExcel excel = new();
      XTable table = new(7);

      // COLUMNS NAMES
      table.AddCell("A1", "NAME");
      table.AddCell("B1", "SYMBOL");
      table.AddCell("C1", "BROKER");
      table.AddCell("D1", "MAXPRICE");
      table.AddCell("E1", "MINPRICE");
      table.AddCell("F1", "LASTPRICE");
      table.AddCell("G1", "CHANGEPERCENT");

      // LINES DEFINITION
      string currentCell = "A2";
      string aux = currentCell;
      foreach(CoinModel coin in lstCoins) {
        table.AddCell(currentCell, coin.Name);
        currentCell = XExcel.GetNextColumn(currentCell);

        table.AddCell(currentCell, coin.Symbol);
        currentCell = XExcel.GetNextColumn(currentCell);

        table.AddCell(currentCell, coin.Broker);
        currentCell = XExcel.GetNextColumn(currentCell);

        table.AddCell(currentCell, coin.HighPrice);
        currentCell = XExcel.GetNextColumn(currentCell);

        table.AddCell(currentCell, coin.LowPrice);
        currentCell = XExcel.GetNextColumn(currentCell);

        table.AddCell(currentCell, coin.LastPrice);
        currentCell = XExcel.GetNextColumn(currentCell);

        table.AddCell(currentCell, coin.ChangePercent.ToString());

        currentCell = XExcel.GetNextLine(aux);
        aux = currentCell;
      }

      excel.WorkSheets = new() {
        new XWorkSheet() {
          WorksheetName = "DashBoard",
          WorkSheetColor = "#FFF",
          Tables = new() { table }
        }
      };

      excel.Generate(filePath);

      return new(filePath);
    }

    public void Notify() {
      Log.Add($"START SENT NOTIFICATION", "USER NOTIFICATION");

      try {
        string login = ConfigurationManager.AppSettings["CredLoginEmail"];
        string password = ConfigurationManager.AppSettings["CredPasswordEmail"];
        string mailTo = ConfigurationManager.AppSettings["mailTo"];
        string mailCopy = ConfigurationManager.AppSettings["mailCopy"];
        XEmail email = new(password, login, "UPDATE COINS");

        List<CoinModel> lstDetails = _coin.Get().Where(x => x.LastUpdate >= _startExecution).ToList();

        if(lstDetails.Count > 0) {
          List<CoinModel> lstNewAdds = lstDetails.Where(x => x.CaptureDate >= _startExecution).OrderByDescending(x => x.ChangePercent).ToList();
          List<CoinModel> lstPriceUpdates = lstDetails.Where(x => x.LastUpdate >= _startExecution && x.CaptureDate != x.LastUpdate).OrderByDescending(x => x.LastPrice).ToList();
          List<Attachment> lstAttachments = new();
          string body = GetBody(lstNewAdds.Count, lstPriceUpdates.Count);

          email.Priority = MailPriority.High;
          email.Subject = "LAST 24h AGO";
          email.Body = body;
          email.To = mailTo;

          if(lstNewAdds.Count > 0)
            lstAttachments.Add(GetAttachment(lstNewAdds, "newCoins"));

          if(lstPriceUpdates.Count > 0)
            lstAttachments.Add(GetAttachment(lstPriceUpdates, "updatedCoins"));

          if(lstAttachments.Count > 0)
            email.Attachment = lstAttachments;

          if(!string.IsNullOrEmpty(email.Copy))
            email.Copy = mailCopy;

          if(email.SendMail(out string message))
            Log.Add($"NOTIFICATION SENT", "USER NOTIFICATION");
          else
            Log.Add($"NO RECORDS TO SEND NOTIFICATION.", "USER NOTIFICATION");
        }

      } catch(Exception ex) {
        Log.Add($"NOTIFICATION NOT SENT - {ex.Message}", "USER NOTIFICATION");

      }

      Log.Add($"END SENT NOTIFICATION", "USER NOTIFICATION");
    }
  }
}
