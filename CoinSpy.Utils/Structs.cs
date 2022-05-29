namespace CoinCheck.Utils {
  public struct ExecDetails {
    public int CountNews { get; set; }
    public int CountUpdated { get; set; }
    public int CountDeleted { get; set; }

    public ExecDetails(int countNews, int countUpdated, int countDeleted) { 
      CountNews = countNews;
      CountUpdated = countUpdated;
      CountDeleted = countDeleted;
    }
  }
}
