using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VNPrototype
{
    public class Settings
    {
        public double MusicVolume { get; set; }
        public double SoundEffectsVolume { get; set; }

        public int DialogueSpeed { get; set; }

        public bool Fullscreen { get; set; }
        public int FadeSpeed { get; } = 25;

        public Settings(double musicVolume, double soundEffectsVolume, int dialogueSpeed, bool fullscreen)
        {
            MusicVolume = musicVolume;
            SoundEffectsVolume = soundEffectsVolume;
            DialogueSpeed = dialogueSpeed;
            Fullscreen = fullscreen;
        }

        public static Settings LoadSettings()
        {
            return JsonConvert.DeserializeObject<Settings>(File.ReadAllText(@"..\..\resources\settings.json"));
        }

        public static void SaveSettings(Settings settings)
        {
            File.WriteAllText(@"..\..\resources\settings.json",JsonConvert.SerializeObject(settings, Formatting.Indented));
        }
    }
}
