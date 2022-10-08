namespace TubePlayer.ViewModels;

public class StartPageViewModel : AppViewModelBase
{
    public StartPageViewModel(IApiService appApiService) : base(appApiService)
    {
        this.Title = "TUBE PLAYER";
    }

   public override async void OnNavigatedTo(object parameters)
    {
        SetDataLodingIndicators(true);

        LoadingText = "Hold on, we are Loading";

        try
        {
            await Task.Delay(3000);

            throw new Exception("Exception");

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
}
