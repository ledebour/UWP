using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Editing;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MediaCompositionSample
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private MediaComposition composition;
        private MediaStreamSource mediaStreamSource;

        public MainPage()
        {
            this.InitializeComponent();
            composition = new MediaComposition();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            mediaPlayerElement.Source = null;
            mediaStreamSource = null;
            base.OnNavigatedFrom(e);

        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
           
            base.OnNavigatedTo(e);
            await PickFileAndAddClip();

        }

        private async Task PickFileAndAddClip()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.VideosLibrary;
            picker.FileTypeFilter.Add(".jpg");
            IReadOnlyList<Windows.Storage.StorageFile> pickedFiles = await picker.PickMultipleFilesAsync();
            if (pickedFiles == null)
            {
                //ShowErrorMessage("File picking cancelled");
                return;
            }

            foreach (StorageFile file in pickedFiles) {
                // These files could be picked from a location that we won't have access to later
                var storageItemAccessList = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList;
                storageItemAccessList.Add(file);

                var clip = await MediaClip.CreateFromImageFileAsync(file, TimeSpan.FromSeconds(3));
                composition.Clips.Add(clip);
            }
            UpdateMediaElementSource();

        }

        public void UpdateMediaElementSource()
        {

            mediaStreamSource = composition.GeneratePreviewMediaStreamSource(
                (int)mediaPlayerElement.ActualWidth,
                (int)mediaPlayerElement.ActualHeight);

            mediaPlayerElement.Source = MediaSource.CreateFromMediaStreamSource(mediaStreamSource);

        }
    }
}
