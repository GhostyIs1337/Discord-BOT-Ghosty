using System;
using System.Collections.Generic;
using System.Text;

namespace PUBGMatch.Services
{
    public class Attributes
    {
        public DateTime createdAt { get; set; }
        public int duration { get; set; }
        public string gameMode { get; set; }
        public string patchVersion { get; set; }
        public string shardId { get; set; }
        public object stats { get; set; }
        public object tags { get; set; }
        public string titleId { get; set; }
    }

    public class Datum
    {
        public string type { get; set; }
        public string id { get; set; }
    }

    public class Assets
    {
        public List<Datum> data { get; set; }
    }

    public class Datum2
    {
        public string type { get; set; }
        public string id { get; set; }
    }

    public class Rosters
    {
        public List<Datum2> data { get; set; }
    }

    public class Rounds
    {
        public List<object> data { get; set; }
    }

    public class Spectators
    {
        public List<object> data { get; set; }
    }

    public class Relationships
    {
        public Assets assets { get; set; }
        public Rosters rosters { get; set; }
        public Rounds rounds { get; set; }
        public Spectators spectators { get; set; }
    }

    public class Links
    {
        public string schema { get; set; }
        public string self { get; set; }
    }

    public class Data
    {
        public string type { get; set; }
        public string id { get; set; }
        public Attributes attributes { get; set; }
        public Relationships relationships { get; set; }
        public Links links { get; set; }
    }

    public class Stats
    {
        public int DBNOs { get; set; }
        public int assists { get; set; }
        public int boosts { get; set; }
        public double damageDealt { get; set; }
        public string deathType { get; set; }
        public int headshotKills { get; set; }
        public int heals { get; set; }
        public int killPlace { get; set; }
        public int killPoints { get; set; }
        public double killPointsDelta { get; set; }
        public int killStreaks { get; set; }
        public int kills { get; set; }
        public int lastKillPoints { get; set; }
        public int lastWinPoints { get; set; }
        public int longestKill { get; set; }
        public int mostDamage { get; set; }
        public string name { get; set; }
        public string playerId { get; set; }
        public int revives { get; set; }
        public double rideDistance { get; set; }
        public int roadKills { get; set; }
        public int teamKills { get; set; }
        public int timeSurvived { get; set; }
        public int vehicleDestroys { get; set; }
        public double walkDistance { get; set; }
        public int weaponsAcquired { get; set; }
        public int winPlace { get; set; }
        public int winPoints { get; set; }
        public double winPointsDelta { get; set; }
        public int? rank { get; set; }
        public int? teamId { get; set; }
    }

    public class Attributes2
    {
        public string actor { get; set; }
        public string shardId { get; set; }
        public Stats stats { get; set; }
        public string won { get; set; }
        public string URL { get; set; }
        public DateTime? createdAt { get; set; }
        public string description { get; set; }
        public string name { get; set; }
    }

    public class Datum3
    {
        public string type { get; set; }
        public string id { get; set; }
    }

    public class Participants
    {
        public List<Datum3> data { get; set; }
    }

    public class Team
    {
        public object data { get; set; }
    }

    public class Relationships2
    {
        public Participants participants { get; set; }
        public Team team { get; set; }
    }

    public class Included
    {
        public string type { get; set; }
        public string id { get; set; }
        public Attributes2 attributes { get; set; }
        public Relationships2 relationships { get; set; }
    }

    public class Links2
    {
        public string self { get; set; }
    }

    public class Meta
    {
    }

    public class PUBGMatchJson
    {
        public Data data { get; set; }
        public List<Included> included { get; set; }
        public Links2 links { get; set; }
        public Meta meta { get; set; }
    }
}
