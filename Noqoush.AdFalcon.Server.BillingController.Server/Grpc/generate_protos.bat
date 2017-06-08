setlocal

@rem enter this directory
cd /d %~dp0

set TOOLS_PATH=..\..\packages\Grpc.Tools.1.3.6\tools\windows_x64
set PROTO_PATH=Protos
%
%TOOLS_PATH%\protoc.exe  --csharp_out=. %PROTO_PATH%\MasterBankerService.proto --grpc_out=%PROTO_PATH%/../  --plugin=protoc-gen-grpc=%TOOLS_PATH%\grpc_csharp_plugin.exe 

endlocal
