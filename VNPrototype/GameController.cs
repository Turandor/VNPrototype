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
        Menu menu;
        public SettingsMenu settingsMenu;
        SoundController music;
        SoundController soundEffect;

        private int statementNumber = 0;

        enum direction
        {
            backward,
            forward
        }
        direction dialogueDirection = direction.forward;

        System.Windows.Threading.DispatcherTimer fadeTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer dialogueTimer = new System.Windows.Threading.DispatcherTimer();

        bool fadedOut = false;
        bool end = false;
        public bool isStepReady = false;


        List<Statement> subtitles;
        public Settings settings = Settings.LoadSettings();


        public GameController(Menu menu, SettingsMenu settingsMenu, MediaElement musicMedia, MediaElement soundEffectMedia)
        {
            this.menu = menu;
            this.settingsMenu = settingsMenu;
            settings = Settings.LoadSettings();

            music = new SoundController(musicMedia);
            soundEffect = new SoundController(soundEffectMedia);

            music.LoadSound("bensound-relaxing.mp3");
            music.ChangeVolume(0.5);
            music.Play();


        }

        public void StartGame()
        {
            menu.DisplayMenu(false);
            LoadScene();
            FadeAnimation(subtitles[statementNumber].Background);
        }

        private void FadeAnimation(string background)
        {
            fadeTimer = new System.Windows.Threading.DispatcherTimer();
            fadeTimer.Tick += (sender, EventArgs) => { fadeTimer_Tick(sender, EventArgs, background); };
            fadeTimer.Interval = new TimeSpan(0, 0, 0, 0, settings.FadeSpeed);
            fadeTimer.Start();
        }

        private void fadeTimer_Tick(object sender, EventArgs e, string background)
        {
            if (menu.BackgroundOpacity > 0 && fadedOut == false)
            {
                menu.BackgroundOpacity = menu.BackgroundOpacity - 0.01;
                if (Math.Round(menu.BackgroundOpacity, 2) == 0)
                {
                    fadedOut = true;
                    menu.ChangeBackground(background);
                }
            }
            else if (menu.BackgroundOpacity < 1 && fadedOut == true)
                menu.BackgroundOpacity = menu.BackgroundOpacity + 0.01;
            else
            {
                fadeTimer.Stop();
                if (!end)
                {
                    menu.DispDialogueBox(true);
                    fadedOut = false;
                    isStepReady = true;
                    NextStep();
                }
                else
                {
                    menu.DisplayMenu(true);
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
                    if (dialogueDirection == direction.backward)
                    {
                        statementNumber++;
                        dialogueDirection = direction.forward;
                    }

                    // If not narrator statement also display character name box
                    if (subtitles[statementNumber].Name != "MC-narrator") 
                    {
                        if(!subtitles[statementNumber].Text.StartsWith("\"") && !subtitles[statementNumber].Text.EndsWith("\""))
                            subtitles[statementNumber].Text = "\"" + subtitles[statementNumber].Text + "\"";
                        menu.DispCharacterNameBox(true, subtitles[statementNumber].Name);
                    }
                    else
                        menu.DispCharacterNameBox(false);

                    DialogueAnimation(subtitles[statementNumber].Text);
                    menu.ChangeBackground(subtitles[statementNumber].Background);

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

        public void PreviousStep()
        {
            if (isStepReady)
            {
                soundEffect.Stop();
                isStepReady = false;

                // Only if not the end of script
                if (statementNumber > 0)
                {
                    if (dialogueDirection == direction.forward)
                    {
                        statementNumber -= 2;
                        dialogueDirection = direction.backward;
                    }
                    else
                        statementNumber--;

                    // If not narrator statement also display character name box
                    if (subtitles[statementNumber].Name != "MC-narrator")
                    {
                        if (!subtitles[statementNumber].Text.StartsWith("\"") && !subtitles[statementNumber].Text.EndsWith("\""))
                            subtitles[statementNumber].Text = "\"" + subtitles[statementNumber].Text + "\"";
                        menu.DispCharacterNameBox(true, subtitles[statementNumber].Name);
                    }
                    else
                        menu.DispCharacterNameBox(false);

                    ChangeDialogue(subtitles[statementNumber].Text);
                    menu.ChangeBackground(subtitles[statementNumber].Background);

                    if (subtitles[statementNumber].soundEffect != "")
                    {
                        soundEffect.LoadSound(subtitles[statementNumber].soundEffect);
                        soundEffect.ChangeVolume(1);
                        soundEffect.Play();

                    }
                }
                isStepReady = true;
            }
        }
        public void DialogueAnimation(string statementText)
        {
            menu.DialogueText = "";
            dialogueTimer = new System.Windows.Threading.DispatcherTimer();
            dialogueTimer.Tick += (sender, EventArgs) => { dialogueTimer_Tick(sender, EventArgs, statementText); };
            dialogueTimer.Interval = new TimeSpan(0, 0, 0, 0, settings.DialogueSpeed);
            dialogueTimer.Start();
        }

        public void ChangeDialogue(string statementText)
        {
            menu.DialogueText = statementText;
        }

        private void dialogueTimer_Tick(object sender, EventArgs e, string statementText)
        {
            if (menu.DialogueText.Length < statementText.Length)
                menu.DialogueText += statementText[menu.DialogueText.Length];
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

            menu.DispDialogueBox(false);
        }

        public void OpenSettings()
        {
            settingsMenu.DisplayMenu(true);
            menu.DisplayMenu(false);
            settingsMenu.LoadSettings(settings);
        }

        public void CloseSettings()
        {
            settingsMenu.DisplayMenu(false);
            menu.DisplayMenu(true);
        }

        public void ApplySettings()
        {
            settings = settingsMenu.ApplySettings();
            music.ChangeVolume(settings.MusicVolume);
            soundEffect.ChangeVolume(settings.SoundEffectsVolume);
            //Fullscreen change is implemented in MainWindow.xaml.cs after apply button click

            Settings.SaveSettings(settings);
        }
    }
}