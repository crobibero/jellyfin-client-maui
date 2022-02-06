namespace Jellyfin.Maui;

/// <summary>
/// The main app.
/// </summary>
public class App : CometApp
{
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        Body = () => new VStack(spacing: 20)
        {
            new Text("Hey!!"),
            new Text("TEST PADDING").Frame(height: 30).Margin(top: 100),
            new Text("This top part is a Microsoft.Maui.VerticalStackLayout"),
            new HStack(spacing: 2)
            {
                new Button("A Button").Frame(width: 100).Color(Colors.White),
                new Button("Hello I'm a button")
                    .Color(Colors.Green)
                    .Background(Colors.Purple),
                new Text("And these buttons are in a HorizontalStackLayout"),
            },
            new Text("Hey!!"),
            new Text("Hey!!"),
        }.Background(Colors.Beige).Margin(top: 30);
    }
}
