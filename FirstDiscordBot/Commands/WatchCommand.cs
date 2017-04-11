using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DSharpPlus.Commands;
using FirstDiscordBot.Interfaces;
using FirstDiscordBot.Model;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace FirstDiscordBot.Commands
{
    public class WatchCommand :ICommand
    {
        public YouTubeService Client;

        public WatchCommand()
        {
            Client = CreateYouTubeService();
        }

        private YouTubeService CreateYouTubeService()
        {
            return new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyDilIbUh2fZGI8BVBHXtdABc-FxYTufKbA",
                ApplicationName = this.GetType().ToString()
            });
        }

        private async Task<List<VideoModel>> GetVideos(string searchQuery,int maxResults)
        {
            var searchListRequest = Client.Search.List("snippet");
            searchListRequest.Q = searchQuery;
            searchListRequest.MaxResults = (maxResults==0)?1:maxResults;
            var searchListResponse = await searchListRequest.ExecuteAsync();

            return (from searchResult in searchListResponse.Items
                where searchResult.Id.Kind.Equals("youtube#video")
                select new VideoModel
                {
                    VideoId = searchResult.Id.VideoId,
                    Url = "https://www.youtube.com/watch?v=" + searchResult.Id.VideoId,
                    Title = searchResult.Snippet.Title,
                    Description = searchResult.Snippet.Description
                }).ToList();
        }

        public async Task Execute(CommandEventArgs e)
        {

            var regex = new Regex(@"\w+|""[\w\s]*""");
            var commands = regex.Matches(e.Message.Content);
            if (commands.Count <2)
                await e.Message.Respond("Missing Video Name");
            else
            {
                var videoName = commands[1].Value.Substring(1, commands[1].Value.Length - 2);
                var maxResult=0;
                if(commands.Count < 3)
                    maxResult = 1;
                else
                    int.TryParse(commands[2].Value, out maxResult);
                var videos = await GetVideos(videoName, maxResult);
                foreach (var video in videos)
                {
                    await e.Message.Respond(video.Url);
                }
            }
        }
    }
}