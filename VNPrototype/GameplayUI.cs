using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace VNPrototype
{
    public class GameplayUI
    {
        private Rectangle dialogueBox;
        private TextBlock dialogueText;
        private Rectangle characterNameBox;
        private TextBlock characterNameText;

        public GameplayUI(Grid backgroundImage, Rectangle dialogueBox,
                          TextBlock dialogueText, Rectangle characterNameBox,
                          TextBlock characterNameText)
        {
            this.backgroundImage = backgroundImage;
            this.dialogueBox = dialogueBox;
            this.dialogueText = dialogueText;
            this.characterNameBox = characterNameBox;
            this.characterNameText = characterNameText;
        }

        public string DialogueText
        {
            get { return dialogueText.Text; }
            set { dialogueText.Text = value; }
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
