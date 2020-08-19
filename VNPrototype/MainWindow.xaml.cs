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

namespace VNPrototype
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;

            //myMediaElement.Source = new Uri(@"C:\Users\Kube\source\repos\VNPrototype\VNPrototype\resources\bensound-relaxing.wav");
            myMediaElement.Source = new Uri(@"..\..\resources\bensound-relaxing.mp3", UriKind.Relative);
            myMediaElement.Play();
            myMediaElement.Volume = 0.5;
            
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
