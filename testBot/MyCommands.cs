using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.VoiceNext;

namespace testBot
{
    public class MyCommands
    {
        [Command("hi")]
        public async Task Hi(CommandContext ctx)
        {
            await ctx.RespondAsync($"👋 Hi, {ctx.User.Mention}!");
        }

        [Command("random")]
        public async Task Random(CommandContext ctx, int min, int max)
        {
            await ctx.TriggerTypingAsync();
            var rnd = new Random();
            await ctx.RespondAsync($"🎲 Your random number is: {rnd.Next(min, max)}");
        }

        [Command("ping")]
        [Description("Example ping command")]
        public async Task Ping(CommandContext ctx) 
        {
            // let's trigger a typing indicator to let users know we're working
            await ctx.TriggerTypingAsync();

            // let's make the message a bit more colourful
            var emoji = DiscordEmoji.FromName(ctx.Client, ":ping_pong:");

            DiscordEmbedBuilder builder = new DiscordEmbedBuilder();
            builder.WithTitle("Ping").WithDescription($"{emoji} Pong! Ping: {ctx.Client.Ping}ms").WithColor(DiscordColor.Blue);

            // respond with ping
            await ctx.RespondAsync("",false,builder.Build());
        }

        [Command("sum"), Description("Sums all given numbers and returns said sum.")]
        public async Task SumOfNumbers(CommandContext ctx, [Description("Integers to sum.")] params int[] args)
        {
            // note the params on the argument. It will indicate
            // that the command will capture all the remaining arguments
            // into a single array

            // let's trigger a typing indicator to let
            // users know we're working
            await ctx.TriggerTypingAsync();

            // calculate the sum
            var sum = args.Sum();

            // and send it to the user
            await ctx.RespondAsync($"The sum of these numbers is {sum.ToString("#,##0")}");
        }
    }
}
