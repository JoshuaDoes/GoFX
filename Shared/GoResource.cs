using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using CitizenFX.Core;

public class GoResource
{
	public void LoadAssembly(string pathAssembly)
	{
		if (Hello())
		{ //Failure point to test if assembly loaded
			string assemblyPath = Path.GetDirectoryName(pathAssembly);
			string assemblyName = Path.GetFileName(pathAssembly);
			Debug.WriteLine("path: {0}, name: {1}\n", assemblyPath, assemblyName);
			//bool inited = InitAssembly(assemblyPath, assemblyName);
			bool inited = InitAssembly(assemblyPath, assemblyName);
			if (!inited)
			{
				throw new InvalidOperationException("Unable to initialize GoFX resource!");
			}
		}
		else
		{
			throw new InvalidOperationException("Unable to call Hello() on GoFX");
		}
	}

	/*[DllImport("server.go", CharSet = CharSet.Unicode)]
	public static extern void GameEventTriggered(Golang.GoString eventName, Golang.GoSlice args);
	[DllImport("server.go", CharSet = CharSet.Unicode)]
	public static extern void OnClientResourceStart(Golang.GoString resourceName);
	[DllImport("server.go", CharSet = CharSet.Unicode)]
	public static extern void OnClientResourceStop(Golang.GoString resourceName);*/


	[DllImport("GoFX", CallingConvention = CallingConvention.Cdecl)]
	public static extern bool Hello();
	[DllImport("GoFX", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
	public static extern bool InitAssembly(string assemblyPath, string assemblyName);

	[DllImport("GoFX", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
	public static extern void GoOnResourceStart(Golang.GoString resourceName);


	/*[DllImport("server.go", CharSet = CharSet.Unicode)]
	public static extern void OnResourceStarting(Golang.GoString resourceName);
	[DllImport("server.go", CharSet = CharSet.Unicode)]
	public static extern void OnResourceStop(Golang.GoString resourceName);
	[DllImport("server.go", CharSet = CharSet.Unicode)]
	public static extern void PlayerConnecting(Player player, Golang.GoString playerName);
	[DllImport("server.go", CharSet = CharSet.Unicode)]
	public static extern void PlayerDropped(Player player, Golang.GoString reason);
	[DllImport("server.go")]
	public static extern void PopulationPedCreating(float posX, float posY, float posZ, uint model);
	[DllImport("server.go", CharSet = CharSet.Unicode)]
	public static extern void RconCommand(Golang.GoString command, Golang.GoSlice args);

	[DllImport("server.go")]
	public static extern byte[] GetNextEvent();
	[DllImport("server.go")]
	public static extern bool WriteNextEvent(byte[] newEvent); //Returns false on JSON unmarshal error

	public void GoGameEventTriggered(string eventName, int[] args)
	{
		GameEventTriggered(new Golang.GoString(eventName, eventName.Length), Golang.GoSliceFromIntArray(args));
	}

	public void GoOnClientResourceStart(string resourceName)
	{
		OnClientResourceStart(new Golang.GoString(resourceName, resourceName.Length));
	}
	public void GoOnClientResourceStop(string resourceName)
	{
		OnClientResourceStop(new Golang.GoString(resourceName, resourceName.Length));
	}*/
	public void OnResourceStart(string resourceName)
	{
		GoOnResourceStart(new Golang.GoString { msg = resourceName, len = resourceName.Length });
	}
	/*public void GoOnResourceStarting(string resourceName)
	{
		OnResourceStarting(new Golang.GoString(resourceName, resourceName.Length));
	}
	public void GoOnResourceStop(string resourceName)
	{
		OnResourceStop(new Golang.GoString(resourceName, resourceName.Length));
	}
	public void GoPlayerConnecting(Player player, string playerName)
	{
		PlayerConnecting(player, new Golang.GoString(playerName, playerName.Length));
	}
	public void GoPlayerDropped(Player player, string reason)
	{
		PlayerDropped(player, new Golang.GoString(reason, reason.Length));
	}
	public void GoPopulationPedCreating(float posX, float posY, float posZ, uint model)
	{
		PopulationPedCreating(posX, posY, posZ, model);
	}
	public void GoRconCommand(string command, string[] args)
	{
		RconCommand(new Golang.GoString(command, command.Length), Golang.GoSliceFromStringArray(args));
	}*/
}