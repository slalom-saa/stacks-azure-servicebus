cd "C:\Source\Stacks\Azure Service Bus"

if (Test-Path ".sonarqube") {
    Remove-Item ".sonarqube" -Recurse -Force
}

SonarQube.Scanner.MSBuild.exe begin /d:sonar.host.url=https://sonarqube.patolus.io /k:"stacks-azure-servicebus" /n:"Stacks - Azure Service Bus" /v:"1.0" /d:sonar.login=6e34d81ee907b527d4f87598e3673bc54571efa1

& "C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\msbuild.exe" /t:Rebuild

SonarQube.Scanner.MSBuild.exe end
