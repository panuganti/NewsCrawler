using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

using HtmlAgilityPack;
using System.Net;

namespace NewsPublisher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] imageUrls = new string[] { };
        int imageIndex = 0;

        string[] texts = new string[] { };
        int textIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Fetch Images, Heading and Text for selection
        private void fetchButton_Click(object sender, RoutedEventArgs e)
        {
            string url = urlTextBox.Text;
            var html = new HtmlDocument();
            html.LoadHtml(new WebClient().DownloadString(url));
            var titleNode = html.DocumentNode.Descendants("title");
            if (titleNode.Any())
            {
                headingTextBox.Text = titleNode.First().InnerText;
            }
            var images = html.DocumentNode.Descendants("img");
            imageUrls = images.Where(t => t.Attributes.Contains("src")).Select(t => t.Attributes["src"].Value).ToArray();
            imageIndex = 0;
            var relativeUrl = imageUrls[imageIndex];
            var uri = new Uri(new Uri(urlTextBox.Text), relativeUrl).AbsoluteUri;
            fetchImageFromUrl(uri);

            // text
            textIndex = 0;
            var textNodes = html.DocumentNode.Descendants("div").Where(t => t.InnerText.Length > 100);
            if (textNodes.Any()) {
                texts = textNodes.Select(t=>t.InnerText).ToArray();
                textTextBox.Text = texts[textIndex];
            }
            
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            // Display Next image
            if (imageIndex + 1 < imageUrls.Length) imageIndex++;            
            var relativeUrl = imageUrls[imageIndex];
            var uri = new Uri(new Uri(urlTextBox.Text), relativeUrl).AbsoluteUri;
            fetchImageFromUrl(uri);
        }

        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            // Display Prev image
            if (imageIndex > 0) imageIndex = imageIndex - 1;
            var relativeUrl = imageUrls[imageIndex];
            var uri = new Uri(new Uri(urlTextBox.Text), relativeUrl).AbsoluteUri;
            fetchImageFromUrl(uri);
        }

        private void publishButton_Click(object sender, RoutedEventArgs e)
        {
            // Verify that all of them meet the criteria
            // Update notification
            // Publish
        }

        private void fetchImageButton_Click(object sender, RoutedEventArgs e)
        {
            fetchImageFromUrl(altImageUrlTextBox.Text);
        }

        private void fetchImageFromUrl(string url)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(url, UriKind.Absolute);
            bitmap.EndInit();

            image.Source = bitmap;
            altImageUrlTextBox.Text = url;
        }

        private void altNextText_Click(object sender, RoutedEventArgs e)
        {
            if (textIndex + 1 < texts.Length) textIndex++;
            textTextBox.Text = texts[textIndex];
        }

        private void prevAltTextButton_Click(object sender, RoutedEventArgs e)
        {
            if (textIndex > 0 ) textIndex = textIndex - 1;
            textTextBox.Text = texts[textIndex];
        }
    }
}
