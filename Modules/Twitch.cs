using Discord.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchDotNet.Clients;

namespace DiscordBot.Modules
{
    public class Twitch : ModuleBase
    {
        [Command("twitch")]

        public async Task Twitty(string games)
        {
            var baseUrl = "https://api.twitch.tv";
            var clientId = "your-api-token";

            // Init an unauthenticated client with base url and client id

            var client = new TwitchClient(baseUrl, clientId);

            // Search for games
            var result = await client.SearchGames($"{games}");
            JObject response_object = JObject.Parse(result?.ToString()); // Parse to JObject
            var found_game_list = JsonConvert.DeserializeObject<List<object>>(response_object["games"]?.ToString()); // Parse to list of game objects (refer to Twitch docs for returned JSON)

            // Get streams summary
            result = await client.GetStreamsSummary();
            response_object = JObject.Parse(result?.ToString()); // Parse to JObject
        }
    }
}