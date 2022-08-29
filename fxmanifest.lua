fx_version 'bodacious'
game 'gta5'

--Only provide access to client-side dependencies within the resource
--When the server resource loads, it can read the resource dependencies on-disk
file 'Client/*.dll'

client_scripts {
	'Client/GoFX.dll',
	'Client/*.go.dll',
	'Client/*.net.dll'
}
server_scripts {
	'Server/GoFX.dll',
	'Server/*.go.dll',
	'Server/*.net.dll'
}

author 'JoshuaDoes'
version '1.0.0'
description 'C# loader for resources written in Go'