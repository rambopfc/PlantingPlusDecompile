using HarmonyLib;

namespace PlantingPlus
{
	[HarmonyPatch(typeof(ItemDrop), "Start")]
	public static class ItemDrop_Start_Hook
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
