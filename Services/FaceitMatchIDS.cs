using System;
using System.Collections.Generic;
using System.Text;

namespace FaceitMatchID.Services
{
    public class Match
    {
        public string game { get; set; }
        public string region { get; set; }
        public string played { get; set; }
        public string status { get; set; }
        public string winner { get; set; }
        public string match_id { get; set; }
        public string match_url { get; set; }
        public string game_mode { get; set; }
        public string best_of { get; set; }
        public string started_at { get; set; }
        public string finished_at { get; set; }
        public string score_1 { get; set; }
        public string score_2 { get; set; }
    }

    public class Data
    {
        public List<Match> matches { get; set; }
    }

    public class FaceitMatchIDS
    {
        public long epoch { get; set; }
        public string env { get; set; }
        public string ver { get; set; }
        public Data data { get; set; }
    }
}
