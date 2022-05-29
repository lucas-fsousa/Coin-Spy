using CoinCheck.Models;
using PublicUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinCheck.Data {
  public class LogDAL {
    private readonly string _connectionString = ConfigurationManager.ConnectionStrings["dataBaseString"].ConnectionString;

    public LogDAL() { }

    public void Add(LogsModel model) {
      using(XSql sql = new(_connectionString)) {
        try {
          SqlCommand cmd = new();
          cmd.Parameters.AddWithValue("placeExec", model.PlaceExecution);
          cmd.Parameters.AddWithValue("description", model.Description);
          cmd.Parameters.AddWithValue("dateExec", model.DateExecution);
          cmd.CommandText = "INSERT INTO TBEXECUTIONHISTORY VALUES (@placeExec, @description, @dateExec)";

          sql.Cmd = cmd;
          sql.GoExec(out string message);
        } catch(Exception) { }

      }

    }

    public List<LogsModel> GetLogs() {
      List<LogsModel> response = new();
      using(XSql sql = new(_connectionString)) {
        try {
          sql.Cmd = new SqlCommand("SELECT * FROM TBEXECUTIONHISTORY");
          response = sql.ReturnData(out string message).DeserializeTable<List<LogsModel>>();
        } catch(Exception) { }
        return response;
      }
    }


  }
}
