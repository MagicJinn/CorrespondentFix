using BepInEx;
using HarmonyLib;
using Sunless.Game.ApplicationProviders;
using Sunless.Game.Entities.Geography;

namespace CorrespondentFix;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

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
        if (!__instance.Correspondent.Selected)
        {
            __result = null; // Return null, the game generates a new chart
        }
        else
        {
            SerializableTileConfig deadCharacterTiles = __instance.DeadCharacter.TileConfig;
            foreach (TileInstance tileInstance in deadCharacterTiles.Tiles)
            {
                // Clear all discovered curiosities from the tile
                tileInstance.DiscoveredFlavourItems.Clear();
                tileInstance.ProceduralTerrain.Clear();
                tileInstance.ProceduralDecals.Clear();
                tileInstance.DiscoveredSpawnPoints.Clear();
                tileInstance.DiscoveredTerrain.Clear();
                tileInstance.DiscoveredLabels.Clear();
                tileInstance.Discoveredphenomena.Clear();
                tileInstance.AbyssBuff.Clear();
            }
            __result = deadCharacterTiles; // Return the modified tiles
        }

        return false; // Don't run the original function
    }
}