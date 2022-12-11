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
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;

            MessageBox.Show(message, caption, button, icon);
        }

        public static MessageBoxResult confirm(string caption, string message)
        {
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;

            MessageBoxResult answer = MessageBox.Show(message, caption, button, icon);

            return answer;
        }
    }
}
