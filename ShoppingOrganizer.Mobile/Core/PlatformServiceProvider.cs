namespace ShoppingOrganizer.Mobile.Core;

/// <summary>
/// Dependency injection can not be utilized in ViewModels
/// </summary>
public static class PlatformServiceProvider
{
    private static IServiceProvider _serviceProvider => IPlatformApplication.Current!.Services;

    public static T GetService<T>() => _serviceProvider.GetService<T>()!;
}
