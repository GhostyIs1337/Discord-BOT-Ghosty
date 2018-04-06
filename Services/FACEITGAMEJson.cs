using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace FACEITGAMEJson.Services
{
    public class Lifetime
    {
        public string Matches { get; set; }
        public string Wins { get; set; }
        [JsonProperty("K/D Ratio")]
        public string KDR { get; set; }
        [JsonProperty("Total Headshots %")]
        public string TotalHeadshotsPercentage { get; set; }
        [JsonProperty("Average K/D Ratio")]
        public string AverageKDR { get; set; }
        [JsonProperty("Win Rate %")]
        public string WinRatePercentage { get; set; }
        [JsonProperty("Average Headshots %")]
        public string AverageHeadshotsPercentage { get; set; }
        [JsonProperty("Current Win Streak")]
        public string CurrentWinStreak { get; set; }
        [JsonProperty("Longest Win Streak")]
        public string LongestWinStreak { get; set; }
    }

public class Stats
{
    public string Matches { get; set; }
    public string Wins { get; set; }
    public string Kills { get; set; }
    public string Deaths { get; set; }
    public string Assists { get; set; }
    public string MVPs { get; set; }
    [JsonProperty("K/D Ratio")]
    public string KDR { get; set; }
    public string Rounds { get; set; }
    public string Headshots { get; set; }
    [JsonProperty("Triple Kills")]
    public string TripleKills { get; set; }
    [JsonProperty("Quadro Kills")]
    public string QuadroKills { get; set; }
    [JsonProperty("Penta Kills")]
    public string PentaKills { get; set; }
    [JsonProperty("Total Headshots %")]
    public string TotalHeadshotsPercentage { get; set; }
    [JsonProperty("K/R Ratio")]
    public string KRRatio { get; set; }
    [JsonProperty("Average Kills")]
    public string AverageKills { get; set; }
    [JsonProperty("Average Deaths")]
    public string AverageDeaths { get; set; }
    [JsonProperty("Average Assists")]
    public string AverageAssists { get; set; }
    [JsonProperty("Average MVPs")]
    public string AverageMVPs { get; set; }
    [JsonProperty("Average K/D Ratio")]
    public string AverageKDRatio { get; set; }
    [JsonProperty("Win Rate %")]
    public string WinRatePercentage { get; set; }
    [JsonProperty("Headshots per Match")]
    public string HeadshotsPerMatch { get; set; }
    [JsonProperty("Average Headshots %")]
    public string AverageHeadshotspercentage { get; set; }
    [JsonProperty("Average K/R Ratio")]
    public string AverageKRRatio { get; set; }
    [JsonProperty("Average Triple Kills")]
    public string AverageTripleKills { get; set; }
    [JsonProperty("Average Quadro Kills")]
    public string AverageQuadroKills { get; set; }
    [JsonProperty("Average Penta Kills")]
    public string AveragePentaKills { get; set; }
}

public class Segment
{
    public string type { get; set; }
    public string label { get; set; }
    public string image_url_regular { get; set; }
    public string image_url_small { get; set; }
    public Stats stats { get; set; }
}

public class Data
{
    public Lifetime lifetime { get; set; }
    public List<Segment> segments { get; set; }
}

public class FACEITGameJson
{
    public long epoch { get; set; }
    public string env { get; set; }
    public string ver { get; set; }
    public Data data { get; set; }
}
}
