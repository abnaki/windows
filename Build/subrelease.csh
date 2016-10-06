#/bin/csh
# working directory should be root of module
# arg 1 is tag
# other args are build output files needing to be committed to master

set tag = $1
shift
gitfetchtagbuild $tag Abnaki.Windows.sln $*  | tee Build/msbuild.out
