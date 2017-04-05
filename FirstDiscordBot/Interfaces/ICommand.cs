using System.Threading.Tasks;
using DSharpPlus.Commands;

namespace FirstDiscordBot.Interfaces
{
    public interface ICommand
    {
       Task Execute(CommandEventArgs e);
    }
}