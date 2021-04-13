using System.Collections.Generic;
using HarmonyLib;

namespace PlantingPlus
{
	[HarmonyPatch(typeof(Player), "SetLocalPlayer")]
	public static class Player_SetLocalPlayer_Hook
	{
		[HarmonyPostfix]
		public static void Postfix(HashSet<string> ___m_knownRecipes, int ___m_removeRayMask)
		{
			if (PlantingPlus.raspberryBushPiece != null && PlantingPlus.blueberryBushPiece != null && PlantingPlus.cloudberryBushPiece != null)
			{
				if (!___m_knownRecipes.Contains(PlantingPlus.raspberryBushPiece.m_name))
				{
					___m_knownRecipes.Add(PlantingPlus.raspberryBushPiece.m_name);
				}
				if (!___m_knownRecipes.Contains(PlantingPlus.blueberryBushPiece.m_name))
				{
					___m_knownRecipes.Add(PlantingPlus.blueberryBushPiece.m_name);
				}
				if (!___m_knownRecipes.Contains(PlantingPlus.cloudberryBushPiece.m_name))
				{
					___m_knownRecipes.Add(PlantingPlus.cloudberryBushPiece.m_name);
				}
			}
			if (PlantingPlus.enableOtherResources.Value && PlantingPlus.pickableMushroomPiece != null && PlantingPlus.pickableYellowMushroomPiece != null && PlantingPlus.pickableBlueMushroomPiece != null && PlantingPlus.pickableThistlePiece != null && PlantingPlus.pickableDandelionPiece != null && PlantingPlus.birchTreeSaplingObject != null && PlantingPlus.oakTreeSaplingObject != null && PlantingPlus.swampTreeSaplingObject != null)
			{
				if (!___m_knownRecipes.Contains(PlantingPlus.pickableMushroomPiece.m_name))
				{
					___m_knownRecipes.Add(PlantingPlus.pickableMushroomPiece.m_name);
				}
				if (!___m_knownRecipes.Contains(PlantingPlus.pickableYellowMushroomPiece.m_name))
				{
					___m_knownRecipes.Add(PlantingPlus.pickableYellowMushroomPiece.m_name);
				}
				if (!___m_knownRecipes.Contains(PlantingPlus.pickableBlueMushroomPiece.m_name))
				{
					___m_knownRecipes.Add(PlantingPlus.pickableBlueMushroomPiece.m_name);
				}
				if (!___m_knownRecipes.Contains(PlantingPlus.pickableThistlePiece.m_name))
				{
					___m_knownRecipes.Add(PlantingPlus.pickableThistlePiece.m_name);
				}
				if (!___m_knownRecipes.Contains(PlantingPlus.pickableDandelionPiece.m_name))
				{
					___m_knownRecipes.Add(PlantingPlus.pickableDandelionPiece.m_name);
				}
				if (!___m_knownRecipes.Contains(PlantingPlus.birchTreeSaplingObject.GetComponent<Piece>().m_name))
				{
					___m_knownRecipes.Add(PlantingPlus.birchTreeSaplingObject.GetComponent<Piece>().m_name);
				}
				if (!___m_knownRecipes.Contains(PlantingPlus.oakTreeSaplingObject.GetComponent<Piece>().m_name))
				{
					___m_knownRecipes.Add(PlantingPlus.oakTreeSaplingObject.GetComponent<Piece>().m_name);
				}
				if (!___m_knownRecipes.Contains(PlantingPlus.swampTreeSaplingObject.GetComponent<Piece>().m_name))
				{
					___m_knownRecipes.Add(PlantingPlus.swampTreeSaplingObject.GetComponent<Piece>().m_name);
				}
			}
		}
	}
}
