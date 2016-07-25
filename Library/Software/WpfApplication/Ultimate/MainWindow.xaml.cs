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
using Abnaki.Windows.Software.Wpf.NetAid;

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

            ButtonBus<SubMenuKey>.HookupSubscriber(HandleMenu);
        }

        MainMenuBus mainMenub;

        public event Action<FileInfo> SavingPanelLayout;

        public event Action<FileInfo> RestoringPanelLayout;


        public void SetMainControl(Abnaki.Windows.GUI.IMainControl c)
        {
            var element = (System.Windows.UIElement)c;
            this.ClientPanel.Children.Add(element);

            c.ConfigureMenu(this.TopMenu);
            this.TopMenu.Completed();

            Abnaki.Windows.Software.Wpf.PreferredControls.Docking.Setup.CompleteSetupOfInterfaces(this, c.DockingSystem);

            c.MainTitle += title => this.Title = title;

            c.EmplacedInWindow();
        }

        void HandleMenu(ButtonMessage<SubMenuKey> m)
        {
            FileInfo fi;
            switch (m.Key)
            {
                case SubMenuKey.ReadUserPlacement:
                    fi = LayoutFileInfo();
                    InvokeRestoringPanelLayout(fi);
                    break;

                case SubMenuKey.ReadDefaultPlacement:
                    ReadDefaultLayout();
                    break;

                case SubMenuKey.SaveUserPlacement:
                    fi = LayoutFileInfo();
                    InvokeSavingPanelLayout(fi);
                    break;

                case SubMenuKey.SaveAsPlacement:
                    using (var dialog = new System.Windows.Forms.SaveFileDialog() )
                    {
                        dialog.SetFilters(Preference.FileExtWithoutDot);
                        dialog.FileName = Preference.ClassPrefsFileOnly<MainWindow>(layoutFileQualifier); 
                        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                        if ( dialog.ShowDialogAndOK() )
                        {
                            fi = new FileInfo(dialog.FileName);
                            InvokeSavingPanelLayout(fi);
                        }
                    }
                    break;
            }
        }

        const string layoutFileQualifier = "Layout";

        static FileInfo LayoutFileInfo()
        {
            return Preference.ClassPrefsFile<MainWindow>(layoutFileQualifier);
        }

        void ReadDefaultLayout()
        {
            FileInfo fi = AbnakiFile.CombinedFilePath(Preference.ApplicationDefaultDir(),
                 Preference.ClassPrefsFileOnly<MainWindow>(layoutFileQualifier));
            
            InvokeRestoringPanelLayout(fi);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            // but in OnClosed(), note that RestoreBounds is Empty 

            base.OnClosing(e);

            try
            {
                SaveBounds();

                MessageTube.Publish(new FarewellMessage());

                FileInfo fi = LayoutFileInfo();
                InvokeSavingPanelLayout(fi);
            }
            catch (Exception ex)
            {
                AbnakiLog.Exception(ex);
            }
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            bool changed = false;
            try
            {
                // note, in OnInitialized(), WindowState does not necessarily maximize on proper screen, so handle Loaded.
                changed = ReloadBounds();

                FileInfo fi = LayoutFileInfo();

                if (fi.Exists)
                    InvokeRestoringPanelLayout(fi);
                else
                    ReadDefaultLayout();
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

        void InvokeSavingPanelLayout(FileInfo fi)
        {
            var h = SavingPanelLayout;
            if (h != null)
                h(fi);
        }

        void InvokeRestoringPanelLayout(FileInfo fi)
        {
            if (fi.Exists)
            {
                var h = RestoringPanelLayout;
                if (h != null)
                    h(fi);
            }
            else
            {
                AbnakiLog.FileInfo(fi, "Nonexistent, ignored");
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
                    AbnakiLog.Comment(GetType().Name + " disregarded undisplayable bounds " + pref.Bounds);
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
