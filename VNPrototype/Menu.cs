using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VNPrototype
{
    public class Menu
    {
        private Button startButton;
        private Button loadButton;
        private Button settingsButton;
        private Button collectionButton;
        private Button exitButton;
        private Grid backgroundImage;

        public Menu(Button startButton, Button loadButton,
                      Button settingsButton, Button collectionButton,
                      Button exitButton, Grid backgroundImage)
        {
            this.startButton = startButton;
            this.loadButton = loadButton;
            this.settingsButton = settingsButton;
            this.collectionButton = collectionButton;
            this.exitButton = exitButton;
            this.backgroundImage = backgroundImage;

        }

        public double BackgroundOpacity
        {
            get { return backgroundImage.Opacity; }
            set { backgroundImage.Opacity = value; }
        }
        public void ChangeBackground(string background)
        {
            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(
                new Uri
                   (@"..\..\resources\" + background, UriKind.Relative));
            myBrush.ImageSource = image.Source;
            backgroundImage.Background = myBrush;
        }

        public void DisplayMenu(bool visible)
        {
            if (visible)
            {
                startButton.Visibility = Visibility.Visible;
                loadButton.Visibility = Visibility.Visible;
                settingsButton.Visibility = Visibility.Visible;
                collectionButton.Visibility = Visibility.Visible;
                exitButton.Visibility = Visibility.Visible;
            }
            else
            {
                startButton.Visibility = Visibility.Hidden;
                loadButton.Visibility = Visibility.Hidden;
                settingsButton.Visibility = Visibility.Hidden;
                collectionButton.Visibility = Visibility.Hidden;
                exitButton.Visibility = Visibility.Hidden;
            }
        }

    }
}
