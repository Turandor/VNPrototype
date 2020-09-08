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
        Button fullscreenWindowedBtn;

        Button applyBtn;
        Button backBtn;

        public SettingsMenu(Grid settingsBG, TextBlock musicVolText,
                    TextBlock soundVolText, TextBlock dialogueSpeedText,
                    TextBlock fullscreenText, Slider musicVolSlider, 
                    Slider soundVolSlider, Slider dialogueSpeedSlider,
                    Button fullscreenOnBtn, Button fullscreenWindowedBtn,
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
            this.fullscreenWindowedBtn = fullscreenWindowedBtn;

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
        }

        public Settings ApplySettings()
        {
            Settings settings = new Settings(musicVolSlider.Value, soundVolSlider.Value, (int)dialogueSpeedSlider.Value, true);
            return settings;
        }
    }
}
