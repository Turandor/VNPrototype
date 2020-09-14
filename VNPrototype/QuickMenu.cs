using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace VNPrototype
{
    public class QuickMenu
    {
        Grid quickMenuBG;
        Button resumeBtn;
        Button saveBtn;
        Button loadBtn;
        Button settingsBtn;
        Button returnBtn;
        Button exitBtn;

        bool isOpened = false;
        public bool IsOpened { get { return isOpened; } }

        public QuickMenu(Grid quickMenuBG, Button resumeBtn,
                  Button saveBtn, Button loadBtn,
                  Button settingsBtn, Button returnBtn,
                  Button exitBtn)
        {
            this.quickMenuBG = quickMenuBG;
            this.resumeBtn = resumeBtn;
            this.saveBtn = saveBtn;
            this.loadBtn = loadBtn;
            this.settingsBtn = settingsBtn;
            this.resumeBtn = returnBtn;
            this.exitBtn = exitBtn;
        }

        public void DisplayMenu(bool isVisible)
        {
            if (isVisible)
                quickMenuBG.Visibility = Visibility.Visible;
            else
                quickMenuBG.Visibility = Visibility.Collapsed;
            isOpened = isVisible;
        }
    }
}
