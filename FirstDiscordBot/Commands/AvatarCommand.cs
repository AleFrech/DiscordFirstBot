using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DSharpPlus.Commands;
using FirstDiscordBot.Interfaces;


namespace FirstDiscordBot.Commands
{
    public class AvatarCommand:ICommand
    {
        public async Task Execute(CommandEventArgs e)
        {
            var regex = new Regex("\\s+");
            var commands = regex.Split(e.Message.Content);

            if (commands.Length == 1)
                await e.Message.Respond(e.Message.Author.AvatarUrl);
            else
            {
                var user = e.Guild.Members.Find(x => x.User.Username == commands[1]);
                if (user != null)
                    await e.Message.Respond(user.User.AvatarUrl);
                else
                    await e.Message.Respond("User Not Found");
            }
        }
    }
}