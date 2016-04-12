del /Q ..\web-client\web-server-0.0.1.jar
del /Q ..\web-client\app.properties
del /Q ..\web-client\build.bat
del /Q ..\web-client\run.bat
copy ..\web-server\target\web-server-0.0.1.jar ..\web-client /Y
copy ..\web-server\app.properties ..\web-client /Y
copy build.bat ..\web-client /Y
copy run.bat ..\web-client /Y