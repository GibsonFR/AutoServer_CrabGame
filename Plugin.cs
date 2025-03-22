global using BepInEx;
global using BepInEx.IL2CPP;
global using HarmonyLib;

namespace AutoServer
{
    [BepInPlugin("9B8711D3-536E-4BB7-AD83-08F7EC9F52AF", "AutoServer", "1.0.0")]
    public class Plugin : BasePlugin
    {
        public static Plugin __instance = null;
        public static bool lobbyCreated;
        public override void Load()
        {
            __instance = this;

            Harmony.CreateAndPatchAll(typeof(Plugin));
            Log.LogInfo("Mod created by Gibson, discord : gib_son");
        }

        // Create a server on Dissonance Awake to prevent issue with microphone
        [HarmonyPatch(typeof(DissonanceManager), nameof(DissonanceManager.Awake))]
        [HarmonyPostfix]
        public static void OnDissonanceManagerAwakePost()
        {
            if (lobbyCreated) return;
          
            SteamManager.Instance.StartLobby();
            __instance.Log.LogInfo("[AutoServer] Lobby successfully created");
            lobbyCreated = true;
        }

        //Anticheat Bypass 
        [HarmonyPatch(typeof(EffectManager), "Method_Private_Void_GameObject_Boolean_Vector3_Quaternion_0")]
        [HarmonyPatch(typeof(LobbyManager), "Method_Private_Void_0")]
        [HarmonyPatch(typeof(MonoBehaviourPublicVesnUnique), "Method_Private_Void_0")]
        [HarmonyPatch(typeof(LobbySettings), "Method_Public_Void_PDM_2")]
        [HarmonyPatch(typeof(MonoBehaviourPublicTeplUnique), "Method_Private_Void_PDM_32")]
        [HarmonyPrefix]
        public static bool Prefix(System.Reflection.MethodBase __originalMethod)
        {
            return false;
        }
    }
}