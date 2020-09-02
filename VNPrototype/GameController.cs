﻿using System;
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

        private int dialogueNumber = 0;

        System.Windows.Threading.DispatcherTimer fadeTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer dialogueTimer = new System.Windows.Threading.DispatcherTimer();

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
            this.dialogueBox = dialogBox;
            this.dialogueText = dialogText;
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

        private void changeBackground(string background)
        {
            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(
                new Uri
                   (@"..\..\resources\" + background, UriKind.Relative));
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
                    changeBackground("bedroom-night.jpg");
                }
            }
            else if (backgroundImage.Opacity < 1 && fadedOut == true)
                backgroundImage.Opacity = backgroundImage.Opacity + 0.01;
            else
            {
                fadeTimer.Stop();
                isStepReady = true;
            }
        }

        public void NextStep()
        {
            dialogueBox.Visibility = Visibility.Visible;
            dialogueText.Visibility = Visibility.Visible;
            LoadScene();
        }

        public void LoadScene()
        {
            List<Dialog> dialogue = JsonConvert.DeserializeObject<List<Dialog>>(File.ReadAllText(@"C:\Users\Kube\source\repos\VNPrototype\VNPrototype\resources\scenario.json"));
            if (dialogue[dialogueNumber].Name == "MC-narrator")
            {
                DialogueAnimation(dialogue[dialogueNumber]);
            }
            else
            {
                dialogueText.Text = dialogue[dialogueNumber].Name + ": \"" + dialogue[dialogueNumber].Text + "\"";
            }
            changeBackground(dialogue[dialogueNumber].Background);

            dialogueNumber++;
            isStepReady = true;
        }

        public void DialogueAnimation(Dialog dialogue)
        {
            dialogueText.Text = "";
            dialogueTimer = new System.Windows.Threading.DispatcherTimer();
            dialogueTimer.Tick += (sender, EventArgs) => { dialogueTimer_Tick(sender, EventArgs, dialogue); };
            dialogueTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dialogueTimer.Start();
        }
        private void dialogueTimer_Tick(object sender, EventArgs e, Dialog dialogue)
        {
            if (dialogueText.Text.Length < dialogue.Text.Length)
                dialogueText.Text += dialogue.Text[dialogueText.Text.Length];
            else
            {
                dialogueTimer.Stop();
            }
        }
    }
}