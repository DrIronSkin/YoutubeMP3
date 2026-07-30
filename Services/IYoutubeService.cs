using System;
using YoutubeMP3.Models;
using System.Threading.Tasks;
namespace YoutubeMP3.Services;

public interface IYoutubeService
{
    Task<Video> TaskGetVideoInformation(string url);
    Task TaskDownloadVideo(string url, string title, IProgress<double> progress);
}