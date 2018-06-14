using Discord;
using Discord.Commands;
using Newtonsoft.Json.Linq;
using PortableSteam;
using SteamWebAPI2.Interfaces;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class Steam : ModuleBase
    {
        readonly ISteamUser steamUser;

        public Steam(ISteamUser steamUser)
        {
            this.steamUser = steamUser;
        }

        [Command("vanityurl")]
        [Alias("resolvevanityurl", "vurl")]
        [Summary("Returns the steamID64 of the user")]
        public async Task ResolveVanityURL(string name = null)
        {
            if (name == null) { name = Context.User.Username; }

            await ReplyAsync(((await steamUser.ResolveVanityUrlAsync(name)).Data).ToString());
        }

        [Command("steam")]
        [Summary("Returns basic steam profile information")]
        public async Task PlayerSummaries(string name = null)
        {
            var player = (await steamUser.GetPlayerSummaryAsync(
                                            (await steamUser.ResolveVanityUrlAsync(name ?? Context.User.Username)).Data)
                                        ).Data;

            await ReplyAsync("", embed: new EmbedBuilder()
            {
                Title = $"Player summary for {player.Nickname}",
                Color = new Color(255, 0, 0),
                ThumbnailUrl = player.AvatarMediumUrl,
                Url = player.ProfileUrl
            }
            .AddField(f =>
            {
                f.IsInline = true;
                f.Name = "SteamID";
                f.Value = player.SteamId;
            })
            .AddField(f =>
            {
                f.IsInline = true;
                f.Name = "Nickname";
                f.Value = player.Nickname;
            })
            .AddField(f =>
            {
                f.IsInline = true;
                f.Name = "Persona state";
                f.Value = (PersonaState)Enum.ToObject(typeof(PersonaState), player.ProfileState);
            })
            .Build());
        }

        [Command("playerbans")]
        [Alias("getplayerbans", "pbans", "pb")]
        [Summary("Returns Community, VAC, and economy ban statuses for given players")]
        public async Task PlayerBans(string name = null)
        {
            var player = (await steamUser.GetPlayerBansAsync(
                                            (await steamUser.ResolveVanityUrlAsync(name ?? Context.User.Username)).Data)
                                        ).Data.FirstOrDefault();

            await ReplyAsync("", embed: new EmbedBuilder()
            {
                Title = $"Community, VAC, and economy ban statuses",
                Color = new Color(255, 0, 0),
            }
            .AddField(f =>
            {
                f.IsInline = true;
                f.Name = "SteamID";
                f.Value = player.SteamId;
            })
            .AddField(f =>
            {
                f.IsInline = true;
                f.Name = "Community banned";
                f.Value = player.CommunityBanned;
            })
            .AddField(f =>
            {
                f.IsInline = true;
                f.Name = "VAC banned";
                f.Value = player.VACBanned;
            })
            .AddField(f =>
            {
                f.IsInline = true;
                f.Name = "Number of VAC bans";
                f.Value = player.NumberOfVACBans;
            })
            .AddField(f =>
            {
                f.IsInline = true;
                f.Name = "Days since last ban";
                f.Value = player.DaysSinceLastBan;
            })
            .AddField(f =>
            {
                f.IsInline = true;
                f.Name = "Number of game bans";
                f.Value = player.NumberOfGameBans;
            })
            .AddField(f =>
            {
                f.IsInline = true;
                f.Name = "Economy ban";
                f.Value = player.EconomyBan;
            })
            .Build());
        }

        [Command("steamprofile")]
        [Alias("sprofile", "sp", "profile")]
        [Summary("Returns the URL to the steam profile of the user")]
        public async Task SteamProfile(string name = null)
            => await ReplyAsync("https://steamcommunity.com/profiles/" +
                    (await steamUser.ResolveVanityUrlAsync(name ?? Context.User.Username)).Data
                );

        [Command("appinfo"), Summary("Fetches up application info from steam.")]
        public async Task Say([Remainder, Summary("The appid to search for")] int appid)
        {
            string json = new WebClient().DownloadString("http://store.steampowered.com/api/appdetails?appids=" + "" + appid);
            JObject obj = JObject.Parse(json);

            string name = (string)obj["" + appid]["data"]["name"];
            int age = (int)obj["" + appid]["data"]["required_age"];
            string website = (string)obj["" + appid]["data"]["website"];
            double price = (int)obj["" + appid]["data"]["price_overview"]["final"] / 100.0;

            await Context.Channel.SendMessageAsync("Fetching Details: \n"
                + "Name: " + name + "\n"
                + "Minimum Age: " + age + "\n"
                + "Price: $" + price + " (USD)\n"
                + "More Info: " + website + " \n");
        }
    }
}