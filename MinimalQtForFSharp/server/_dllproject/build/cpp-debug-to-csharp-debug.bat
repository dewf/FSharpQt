copy /y .\cmake-build-debug\*.dll ..\..\..\..\AppRunner\bin\Debug\net8.0
rem robocopy cmake-build-debug\plugins ..\..\..\..\AppRunner\bin\Debug\net8.0\plugins /e

copy /y .\cmake-build-debug\*.dll ..\..\..\..\Workbench\bin\Debug\net8.0
rem robocopy cmake-build-debug\plugins ..\..\..\..\Workbench\bin\Debug\net8.0\plugins /e

rem copy /y .\cmake-build-debug\*.dll ..\..\..\..\SevenGuisFsharp\bin\Debug\net8.0
rem robocopy cmake-build-debug\plugins ..\..\..\..\SevenGuisFsharp\bin\Debug\net8.0\plugins /e

pause


	