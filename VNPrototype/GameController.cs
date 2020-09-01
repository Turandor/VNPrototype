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
using System.Threading;
using System.Timers;
using System.Diagnostics;
using System.Windows.Navigation;
using System.Windows.Input;

namespace VNPrototype
{
    public class GameController
    {
        private Button startButton;
        private Button loadButton;
        private Button settingsButton;
        private Button collectionButton;
        private Button exitButton;
        private Grid backgroundImage;
        private Rectangle dialogBox;
        private TextBox dialogText;

        System.Windows.Threading.DispatcherTimer fadeTimer = new System.Windows.Threading.DispatcherTimer();
        bool fadedOut = false;

        public bool isStepReady = false;

        public GameController(Button startButton, Button loadButton, 
                              Button settingsButton, Button collectionButton, 
                              Button exitButton, Grid backgroundImage, 
                              Rectangle dialogBox, TextBox dialogText)
        {
            this.startButton = startButton;
            this.loadButton = loadButton;
            this.settingsButton = settingsButton;
            this.collectionButton = collectionButton;
            this.exitButton = exitButton;
            this.backgroundImage = backgroundImage;
            this.dialogBox = dialogBox;
            this.dialogText = dialogText;
        }

        public void StartGame()
        {
            startButton.Visibility = Visibility.Hidden;
            loadButton.Visibility = Visibility.Hidden;
            settingsButton.Visibility = Visibility.Hidden;
            collectionButton.Visibility = Visibility.Hidden;
            exitButton.Visibility = Visibility.Hidden;

            fadeTimer.Tick += fadeTimer_Tick;
            fadeTimer.Interval = new TimeSpan(0, 0, 0, 0, 25);
            fadeTimer.Start();
        }

        private void changeBackground()
        {
            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(
                new Uri
                   (@"..\..\resources\bedroom-night.jpg", UriKind.Relative));
            myBrush.ImageSource = image.Source;
            backgroundImage.Background = myBrush;
        }

        private void fadeTimer_Tick(object sender, EventArgs e)
        {
            if (backgroundImage.Opacity > 0 && fadedOut == false)
            {
                backgroundImage.Opacity = backgroundImage.Opacity - 0.01;
                if (Math.Round(backgroundImage.Opacity, 2) == 0)
                {
                    fadedOut = true;
                    changeBackground();
                } 
            }
            else if (backgroundImage.Opacity < 1 && fadedOut == true)
                backgroundImage.Opacity = backgroundImage.Opacity + 0.01;
            else
            {
                fadeTimer.Stop();
                isStepReady = true;
                //dialogBox.Visibility = Visibility.Visible;
                //dialogText.Visibility = Visibility.Visible;
            }
        }

        public void NextStep()
        {
            dialogBox.Visibility = Visibility.Visible;
            dialogText.Visibility = Visibility.Visible;
        }
    }
}
