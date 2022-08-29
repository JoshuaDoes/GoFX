using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace gofx.Server
{
    public class ServerMain : BaseScript
    {
        List<GoResource> goResources = new List<GoResource>();

        public ServerMain()
        {
            /*EventHandlers["gameEventTriggered"] += new Action<string, int[]>(GameEventTriggered);
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
            EventHandlers["onClientResourceStop"] += new Action<string>(OnClientResourceStop);*/
            EventHandlers["onResourceStart"] += new Action<string>(OnResourceStart);
            /*EventHandlers["onResourceStarting"] += new Action<string>(OnResourceStarting);
            EventHandlers["onResourceStop"] += new Action<string>(OnResourceStop);
            EventHandlers["playerConnecting"] += new Action<Player, string>(PlayerConnecting);
            EventHandlers["playerDropped"] += new Action<Player, string>(PlayerDropped);
            EventHandlers["populationPedCreating"] += new Action<float, float, float, uint>(PopulationPedCreating);
            EventHandlers["rconCommand"] += new Action<string, string[]>(RconCommand);*/
        }

        /*private void GameEventTriggered(string eventName, int[] args)
        {
            for (int i = 0; i < goResources.Count; i++)
            {
                goResources[i].GoGameEventTriggered(eventName, args);
            }
        }
        private void OnClientResourceStart(string resourceName)
        {
            for (int i = 0; i < goResources.Count; i++)
            {
                goResources[i].GoOnClientResourceStart(resourceName);
            }
        }
        private void OnClientResourceStop(string resourceName)
        {
            for (int i = 0; i < goResources.Count; i++)
            {
                goResources[i].GoOnClientResourceStop(resourceName);
            }
        }*/
        private void OnResourceStart(string resourceName)
        {
            //Search all server subdirectories for Go resources
            string resourceDir = CitizenFX.Core.Native.API.GetResourcePath(resourceName);
            Debug.WriteLine("Getting list of Go server resources from {0}", resourceDir);
            string[] goAssemblies = Directory.GetFiles(resourceDir, "server.go.dll", SearchOption.AllDirectories);

            if (goAssemblies.Length == 0)
            {
                Debug.WriteLine("No Go server resources to load!");
            }
            else
            {
                //Start loading each resource into memory
                for (int i = 0; i < goAssemblies.Length; i++)
                {
                    Debug.WriteLine("Loading Go server resource {0}: {1}", i, goAssemblies[i]);
                    string goPath = Path.GetDirectoryName(goAssemblies[i]);
                    string goFile = Path.GetFileName(goAssemblies[i]);
                    AppDomain.CurrentDomain.ClearPrivatePath();
                    AppDomain.CurrentDomain.AppendPrivatePath(goPath);

                    GoResource goResource = new GoResource();
                    try
                    {
						goResource.LoadAssembly(goAssemblies[i]);
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine("Failed to load Go server resource {0} from {1}: {2}", i, goAssemblies[i], ex.ToString());
                        continue;
                    }
                    goResources.Add(goResource);
                }
                if (goResources.Count > 0) {
                    Debug.WriteLine("Finished loading {0} Go server resources!", goResources.Count);
                } else
				{
                    Debug.WriteLine("No Go server resources were loaded!");
				}
            }

            for (int i = 0; i < goResources.Count; i++)
            {
                goResources[i].OnResourceStart(resourceName);
            }
        }
        /*private void OnResourceStarting(string resourceName)
        {
            for (int i = 0; i < goResources.Count; i++)
            {
                goResources[i].GoOnResourceStarting(resourceName);
            }
        }
        private void OnResourceStop(string resourceName)
        {
            for (int i = 0; i < goResources.Count; i++)
            {
                goResources[i].GoOnResourceStart(resourceName);
            }
        }
        private void PlayerConnecting([FromSource] Player player, string playerName)
        {
            for (int i = 0; i < goResources.Count; i++)
            {
                goResources[i].GoPlayerConnecting(player, playerName);
            }
        }
        private void PlayerDropped([FromSource] Player player, string reason)
        {
            for (int i = 0; i < goResources.Count; i++)
            {
                goResources[i].GoPlayerDropped(player, reason);
            }
        }
        private void PopulationPedCreating(float posX, float posY, float posZ, uint model)
        {
            for (int i = 0; i < goResources.Count; i++)
            {
                goResources[i].GoPopulationPedCreating(posX, posY, posZ, model);
            }
        }
        private void RconCommand(string command, string[] args)
        {
            for (int i = 0; i < goResources.Count; i++)
            {
                goResources[i].GoRconCommand(command, args);
            }
        }*/
    }
}