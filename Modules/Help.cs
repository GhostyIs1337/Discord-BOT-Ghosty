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

    public class Help : ModuleBase
    {
        private CommandService commands;

        public Help(CommandService _commands)
        {
            commands = _commands;
        }

        [Command("help")]
        [Summary("Displays all commands that control this bot.")]
        public async Task HelpCommand([Remainder, Summary("Command to retrieve help for.")] string command = null)
        {
            string prefix = ".";

            if (command == null)
            {
                var builder = new EmbedBuilder()
                {
                    Color = new Color(204, 0, 102),
                    Description = "These are the commands available for this bot."
                };

                foreach (var module in commands.Modules)
                {
                    string description = null;
                    foreach (var cmd in module.Commands)
                    {
                        var result = await cmd.CheckPreconditionsAsync(Context);
                        if (result.IsSuccess)
                            description += $"{prefix}{cmd.Aliases.First()}\n";
                    }

                    if (!string.IsNullOrWhiteSpace(description))
                    {
                        builder.AddField(x =>
                        {
                            x.Name = module.Name;
                            x.Value = description;
                            x.IsInline = false;
                        });
                    }


                }

                await ReplyAsync("", false, builder.Build());
            }
            else
            {
                var result = commands.Search(Context, command);

                if (!result.IsSuccess)
                {
                    await ReplyAsync($"Sorry, I couldn't find a command like **{command}**.");
                    return;
                }

                var builder = new EmbedBuilder()
                {
                    Color = new Color(204, 0, 102),
                    Description = $"Help for command: **{prefix}{command}**\n\nAliases: "
                };

                foreach (var match in result.Commands)
                {
                    var cmd = match.Command;

                    builder.AddField(x =>
                    {
                        x.Name = string.Join(", ", cmd.Aliases);
                        x.Value =
                            $"Summary: {cmd.Summary}\n" +
                            $"Parameters: {string.Join(", ", cmd.Parameters.Select(p => p.Name))}: {string.Join("", cmd.Parameters.Select(p => p.Summary))}\n";
                        x.IsInline = false;
                    });
                }

                await ReplyAsync("", false, builder.Build());
            }
        }
    }
}
