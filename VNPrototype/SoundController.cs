using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VNPrototype
{
    public class SoundController
    {
        MediaElement myMediaElement;
        string soundName;

        public SoundController(MediaElement myMediaElement)
        {
            this.myMediaElement = myMediaElement;
            myMediaElement.Volume = 0.5;
        }

        public void LoadSound(string sound)
        {
            myMediaElement.Source = new Uri(@"..\..\resources\" + sound, UriKind.Relative);
            soundName = sound;
        }

        public void Play()
        {
            myMediaElement.Play();
        }
        public void Stop()
        {
            myMediaElement.Stop();
        }

        public void ChangeVolume(double volume)
        {
            if (volume <= 1 || volume >= 0)
            {
                myMediaElement.Volume = volume;
            }
        }
    }
}
