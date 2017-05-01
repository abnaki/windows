#/bin/csh
# working directory should be root of module
# other products may have a script calling this

source Build/subrelease.csh interim/v2.0.66 \
 ./Library/Commonality/bin/Release/Abnaki.Windows.Commonality.??l \
 ./Library/Software/WpfApplication/bin/Release/Abnaki.Windows.Software.Wpf.Application.??l \
 ./Library/Software/WpfBasis/bin/Release/Abnaki.Windows.Software.Wpf.Basis.??l \
 ./Library/Software/WpfPreferredControls/bin/Release/Abnaki.Windows.Software.Wpf.PreferredControls.??l

 # ??l implies dll and xml
