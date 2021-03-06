using CommunityToolkit.Maui.Converters;
using Jellyfin.Maui.ContentViews.Facades;
using Jellyfin.Maui.DataTemplates;
using Jellyfin.Maui.Models;

namespace Jellyfin.Maui.ContentViews;

/// <summary>
/// Home row view.
/// </summary>
public class HomeRowView : BaseContentView<HomeRowModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HomeRowView"/> class.
    /// </summary>
    public HomeRowView()
    {
        Content = new VerticalStackLayout
        {
            Style = BaseStyles.HomeRow,
            Children =
            {
                new Label()
                    .Bind(Label.TextProperty, mode: BindingMode.OneTime, path: nameof(HomeRowModel.Name)),
                new CollectionView
                    {
                        ItemTemplate = TemplateHelper.CardTemplateSelector,
                        ItemsLayout = LinearItemsLayout.Horizontal,
                        ItemsUpdatingScrollMode = ItemsUpdatingScrollMode.KeepLastItemInView,
                        SelectionMode = SelectionMode.Single
                    }
                    .Bind(ItemsView.ItemsSourceProperty, mode: BindingMode.OneWay, path: nameof(HomeRowModel.Items))
            }
        }.Bind(IsVisibleProperty, nameof(HomeRowModel.Items), converter: new IsListNotNullOrEmptyConverter(), mode: BindingMode.OneWay);
    }
}
