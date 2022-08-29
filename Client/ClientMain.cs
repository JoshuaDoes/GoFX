using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace gofx.Client
{
    public class ClientMain : BaseScript
    {
        public ClientMain()
        {
            Debug.WriteLine("Hi from gofx.Client!");
        }

        [Tick]
        public Task OnTick()
        {
            CitizenFX.Core.Native.API.DrawRect(0.5f, 0.5f, 0.5f, 0.5f, 255, 255, 255, 150);

            return Task.FromResult(0);
        }
    }
}