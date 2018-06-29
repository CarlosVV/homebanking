param(
	[string] $env = "Dev",
	[string] $password
)

&"C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.exe" DinersClubOnline.sln /p:Configuration=$env /p:DeployOnBuild=true /p:PublishProfile=Dev /p:Password=$password