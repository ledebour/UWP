using System.Collections.ObjectModel;

namespace LotsOfItems
{
    public class ExampleItem
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }

    public class ExampleItemViewModel
    {
        private ObservableCollection<ExampleItem> exampleItems = new ObservableCollection<ExampleItem>();
        public ObservableCollection<ExampleItem> ExampleItems { get { return this.exampleItems; } }

        public ExampleItemViewModel()
        {
            for (int i = 1; i < 150000; i++)
            {
                this.exampleItems.Add(new ExampleItem()
                {
                    Title = "Title: " + i,
                    Subtitle = "Sub: " + i,
                    Description = "Desc: " + i,
                    ImagePath = "Assets/teste.jpg"
                });
            }
        }
    }
}
