namespace TubePlayer.Views;

public partial class VideoDetailsPage : ViewBase<VideoDetailsPageViewModel>
{
    public VideoDetailsPage(object initParams) : base(initParams)
    {
		InitializeComponent();

        this.ViewModelInitialized += (s, e) =>
        {
            (this.BindingContext as VideoDetailsPageViewModel).DownloadCompleted += VideoDetailsPage_DownloadCompleted;
        };
    }

    protected override void OnDisappearing()
    {
        (this.BindingContext as VideoDetailsPageViewModel).DownloadCompleted -= VideoDetailsPage_DownloadCompleted;

        base.OnDisappearing();
    }

    private void VideoDetailsPage_DownloadCompleted(object sender, EventArgs e)
    {
        //Video information downloaded. Start showing items.

        if ((this.BindingContext as VideoDetailsPageViewModel).IsErrorState)
            return;

        if (this.AnimationIsRunning("TransitionAnimation"))
            return;


        var parentAnimation = new Animation();

        //Poster Image Animation
        parentAnimation.Add(0.0, 0.7,
            new Animation(v => HeaderView.Opacity = v, 0, 1, Easing.CubicIn));

        //Video Title Container Animation
        parentAnimation.Add(0.4, 0.7,
            new Animation(v => VideoTitle.Opacity = v, 0, 1, Easing.CubicIn));
        
        //Commit the animation
        parentAnimation.Commit(this, "TransitionAnimation", 16, Constants.ExtraLongDuration, null,
            (v, c) =>
            {
                //Action to perform on completion (if any)
            });

    }
}