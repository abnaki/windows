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
//using CodeRed.Serialization; // SerializableDictionary

using Abnaki.Windows.GUI;
using Abnaki.Windows.Software.Wpf.Ultimate;
using Abnaki.Windows.Software.Wpf.Profile;
using System.Diagnostics;

namespace Abnaki.Windows.Software.Wpf.Menu
{
    /// <summary>
    /// A control for a customary main menu to populate programmatically
    /// </summary>
    /// <remarks>
    /// Because the menu is wrapped in this class, users do not populate it with XAML.
    /// Menu commands should be sufficiently simple, not needing much customization;
    /// this public interface allows programmatic possibilities; 
    /// and lets developers avoid time-consuming designer interaction.
    /// In some situations, menu commands are known at run time, not design time.
    /// </remarks>
    public partial class MainMenu : UserControl, IMainMenu
    {
        public MainMenu()
        {
            InitializeComponent();

            MessageTube.Subscribe<FarewellMessage>(Farewell);
        }

        /// <summary>
        /// Keys for which value is true imply the menu command has mutually exclusive checking
        /// </summary>
        Dictionary<object, bool> mapKeyExclusivity = new Dictionary<object, bool>();

        public void AddCommand<Tkey>(MenuSeed<Tkey> seed)
        {
#if DEBUG
#else
            if (seed.DebugOnly)
                return;
#endif

            MenuItem item;
            string label = seed.Label ?? AbnakiReflection.LabelOfEnum(seed.Key);

            if (seed.ParentKey == null)
                item = AddMenuItem(seed.Key, label, seed.DefaultCheck);
            else
                item = AddItemChild(seed.ParentKey, seed.Key, label, seed.DefaultCheck);

            CompleteItem<Tkey>(item, seed);
        }

        public void AddCommand<Tkey>(Tkey key, string label, bool? defaultCheck = null)
        {
            AddCommand<Tkey>(new MenuSeed<Tkey>(key, label, defaultCheck));
        }

        public void AddCommandChild<Tkey>(object parentKey, Tkey childKey, string label, bool? defaultCheck = null)
        {
            AddCommand<Tkey>(new MenuSeed<Tkey>(childKey, label, defaultCheck) { ParentKey = parentKey });
        }

        //public void AddExclusiveCommands<TKey>(object parentKey, IEnumerable<MenuSeed<TKey>> seeds)
        //{
        //    foreach ( var seed in seeds )
        //    {
        //        seed.MutuallyExclusive = true;
        //        seed.ParentKey = parentKey;
        //        AddCommand(seed);
        //    }
        //}

        void CompleteItem<Tkey>(MenuItem item, MenuSeed<Tkey> seed)
        {
            RichTag rt = (RichTag)item.Tag;
            ICommand command = seed.MutuallyExclusive ? new ClickCommandExclusive<Tkey>() : new ClickCommand<Tkey>();
            item.Command = command;
            item.CommandParameter = item.Tag;

            if ( seed.DefaultCheck.HasValue )
            {
                mapKeyExclusivity[seed.Key] = seed.MutuallyExclusive;
            }

            string tip = seed.Tooltip ?? rt.LabelDetail;
            //  maybe wrap
            //  Regex rgx = new Regex("(.{50}\\s)"); string s = rgx.Replace(longMessage,"$1\n"); 

            if ( seed.ShortcutKey != Key.None )
            {
                KeyGesture keyg = new KeyGesture(seed.ShortcutKey, seed.ShortcutModifier);
                Application.Current.MainWindow.InputBindings.Add(new InputBinding(item.Command, keyg) { CommandParameter = item.CommandParameter });

                if (!string.IsNullOrWhiteSpace(seed.Tooltip))
                    tip += " ";

                tip += "(";
                if (seed.ShortcutModifier != ModifierKeys.None)
                    tip += seed.ShortcutModifier + "-";

                tip += seed.ShortcutKey + ")";
            }
            item.ToolTip = tip;

            if ( seed.Enabled.HasValue )
                item.IsEnabled = seed.Enabled.Value;

            CompleteParentMenuItem(item);
        }

        static void CompleteParentMenuItem(MenuItem item)
        {
            // expects child items to have Tag using RichTag because it is IComparable
            item.Items.SortDescriptions.Clear();
            item.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Tag", System.ComponentModel.ListSortDirection.Ascending));
        }

        internal class RichTag : IComparable<RichTag>, IComparable
        {
            public RichTag(MenuItem item, object tag)
            {
                this.Item = item;
                this.Tag = tag;
            }

            public MenuItem Item { get; set; }
            
            /// <summary>
            /// Enum value
            /// </summary>
            public object Tag { get; set; }

            public string Qualifier
            {
                get { return this.Tag.GetType().FullName + "." + this.Tag; }
            }

            public string LabelDetail
            {
                get { return AbnakiReflection.DetailOfEnum(this.Tag); }
            }

            /// <summary>
            /// required for disparate enums via SortDescriptions
            /// </summary>
            int TagInteger { get { return (int)this.Tag; } }

            public int CompareTo(RichTag other)
            {
                return this.TagInteger.CompareTo(other.TagInteger);
            }

            int IComparable.CompareTo(object other)
            {
                return CompareTo((RichTag)other);
            }
        }

        class ClickCommand<Tkey> : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public virtual void Execute(object parameter)
            {
                RichTag rt = (RichTag)parameter;
                MenuItem item = (MenuItem)rt.Item;
                if (false == item.IsEnabled)
                    return;

                Tkey key = (Tkey)rt.Tag;
                bool? isChecked = item.IsCheckable ? (bool?)item.IsChecked : (bool?)null;

                ButtonBus<Tkey>.Handle(key, isChecked); // the whole point

                //e.Handled = true;  // otherwise, Click event tunneled down through MenuItem hierarchy
            }
        }

        class ClickCommandExclusive<Tkey> : ClickCommand<Tkey>
        {
            public override void Execute(object parameter)
            {
                base.Execute(parameter);

                // turn off sibiling items
                RichTag rt = (RichTag)parameter;
                MenuItem item = (MenuItem)rt.Item;
                if (false == item.IsEnabled)
                    return;

                MenuItem parent = (MenuItem)item.Parent;
                foreach (MenuItem otherItem in parent.Items)
                {
                    if (otherItem != item)
                        otherItem.IsChecked = false;
                }
            }
        }

        MenuItem AddMenuItem(object key, string label, bool? defaultCheck)
        {
            return AddMenuItem(this.RootMenu, key, label, defaultCheck);
        }

        MenuItem AddItemChild(object parentKey, object childKey, string label, bool? defaultCheck)
        {
            MenuItem parentItem = FindItem(this.RootMenu.Items, parentKey);

            MenuItem itemChild = AddMenuItem(parentItem, childKey, label, defaultCheck);

            // should be unnecessary
            //itemChild.Background = parentItem.Background;
            //itemChild.Foreground = parentItem.Foreground;
            //itemChild.Style = parentItem.Style;
            //itemChild.Template = parentItem.Template;

            return itemChild;
        }

        public void Completed()
        {
            RaiseExistingChecks(this.RootMenu.Items);

            MenuCheckPreference pref = Preference.ReadClassPrefs<MainMenu, MenuCheckPreference>();
            if (pref != null)
            {
                RaiseExistingChecks(pref.CheckedEnums, true);
                RaiseExistingChecks(pref.UncheckedEnums, false);
            }

            // hide empty unused commands
            foreach (MenuItem item in this.RootMenu.Items)
            {
                item.Visibility = (item.Items.Count > 0) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        void RaiseExistingChecks(IEnumerable<string> qualifiers, bool chk)
        {
            foreach (string qualifier in qualifiers)
            {
                MenuItem item = FindItem(this.RootMenu.Items, tag => qualifier == tag.Qualifier);
                if (item != null)
                {
                    item.IsChecked = chk;
                    RaiseExistingCheck(item);
                }
            }
        }

        MenuItem FindItem(ItemCollection items, object tag)
        {
            return FindItem(items, othertag => othertag.Tag.Equals(tag));
        }

        MenuItem FindItem(ItemCollection items, Func<RichTag, bool> testTag)
        {
            MenuItem foundItem = items.Cast<MenuItem>().FirstOrDefault(item => testTag(item.Tag as RichTag));
            if (foundItem == null )
            { // recurse
                foreach ( MenuItem item in items )
                {
                    foundItem = FindItem(item.Items, testTag);
                    if (foundItem != null)
                        break;
                }
            }
            return foundItem;
        }

        static void RaiseExistingCheck(MenuItem item)
        {
            //item.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
            item.Command.Execute(item.CommandParameter);
        }

        static void RaiseExistingChecks(ItemCollection items)
        {
            foreach ( MenuItem item in items )
            {
                if (item.IsChecked)
                    RaiseExistingCheck(item);

                RaiseExistingChecks(item.Items);
            }
        }

        MenuItem AddMenuItem(ItemsControl container, object key, string label, bool? defaultCheck)
        {
            if (container == null)
                throw new ArgumentException("Null container of menu items for " + key);

            MenuItem item = new MenuItem();
            item.Header = label;
            
            item.Tag = new RichTag(item, key);

            item.IsCheckable = defaultCheck.HasValue;
            if (defaultCheck.HasValue)
                item.IsChecked = defaultCheck.Value;

            container.Items.Add(item);

            if (container.Items.Count > 1)
            {  // effect of AssignIntegerSequence()
                container.Items.Refresh();
            }

            return item;
        }

        void Farewell(FarewellMessage msg)
        {
            Func<RichTag, bool> allowUncheck = rt => mapKeyExclusivity.ContainsKey(rt.Tag) && !mapKeyExclusivity[rt.Tag];

            MenuCheckPreference pref = new MenuCheckPreference(this.RootMenu.Items, allowUncheck);

            Preference.WriteClassPrefs<MainMenu, MenuCheckPreference>(pref);
        }

        public class MenuCheckPreference
        {
            /// <summary>
            /// For all checked commands, tag's Type.FullName + enum value.
            /// Ignore unchecked commands.
            /// </summary>
            public readonly List<string> CheckedEnums = new List<string>();

            /// <summary>
            /// Same style as CheckedEnums, but for independent items that are consciously unchecked.
            /// </summary>
            public readonly List<string> UncheckedEnums = new List<string>();

            public MenuCheckPreference() // serializ.
            {

            }

            internal MenuCheckPreference(ItemCollection items, Func<RichTag,bool> allowUncheck)
            {
                Descend(items, allowUncheck);
            }

            void Descend(ItemCollection items, Func<RichTag, bool> allowUncheck)
            {
                foreach (MenuItem item in items)
                {
                    RichTag rt = item.Tag as RichTag;
                    if (rt != null)
                    {
                        string qualifier = rt.Qualifier;

                        if (item.IsChecked)
                        {
                            CheckedEnums.Add(qualifier);
                        }
                        else if (allowUncheck(rt))
                        {
                            UncheckedEnums.Add(qualifier);
                        }
                    }
                    Descend(item.Items, allowUncheck);
                }
            }

        }

    }

}
