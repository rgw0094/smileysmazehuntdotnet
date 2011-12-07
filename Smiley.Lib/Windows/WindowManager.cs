using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smiley.Lib.Windows
{
    public class WindowManager
    {
        public bool IsTextBoxOpen { get; private set; }

        public bool IsAnyWindowOpen { get; private set; }

        public int FrameLastWindowClosed { get; private set; }

        public void OpenHintTextBox()
        {

        }
    }
}
