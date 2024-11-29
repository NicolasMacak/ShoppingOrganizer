namespace ShoppingOrganizer.Mobile.Shared.Helpers;

public static class ServiceHelper // todo. change to service injector
{
    public static TService GetService<TService>()
        => Current.GetService<TService>();

    public static IServiceProvider Current =>
#if ANDROID
        IPlatformApplication.Current.Services;
#elif IOS || MACCATALYST
      IPlatformApplication.Current.Services;
#elif WINDOWS10_0_17763_0_OR_GREATER
      IPlatformApplication.Current.Services;
#else
    null;
#endif
}