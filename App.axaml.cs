using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using YoutubeMP3.Services;
using YoutubeMP3.ViewModels;

namespace YoutubeMP3;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            IYoutubeService  youtubeService = new YoutubeService();
            var mainViewModel = new MainViewModel(youtubeService);
            desktop.MainWindow = new Views.MainWindow
            {
                DataContext = mainViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}