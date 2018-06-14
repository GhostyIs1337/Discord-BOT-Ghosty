using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using System.Net.Http;
using Newtonsoft.Json;
using FaceitPlayerJson.Services;
using FACEITGAMEJson.Services;
using FaceitMatch.Services;
using FaceitMatchID.Services;
using DiscordBot.API_Keys;

namespace DiscordBot.Modules
{
    public class FACEIT : ModuleBase
    {
        [Command("faceit")]
        [Alias("fi")]
        [Summary("Displays a users FACEIT stats.")]

        public async Task FaceIT(string user)
        {
            var baseAddress = new Uri("https://open.faceit.com/data/v3/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "" + Keys.faceit);

                using (var response = await httpClient.GetAsync($"players?nickname={user}"))
                {

                    string responseData = await response.Content.ReadAsStringAsync();

                    FACEITPlayerJSON faceitStats = JsonConvert.DeserializeObject<FACEITPlayerJSON>(responseData);


                    var nickname = faceitStats.data.nickname;
                    var pID = faceitStats.data.player_id;
                    var hp = faceitStats.data.homepage;
                    var ava = faceitStats.data.avatar;
                    var gameID = faceitStats.data.games.csgo.game_profile_id;

                    var embed = new EmbedBuilder()
                    {
                        Color = new Color(148, 0, 211)
                    };

                    embed.Title = $"**{nickname}** information:";
                    embed.Description = $"URL: **{hp}**\n"
                        + $"User ID: **{pID}**\n"
                        + $"Avatar: **{ava}**\n"
                        + $"Game profile ID: **{gameID}**\n";

                    await ReplyAsync("", false, embed.Build());

                }
            }
        }


        [Command("faceit -l")]
        [Alias("fig")]
        [Summary("Displays a users FACEIT game stats.")]

        public async Task FaceITGame(string user, string game)
        {
            var baseAddress = new Uri("https://open.faceit.com/data/v3/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Keys.faceit);

                using (var response = await httpClient.GetAsync($"players/{user}/games/{game}/stats"))
                {

                    string responseData = await response.Content.ReadAsStringAsync();

                    FACEITGameJson faceitGameStats = JsonConvert.DeserializeObject<FACEITGameJson>(responseData);

                    var uID = user;
                    var lifetimeMatches = faceitGameStats.data.lifetime.Matches;
                    var lifetimeWins = faceitGameStats.data.lifetime.Wins;
                    var lifetimeCurrentWinStreak = faceitGameStats.data.lifetime.CurrentWinStreak;
                    var lifetimeWinStreak = faceitGameStats.data.lifetime.LongestWinStreak;
                    var lifetimeWinRatePercentage = faceitGameStats.data.lifetime.WinRatePercentage;
                    var lifetimeKDR = faceitGameStats.data.lifetime.KDR;
                    var lifetimeAverageKDR = faceitGameStats.data.lifetime.AverageKDR;
                    var lifetimeTotalHS = faceitGameStats.data.lifetime.TotalHeadshotsPercentage;
                    var lifetimeAverageHSPercentage = faceitGameStats.data.lifetime.AverageHeadshotsPercentage;


                    var embed = new EmbedBuilder()
                    {
                        Color = new Color(148, 0, 211)
                    };

                    embed.Title = $"**{uID}** information:";
                    embed.Description = $"Lifetime matches: **{lifetimeMatches}**\n"
                    + $"Lifetime wins: **{lifetimeWins}**\n"
                    + $"Current win streak: **{lifetimeCurrentWinStreak}**\n"
                    + $"Longest win streak: **{lifetimeWinStreak}**\n"
                    + $"Lifetime win rate percentage: **{lifetimeWinRatePercentage}%**\n"
                    + $"Lifetime average kill death ratio: **{lifetimeAverageKDR}%**\n"
                    + $"Lifetime total headshots: **{lifetimeTotalHS}**\n"
                    + $"Lifetime average headshots percentage: **{lifetimeAverageHSPercentage}%**\n";

                    await ReplyAsync("", false, embed.Build());

                }
            }
        }

        [Command("faceit -id")]
        [Alias("fig")]
        [Summary("Displays a users FACEIT game stats.")]

        public async Task FaceITMatchID(string user)
        {
            var baseAddress = new Uri("https://open.faceit.com/data/v3/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Keys.faceit);

                using (var response = await httpClient.GetAsync($"players/{user}/games/csgo/history?limit=10"))
                {

                    string responseData = await response.Content.ReadAsStringAsync();

                    FaceitMatchIDS faceitMatchID = JsonConvert.DeserializeObject<FaceitMatchIDS>(responseData);

                    var uID = user;
                    var matchID = faceitMatchID.data.matches[0].match_id;


                    var embed = new EmbedBuilder()
                    {
                        Color = new Color(148, 0, 211)
                    };

                    embed.Title = $"**{uID}** information:";
                    embed.Description = $"Last ID of a FaceIT matches: **{matchID}**\n";

                    await ReplyAsync("", false, embed.Build());

                }
            }
        }

        [Command("faceit -m")]
        [Alias("fim")]
        [Summary("Displays a users FACEIT match stats.")]

        public async Task FaceITGameStats(string matchid)
        {
            var baseAddress = new Uri("https://open.faceit.com/data/v3/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Keys.faceit);

                using (var response = await httpClient.GetAsync($"matches/{matchid}"))
                {

                    string responseData = await response.Content.ReadAsStringAsync();

                    FaceitMatchJson faceitMatchStats = JsonConvert.DeserializeObject<FaceitMatchJson>(responseData);

                    var fact1Name = faceitMatchStats.data.faction1_name;
                    var fact2Name = faceitMatchStats.data.faction2_name;
                    var fact1Leader = faceitMatchStats.data.faction1_leader;
                    var fact2Leader = faceitMatchStats.data.faction2_leader;
                    var matchURL = faceitMatchStats.data.match_url;
                    var mapName = faceitMatchStats.data.voted_entities[0].map.name;
                    var servLoc = faceitMatchStats.data.voted_entities[0].location.country;
                    var startAt = faceitMatchStats.data.started_at;
                    var endAt = faceitMatchStats.data.finished_at;
                    var winner = faceitMatchStats.data.winner;


                    var embed = new EmbedBuilder()
                    {
                        Color = new Color(148, 0, 211)
                    };

                    embed.Title = $"**{matchid}** information:";
                    embed.Description = $"team_{fact1Name} VERSUS team_{fact2Name}\n"
                    + $"Captains: **{fact1Name}** & **{fact2Name}**\n"
                    + $"Match URL: **{matchURL}**\n"
                    + $"Map name: **{mapName}**\n"
                    + $"Server location: **{servLoc}**\n"
                    + $"Started at: **{startAt}**\n"
                    + $"Ended at: **{endAt}**\n"
                    + $"Winner: **{winner}**\n";

                    await ReplyAsync("", false, embed.Build());

                }
            }
        }
    }
}
