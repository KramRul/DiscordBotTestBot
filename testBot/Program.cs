using System;
using DSharpPlus;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.VoiceNext;

namespace testBot
{
    class Program
    {
        static DiscordClient discord;
        
        static CommandsNextModule commands;

        static VoiceNextClient voice;

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            //Создание экземпляра клиента
            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = "NDU3NTI3NzA1NDgzODcwMjA4.DgaZfw.RfjvspNG8wln0wNISD21EeiTMmY",
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            });

            voice = discord.UseVoiceNext();

            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = "+++",
                EnableDms = false
            });

            commands.RegisterCommands<MyCommands>();
            commands.RegisterCommands<GrouppedCommands>();
            commands.RegisterCommands<VoiceCommands>();
            commands.RegisterCommands<TheDivisionCommands>();

            //Обработчик асинхронного параметризированного события (Parameterized event handler) 
            /*discord.MessageCreated += async e =>
            {
                //if (e.Message.Content.ToLower().StartsWith("ping"))
                //    await e.Message.RespondAsync("pong!");
            };*/

            await discord.ConnectAsync();

            await Task.Delay(-1);
        }
    }
}
