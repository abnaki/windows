using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace Ex01_Hello
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            // Abnaki.Windows.Software.Wpf.Notifier.Notify("You hit " + e.Key);

            switch (e.Key)
            {
                case Key.S:
                    using ( SaveFileDialog dialog = new SaveFileDialog() )
                    {
                        dialog.Filter = "XML|.xml";
                        dialog.DefaultExt = ".xml";
                        dialog.Title = "Save Log File";

                        if (  dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK )
                        {
                            Abnaki.Windows.Log.Write(dialog.FileName);
                        }
                    }
                    break;

                case Key.X:
                    throw new ApplicationException("Demo exception because you hit " + e.Key);
                // will be caught by Abnaki code, not crash.
            }

        }
    }
}
