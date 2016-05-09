# 
goto tail


git add Library/Software/WpfApplication/Notifier.cs
git add Library/Software/WpfApplication/Properties/AssemblyInfo.cs
git add Library/Software/WpfApplication/Starter.cs
git add Library/Software/WpfApplication/WpfApplication.csproj

git add Abnaki.Windows.AssemblyInfo.cs Abnaki.Windows.sln
git status -s


git commit -m '+WpfApplication project'

~/bin/gitcommit '+Notify()' Library/Software/WpfApplication/Notifier.cs

~/bin/gitcommit '' Examples/WPF/Ex01_Hello/ExStarter.cs

~/bin/gitcommit 'Start() replaces Main()' Library/Software/WpfApplication/Starter.cs

~/bin/gitcommit 'Uses ExStarter' Examples/WPF/Ex01_Hello/Ex01_Hello.csproj
~/bin/gitcommit 'Comment'  Examples/WPF/Ex01_Hello/MainWindow.xaml.cs

~/bin/gitcommit 'X key demonstrates benefit of exception handling'  Examples/WPF/Ex01_Hello/MainWindow.xaml.cs

~/bin/gitcommit 'Title' Examples/WPF/Ex01_Hello/MainWindow.xaml
~/bin/gitcommit '' README.md

~/bin/gitcommit '+Commonality' Abnaki.Windows.sln
~/bin/gitcommit 'Uses Commonality' Examples/WPF/Ex01_Hello/Ex01_Hello.csproj
~/bin/gitcommit 'S key can save log' Examples/WPF/Ex01_Hello/MainWindow.xaml.cs
~/bin/gitcommit 'Uses Log.Exception()' Library/Software/WpfApplication/Notifier.cs
~/bin/gitcommit 'Uses Log.Comment()' Library/Software/WpfApplication/Starter.cs
~/bin/gitcommit 'Uses Commonality' Library/Software/WpfApplication/WpfApplication.csproj


~/bin/gitcommit 'Contains Log' Library/Commonality/Commonality.csproj
~/bin/gitcommit 'Good' Library/Commonality/Log.cs
~/bin/gitcommit '' Library/Commonality/Properties/AssemblyInfo.cs


~/bin/gitcommit '' Examples/WPF/Ex01_Hello/ViewModels/VmMain.cs
~/bin/gitcommit '' Examples/WPF/Ex01_Hello/Commands/SaveLog.cs
~/bin/gitcommit '' Examples/WPF/Ex01_Hello/Commands/ThrowException.cs

~/bin/gitcommit '+VmMain and Commands' Examples/WPF/Ex01_Hello/Ex01_Hello.csproj
~/bin/gitcommit 'Does not need to use App' Examples/WPF/Ex01_Hello/ExStarter.cs
~/bin/gitcommit '2 buttons' Examples/WPF/Ex01_Hello/MainWindow.xaml
~/bin/gitcommit '+DataContext.  Moved logic to VmMain' Examples/WPF/Ex01_Hello/MainWindow.xaml.cs
~/bin/gitcommit 'Renaming, log info' Library/Software/WpfApplication/Starter.cs
~/bin/gitcommit '+AbnakiViewModel.cs' Library/Software/WpfApplication/WpfApplication.csproj
~/bin/gitcommit 'AbnakiViewModel' Library/Software/WpfApplication/AbnakiViewModel.cs

~/bin/gitcommit 'Uses MessageTube and ButtonMessage' Examples/WPF/Ex01_Hello/ViewModels/VmButtonBus.cs
~/bin/gitcommit 'VmButtonBus.cs' Examples/WPF/Ex01_Hello/Ex01_Hello.csproj
~/bin/gitcommit 'TestMessage button' Examples/WPF/Ex01_Hello/MainWindow.xaml
~/bin/gitcommit 'Uses VmButtonBus' Examples/WPF/Ex01_Hello/MainWindow.xaml.cs
~/bin/gitcommit 'Neater' Examples/WPF/Ex01_Hello/ViewModels/VmMain.cs
~/bin/gitcommit '+SubscribeEvents()' Library/Software/WpfApplication/AbnakiViewModel.cs
~/bin/gitcommit 'Uses Prism.Core' Library/Software/WpfApplication/WpfApplication.csproj
~/bin/gitcommit '+Prism.Core' Library/Software/WpfApplication/packages.config


~/bin/gitcommit 'Size/geometry' Examples/WPF/Ex01_Hello/MainWindow.xaml
~/bin/gitcommit 'EntryFile' Library/Commonality/Log.cs
~/bin/gitcommit 'AssemblyLoad handler to log detail' Library/Software/WpfApplication/Starter.cs

~/bin/gitcommit 'Moved ButtonKey here' Examples/WPF/Ex01_Hello/Enums.cs
git add Examples/WPF/Ex01_Hello/ExButtonBus.cs && git rm Examples/WPF/Ex01_Hello/ViewModels/VmButtonBus.cs && git commit -m 'Refactored into ExButtonBus and ButtonBus'
~/bin/gitcommit 'Refactored VmButtonBus into ExButtonBus' Examples/WPF/Ex01_Hello/Ex01_Hello.csproj
~/bin/gitcommit 'Refactored HandleButton into ButtonBus' Examples/WPF/Ex01_Hello/MainWindow.xaml.cs
~/bin/gitcommit 'Inherits ButtonBus' Examples/WPF/Ex01_Hello/ViewModels/VmButtonBus.cs
~/bin/gitcommit 'Extracted from Ex01_Hello VmButtonBus' Library/Software/WpfApplication/Menu/ButtonMessage.cs
~/bin/gitcommit 'Extracted from Ex01_Hello VmButtonBus' Library/Software/WpfApplication/Menu/ButtonBus.cs
~/bin/gitcommit 'ButtonBus' Library/Software/WpfApplication/WpfApplication.csproj

~/bin/gitcommit 'Comment' Examples/WPF/Ex01_Hello/App.xaml.cs
~/bin/gitcommit 'Start() generic overload' Examples/WPF/Ex01_Hello/ExStarter.cs
~/bin/gitcommit 'Uses simpler Start()' Library/Software/WpfApplication/Starter.cs

~/bin/gitcommit 'Does not need App class' Examples/WPF/Ex01_Hello/Ex01_Hello.csproj

git add Examples/WPF/Ex02_Menu/App.config
git add Examples/WPF/Ex02_Menu/App.xaml
git add Examples/WPF/Ex02_Menu/App.xaml.cs
git add Examples/WPF/Ex02_Menu/Ex02_Menu.csproj
git add Examples/WPF/Ex02_Menu/MainWindow.xaml
git add Examples/WPF/Ex02_Menu/MainWindow.xaml.cs
git add Examples/WPF/Ex02_Menu/Properties/AssemblyInfo.cs
git add Examples/WPF/Ex02_Menu/Properties/Resources.Designer.cs
git add Examples/WPF/Ex02_Menu/Properties/Resources.resx
git add Examples/WPF/Ex02_Menu/Properties/Settings.Designer.cs
git add Examples/WPF/Ex02_Menu/Properties/Settings.settings
~/bin/gitcommit '+Ex02_Menu' Abnaki.Windows.sln


tail:

echo About to do something... && sleep 4

git add '' Library/Software/WpfApplication/Menu/MainMenu.xaml
git add '' Library/Software/WpfApplication/Menu/MainMenu.xaml.cs
~/bin/gitcommit '' Library/Software/WpfApplication/WpfApplication.csproj

git add 'Menu enums' Examples/WPF/Ex02_Menu/Enums.cs
~/bin/gitcommit '+Enums.cs, refs MainMenu class' Examples/WPF/Ex02_Menu/Ex02_Menu.csproj
git add 'Uses MainMenu class' Examples/WPF/Ex02_Menu/MainWindow.xaml
~/bin/gitcommit '+PopulateMenu()' Examples/WPF/Ex02_Menu/MainWindow.xaml.cs
