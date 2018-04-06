using System;
using System.Collections.Generic;
using System.Text;

namespace FaceitMatch.Services
{
    public class Membership
    {
        public string type { get; set; }
    }

    public class QuickMatch
    {
        public string join_type { get; set; }
        public List<string> selected_members_ids { get; set; }
        public string team_id { get; set; }
    }

    public class Streaming
    {
        public string twitch_id { get; set; }
        public bool is_streaming { get; set; }
    }

    public class Faction1
    {
        public int csgo_skill_level { get; set; }
        public string active_team_id { get; set; }
        public string csgo_id { get; set; }
        public string league_icon_url { get; set; }
        public string nickname { get; set; }
        public string guid { get; set; }
        public string csgo_name { get; set; }
        public string avatar { get; set; }
        public Membership membership { get; set; }
        public QuickMatch quick_match { get; set; }
        public string csgo_skill_level_label { get; set; }
        public Streaming streaming { get; set; }
    }

    public class Streaming2
    {
        public string twitch_id { get; set; }
    }

    public class Membership2
    {
        public string type { get; set; }
    }

    public class QuickMatch2
    {
        public string join_type { get; set; }
        public List<string> selected_members_ids { get; set; }
        public string team_id { get; set; }
    }

    public class Faction2
    {
        public int csgo_skill_level { get; set; }
        public Streaming2 streaming { get; set; }
        public string active_team_id { get; set; }
        public string csgo_id { get; set; }
        public string league_icon_url { get; set; }
        public string nickname { get; set; }
        public string guid { get; set; }
        public string csgo_name { get; set; }
        public string avatar { get; set; }
        public Membership2 membership { get; set; }
        public QuickMatch2 quick_match { get; set; }
        public string csgo_skill_level_label { get; set; }
    }

    public class Location
    {
        public string name { get; set; }
        public string game_location_id { get; set; }
        public string country { get; set; }
        public string guid { get; set; }
    }

    public class Map
    {
        public string name { get; set; }
        public string game_map_id { get; set; }
        public string class_name { get; set; }
        public string guid { get; set; }
    }

    public class VotedEntity
    {
        public Location location { get; set; }
        public Map map { get; set; }
    }

    public class Data
    {
        public string game { get; set; }
        public string region { get; set; }
        public int score1 { get; set; }
        public int score2 { get; set; }
        public string winner { get; set; }
        public string loser { get; set; }
        public string state { get; set; }
        public string rule { get; set; }
        public List<Faction1> faction1 { get; set; }
        public List<Faction2> faction2 { get; set; }
        public string match_id { get; set; }
        public object competition_id { get; set; }
        public object competition_type { get; set; }
        public int team_size { get; set; }
        public string faction1_nickname { get; set; }
        public string faction1_name { get; set; }
        public string faction2_nickname { get; set; }
        public string faction2_name { get; set; }
        public string faction1_leader { get; set; }
        public string faction2_leader { get; set; }
        public string faction1_id { get; set; }
        public object faction1_avatar { get; set; }
        public string faction2_id { get; set; }
        public object faction2_avatar { get; set; }
        public object winner_nickname { get; set; }
        public object loser_nickname { get; set; }
        public string started_at { get; set; }
        public object start_label { get; set; }
        public string finished_at { get; set; }
        public string match_url { get; set; }
        public string game_type { get; set; }
        public int best_of { get; set; }
        public int played { get; set; }
        public List<VotedEntity> voted_entities { get; set; }
        public int max_players { get; set; }
        public string created_at { get; set; }
        public string configuring_timestamp { get; set; }
        public string match_type_label { get; set; }
        public string updated_at { get; set; }
        public string match_type { get; set; }
        public List<string> demo_url { get; set; }
    }

    public class FaceitMatchJson
    {
        public long epoch { get; set; }
        public string env { get; set; }
        public string ver { get; set; }
        public Data data { get; set; }
    }
}
