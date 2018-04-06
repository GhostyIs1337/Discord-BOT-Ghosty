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
    public class Registration : ModuleBase
    {
        [Command("register")]
        [Summary("Registeres a guest")]

        public async Task Guest()
        {
            var unregisteredRole = Context.Guild.GetRole(428100630427598848);
            var registered = Context.Guild.GetRole(228911048374222850);

            var userList = await Context.Guild.GetUsersAsync();
            var user = userList.Where(input => input.Username == Context.Message.Author.Username).FirstOrDefault() as SocketGuildUser;


            if (user.Roles.Contains(unregisteredRole))
            {
                await user.RemoveRoleAsync(unregisteredRole);
                await user.AddRoleAsync(registered);

                await ReplyAsync("Congratulations! We've removed your guest role and added the member role. " +
                    "Keep on being active and you'll be promoted through the ranks, private." +
                    "Best of luck!");
            }
            else
            {
                await user.AddRoleAsync(registered);
            }

        }
    }
}
