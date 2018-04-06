using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Discord;
using DiscordBot;
using System.Linq;

namespace DiscordBot.Modules
{
    public class Userinfo : ModuleBase
    {
        [Command("userinfo")]
        [Alias("user", "info", "ui")]
        [Summary("Outputs a users info")]

        public async Task User([Remainder, Summary("Outputs a users info")] IGuildUser user = null)
        {
            if (user == null)
            {
                await ReplyAsync("Please include a name.");
            }

            else
            {
                var app = await Context.Client.GetApplicationInfoAsync();
                var thumbURL = user.GetAvatarUrl();
                var date = $"{user.CreatedAt.Day}/{user.CreatedAt.Month}/{user.CreatedAt.Year}";
                var auth = new EmbedAuthorBuilder()
                {
                    Name = user.Username,
                    IconUrl = thumbURL,
                };

                var embed = new EmbedBuilder()
                {
                    Color = new Color(148, 0, 211),
                    Author = auth,
                };

                var us = user as SocketGuildUser;

                var username = us.Username;
                var disc = us.Discriminator;
                var id = us.Id;
                var dat = date;
                var stat = us.Status;
                var CC = us.JoinedAt;
                var game = us.Game;
                var nick = us.Nickname;

                embed.Title = $"**{us.Username}** information:";
                embed.Description = $"Username: **{username}**\n"
                    + $"Discriminator: **{disc}**\n"
                    + $"User ID: **{id}**\n"
                    + $"Nickname: **{nick}**\n"
                    + $"Created at: **{date}**\n"
                    + $"Current status: **{stat}**\n"
                    + $"Playing: **{game}**\n"
                    + $"Joined at: **{CC}**\n";

                await ReplyAsync("", false, embed.Build());

            }
        }
    }
}
