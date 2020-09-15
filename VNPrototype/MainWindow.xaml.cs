using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Media;
using System.Threading;
using System.IO;
using Newtonsoft.Json;

namespace VNPrototype
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Menu menu = new Menu(startButton, loadButton, settingsButton,
                                 collectionButton, exitButton, backgroundImage);

            GameplayUI gameplayUI = new GameplayUI(dialogueBox, dialogueText, characterNameBox, characterNameText);
            gameplayUI.DispDialogueBox(false);
            QuickMenu quickMenu = new QuickMenu(quickMenuBG, returnBtnQ, saveBtnQ, loadBtnQ, settingsBtnQ, returnBtnQ, exitBtnQ);
            quickMenu.DisplayMenu(false);
            SettingsMenu settingsMenu = new SettingsMenu(settingsBG, musicVolText, soundVolText,
                                                        dialogueSpeedText, fullscreenText, musicVolSlider,
                                                        soundVolSlider, dialogueSpeedSlider, fullscreenOnBtn,
                                                        fullscreenOffBtn, applyBtn, backBtn);

            GameController gameController = new GameController(menu, gameplayUI, settingsMenu, quickMenu, musicMedia, soundEffectMedia);

            // Modification of events
            startButton.Click += (sender, EventArgs) => { Start_Button_Click(sender, EventArgs, gameController); };

            backgroundImage.MouseLeftButtonDown += (sender, EventArgs) => { BackgroundImage_LeftMouseDown(sender, EventArgs, gameController); };
            backgroundImage.MouseWheel += (sender, MouseWheelEventArgs) => { BackgroundImage_Scroll(sender, MouseWheelEventArgs, gameController); };
            backgroundImage.MouseRightButtonDown += (sender, EventArgs) => { BackgroundImage_RightMouseDown(sender, EventArgs, gameController); };

            settingsButton.Click += (sender, EventArgs) => { SettingsButton_Click(sender, EventArgs, gameController); };

            resumeBtnQ.Click += (sender, EventArgs) => { BackgroundImage_RightMouseDown(sender, EventArgs, gameController); };
            settingsBtnQ.Click += (sender, EventArgs) => { QuickSettingsButton_Click(sender, EventArgs, gameController); };

            backBtn.Click += (sender, EventArgs) => { backBtn_Click(sender, EventArgs, gameController); };
            applyBtn.Click += (sender, EventArgs) => { applyBtn_Click(sender, EventArgs, gameController); };
            fullscreenOnBtn.Click += (sender, EventArgs) => { fullscreenOnBtn_Click(sender, EventArgs, gameController); };
            fullscreenOffBtn.Click += (sender, EventArgs) => { fullscreenOffBtn_Click(sender, EventArgs, gameController); };



            if (gameController.settings.Fullscreen)
            {
                WindowState = WindowState.Maximized;
                WindowStyle = WindowStyle.None;
            }
            else
            {
                WindowState = WindowState.Normal;
                WindowStyle = WindowStyle.ThreeDBorderWindow;
            }
        }



        // Start Game
        private void Start_Button_Click(object sender, RoutedEventArgs e, GameController gameController)
        {
            gameController.StartGame(); 
        }

        // Settings
        private void SettingsButton_Click(object sender, RoutedEventArgs e, GameController gameController)
        {
            gameController.ChangeGameplayBlocade(true);
            gameController.DisplaySettings(true);
        }
        private void QuickSettingsButton_Click(object sender, RoutedEventArgs e, GameController gameController)
        {
            gameController.ChangeGameplayBlocade(true);
            gameController.DisplayQuickSettings(true);
        }

        // Exit
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }

        /************** Game Interactions **********************/
        private void BackgroundImage_LeftMouseDown(object sender, RoutedEventArgs e, GameController gameController)
        {
            gameController.NextStep();
        }

        private void BackgroundImage_RightMouseDown(object sender, RoutedEventArgs e, GameController gameController)
        {
            gameController.DisplayQuickMenu();
        }

        private void BackgroundImage_Scroll(object sender, MouseWheelEventArgs e, GameController gameController)
        {
            if (e.Delta > 0)
            {
                // scroll up
                
            }
            else
            {
                // scroll down
                gameController.PreviousStep();
            }
        }
        /********************************************************/


        /**************** Settings Menu Interactions *******************/
        private void backBtn_Click(object sender, RoutedEventArgs e, GameController gameController)
        {
            if (gameController.end)
                gameController.DisplaySettings(false);
            else
            {
                gameController.DisplayQuickSettings(false);
                gameController.ChangeGameplayBlocade(false);
            }

        }

        private void applyBtn_Click(object sender, RoutedEventArgs e, GameController gameController)
        {
            gameController.ApplySettings();
            if (gameController.settings.Fullscreen)
            {
                WindowStyle = WindowStyle.None;
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
                WindowStyle = WindowStyle.ThreeDBorderWindow;
            }
        }

        private void fullscreenOnBtn_Click(object sender, RoutedEventArgs e, GameController gameController)
        {
            gameController.settingsMenu.SetFullscreenOn();
        }

        private void fullscreenOffBtn_Click(object sender, RoutedEventArgs e, GameController gameController)
        {
            gameController.settingsMenu.SetFullscreenOff();
        }
        /*******************************************************************/

        /**************** Quick Menu Interactions *******************/

        
       
        /*******************************************************************/
    }
}
