#/bin/csh
# working directory should be root of module
# other products may have a script calling this

source Build/subrelease.csh interim/v2.0.14 \
 ./Library/Commonality/bin/Release/Abnaki.Windows.Commonality.dll \
 ./Library/Software/WpfApplication/bin/Release/Abnaki.Windows.Software.Wpf.Application.dll \
 ./Library/Software/WpfBasis/bin/Release/Abnaki.Windows.Software.Wpf.Basis.dll \
 ./Library/Software/WpfPreferredControls/bin/Release/Abnaki.Windows.Software.Wpf.PreferredControls.dll
