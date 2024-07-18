@Echo Off
echo Only supports x64 windows
echo  Create desktop shortcut
mshta VBScript:Execute("Set a=CreateObject(""WScript.Shell""):Set b=a.CreateShortcut(a.SpecialFolders(""Desktop"") & ""\ox.web.lnk""):b.TargetPath=""%~dp0ox.web.exe"":b.IconLocation =""%~dp0favicon.web.ico"":b.WorkingDirectory=""%~dp0"":b.Save:close")
mshta VBScript:Execute("Set a=CreateObject(""WScript.Shell""):Set b=a.CreateShortcut(a.SpecialFolders(""Desktop"") & ""\ox.mix.lnk""):b.TargetPath=""%~dp0ox.mix.exe"":b.IconLocation =""%~dp0favicon.mix.ico"":b.WorkingDirectory=""%~dp0"":b.Save:close")
mshta VBScript:Execute("Set a=CreateObject(""WScript.Shell""):Set b=a.CreateShortcut(a.SpecialFolders(""Desktop"") & ""\ox.box.lnk""):b.TargetPath=""%~dp0ox.box.exe"":b.IconLocation =""%~dp0favicon.box.ico"":b.WorkingDirectory=""%~dp0"":b.Save:close")
echo install windowsdesktop-runtime-7.0.20-win-x64
windowsdesktop-runtime-7.0.20-win-x64
echo install aspnetcore-runtime-7.0.20-win-x64
aspnetcore-runtime-7.0.20-win-x64