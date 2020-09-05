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
        private Rectangle dialogueBox;
        private TextBox dialogueText;
        private Rectangle characterNameBox;
        private TextBox characterNameText;

        public Menu(Button startButton, Button loadButton,
                      Button settingsButton, Button collectionButton,
                      Button exitButton, Grid backgroundImage,
                      Rectangle dialogueBox, TextBox dialogueText,
                      Rectangle charachterNameBox, TextBox characterNameText)
        {
            this.startButton = startButton;
            this.loadButton = loadButton;
            this.settingsButton = settingsButton;
            this.collectionButton = collectionButton;
            this.exitButton = exitButton;
            this.backgroundImage = backgroundImage;
            this.dialogueBox = dialogueBox;
            this.dialogueText = dialogueText;
            this.characterNameBox = charachterNameBox;
            this.characterNameText = characterNameText;
        }

        public string DialogueText 
        {
            get { return dialogueText.Text; }
            set { dialogueText.Text = value; }
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

        public void DispDialogueBox(bool isVisible)
        {
            if (isVisible)
            {
                dialogueBox.Visibility = Visibility.Visible;
                dialogueText.Visibility = Visibility.Visible;
            }
            else
            {
                dialogueBox.Visibility = Visibility.Hidden;
                dialogueText.Visibility = Visibility.Hidden;
                characterNameBox.Visibility = Visibility.Hidden;
                characterNameText.Visibility = Visibility.Hidden;
            }
        }

        public void DispCharacterNameBox(bool isVisible, string characterName)
        {
            if (isVisible)
            {
                characterNameBox.Visibility = Visibility.Visible;
                characterNameText.Visibility = Visibility.Visible;
                characterNameText.Text = characterName;
            }
            else
            {
                characterNameBox.Visibility = Visibility.Hidden;
                characterNameText.Visibility = Visibility.Hidden;
            }
        }
        public void DispCharacterNameBox(bool isVisible)
        {
            if (isVisible)
            {
                characterNameBox.Visibility = Visibility.Visible;
                characterNameText.Visibility = Visibility.Visible;
            }
            else
            {
                characterNameBox.Visibility = Visibility.Hidden;
                characterNameText.Visibility = Visibility.Hidden;
            }
        }
    }
}
