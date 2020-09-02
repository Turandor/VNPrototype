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

namespace VNPrototype
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GameController gameController = new GameController(startButton, loadButton, settingsButton, collectionButton, exitButton, backgroundImage, dialogueBox, dialogueText);
            startButton.Click += (sender, EventArgs) => { Start_Button_Click(sender, EventArgs, gameController); };
            backgroundImage.MouseDown += (sender, EventArgs) => { backgroundImage_MouseDown(sender, EventArgs, gameController); };

            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;

            //myMediaElement.Source = new Uri(@"C:\Users\Kube\source\repos\VNPrototype\VNPrototype\resources\bensound-relaxing.wav");
            myMediaElement.Source = new Uri(@"..\..\resources\bensound-relaxing.mp3", UriKind.Relative);
            myMediaElement.Play();
            myMediaElement.Volume = 0.5;
            
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
            if (gameController.isStepReady)
            {
                gameController.isStepReady = false;
                gameController.NextStep();
            }
        }
    }
}
