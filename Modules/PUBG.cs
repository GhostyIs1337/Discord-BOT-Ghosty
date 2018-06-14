using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Discord;
using DiscordBot;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using PUBG.Services;
using System.Net.Http.Headers;
using PUBGMatch.Services;

namespace DiscordBot.Modules
{
    public class PUBG : ModuleBase
    {
        [Command("pubg")]
        [Summary("Displays a users PUBG IDs (account, matches).")]

        public async Task PUBGID(string region, string user)
        {
            HttpClient httpClient = new HttpClient();

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://api.playbattlegrounds.com/shards/{region}/players?filter[playerNames]={user}");

            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.api+json"));

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "your-api-token");

            HttpResponseMessage response = await httpClient.SendAsync(requestMessage);

            string responseData = await response.Content.ReadAsStringAsync();

            PUBGJson pubgStats = JsonConvert.DeserializeObject<PUBGJson>(responseData);

            var name = pubgStats.data[0].attributes.Name;
            var titleID = pubgStats.data[0].attributes.titleId;
            var patchVer = pubgStats.data[0].attributes.patchVersion;
            var ID = pubgStats.data[0].id;

            var embed = new EmbedBuilder()
            {
                Color = new Color(148, 0, 211)
            };

            embed.Title = $"**{name}** information:";
            embed.Description = $"Game: **{titleID}**\n"
                + $"Account ID: **{ID}**\n"
                + $"Last 5 match IDs:\n";

            foreach (var match in pubgStats.data[0].relationships.matches.data.Take(5))
            {
                embed.Description += $"{match.id}\n";
            }

            await ReplyAsync("", false, embed.Build());
        }

        [Command("pubg -m")]
        [Summary("Displays a users match information.")]

        public async Task PUBGMatch(string region, string matchID)
        {
            HttpClient httpClient = new HttpClient();

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://api.playbattlegrounds.com/shards/{region}/matches/{matchID}");

            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.api+json"));

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "your-api-token");

            HttpResponseMessage response = await httpClient.SendAsync(requestMessage);

            string responseData = await response.Content.ReadAsStringAsync();

            PUBGMatchJson pubgMatchStats = JsonConvert.DeserializeObject<PUBGMatchJson>(responseData);

            var ID = pubgMatchStats.data.id;
            var gameMode = pubgMatchStats.data.attributes.gameMode;
            var duration = pubgMatchStats.data.attributes.duration;
            var kills = pubgMatchStats.included[1].attributes.stats.kills;
            var assists = pubgMatchStats.included[1].attributes.stats.assists;
            var downed = pubgMatchStats.included[1].attributes.stats.DBNOs;
            var headshots = pubgMatchStats.included[1].attributes.stats.headshotKills;
            var longestKill = pubgMatchStats.included[1].attributes.stats.longestKill;
            var name = pubgMatchStats.included[1].attributes.stats.name;
            var revives = pubgMatchStats.included[1].attributes.stats.revives;
            var dealtDamage = pubgMatchStats.included[1].attributes.stats.damageDealt;
            var deathType = pubgMatchStats.included[1].attributes.stats.deathType;
            var heals = pubgMatchStats.included[1].attributes.stats.heals;
            var lastWinPoints = pubgMatchStats.included[1].attributes.stats.lastWinPoints;
            var roadKills = pubgMatchStats.included[1].attributes.stats.roadKills;
            var rideDistance = pubgMatchStats.included[1].attributes.stats.rideDistance;
            var teamKills = pubgMatchStats.included[1].attributes.stats.teamKills;
            var timeSurvived = pubgMatchStats.included[1].attributes.stats.timeSurvived;
            var walkDistance = pubgMatchStats.included[1].attributes.stats.walkDistance;
            var weaponsAcquired = pubgMatchStats.included[1].attributes.stats.weaponsAcquired;


            var embed = new EmbedBuilder()
            {
                Color = new Color(148, 0, 211)
            };

            embed.Title = $"Match: **{ID}** stats for {name}:";
            embed.Description = $"Game mode: {gameMode}\n"
                + $"Duration: {duration}\n"
                + $"Kills: {kills}\n"
                + $"Assists: {assists}\n"
                + $"Longest kill: {longestKill}\n"
                + $"Headshots: {headshots}\n"
                + $"Knocked out: {downed}\n"
                + $"Damage dealt: {dealtDamage}\n"
                + $"Team kills: {teamKills}\n"
                + $"Road kills: {roadKills}\n"
                + $"Revives: {revives}\n"
                + $"Heals: {heals}\n"
                + $"Distance driving: {rideDistance}\n"
                + $"Distance walking: {walkDistance}\n"
                + $"Time survived: {timeSurvived}\n"
                + $"Weapons acquired: {weaponsAcquired}\n"
                + $"Death type: {deathType}\n";

            await ReplyAsync("", false, embed.Build());


        }


    }
}

