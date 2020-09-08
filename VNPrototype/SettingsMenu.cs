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

namespace VNPrototype
{
    public class SettingsMenu
    {
        Grid settingsBG;
        TextBlock musicVolText;
        TextBlock soundVolText;
        TextBlock dialogueSpeedText;
        TextBlock fullscreenText;

        Slider musicVolSlider;
        Slider soundVolSlider;
        Slider dialogueSpeedSlider;

        Button fullscreenOnBtn;
        Button fullscreenOffBtn;

        Button applyBtn;
        Button backBtn;

        bool fullscreen;

        public SettingsMenu(Grid settingsBG, TextBlock musicVolText,
                    TextBlock soundVolText, TextBlock dialogueSpeedText,
                    TextBlock fullscreenText, Slider musicVolSlider, 
                    Slider soundVolSlider, Slider dialogueSpeedSlider,
                    Button fullscreenOnBtn, Button fullscreenOffBtn,
                    Button applyBtn, Button backBtn)
        {
            this.settingsBG = settingsBG;
            this.musicVolText = musicVolText;
            this.soundVolText = soundVolText;
            this.dialogueSpeedText = dialogueSpeedText;
            this.fullscreenText = fullscreenText;

            this.musicVolSlider = musicVolSlider;
            this.soundVolSlider = soundVolSlider;
            this.dialogueSpeedSlider = dialogueSpeedSlider;

            this.fullscreenOnBtn = fullscreenOnBtn;
            this.fullscreenOffBtn = fullscreenOffBtn;

            this.applyBtn = applyBtn;
            this.backBtn = backBtn;
        }

        public void DisplayMenu(bool isVisible)
        {
            if (isVisible)
                settingsBG.Visibility = Visibility.Visible;
            else
                settingsBG.Visibility = Visibility.Collapsed;
        }

        public void LoadSettings(Settings settings)
        {
            musicVolSlider.Value = settings.MusicVolume;
            soundVolSlider.Value = settings.SoundEffectsVolume;
            dialogueSpeedSlider.Value = settings.DialogueSpeed;

            if (settings.Fullscreen)
                SetFullscreenOn();
            else
                SetFullscreenOff();
        }

        public Settings ApplySettings()
        {
            Settings settings = new Settings(musicVolSlider.Value, soundVolSlider.Value, (int)dialogueSpeedSlider.Value, fullscreen);
            return settings;
        }

        public void SetFullscreenOn()
        {
            fullscreen = true;
            fullscreenOnBtn.BorderBrush = Brushes.Red;
            fullscreenOffBtn.BorderBrush = Brushes.White;
        }

        public void SetFullscreenOff()
        {
            fullscreen = false;
            fullscreenOnBtn.BorderBrush = Brushes.White;
            fullscreenOffBtn.BorderBrush = Brushes.Red;
        }
    }
}
