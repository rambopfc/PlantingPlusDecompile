using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace PlantingPlus
{
	[HarmonyPatch(typeof(UnityEngine.Object), "Instantiate", new Type[]
	{
		typeof(UnityEngine.Object),
		typeof(Vector3),
		typeof(Quaternion)
	})]
	public static class PlacePiece_Hook
	{
		[HarmonyPostfix]
		public static void Postfix(UnityEngine.Object __result)
		{
			if (PlantingPlus.resourcesSpawnEmpty.Value && (bool)typeof(TerrainModifier).GetField("m_triggerOnPlaced", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null) && __result is GameObject)
			{
				GameObject gameObject = (GameObject)__result;
				if (gameObject.name.StartsWith("RaspberryBush") || gameObject.name.StartsWith("BlueberryBush") || gameObject.name.StartsWith("CloudberryBush") || gameObject.name.StartsWith("Pickable_Mushroom") || gameObject.name.StartsWith("Pickable_Mushroom_yellow") || gameObject.name.StartsWith("Pickable_Mushroom_blue") || gameObject.name.StartsWith("Pickable_Thistle") || gameObject.name.StartsWith("Pickable_Dandelion"))
				{
					typeof(Pickable).GetMethod("SetPicked", BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[1]
					{
						typeof(bool)
					}, null).Invoke(gameObject.GetComponent<Pickable>(), new object[1]
					{
						true
					});
				}
			}
		}
	}
}
