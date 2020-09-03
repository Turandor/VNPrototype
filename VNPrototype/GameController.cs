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
        private Button startButton;
        private Button loadButton;
        private Button settingsButton;
        private Button collectionButton;
        private Button exitButton;
        private Grid backgroundImage;
        private Rectangle dialogueBox;
        private TextBox dialogueText;

        SoundController music;
        SoundController soundEffect;

        private int statementNumber = 0;

        System.Windows.Threading.DispatcherTimer fadeTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer dialogueTimer = new System.Windows.Threading.DispatcherTimer();

        bool fadedOut = false;
        bool end = false;
        public bool isStepReady = false;


        List<Statement> subtitles;

        public GameController(Button startButton, Button loadButton,
                              Button settingsButton, Button collectionButton,
                              Button exitButton, Grid backgroundImage,
                              Rectangle dialogueBox, TextBox dialogueText,
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
            music = new SoundController(musicMedia);
            soundEffect = new SoundController(soundEffectMedia);

            music.LoadSound("bensound-relaxing.mp3");
            music.ChangeVolume(0.5);
            music.Play();
        }

        public void StartGame()
        {
            DisplayMenu(false);

            FadeAnimation("bedroom-night.jpg");   // *TO DO* first background loading from JSON file
        }

        private void FadeAnimation(string background)
        {
            fadeTimer = new System.Windows.Threading.DispatcherTimer();
            fadeTimer.Tick += (sender, EventArgs) => { fadeTimer_Tick(sender, EventArgs, background); };
            fadeTimer.Interval = new TimeSpan(0, 0, 0, 0, 25);
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
                    dialogueBox.Visibility = Visibility.Visible;
                    dialogueText.Visibility = Visibility.Visible;
                    fadedOut = false;
                    LoadScene();
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
                if (subtitles.Count != statementNumber)
                {
                    if (subtitles[statementNumber].Name == "MC-narrator")
                    {
                        DialogueAnimation(subtitles[statementNumber]);
                    }
                    else
                    {
                        dialogueText.Text = subtitles[statementNumber].Name;
                        DialogueAnimation(subtitles[statementNumber]);
                        dialogueText.Text = subtitles[statementNumber].Name + ": \"" + subtitles[statementNumber].Text + "\"";
                    }
                    ChangeBackground(subtitles[statementNumber].Background);
                    if (subtitles[statementNumber].soundEffect != "")
                    {
                        soundEffect.LoadSound(subtitles[statementNumber].soundEffect);
                        soundEffect.ChangeVolume(1);
                        soundEffect.Play();

                    }
                    statementNumber++;
                }
                else
                {
                    EndGame();
                }

            }
        }

        public void DialogueAnimation(Statement statement)
        {
            dialogueText.Text = "";
            dialogueTimer = new System.Windows.Threading.DispatcherTimer();
            dialogueTimer.Tick += (sender, EventArgs) => { dialogueTimer_Tick(sender, EventArgs, statement); };
            dialogueTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dialogueTimer.Start();
        }

        private void dialogueTimer_Tick(object sender, EventArgs e, Statement statement)
        {
            if (dialogueText.Text.Length < statement.Text.Length)
                dialogueText.Text += statement.Text[dialogueText.Text.Length];
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

            dialogueBox.Visibility = Visibility.Hidden;
            dialogueText.Visibility = Visibility.Hidden;
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