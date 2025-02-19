using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Sunless.Game.ApplicationProviders;
using Sunless.Game.Entities.Geography;
using Sunless.Game.UI.Storylet;

namespace CorrespondentFix;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    public ManualLogSource Logga;
    public static Plugin Instance;

    private void Awake()
    {
        // Plugin startup logic
        Logga = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        Instance = this;

        Harmony.CreateAndPatchAll(typeof(CorrespondentChartPatch));
    }
}

[HarmonyPatch(typeof(LegacyProvider), MethodType.Getter)]
public static class CorrespondentChartPatch
{
    [HarmonyPrefix]
    [HarmonyPatch("Chart")]
    private static bool PrefixChart(ref SerializableTileConfig __result, LegacyProvider __instance)
    {
        Plugin.Instance.Logga.LogInfo("CorrespondentChartPatch PrefixChart");

        if (__instance.Correspondent.Selected)
        {
            SerializableTileConfig deadCharacterTiles = __instance.DeadCharacter.TileConfig;
            foreach (TileInstance tileInstance in deadCharacterTiles.Tiles)
            {
                tileInstance.DiscoveredFlavourItems.Clear();
                tileInstance.ProceduralTerrain.Clear();
                tileInstance.ProceduralDecals.Clear();
                tileInstance.DiscoveredSpawnPoints.Clear();
                tileInstance.DiscoveredTerrain.Clear();
                tileInstance.DiscoveredLabels.Clear();
                tileInstance.Discoveredphenomena.Clear();
                tileInstance.AbyssBuff.Clear();
            }
            __result = deadCharacterTiles;
            return false; // Don't run the original function
        }
        __result = null;

        return false; // Don't run the original function
    }
}