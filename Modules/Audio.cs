using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Discord;
using DiscordBot;
using DiscordBot.Services;

namespace DiscordBot.Modules
{
    public class Audio : ModuleBase
    {
        // Scroll down further for the AudioService.
        // Like, way down
        private readonly AudioService _service;

        // Remember to add an instance of the AudioService
        // to your IServiceCollection when you initialize your bot
        private Audio(AudioService service)
        {
            _service = service;

        }

        // You *MUST* mark these commands with 'RunMode.Async'
        // otherwise the bot will not respond until the Task times out.
        [Command("join", RunMode = RunMode.Async)]
        [Summary("Joins the voice channel you are currently in.")]
        public async Task JoinCmd()
        {
            await _service.JoinAudio(Context.Guild, (Context.User as IVoiceState).VoiceChannel);
        }

        // Remember to add preconditions to your commands,
        // this is merely the minimal amount necessary.
        // Adding more commands of your own is also encouraged.
        [Command("leave", RunMode = RunMode.Async)]
        [Summary("Leaves the voice channel the bot is currently in.")]
        public async Task LeaveCmd()
        {
            await _service.LeaveAudio(Context.Guild);
        }

        [Command("play", RunMode = RunMode.Async)]
        [Summary("Plays the song/video you input.")]
        public async Task PlayCmd([Remainder] string song)
        {
            await _service.SendLinkAsync(Context.Guild, Context.Channel, song, Context.User);
        }

        [Command("restart", RunMode = RunMode.Async)]
        [Summary("Restarts the current video.")]
        public async Task RestartCmd()
        {
            await _service.RestartAudio(Context.Channel);
        }

        [Command("skip", RunMode = RunMode.Async)]
        [Summary("Skips the current video.")]
        public async Task SkipCmd()
        {
            await _service.Skip(Context.Channel);
        }

        [Command("skip -n", RunMode = RunMode.Async)]
        [Summary("Skips the number of videos you type in.")]
        public async Task SkipCmd([Remainder] int numTracks)
        {
            await _service.SkipNum(Context.Channel, numTracks);
        }

        [Command("pause", RunMode = RunMode.Async)]
        [Summary("Pauses the current video.")]
        public async Task PauseCmd()
        {
            await _service.PauseAudio(Context.Channel);
        }

        [Command("queue", RunMode = RunMode.Async)]
        [Summary("Displays the current queue.")]
        public async Task ShowQueueCmd()
        {
            await _service.DisplayQueue(Context.Channel);
        }

        [Command("stop", RunMode = RunMode.Async)]
        [Summary("Stops the current video.")]
        public async Task StopCmd()
        {
            await _service.StopAudio(Context.Guild);
        }

        [Command("nowplaying", RunMode = RunMode.Async)]
        [Summary("Displays the currently playing video.")]
        public async Task NowPlayingCmd()
        {
            await _service.DisplayNowPlaying(Context.Channel);
        }
    }
}
