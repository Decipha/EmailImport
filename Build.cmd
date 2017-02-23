del Build.log /Q
rd /S /Q bin

if exist "C:\Program Files (x86)" (
   "C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\DevEnv.EXE" "EmailImport.sln" /Out Build.log /Rebuild Release
) else (
   "C:\Program Files\Microsoft Visual Studio 12.0\Common7\IDE\DevEnv.EXE" "EmailImport.sln" /Out Build.log /Rebuild Release
)

md bin
xcopy src\EmailImport\bin\Release\*.* bin /E
xcopy src\EmailImport.Configuration\bin\Release\EmailImport.Configuration.exe* bin /E
xcopy src\EmailImport.Viewer\bin\Release\EmailImport.Viewer.exe* bin /E
rem xcopy src\EmailImport.FileUpload\bin\Release\EmailImport.FileUpload.exe* bin /E

if exist "C:\Program Files (x86)" (
   xcopy "C:\Program Files (x86)\Microsoft.NET\Primary Interop Assemblies\Microsoft.mshtml.dll" bin /E
) else (
   xcopy "C:\Program Files\Microsoft.NET\Primary Interop Assemblies\Microsoft.mshtml.dll" bin /E
)

del /S /Q bin\*.pdb
del /S /Q bin\*.xml
del bin\EmailImport.vshost.*

