using HarmonyLib;

namespace PlantingPlus
{
	[HarmonyPatch(typeof(ZNetScene), "Awake")]
	public static class ZNetScene_Awake_Hook
	{
		[HarmonyPrefix]
		public static void HarmonyPrefix(ZNetScene __instance)
		{
			if (PlantingPlus.enableOtherResources.Value && __instance != null && PlantingPlus.birchConeObject != null && PlantingPlus.oakSeedsObject != null && PlantingPlus.ancientSeedsObject != null && PlantingPlus.birchTreeSaplingObject != null && PlantingPlus.oakTreeSaplingObject != null && PlantingPlus.swampTreeSaplingObject != null)
			{
				__instance.m_prefabs.Add(PlantingPlus.birchConeObject);
				__instance.m_prefabs.Add(PlantingPlus.oakSeedsObject);
				__instance.m_prefabs.Add(PlantingPlus.ancientSeedsObject);
				__instance.m_prefabs.Add(PlantingPlus.birchTreeSaplingObject);
				__instance.m_prefabs.Add(PlantingPlus.oakTreeSaplingObject);
				__instance.m_prefabs.Add(PlantingPlus.swampTreeSaplingObject);
			}
		}
	}
}
