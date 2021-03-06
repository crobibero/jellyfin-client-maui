namespace Jellyfin.Maui.ContentViews;

/// <summary>
/// The poster card view.
/// </summary>
public class PosterCardView : BaseCardView
{
    private static readonly (Row, GridLength)[] _rowDefinitions = new (Row, GridLength)[]
    {
        (Row.Image, 300),
        (Row.Title, 50),
        (Row.Description, 50)
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="PosterCardView"/> class.
    /// </summary>
    public PosterCardView()
        : base(
              BaseStyles.PosterCard,
              null,
              null,
              null,
              ImageType.Primary,
              _rowDefinitions)
    {
    }
}
