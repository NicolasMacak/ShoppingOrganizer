namespace ShoppingOrganizer.Mobile.Infrastructure;

/// <summary>
/// Viewmodel constructor has to be parameterless. This allows utilizing DI within construtor
/// </summary>
public static class PlatformServiceProvider
{
    private static IServiceProvider _serviceProvider => IPlatformApplication.Current!.Services;

    public static T GetService<T>() => _serviceProvider.GetService<T>()!;
}
