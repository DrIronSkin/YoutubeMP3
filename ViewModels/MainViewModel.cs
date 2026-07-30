using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using YoutubeMP3.Models;
using YoutubeMP3.Services;

namespace YoutubeMP3.ViewModels;

public partial class MainViewModel(IYoutubeService youtubeService) : ObservableObject
{
    private readonly IYoutubeService _youtubeService = youtubeService;
    
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
    
    
    [RelayCommand]
    public async Task GetVideoInformation()
    {
        if (string.IsNullOrWhiteSpace(Url)) return;
        try
        {
            StatusMessage = "Video Data Recovery";
            CurrentVideo = await _youtubeService.TaskGetVideoInformation(Url);
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
        if (string.IsNullOrWhiteSpace(Url)) return;
        
        try
        {
            var progress = new Progress<double>(p => ProgressValue = p);
            StatusMessage = "Downloading....";
            IsDownloading = true;
            
            await _youtubeService.TaskDownloadVideo(Url, CurrentVideo!.Title, progress);
            
            StatusMessage = "Download complete.";
            Url = string.Empty;
            CurrentVideo = null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Url = string.Empty;
            CurrentVideo = null;
        }
    }
}