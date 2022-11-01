$filePath = "$($PSScriptRoot)/app.config"
if (-not(Test-Path -Path $filePath -PathType Leaf)) {
	
	Write-Host ""
	Write-Host " *`tcreating app.config"
	
	
	New-Item -PATH $filePath | Out-Null

	"<?xml version=""1.0"" encoding=""utf-8"" ?>" | Out-File -FilePath $filePath -Append
	"<configuration>" | Out-File -FilePath $filePath -Append
	"`t<appSettings>" | Out-File -FilePath $filePath -Append
	"`t`t<add key=""AuthToken"" value="""" />" | Out-File -FilePath $filePath -Append
	"`t`t<add key=""Year"" value=""2022"" />" | Out-File -FilePath $filePath -Append
	"`t`t<add key=""Day"" value=""1"" />" | Out-File -FilePath $filePath -Append
	"`t</appSettings>" | Out-File -FilePath $filePath -Append
	"</configuration>" | Out-File -FilePath $filePath -Append
	
	Write-Host " *`tapp.config successfully created!"
	Write-Host ""
}
else {
	Write-Host ""
	Write-Host " *`t app.config already exists!"
	Write-Host ""
}