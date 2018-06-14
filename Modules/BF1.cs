using Discord;
using Discord.Commands;
using DiscordBot.Services;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using DiscordBot.API_Keys;

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
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("trn-api-key", "" + Keys.BF1);

                using (var response = await httpClient.GetAsync($"Stats/DetailedStats?platform=3&displayName={user}"))
                {
                    var responseData = await response.Content.ReadAsStringAsync();

                    var bfStats = JsonConvert.DeserializeObject<BF1Json>(responseData);

                    var displayName = bfStats.profile.displayName;
                    var uRL = bfStats.profile.trackerUrl;
                    var kills = bfStats.result.basicStats.kills;
                    var rank = bfStats.result.basicStats.rank;
                    var wins = bfStats.result.basicStats.wins;
                    var spm = bfStats.result.basicStats.spm;
                    var timePlayed = bfStats.result.basicStats.timePlayed;
                    var favClass = bfStats.result.favoriteClass;
                    var hS = bfStats.result.headShots;
                    var longHS = bfStats.result.longestHeadShot;
                    var dogsTaken = bfStats.result.dogtagsTaken;
                    var highKS = bfStats.result.highestKillStreak;

                    var embed = new EmbedBuilder()
                    {
                        Color = new Color(148, 0, 211)
                    };

                    embed.Title = $"**{displayName}** information:";
                    embed.Description = $"Tracker URL: **{uRL}**\n"
                        + $"Rank: **{rank}**\n"
                        + $"Kills: **{kills}**\n"
                        + $"Wins: **{wins}**\n"
                        + $"Score per minute: **{spm}**\n"
                        + $"Favorite class: **{favClass}**\n"
                        + $"Dog tags taken: **{dogsTaken}**\n"
                        + $"Headshots: **{hS}**\n"
                        + $"Longest headshot: **{longHS}**\n"
                        + $"Highest kill streak: **{highKS}**\n"
                        + $"Time played: **{timePlayed}**";

                    await ReplyAsync("", false, embed.Build());
                }
            }
        }
    }
}