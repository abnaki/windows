#/bin/csh
# working directory should be root of module
# arg 1 is tag

gitfetchtagbuild $1 Abnaki.Windows.sln  | tee Build/msbuild.out
