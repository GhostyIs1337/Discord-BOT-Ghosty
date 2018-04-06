using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.Services
{
        public class Stat
        {
            public string totalKills { get; set; }
            public int value { get; set; }
            public string totalDeaths { get; set; }
            public string name { get; set; }
        }

        public class Achievement
        {
            public string name { get; set; }
            public int achieved { get; set; }
        }

        public class Playerstats
        {
            public string steamID { get; set; }
            public string gameName { get; set; }
            public List<Stat> stats { get; set; }
            public List<Achievement> achievements { get; set; }
            public string totalKills { get; set; }
            public int value { get; set; }
            public string totalDeaths { get; set; }
            public string name { get; set; }
        }

        public class CSJson
        {
            public Playerstats playerstats { get; set; }
        }
}
