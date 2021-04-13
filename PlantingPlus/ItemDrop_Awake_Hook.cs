using HarmonyLib;

namespace PlantingPlus
{
	[HarmonyPatch(typeof(ItemDrop), "Awake")]
	public static class ItemDrop_Awake_Hook
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
