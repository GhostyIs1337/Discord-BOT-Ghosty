using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Discord;

namespace DiscordBot.Modules
{
    public class Talking : ModuleBase
    {
        [Command("say")]
        [Summary("Echos a message.")]
        public async Task Say([Remainder, Summary("The text to echo")] string echo)
        {
            await ReplyAsync(echo);
        }
    }

    /*  public class Reversev2 : ModuleBase
      {
          [Command("rev"), Summary("Reverses a message.")]
          public async Task Reverse([Remainder, Summary("The text to reverse")] string echo)
          {
              char[] charArray = echo.ToCharArray();
              Array.Reverse(charArray);
              new string(charArray);
              await ReplyAsync(charArray);

          }
      }*/
}