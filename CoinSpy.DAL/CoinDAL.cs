using CoinCheck.Models;
using PublicUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace CoinCheck.Data {
  public class CoinDAL {
    private readonly string _connectionString = ConfigurationManager.ConnectionStrings["dataBaseString"].ConnectionString;

    public void Clear() {
      using(XSql sql = new(_connectionString)) {
        try {
          string message;
          sql.Cmd = new SqlCommand("TRUNCATE TABLE TBMARKET");
          sql.GoExec(out message);

          if(message.Contains("ERRO"))
            throw new Exception($"TRUNCATE TABLE # {message}");

        } catch(Exception ex) {
          throw new Exception($"TRUNCATE TABLE TBMARKET # ERRO {ex.Message}");
        }

      }
    }

    public void Insert(CoinModel coin) {
      using(XSql sql = new(_connectionString)) {
        try {
          SqlCommand cmd = new();
          StringBuilder query = new();

          query.Append("INSERT INTO TBMARKET VALUES ");
          query.Append("  ( ");
          query.Append("    @name, @baseSymbol, @symbol, @broker, @highPrice, @lowPrice, @volume, @openTime, ");
          query.Append("    @closeTime, @openPrice, @changePercent, @lastPrice, @lastUpdate, @captureDate ");
          query.Append("  ) ");

          cmd.CommandText = query.ToString();
          cmd.Parameters.AddWithValue("name", coin.Name);
          cmd.Parameters.AddWithValue("baseSymbol", coin.BaseSymbol);
          cmd.Parameters.AddWithValue("symbol", coin.Symbol);
          cmd.Parameters.AddWithValue("broker", coin.Broker);
          cmd.Parameters.AddWithValue("highPrice", coin.HighPrice);
          cmd.Parameters.AddWithValue("lowPrice", coin.LowPrice);
          cmd.Parameters.AddWithValue("volume", coin.Volume);
          cmd.Parameters.AddWithValue("openTime", coin.OpenTime);
          cmd.Parameters.AddWithValue("closeTime", coin.CloseTime);
          cmd.Parameters.AddWithValue("openPrice", coin.OpenPrice);
          cmd.Parameters.AddWithValue("changePercent", coin.ChangePercent);
          cmd.Parameters.AddWithValue("lastPrice", coin.LastPrice);
          cmd.Parameters.AddWithValue("lastUpdate", coin.LastUpdate);
          cmd.Parameters.AddWithValue("captureDate", coin.CaptureDate);

          sql.Cmd = cmd;
          sql.GoExec(out string message);
          if(message.Contains("ERRO"))
            throw new Exception($"ADD COIN # {message}");

        } catch(Exception ex) {
          throw new Exception($"ADD COIN # ERRO # {ex.Message}");
        }

      }
    }

    public void Update(CoinModel coin) {
      using(XSql sql = new(_connectionString)) {
        try {
          SqlCommand cmd = new();
          StringBuilder query = new();

          query.Append("UPDATE TBMARKET SET ");
          query.Append("  HighPrice = @highPrice, ");
          query.Append("  LowPrice = @lowPrice, ");
          query.Append("  Volume = @volume, ");
          query.Append("  ChangePercent = @changePercent, ");
          query.Append("  LastPrice = @lastPrice, ");
          query.Append("  LastUpdate = @lastUpdate ");
          query.Append("WHERE Id = @id");

          cmd.CommandText = query.ToString();
          cmd.Parameters.AddWithValue("highPrice", coin.HighPrice);
          cmd.Parameters.AddWithValue("lowPrice", coin.LowPrice);
          cmd.Parameters.AddWithValue("volume", coin.Volume);;
          cmd.Parameters.AddWithValue("changePercent", coin.ChangePercent);
          cmd.Parameters.AddWithValue("lastPrice", coin.LastPrice);
          cmd.Parameters.AddWithValue("lastUpdate", coin.LastUpdate);
          cmd.Parameters.AddWithValue("id", coin.Id);

          sql.Cmd = cmd;
          sql.GoExec(out string message);
          if(message.Contains("ERRO"))
            throw new Exception($"UPDATE COIN # {message}");

        } catch(Exception ex) {
          throw new Exception($"UPDATE COIN # ERRO # {ex.Message}");
        }

      }
    }

    public List<CoinModel> Get(CoinModel model = null) {
      List<CoinModel> result = new();
      using(XSql sql = new(_connectionString)) {
        try {
          SqlCommand cmd = new();
          StringBuilder query = new();

          query.Append("SELECT * FROM TBMARKET ");

          if(model != null) {
            query.Append("WHERE Symbol = @symbol ");
            query.Append("AND Broker = @broker ");

            cmd.Parameters.AddWithValue("symbol", model.Symbol);
            cmd.Parameters.AddWithValue("broker", model.Broker);
          }

          cmd.CommandText = query.ToString();
          sql.Cmd = cmd;
          var lst = sql.ReturnData(out string message).DeserializeTable<List<CoinModel>>();
          if(lst != null)
            result = lst;

        } catch(Exception ex) {
          throw new Exception($"GET COINS # ERRO # {ex.Message}");
        }

      }

      return result;
    }

    public void Delete(CoinModel coin) {
      using(XSql sql = new(_connectionString)) {
        try {
          SqlCommand cmd = new();
          StringBuilder query = new();
          query.Append("DELETE FROM TBMARKET ");
          query.Append("WHERE Id = @id");

          cmd.CommandText = query.ToString();
          cmd.Parameters.AddWithValue("id", coin.Id);

          sql.Cmd = cmd;
          sql.GoExec(out string message);
          if(message.Contains("ERRO"))
            throw new Exception($"DELETE COIN # {message}");

        } catch(Exception ex) {
          throw new Exception($"DELETE {coin.Symbol} FROM TBMARKET # ERRO {ex.Message}");

        }

      }
    }

  }
}
