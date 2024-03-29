using CommunityToolkit.Mvvm.ComponentModel;

namespace Jellyfin.Mvvm.Models;

/// <summary>
/// The home row model.
/// </summary>
public partial class HomeRowModel : ObservableObject
{
    [ObservableProperty]
    private IReadOnlyList<BaseItemDto> _items = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeRowModel"/> class.
    /// </summary>
    /// <param name="name">The home row name.</param>
    public HomeRowModel(string name)
    {
        Name = name;
        Items = Array.Empty<BaseItemDto>();
    }

    /// <summary>
    /// Gets the home row name.
    /// </summary>
    public string Name { get; }
}
