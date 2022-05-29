using PublicUtility;
using System;
using System.Net.Http;
using System.Text.Json;

namespace CoinCheck.Utils {
  public static class AppAux {
    public static string GetJsonString(Uri url) {
      string json = string.Empty;

      HttpResponseMessage response = XRequest.HttpGet(url).GetAwaiter().GetResult();

      if(response == null)
        return json;

      if(!response.IsSuccessStatusCode)
        return json;

      json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
      return json;
    }

    public static T JsonToObj<T>(string json) {
      T response = default;
      try {
        response = JsonSerializer.Deserialize<T>(json);

      } catch(Exception ex) {
        Log.Add($"ERROR ON TRY DESEREALIZE JSON OBJECT # APPAUX # DETAILS: {ex.Message}", "APP AUX");

      }
      return response;
    }

    public static T GetObjFromJsonAPI<T>(Uri url) {
      T result = default;
      string response = GetJsonString(url);
      if(!string.IsNullOrEmpty(response)) {
        result = JsonToObj<T>(response);
      }

      return result;
    }

    public static T GetObjFromJsonAPI<T>(string url) => GetObjFromJsonAPI<T>(new Uri(url));

  }

}
