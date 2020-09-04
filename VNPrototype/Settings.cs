using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VNPrototype
{
    public class Settings
    {
        public double MusicVolume { get; set; }
        public double SoundEffectsVolume { get; set; }

        public int FontSize { get; set; }
        public int DialogueSpeed { get; set; }

        public bool Fullscreen { get; set; }
        public int FadeSpeed { get; } = 25;
    }
}
