using SQLiteTutorial4.Models;
using System.IO;
using Microsoft.Maui.Controls;

namespace SQLiteTutorial4.Views
{
    [QueryProperty(nameof(ImageId), "Id")]
    public partial class ImageDetailPage : ContentPage
    {
        public int ImageId { get; set; }
        ImageItem imageItem;

        // Add a field for FullImage and initialize it in the constructor if not already defined in XAML
        private Image? _fullImage;

        public ImageDetailPage()
        {
            InitializeComponent();

            // If FullImage is not defined in XAML, create and add it to the page
            if (FindByName("FullImage") is Image image)
            {
                _fullImage = image; // Reference the existing Image defined in XAML
            }
            else
            {
                _fullImage = new Image { AutomationId = "FullImage" }; // Set properties as needed 
                Content = _fullImage; // Or add to a layout if needed
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (ImageId != 0)
            {
                imageItem = await App.Database.GetImageByIdAsync(ImageId);
                if (imageItem != null && File.Exists(imageItem.FilePath) && _fullImage != null)
                {
                    _fullImage.Source = ImageSource.FromFile(imageItem.FilePath);
                }
            }
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (imageItem == null) return;

            var confirm = await DisplayAlert("Delete", "Delete this image?", "Yes", "No");
            if (!confirm) return;

            // delete file
            if (!string.IsNullOrEmpty(imageItem.FilePath) && File.Exists(imageItem.FilePath))
            {
                try { File.Delete(imageItem.FilePath); }
                catch { /* ignore file delete exceptions for now */ }
            }

            // delete DB record
            await App.Database.DeleteImageAsync(imageItem);

            // Go back to previous page
            await Shell.Current.GoToAsync("..");
        }

        private async void OnCloseClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
