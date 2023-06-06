namespace MusicPlayerAPI.Interfaces
{
    //
    // Smmary:
    //      Provides DateTime functionality that allows classes to be more test friendly
    //      By being abe to mock DateTime
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
