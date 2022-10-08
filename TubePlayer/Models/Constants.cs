namespace TubePlayer.Models;

public static class Constants
{
    public static string ApplicationName = "Tube Player";
    public static string EmailAddress = @"aadipoddarmail@gmail.com";
    public static string ApplicationId = "aadi.tubeplayer.app";
    public static string ApiServiceURL = @"https://youtube.googleapis.com/youtube/v3/";
    public static string ApiKey = @"AIzaSyAIUijE78_dLz3JheEcHk259TijbU2iV3Y";


    public static uint MicroDuration { get; set; } = 100;
    public static uint SmallDuration { get; set; } = 300;
    public static uint MediumDuration { get; set; } = 600;
    public static uint LongDuration { get; set; } = 1200;
    public static uint ExtraLongDuration { get; set; } = 1800;
}