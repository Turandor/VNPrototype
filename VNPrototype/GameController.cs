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
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Threading;

namespace VNPrototype
{
    public class GameController
    {
        /************- To Menu Class*********/
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
        /************ To Menu Class*********/

        SoundController music;
        SoundController soundEffect;

        private int statementNumber = 0;

        System.Windows.Threading.DispatcherTimer fadeTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer dialogueTimer = new System.Windows.Threading.DispatcherTimer();

        bool fadedOut = false;
        bool end = false;
        public bool isStepReady = false;


        List<Statement> subtitles;
        public Settings settings;

        /************ To Menu Class*********/
        // without sound controller
        public GameController(Button startButton, Button loadButton,
                              Button settingsButton, Button collectionButton,
                              Button exitButton, Grid backgroundImage,
                              Rectangle dialogueBox, TextBox dialogueText,
                              Rectangle charachterNameBox, TextBox characterNameText,
                              MediaElement musicMedia, MediaElement soundEffectMedia)
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

            settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(@"..\..\resources\settings.json"));

            music = new SoundController(musicMedia);
            soundEffect = new SoundController(soundEffectMedia);

            music.LoadSound("bensound-relaxing.mp3");
            music.ChangeVolume(0.5);
            music.Play();


        }
        /************ To Menu Class*********/

        public void StartGame()
        {
            DisplayMenu(false);
            LoadScene();
            FadeAnimation(subtitles[statementNumber].Background);   // *TO DO* first background loading from JSON file
        }

        private void FadeAnimation(string background)
        {
            fadeTimer = new System.Windows.Threading.DispatcherTimer();
            fadeTimer.Tick += (sender, EventArgs) => { fadeTimer_Tick(sender, EventArgs, background); };
            fadeTimer.Interval = new TimeSpan(0, 0, 0, 0, settings.FadeSpeed);
            fadeTimer.Start();
        }

        private void ChangeBackground(string background)
        {
            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(
                new Uri
                   (@"..\..\resources\" + background, UriKind.Relative));
            myBrush.ImageSource = image.Source;
            backgroundImage.Background = myBrush;
        }

        private void fadeTimer_Tick(object sender, EventArgs e, string background)
        {
            if (backgroundImage.Opacity > 0 && fadedOut == false)
            {
                backgroundImage.Opacity = backgroundImage.Opacity - 0.01;
                if (Math.Round(backgroundImage.Opacity, 2) == 0)
                {
                    fadedOut = true;
                    ChangeBackground(background);
                }
            }
            else if (backgroundImage.Opacity < 1 && fadedOut == true)
                backgroundImage.Opacity = backgroundImage.Opacity + 0.01;
            else
            {
                fadeTimer.Stop();
                if (!end)
                {
                    DispDialogueBox(true);
                    fadedOut = false;
                    isStepReady = true;
                    NextStep();
                }
                else
                {
                    DisplayMenu(true);
                    end = false;
                    fadedOut = false;
                }
            }
        }

        public void LoadScene()
        {
            subtitles = JsonConvert.DeserializeObject<List<Statement>>(File.ReadAllText(@"..\..\resources\scenario.json"));
        }

        public void NextStep()
        {
            if (isStepReady)
            {
                soundEffect.Stop();
                isStepReady = false;

                // Only if not the end of script
                if (subtitles.Count != statementNumber) 
                {
                    // If not narrator statement also display character name box
                    if (subtitles[statementNumber].Name != "MC-narrator") 
                    {
                        subtitles[statementNumber].Text = "\"" + subtitles[statementNumber].Text + "\"";
                        DispCharacterNameBox(true);
                    }
                    else
                        DispCharacterNameBox(false);

                    DialogueAnimation(subtitles[statementNumber].Text);
                    ChangeBackground(subtitles[statementNumber].Background);

                    if (subtitles[statementNumber].soundEffect != "")
                    {
                        soundEffect.LoadSound(subtitles[statementNumber].soundEffect);
                        soundEffect.ChangeVolume(1);
                        soundEffect.Play();

                    }
                    statementNumber++; // Next step with next statement from script
                }
                // The end of script - return to menu
                else
                {
                    EndGame();
                }

            }
        }

        public void DialogueAnimation(string statementText)
        {
            dialogueText.Text = "";
            dialogueTimer = new System.Windows.Threading.DispatcherTimer();
            dialogueTimer.Tick += (sender, EventArgs) => { dialogueTimer_Tick(sender, EventArgs, statementText); };
            dialogueTimer.Interval = new TimeSpan(0, 0, 0, 0, settings.DialogueSpeed);
            dialogueTimer.Start();
        }

        private void dialogueTimer_Tick(object sender, EventArgs e, string statementText)
        {
            if (dialogueText.Text.Length < statementText.Length)
                dialogueText.Text += statementText[dialogueText.Text.Length];
            else
            {
                dialogueTimer.Stop();
                isStepReady = true;
            }
        }

        private void EndGame()
        {
            end = true;
            FadeAnimation("menu-backgound.jpg");
            statementNumber = 0;
            isStepReady = false;

            DispDialogueBox(false);
        }

        /************ To Menu Class*********/
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
        /************ To Menu Class*********/

        /************ To Menu Class*********/
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
        /************ To Menu Class*********/

        /************ To Menu Class*********/
        public void DispCharacterNameBox(bool isVisible)
        {
            if (isVisible)
            {
                characterNameBox.Visibility = Visibility.Visible;
                characterNameText.Visibility = Visibility.Visible;
                characterNameText.Text = subtitles[statementNumber].Name; // propaply to cut out and put somwhere else
            }
            else
            {
                characterNameBox.Visibility = Visibility.Hidden;
                characterNameText.Visibility = Visibility.Hidden;
            }
        }
        /************ To Menu Class*********/

    }
}