using YoutubeMP3.Models;
using System.Threading.Tasks;
using System;
using System.IO;
using DotNetEnv;
using YoutubeExplode;
using YoutubeExplode.Converter;
namespace YoutubeMP3.Services;


public class YoutubeService: IYoutubeService
{
    private readonly YoutubeClient _youtube = new YoutubeClient();
    
    public async Task<Video> TaskGetVideoInformation(string url)
    {
        try
        {
            var video = await _youtube.Videos.GetAsync(url);
            var title = $"{video.Title}.mp3";
            var author = $"{video.Author}";
            var duration = video.Duration;
            var currentUrl = video.Url;
            
            return new Video(
                title, author, currentUrl, duration ?? TimeSpan.Zero);
        }
        catch (Exception ex)
        {
            Console.WriteLine("================ DETAILED ERROR ================");
            Console.WriteLine($"Message    : {ex.Message}");
            throw new Exception(ex.Message);
        }
    }

    public async Task TaskDownloadVideo(string url, string title, IProgress<double> progress)
    {
        Env.Load();
        var filePath = $"{Env.GetString("PATH")}{title}";
        var progressHandler = new Progress<double>(p =>
        {
            progress.Report(p * 100); 
        }); 
        try {
            var ffmpegPath = Path.Combine(AppContext.BaseDirectory, "ffmpeg.exe");
            await _youtube.Videos.DownloadAsync(url, filePath, format => 
                format.SetContainer("mp3")
                    .SetPreset(ConversionPreset.UltraFast)
                    .SetFFmpegPath(ffmpegPath),
                progressHandler);
        } catch(Exception ex)
        {
            Console.WriteLine("================ DETAILED ERROR ================");
            Console.WriteLine($"Message    : {ex.Message}");
            Console.WriteLine(ex.ToString());
        }
    }
}