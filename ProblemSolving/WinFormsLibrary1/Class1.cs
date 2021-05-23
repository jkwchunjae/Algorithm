using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinFormsLibrary1
{
    public static class Helper
    {
        public static void CopyText(string text)
        {
            Clipboard.SetText(text);
        }
    }
}
