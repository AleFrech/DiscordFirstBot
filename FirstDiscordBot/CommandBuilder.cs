using FirstDiscordBot.Commands;

namespace FirstDiscordBot
{
    public class CommandBuilder
    {
        private readonly DiscordServices _discordServices;

        public CommandBuilder(DiscordServices discordServices)
        {
            _discordServices = discordServices;
            _discordServices.SetCommandPrefix("#", false);
        }

        public void BuildCommands()
        {
            _discordServices.AddCommand("Tucu", new TucuCommand());
            _discordServices.AddCommand("watch", new WatchCommand());
            _discordServices.AddCommand("Pendejo", new PendejoCommand());
            _discordServices.AddCommand("avatar", new AvatarCommand());
            _discordServices.AddCommand("ask", new AskCommand());
        }
    }
}