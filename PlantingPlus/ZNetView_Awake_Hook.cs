using HarmonyLib;

namespace PlantingPlus
{
	[HarmonyPatch(typeof(ZNetView), "Awake")]
	public static class ZNetView_Awake_Hook
	{
		[HarmonyPrefix]
		public static bool Prefix()
		{
			if (PlantingPlus.isCloningPrefab)
			{
				return false;
			}
			return true;
		}
	}
}
