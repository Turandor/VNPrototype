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
                                 collectionButton, exitButton, backgroundImage, dialogueBox,
                                 dialogueText, characterNameBox, characterNameText);
            menu.DispDialogueBox(false);

            GameController gameController = new GameController(menu, musicMedia, soundEffectMedia);

            // Modification of events
            startButton.Click += (sender, EventArgs) => { Start_Button_Click(sender, EventArgs, gameController); };
            backgroundImage.MouseDown += (sender, EventArgs) => { backgroundImage_MouseDown(sender, EventArgs, gameController); };


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

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e, GameController gameController)
        {
            gameController.StartGame(); 
        }

        private void backgroundImage_MouseDown(object sender, RoutedEventArgs e, GameController gameController)
        {
            gameController.NextStep();
        }
    }
}
