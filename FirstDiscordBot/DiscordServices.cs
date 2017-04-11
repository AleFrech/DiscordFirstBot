using DSharpPlus;
using DSharpPlus.Commands;
using FirstDiscordBot.Interfaces;

namespace FirstDiscordBot
{
    public class DiscordServices
    {
        public DiscordClient Client;
        private readonly string _token;

        public DiscordServices()
        {
            _token = "Token Here";
            Client = CreateDiscordCLient();
        }

        private DiscordClient CreateDiscordCLient()
        {
            return new DiscordClient(new DiscordConfig
            {
                AutoReconnect = true,
                DiscordBranch = Branch.Stable,
                LargeThreshold = 250,
                LogLevel = LogLevel.Unnecessary,
                Token = _token,
                TokenType = TokenType.Bot,
                UseInternalLogHandler = false
            });
        }

        public void SetCommandPrefix(string prefix, bool selfBot)
        {
            Client.UseCommands(new CommandConfig
            {
                Prefix = prefix,
                SelfBot = selfBot
            });
        }

        public void AddCommand(string commandName, ICommand command)
        {
            Client.AddCommand(commandName, async e => { await command.Execute(e); });
        }
    }
}
