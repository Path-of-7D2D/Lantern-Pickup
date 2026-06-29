using System.Reflection;
using HarmonyLib;
using UnityEngine.Scripting;

namespace LanternPickup
{
    [Preserve]
    public class LanternPickupModApi : IModApi
    {
        public void InitMod(Mod _modInstance)
        {
            var harmony = new Harmony("com.pathof7d2d.lanternpickup");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Log.Out("[LanternPickup] Loaded lantern pickup activation patches.");
        }
    }
}
