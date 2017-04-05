using System.Threading.Tasks;
using DSharpPlus.Commands;
using FirstDiscordBot.Interfaces;

namespace FirstDiscordBot.Commands
{
    public class TucuCommand:ICommand
    {
        public async Task Execute(CommandEventArgs e)
        {
            await e.Message.Respond("Mifi");
        }
    }
}