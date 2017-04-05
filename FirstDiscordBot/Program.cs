using System.Threading.Tasks;

namespace FirstDiscordBot
{
    public class Program
    {
        public static void Main()
        {
            Run().GetAwaiter().GetResult();
        }

        public static async Task Run()
        {
            var discordServices = new DiscordServices();
            var commandBuilder = new CommandBuilder(discordServices);
            commandBuilder.BuildCommands();
            await discordServices.Client.Connect();
            await Task.Delay(-1);
        }
    }
}