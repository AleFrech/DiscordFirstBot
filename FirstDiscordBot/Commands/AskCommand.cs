using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Commands;
using FirstDiscordBot.Interfaces;
using WebSocketSharp;
using WolframAlpha.Api.v2;
using WolframAlpha.Api.v2.Requests;

namespace FirstDiscordBot.Commands
{
    public class AskCommand:ICommand
    {
        private readonly QueryBuilder _wolfram;
        public AskCommand()
        {
            _wolfram = new QueryBuilder {AppId = "APV28R-3429WLQ6HL"};
        }

        public async Task Execute(CommandEventArgs e)
        {
            var regex = new Regex("\\s+");
            var commands = e.Message.Content.Split(new[]{' '},2);

            if (commands.Length == 1)
                await e.Message.Respond("Missing Expression");
            else
            {
                _wolfram.Input = commands[1];
                var request = new QueryRequest();
                var result = await request.ExecuteAsync(_wolfram.QueryUri);
                var fields = new List<DiscordEmbedField>();
                foreach (var pod in result.Pods)
                {
                    fields.AddRange(from subpod in pod.SubPods
                        where subpod.PlainText != null && !subpod.PlainText.IsNullOrEmpty()
                        select new DiscordEmbedField
                        {
                            Name = pod.Title,
                            Value = subpod.PlainText,
                            Inline = false
                        });
                }
                await e.Message.Respond("", false, new DiscordEmbed{Fields = fields});
            }
        }
    }
}