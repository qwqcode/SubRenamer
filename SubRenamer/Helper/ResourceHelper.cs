using Avalonia;
using Avalonia.Styling;

namespace SubRenamer.Helper;

public static class ResourceHelper
{
    /// <summary>
    /// Gets the first resource matching the given key within the application resources dictionary.
    /// </summary>
    /// <typeparam name="T">The type of resource to return.</typeparam>
    /// <param name="app">The application instance.</param>
    /// <param name="key">The resource key.</param>
    /// <returns>The located resource or default(T).</returns>
    public static T? GetResource<T>(
        this Application? app,
        object key)
    {
        if (Application.Current?.Resources.TryGetResource(key, null, out var resource) ?? false)
        {
            if (resource is T value)
            {
                return value;
            }
        }

        return default(T);
    }
}