using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using YoutubeMP3.Models;
using YoutubeMP3.Services;

namespace YoutubeMP3.ViewModels;

public partial class MainViewModel(IYoutubeService youtubeService) : ObservableObject
{
    private readonly IYoutubeService _youtubeService = youtubeService;
    private const string Pattern = @"^https://youtu\.be/([a-zA-Z0-9_-]{11})$";
    
    [ObservableProperty]
    private string _url = string.Empty;
    
    [ObservableProperty]
    private Video? _currentVideo;
    
    [ObservableProperty]
    private string _statusMessage = string.Empty;
    
    [ObservableProperty]
    private bool _isDownloading;
    
    [ObservableProperty]
    private double _progressValue;

    [ObservableProperty] 
    private ObservableCollection<Video>? _videos = new();
    
    public ObservableCollection<string> Choices { get; } = new()
    {
        "Video", "Playlist"
    };
    
    
    [RelayCommand]
    public async Task GetVideoInformation()
    {
       if(!VerifyUrl(Url))
       {
           StatusMessage = "The video URL is not valid. Please try again.";
           return;
       }
       CurrentVideo = null;
       
       try
       {
           StatusMessage = "Retrieving video data...";
           CurrentVideo = await _youtubeService.TaskGetVideoInformation(Url);
           StatusMessage = "The video data has been retrieved.";
       }
       catch (Exception ex)
       {
           Console.WriteLine(ex.Message);
           Url = string.Empty;
           CurrentVideo = null;
       }
    }
    
    [RelayCommand]
    public async Task Download()
    { 
        try
        {
            var progress = new Progress<double>(p => ProgressValue = p);
            StatusMessage = "Downloading video audio....";
            IsDownloading = true;
            
            await _youtubeService.TaskDownloadVideo(Url, CurrentVideo!.Title, progress);
            
            StatusMessage = "Download completed.";
            Url = string.Empty;
            IsDownloading = false;
            CurrentVideo = null;
            ProgressValue = 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Url = string.Empty;
            CurrentVideo = null;
        }
    }
    
    private static bool VerifyUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) return false;
        
        string[] newUrl = url.Split('?');
        Regex regex = new Regex(Pattern);
        
        if (!regex.IsMatch(newUrl[0])) return false;
        
        return true;
    } 
}