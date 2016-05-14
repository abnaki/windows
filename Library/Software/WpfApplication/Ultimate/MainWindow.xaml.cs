using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
using System.Diagnostics;

using Abnaki.Windows.Software.Wpf.Menu;
using Abnaki.Windows.Software.Wpf.Profile;

namespace Abnaki.Windows.Software.Wpf.Ultimate
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// MS does not support inheriting XAML properly across assemblies.  XAML is used for this class,
    /// and you will not have a subclass.   You can use public functionality.  Also see UltimateStarter.
    /// </remarks>
    public sealed partial class MainWindow : Window,
        Abnaki.Windows.GUI.IWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;

            mainMenub = new MainMenuBus(this.TopMenu);
        }

        MainMenuBus mainMenub;

        public event Action<FileInfo> SavingLayout;

        public event Action<FileInfo> RestoringLayout;


        public void SetMainControl(Abnaki.Windows.GUI.IMainControl c)
        {
            var element = (System.Windows.UIElement)c;
            this.ClientPanel.Children.Add(element);

            Abnaki.Windows.Software.Wpf.PreferredControls.Extensions.CompleteSetupOfInterfaces(this, c.DockingSystem);
        }

        static FileInfo LayoutFileInfo()
        {
            return Preference.ClassPrefsFile<MainWindow>("Layout");
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            // but in OnClosed(), note that RestoreBounds is Empty 

            base.OnClosing(e);

            try
            {
                SaveBounds();

                var h = SavingLayout;
                if ( h != null )
                {
                    FileInfo fi = LayoutFileInfo();
                    h(fi);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            bool changed = false;
            try
            {
                // note, in OnInitialized(), WindowState does not necessarily maximize on proper screen, so handle Loaded.
                changed = ReloadBounds();

                var h = RestoringLayout;
                if ( h != null )
                {
                    FileInfo fi = LayoutFileInfo();
                    h(fi);
                }

            }
            catch (Exception ex)
            {
                Diplomat.Notifier.Error(ex);
            }
            finally
            {
                if (changed) // previously hidden
                {
                    // this.Visibility = System.Windows.Visibility.Visible; // ineffective
                    Show();
                }
            }
        }

        void SaveBounds()
        {
            Prefs pref = new Prefs();
            if (this.RestoreBounds.IsEmpty)
                pref.Bounds = new Rect(Left, Top, Width, Height);
            else
                pref.Bounds = this.RestoreBounds;

            pref.State = this.WindowState;
            Abnaki.Windows.Software.Wpf.Profile.Preference.WriteClassPrefs<MainWindow, Prefs>(pref);
        }

        bool ReloadBounds()
        {
            bool changed = false;

            Prefs pref = Abnaki.Windows.Software.Wpf.Profile.Preference.ReadClassPrefs<MainWindow, Prefs>();
            if (pref != null)
            {
                if (WpfScreenHelper.Screen.AllScreens.Any(sc => sc.WorkingArea.IntersectsWith(pref.Bounds)))
                {
                    this.Hide(); // show later
                    Top = pref.Bounds.Top;
                    Left = pref.Bounds.Left;
                    Width = pref.Bounds.Width;
                    Height = pref.Bounds.Height;

                    this.WindowState = pref.State;
                    changed = true;
                }
                else
                {
                    Log.Comment(GetType().Name + " disregarded undisplayable bounds " + pref.Bounds);
                }
            }

            return changed;
        }

        public class Prefs
        {
            public Rect Bounds { get; set; }
            public WindowState State { get; set; }
        }

    }
}
