﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Discord;

namespace DiscordBot.Modules
{
    public class Choice : ModuleBase
    {
        [Command("pick")]
        [Summary("The bot will choose one item, between the items you type in. Split them with |.")]
        public async Task PickOne([Remainder, Summary("The bot will choose one item, between the items you type in. Split them with |.")] string rando)
        {
            string[] options = rando.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            Random r = new Random();
            string selection = options[r.Next(0, options.Length)];

            var embed = new EmbedBuilder();
            embed.WithTitle("Choice for: " + Context.User.Username);
            embed.WithDescription(selection);
            embed.WithColor(new Color(0, 191, 255));


            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("roll")]
        [Summary("Rolls a dice of arbitrary size. (example: \'!roll 33\' rolls a 33-sided die)")]
        public async Task Say(string argSize)
        {
            Random rand = new Random();
            uint num = 0;
            num = (uint)rand.Next(1, (int)Convert.ToUInt32(argSize, 10));
            await Context.Channel.SendMessageAsync("You rolled a " + num);
        }
    }
}
