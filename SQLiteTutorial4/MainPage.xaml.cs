using SQLiteTutorial4.Models;
using Microsoft.Maui.Storage; // for MediaPicker
using System.IO;
using SQLiteTutorial4.Views;

namespace SQLiteTutorial4
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadImages();
        }

        private async Task LoadImages()
        {
            ImagesCollection.ItemsSource = await App.Database.GetImagesAsync();
        }

        // Pick existing photo
        private async void OnPickPhotoClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.Default.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Pick a photo"
                });

                if (result != null)
                {
                    await SaveFileAndRecordAsync(result);
                    await LoadImages();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        // Capture new photo with camera
        private async void OnCapturePhotoClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Take a photo"
                });

                if (result != null)
                {
                    await SaveFileAndRecordAsync(result);
                    await LoadImages();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        // Copy picked/captured file into app folder and save DB record
        private async Task SaveFileAndRecordAsync(FileResult fileResult)
        {
            // ensure App folder for images
            string imagesDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "images");
            Directory.CreateDirectory(imagesDir);

            // unique filename
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(fileResult.FileName)}";
            string destPath = Path.Combine(imagesDir, fileName);

            // copy stream
            using (var source = await fileResult.OpenReadAsync())
            using (var dest = File.OpenWrite(destPath))
            {
                await source.CopyToAsync(dest);
            }

            // Save DB record
            var item = new ImageItem
            {
                FilePath = destPath,
                CreatedAt = DateTime.UtcNow
            };

            await App.Database.SaveImageAsync(item);
        }

        // When a thumbnail is tapped: navigate to detail page, passing ID
        private async void OnImageTapped(object sender, EventArgs e)
        {
            // The tapped Image is inside the Frame; get BindingContext to retrieve the ImageItem
            if (sender is TapGestureRecognizer tr && tr.BindingContext is ImageItem)
            {
                // Not usually fired this way; instead, get BindingContext from sender's parent
            }

            // Simpler: get tapped view from sender as VisualElement via event args sender of Frame? 
            // Because we attached Tap handler to Frame, the BindingContext of the Frame is the ImageItem.
            var tapped = (VisualElement)sender;
            var imageItem = tapped?.BindingContext as ImageItem;

            // If we can't get via sender, try using GestureRecognizer's BindingContext from the view
            if (imageItem == null)
            {
                // Try to find binding context from the currently focused element (fallback)
                return;
            }

            // Navigate to detail page by passing ID
            await Shell.Current.GoToAsync($"{nameof(ImageDetailPage)}?Id={imageItem.Id}");
        }
    }
}

