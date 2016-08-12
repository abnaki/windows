; Inno Setup 
#define AppVer GetFileVersion('bin\Release\Ex03_Ultimate.exe')
#if AppVer == ""
#error GetFileVersion() failed maybe because file does not exist
#endif

[Setup]
AppName=Abnaki Example 3
AppVersion={#AppVer}
AppCopyright=Copyright (C) 2016- Abnaki Light Industry LLC
LicenseFile=..\..\..LICENSE
OutputBaseFilename=Abnaki-Example03-{#AppVer}-Setup
DefaultDirName={pf}\Abnaki\Example03
DefaultGroupName=Abnaki
UninstallDisplayIcon={app}\Ex03_Ultimate.exe
ArchitecturesInstallIn64BitMode=x64 ia64
DisableProgramGroupPage=yes
Compression=lzma2
SolidCompression=yes
; SetupIconFile=
OutputDir=SetupOutput
; SignTool=signtool ; can't prompt for passphrase

[Files]
Source: "bin\Release\*"; Excludes: "*.pdb,*vshost*"; DestDir: "{app}"; Flags: recursesubdirs
Source: "Other\OtherLicenses\*"; DestDir: "{app}\OtherLicenses"; Flags: recursesubdirs

; [Icons]
