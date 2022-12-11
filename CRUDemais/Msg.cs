using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CRUDemais
{
    class Msg
    {
        public static void alert(string caption, string message)
        {
            string messageBoxText = message;
            string captionText = caption;
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;

            MessageBox.Show(messageBoxText, captionText, button, icon);
        }
    }
}
