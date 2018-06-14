using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Services;
using Microsoft.Extensions.DependencyInjection;
using SteamWebAPI2.Interfaces;
using System;
using System.Reflection;
using System.Threading.Tasks;
using DiscordBot.API_Keys;

namespace DiscordBot
{
    public class Program
    {

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private CommandService commands;
        private DiscordSocketClient client;
        private IServiceProvider services;
        private AudioService audioService;

        public async Task MainAsync()
        {
            client = new DiscordSocketClient();
            var config = new DiscordSocketConfig { MessageCacheSize = 100, AlwaysDownloadUsers = true, LogLevel = LogSeverity.Verbose };
            commands = new CommandService();

            client = new DiscordSocketClient(config);

            audioService = new AudioService(ref client);

            services = new ServiceCollection()
                .AddSingleton(audioService)
                .AddSingleton<ISteamUser>(new SteamUser("" + Keys.steam))
                .AddSingleton<ISteamUserStats>(new SteamUserStats("" + Keys.steam))
                .BuildServiceProvider();

            client.Log += Log;

            client.UserJoined += AnnounceJoinedUser;
            client.UserLeft += AnnounceLeftUser;
            client.UserBanned += AnnounceUserBanned;
            client.UserLeft += AnnounceUserLeft;
            client.UserUnbanned += AnnounceUserUnbanned;
            client.GuildMemberUpdated += AnnounceUpdatedUser;

            await client.LoginAsync(TokenType.Bot, "" + Keys.bot);
            await client.StartAsync();
            await InstallCommands();

            client.MessageUpdated += MessageUpdated;
            client.Ready += () =>
            {
                Console.WriteLine("\n-----------------The bot has connected!-------------------\n");
                return Task.CompletedTask;
            };


            await client.SetGameAsync("Programming", "https://twitch.tv/Ghosty1337", StreamType.NotStreaming);

            // Block this task until the program is closed.
            await Task.Delay(-1);


        }

        private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            var message = await before.GetOrDownloadAsync();
            Console.WriteLine($"{message} -> {after} -> {channel}");
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public async Task InstallCommands()
        {
            // Hook the MessageReceived Event into our Command Handler
            client.MessageReceived += HandleCommand;
            // Discover all of the commands in this assembly and load them.
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());

        }

        public async Task HandleCommand(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            int argPos = 0;

            if (!(message.HasCharPrefix('.', ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))) return;
            var context = new CommandContext(client, message);
            var result = await commands.ExecuteAsync(context, argPos, services);

            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);

        }

        public async Task AnnounceJoinedUser(SocketGuildUser user)
        {
            var channel = client.GetChannel(230003727279456257) as SocketTextChannel;
            var info = client.GetChannel(230010672837033984) as SocketTextChannel;
            var genENG = client.GetChannel(380691639796236288) as SocketTextChannel;
            var genSLO = client.GetChannel(380692078687944714) as SocketTextChannel;
            var guest = user.Guild.GetRole(428100630427598848);
            var member = user.Guild.GetRole(228911048374222850);

            await user.AddRoleAsync(guest);

            await channel.SendMessageAsync("Welcome " + user.Mention + " to the best server on the interwebs! " +
                "You currently have a " + guest.Mention + " role. Please visit our " + info.Mention + " text channel. " +
                "Once you've done that, type in .register in one of these two channels: " + genENG.Mention + " " + genSLO.Mention +
                " so you'll get a " + member.Mention + " role added to your account. " +
                "Have a nice day!");

        }

        public async Task AnnounceLeftUser(SocketGuildUser user)
        {
            var channel = client.GetChannel(230003727279456257) as SocketTextChannel;
            await channel.SendMessageAsync(user.Mention + " has just left the building. :no_mouth:");
        }

        private async Task AnnounceUserBanned(SocketUser user, SocketGuild arg2)
        {
            var logChannel = client.GetChannel(349220360836612108) as SocketTextChannel;
            await logChannel.SendMessageAsync($"User banned: {user.Mention}");
        }

        private async Task AnnounceUserUnbanned(SocketUser user, SocketGuild arg2)
        {
            var logChannel = client.GetChannel(349220360836612108) as SocketTextChannel;
            await logChannel.SendMessageAsync($"User unbanned: {user.Mention}");
        }

        private async Task AnnounceUserLeft(SocketUser user)
        {
            var logChannel = client.GetChannel(349220360836612108) as SocketTextChannel;
            await logChannel.SendMessageAsync($"User left: {user.Mention}");
        }

        public async Task AnnounceUpdatedUser(SocketGuildUser user, SocketGuildUser newUser)
        {
            var logChannel = client.GetChannel(349220360836612108) as SocketTextChannel;

            if (user.Nickname != newUser.Nickname)
            {
                await logChannel.SendMessageAsync($"User {user.Nickname} has changed his nickname to {newUser.Nickname}");
            }
        }

    }
}