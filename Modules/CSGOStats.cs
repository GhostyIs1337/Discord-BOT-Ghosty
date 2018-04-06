using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using SteamWebAPI2.Utilities;
using SteamWebAPI2.Models;
using SteamWebAPI2.Exceptions;
using PortableSteam.Fluent.Game.Generic.IEconItems;
using PortableSteam.Infrastructure;
using SteamWebAPI2;
using PortableSteam;
using SteamWebAPI2.Interfaces;

namespace DiscordBot.Modules
{
    public class CSGOStats : ModuleBase
    {
        private readonly ISteamUser _steamUser;
        private readonly ISteamUserStats _steamUserStats;

        public CSGOStats(ISteamUser steamUser, ISteamUserStats steamUserStats)
        {
            _steamUser = steamUser;
            _steamUserStats = steamUserStats;
        }


        [Command("csgo")]
        [Alias("cs", "csgostats")]
        [Summary("Displays a users CSGO stats. When searching for a user, use his Steam 64 ID. You can convert it at: https://steamid.io")]

        public async Task CSGOStatsAPI(string name)
        {
            if (name == null) { name = Context.User.Username; }

            Dictionary<string, double> dict = (await _steamUserStats.GetUserStatsForGameAsync(
                                                 (await _steamUser.ResolveVanityUrlAsync(name)).Data,
                                                 730)
                                             ).Data.Stats.ToDictionary(x => x.Name, x => x.Value);

            await ReplyAsync("", embed: new EmbedBuilder()
            {
                Title = $"CS:GO stats for {name}",
                Color = new Color(255, 0, 0)
            }
            .AddField(f =>
            {
                f.IsInline = true;
                f.Name = "Kills";
                f.Value = dict["total_kills"].ToString();
            })
            .AddField(f =>
            {
                f.IsInline = true;
                f.Name = "Deaths";
                f.Value = dict["total_deaths"].ToString();
            })
            .AddField(f =>
            {
                f.IsInline = true;
                f.Name = "K/D";
                f.Value = Math.Round(dict["total_kills"] / dict["total_deaths"], 2).ToString();
            })
            .AddField(f =>
            {
                f.IsInline = true;
                f.Name = "Headshots";
                f.Value = Math.Round(100.0 / dict["total_kills"] * dict["total_kills_headshot"], 2) + "%";
            })
            .AddField(f =>
            {
                f.IsInline = true;
                f.Name = "Accuracy";
                f.Value = Math.Round(100.0 / dict["total_shots_fired"] * dict["total_shots_hit"], 2) + "%";
            })
            .AddField(f =>
            {
                f.IsInline = true;
                f.Name = "Playtime";
                f.Value = Math.Round(dict["total_time_played"] / 60 / 60, 2) + " hours";
            }).Build());
        }

        [Command("csgolastmatch")]
        [Alias("csgom", "cslast", "lastmatch")]
        [Summary("Returns stats of the player's last CS:GO match")]
        public async Task CSGOLastMatch(string name = null)
        {
            if (name == null) { name = Context.User.Username; }

            Dictionary<string, double> dict = (await _steamUserStats.GetUserStatsForGameAsync(
                                                (await _steamUser.ResolveVanityUrlAsync(name)).Data,
                                                730)
                                            ).Data.Stats.ToDictionary(x => x.Name, x => x.Value);

            await ReplyAsync("", embed: new EmbedBuilder()
            {
                Title = $"Last match CS:GO stats for {name}",
                Color = new Color(255, 0, 0)
            }
            .AddField(f =>
            {
                f.IsInline = true;
                f.Name = "Kills";
                f.Value = dict["last_match_kills"].ToString();
            })
            .AddField(f =>
            {
                f.IsInline = true;
                f.Name = "Deaths";
                f.Value = dict["last_match_deaths"].ToString();
            })
            .AddField(f =>
            {
                f.IsInline = true;
                f.Name = "K/D";
                f.Value = Math.Round(dict["last_match_kills"] / dict["last_match_deaths"], 2).ToString();
            })
            .AddField(f =>
            {
                f.IsInline = true;
                f.Name = "MVP";
                f.Value = dict["last_match_mvps"].ToString();
            })
            .Build());
        }
    }
}
