using Windows.UI.Xaml.Controls;

namespace LotsOfItems
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.ViewModel = new ExampleItemViewModel();
        }

        public ExampleItemViewModel ViewModel { get; set; }

        // Display each item incrementally to improve performance.
        private void GridView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 0)
            {
                throw new System.Exception("We should be in phase 0, but we are not.");
            }

            // It's phase 0, so this item's title will already be bound and displayed.

            args.RegisterUpdateCallback(this.ShowSubtitle);

            args.Handled = true;
        }

        private void ShowSubtitle(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 1)
            {
                throw new System.Exception("We should be in phase 1, but we are not.");
            }

            // It's phase 1, so show this item's subtitle.
            var templateRoot = args.ItemContainer.ContentTemplateRoot as StackPanel;
            var textBlock = templateRoot.Children[1] as TextBlock;
            textBlock.Text = (args.Item as ExampleItem).Subtitle;
            textBlock.Opacity = 1;

            args.RegisterUpdateCallback(this.ShowDescription);
        }

        private void ShowDescription(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 2)
            {
                throw new System.Exception("We should be in phase 2, but we are not.");
            }

            // It's phase 2, so show this item's description.
            var templateRoot = args.ItemContainer.ContentTemplateRoot as StackPanel;
            var textBlock = templateRoot.Children[2] as TextBlock;
            textBlock.Text = (args.Item as ExampleItem).Description;
            textBlock.Opacity = 1;
        }
    }
}
