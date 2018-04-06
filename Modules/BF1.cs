using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Discord;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using DiscordBot.Services;

namespace DiscordBot.Modules
{
    public class BF1 : ModuleBase
    {
        [Command("battlefield")]
        [Alias("bf", "bf1")]
        [Summary("Displays a users Battlefield 1 stats.")]

        public async Task Battlefield(string user)
        {
            var baseAddress = new Uri("https://battlefieldtracker.com/bf1/api/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("trn-api-key", "your-api-token");

                using (var response = await httpClient.GetAsync($"Stats/DetailedStats?platform=3&displayName={user}"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();

                    BF1Json bfStats = JsonConvert.DeserializeObject<BF1Json>(responseData);

                    var displayName = bfStats.profile.displayName;
                    var URL = bfStats.profile.trackerUrl;
                    var kills = bfStats.result.basicStats.kills;
                    var rank = bfStats.result.basicStats.rank;
                    var wins = bfStats.result.basicStats.wins;
                    var spm = bfStats.result.basicStats.spm;
                    var timePlayed = bfStats.result.basicStats.timePlayed;
                    var favClass = bfStats.result.favoriteClass;
                    var HS = bfStats.result.headShots;
                    var longHS = bfStats.result.longestHeadShot;
                    var dogsTaken = bfStats.result.dogtagsTaken;
                    var highKS = bfStats.result.highestKillStreak;

                    var embed = new EmbedBuilder()
                    {
                        Color = new Color(148, 0, 211)
                    };

                    embed.Title = $"**{displayName}** information:";
                    embed.Description = $"Tracker URL: **{URL}**\n"
                        + $"Rank: **{rank}**\n"
                        + $"Kills: **{kills}**\n"
                        + $"Wins: **{wins}**\n"
                        + $"Score per minute: **{spm}**\n"
                        + $"Favorite class: **{favClass}**\n"
                        + $"Dog tags taken: **{dogsTaken}**\n"
                        + $"Headshots: **{HS}**\n"
                        + $"Longest headshot: **{longHS}**\n"
                        + $"Highest kill streak: **{highKS}**\n"
                        + $"Time played: **{timePlayed}**";



                    await ReplyAsync("", false, embed.Build());

                }
            }
        }
    }
}