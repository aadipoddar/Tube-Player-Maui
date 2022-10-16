namespace TubePlayer.ViewModels;

public partial class StartPageViewModel : AppViewModelBase
{
    private string nextToken = string.Empty;
    private string searchTerm = "iPhone 14";

    [ObservableProperty]
    private ObservableCollection<YoutubeVideo> youtubeVideos;

    [ObservableProperty]
    private bool isLoadingMore;


    public StartPageViewModel(IApiService appApiService) : base(appApiService)
    {
        this.Title = "TUBE PLAYER";
    }

    public override async void OnNavigatedTo(object parameters)
    {
        Search();
    }

    private async void Search()
    {
        SetDataLodingIndicators(true);

        LoadingText = "Hold on while we Search for Youtube Videos ...";

        YoutubeVideos = new();

        try
        {
            // Search for Videos
            await GetYouTubeVideos();

            this.DataLoaded = true;
        }
        catch (InternetConnectionException)
        {
            this.IsErrorState = true;
            this.ErrorMessage = "Slow or no internet connection" + Environment.NewLine + "Please Check your Internet Connection and try again";
            ErrorImage = "nointernet.png";
        }
        catch (Exception ex)
        {
            this.IsErrorState = true;
            this.ErrorMessage = $"Something went wrong. If the problem persists, please contact support at {Constants.EmailAddress} with the error message:" + Environment.NewLine + Environment.NewLine + ex.Message;
            ErrorImage = "error.png";
        }
        finally
        {
            SetDataLodingIndicators(false);
        }
    }

    private async Task GetYouTubeVideos()
    {
        //Search the videos
        var videoSearchResult = await _appApiService.SearchVideos(searchTerm, nextToken);

        nextToken = videoSearchResult.NextPageToken;

        //Get Channel URLs
        var channelIDs = string.Join(",",
            videoSearchResult.Items.Select(video => video.Snippet.ChannelId).Distinct());

        var channelSearchResult = await _appApiService.GetChannels(channelIDs);


        //Set Channel URL in the Video
        videoSearchResult.Items.ForEach(video =>
            video.Snippet.ChannelImageURL = channelSearchResult.Items.Where(channel =>
                channel.Id == video.Snippet.ChannelId).First().Snippet.Thumbnails.High.Url);

        //Add the Videos for Display
        YoutubeVideos.AddRange(videoSearchResult.Items);
    }

    [RelayCommand]
    private async void OpenSettingPage()
    {
        await PageService.DisplayAlert("Settings", "Coming Soon", "Ok");
    }

    [RelayCommand]
    private async Task LoadMoreVideos()
    {
        if (IsLoadingMore || string.IsNullOrEmpty(nextToken))
            return;

        IsLoadingMore = true;
        await Task.Delay(2000);
        await GetYouTubeVideos();
        IsLoadingMore = false;
    }
}
