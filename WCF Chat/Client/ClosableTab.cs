using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Client
{
    class ClosableTab : TabItem
    {
        public string Title
        {
            set
            {
                ((CloseableHeader)this.Header).label_TabTitle.Content = value;
            }
            get
            {
                return ((CloseableHeader)this.Header).label_TabTitle.Content.ToString();
            }
        }

        public bool CloseIsEnabled
        {
            set
            {
                ((CloseableHeader)this.Header).button_close.IsEnabled = value;
            }
        }

        public ClosableTab()
        {
            CloseableHeader closableTabHeader = new CloseableHeader();
            this.Header = closableTabHeader;
           
        }
    }
}
