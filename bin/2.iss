; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Manager"
#define MyAppVersion "1.1.0"
#define MyAppPublisher "������ �.�. ���-17"
#define MyAppExeName "Manager.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{4AD968A1-31FD-4720-A59F-B920A598AD1C}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputBaseFilename=Manager_setup
SetupIconFile=C:\Users\Philka\source\repos\Manager\Manager\Icon.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\Philka\source\repos\Manager\Manager\bin\Debug\Manager.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Philka\source\repos\Manager\Manager\bin\Debug\Download.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Philka\source\repos\Manager\Manager\bin\Debug\Manager.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Philka\source\repos\Manager\Manager\bin\Debug\Manager.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Philka\source\repos\Manager\Manager\bin\Debug\Microsoft.Expression.Interactions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Philka\source\repos\Manager\Manager\bin\Debug\Microsoft.Expression.Interactions.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Philka\source\repos\Manager\Manager\bin\Debug\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Philka\source\repos\Manager\Manager\bin\Debug\Newtonsoft.Json.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Philka\source\repos\Manager\Manager\bin\Debug\System.Windows.Interactivity.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Philka\source\repos\Manager\Manager\bin\Debug\System.Windows.Interactivity.xml"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

