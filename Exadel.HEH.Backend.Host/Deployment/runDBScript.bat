@ECHO OFF
SET /P exeFolder=Enter path leading to mongo.exe file (for example: C:\Program Files\MongoDB\Server\4.4\bin):
SET /P connectionString=Enter СonnectionString for DB (for example: localhost:27017/ExadelHEH):
SET /P jsFolder=Enter path leading to script files (for example: D:\...\Exadel.HEH.Backend.Host\Deployment\DBScripts):
SET /P loadFile=Enter load script name (for example: db.js):
SET exePath="%exeFolder%\mongo.exe"
CD /D %jsFolder%
SET command=%exePath% %connectionString% %loadFile%
%command%
PAUSE
@ECHO ON