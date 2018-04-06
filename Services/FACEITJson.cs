using System;
using System.Collections.Generic;
using System.Text;

namespace FaceitPlayerJson.Services
{
    public class Streaming
    {
        public string twitch_id { get; set; }
    }

    public class EU
    {
        public string selected_ladder_id { get; set; }
    }

    public class Regions
    {
        public EU EU { get; set; }
    }

    public class Csgo
    {
        public string region { get; set; }
        public Regions regions { get; set; }
        public string game_player_id { get; set; }
        public string game_player_name { get; set; }
        public string game_profile_id { get; set; }
        public string skill_level { get; set; }
        public string skill_level_icon { get; set; }
    }

    public class Games
    {
        public Csgo csgo { get; set; }
    }

    public class OngoingRooms
    {
    }

    public class Data
    {
        public string nickname { get; set; }
        public string homepage { get; set; }
        public string avatar { get; set; }
        public Streaming streaming { get; set; }
        public Games games { get; set; }
        public string player_id { get; set; }
        public OngoingRooms ongoing_rooms { get; set; }
        public object ongoing_tournaments { get; set; }
    }

    public class FACEITPlayerJSON
    {
        public long epoch { get; set; }
        public string env { get; set; }
        public string ver { get; set; }
        public Data data { get; set; }
    }
}
