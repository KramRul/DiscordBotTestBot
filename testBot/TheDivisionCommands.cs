using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.VoiceNext;
using ScrapySharp;
using ScrapySharp.Network;
using ScrapySharp.Html.Forms;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Html;

namespace testBot
{
    public class TheDivisionCommands
    {
        [Command("short")]
        public async Task Join(CommandContext ctx, [RemainingText] string nickname)
        {
            await ctx.TriggerTypingAsync();

            ScrapingBrowser browser = new ScrapingBrowser();

            //set UseDefaultCookiesParser as false if a website returns invalid cookies format
            //browser.UseDefaultCookiesParser = false;

            WebPage homePage = browser.NavigateToPage(new Uri("http://divisiontracker.com/profile/uplay/"+ nickname));

            List<HtmlNode> resultNames = homePage.Html.CssSelect("div.stats-stat>div.name").ToList();

            List<HtmlNode> resultValues = homePage.Html.CssSelect("div.stats-stat>div.value").ToList();

            Dictionary<string,string> namesAndValues = new Dictionary<string, string>();

            for(int i=0,j=0;i<resultNames.Count;i++,j++)
            {
                namesAndValues.Add(resultNames[i].InnerText, resultValues[j].InnerText);
            }

            DiscordEmbedBuilder builder = new DiscordEmbedBuilder();
            builder.WithTitle($"{ctx.Client.CurrentUser.Username}\n{ctx.Command.Name}\n{nickname}").WithDescription($"PLAYTIME: {namesAndValues["\nPlaytime \n"]}ROGUE PLAYERS KILLED: \n{namesAndValues["\nRogue Players Killed \n"]}").WithColor(DiscordColor.Orange);

            await ctx.RespondAsync("", false, builder.Build());
        }
    }
}
