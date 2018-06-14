using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Discord;

namespace DiscordBot.Modules
{
    public class Administration : ModuleBase
    {
        [Command("kick")]
        [Summary("This will kick a user.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task KickUser(IGuildUser user, string reason = "No reason provided.")
        {
            await user.KickAsync(reason);
        }

        [Command("ban")]
        [Summary("This will ban a user, and delete all of his messages for the past 5 days.")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanUser(IGuildUser user, string reason = "No reason provided.")
        {
            await user.Guild.AddBanAsync(user, 5, reason);
        }

        [Command("purge")]
        [Summary("This will delete as many messages you input.")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        [Alias("clear", "delete")]
        public async Task Purge([Remainder] int num = 0)
        {
            if (num <= 100)
            {
                var msgToDelete = await Context.Channel.GetMessagesAsync(num + 1).Flatten();
                await Context.Channel.DeleteMessagesAsync(msgToDelete);

                if (num == 1)
                {
                    await Context.Channel.SendMessageAsync(Context.User.Username + " deleted 1 message.");
                }

                else
                {
                    await Context.Channel.SendMessageAsync(Context.User.Username + " deleted " + num + " messages.");
                }
            }
            else
            {
                await ReplyAsync("You can't delete more than 100 messages at once!");
            }
        }
    }
}
