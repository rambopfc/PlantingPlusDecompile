using HarmonyLib;
using UnityEngine;

namespace PlantingPlus
{
	[HarmonyPatch(typeof(ObjectDB), "CopyOtherDB")]
	public static class ObjectDB_CopyOtherDB_Hook
	{
		[HarmonyPostfix]
		public static void Postfix()
		{
			if (!PlantingPlus.enableOtherResources.Value)
			{
				return;
			}
			while (PlantingPlus.birchConeObject == null || PlantingPlus.oakSeedsObject == null || PlantingPlus.ancientSeedsObject == null)
			{
				Object[] array = Resources.FindObjectsOfTypeAll(typeof(GameObject));
				for (int i = 0; i < array.Length; i++)
				{
					GameObject gameObject = (GameObject)array[i];
					if (gameObject.name == "PineCone" && PlantingPlus.birchConeObject == null)
					{
						PlantingPlus.isCloningPrefab = true;
						PlantingPlus.birchConeObject = Object.Instantiate(gameObject);
						Object.DontDestroyOnLoad(PlantingPlus.birchConeObject);
						PlantingPlus.birchConeObject.hideFlags = HideFlags.HideInHierarchy;
						PlantingPlus.birchConeObject.name = "BirchCone";
						PlantingPlus.isCloningPrefab = false;
					}
					if (gameObject.name == "BeechSeeds" && PlantingPlus.oakSeedsObject == null)
					{
						PlantingPlus.isCloningPrefab = true;
						PlantingPlus.oakSeedsObject = Object.Instantiate(gameObject);
						Object.DontDestroyOnLoad(PlantingPlus.oakSeedsObject);
						PlantingPlus.oakSeedsObject.hideFlags = HideFlags.HideInHierarchy;
						PlantingPlus.oakSeedsObject.name = "OakSeeds";
						PlantingPlus.isCloningPrefab = false;
					}
					if (gameObject.name == "BeechSeeds" && PlantingPlus.ancientSeedsObject == null)
					{
						PlantingPlus.isCloningPrefab = true;
						PlantingPlus.ancientSeedsObject = Object.Instantiate(gameObject);
						Object.DontDestroyOnLoad(PlantingPlus.ancientSeedsObject);
						PlantingPlus.ancientSeedsObject.hideFlags = HideFlags.HideInHierarchy;
						PlantingPlus.ancientSeedsObject.name = "AncientSeeds";
						PlantingPlus.isCloningPrefab = false;
					}
				}
				if (PlantingPlus.birchConeObject != null && PlantingPlus.oakSeedsObject != null && PlantingPlus.ancientSeedsObject != null)
				{
					PlantingPlus.birchConeObject.GetComponent<ItemDrop>().name = "BirchCone";
					PlantingPlus.birchConeObject.GetComponent<ItemDrop>().m_itemData.m_shared.m_name = "Birch Cone";
					PlantingPlus.birchConeObject.GetComponent<ItemDrop>().m_itemData.m_shared.m_description = "Plant it to grow a birch tree.";
					PlantingPlus.oakSeedsObject.GetComponent<ItemDrop>().name = "OakSeeds";
					PlantingPlus.oakSeedsObject.GetComponent<ItemDrop>().m_itemData.m_shared.m_name = "Oak Seeds";
					PlantingPlus.oakSeedsObject.GetComponent<ItemDrop>().m_itemData.m_shared.m_description = "Plant them to grow an oak tree.";
					PlantingPlus.ancientSeedsObject.GetComponent<ItemDrop>().name = "AncientSeeds";
					PlantingPlus.ancientSeedsObject.GetComponent<ItemDrop>().m_itemData.m_shared.m_name = "Ancient Seeds";
					PlantingPlus.ancientSeedsObject.GetComponent<ItemDrop>().m_itemData.m_shared.m_description = "Plant them to grow an ancient tree.";
				}
			}
			if (!ObjectDB.instance.m_items.Contains(PlantingPlus.birchConeObject))
			{
				ObjectDB.instance.m_items.Add(PlantingPlus.birchConeObject);
			}
			if (!ObjectDB.instance.m_items.Contains(PlantingPlus.oakSeedsObject))
			{
				ObjectDB.instance.m_items.Add(PlantingPlus.oakSeedsObject);
			}
			if (!ObjectDB.instance.m_items.Contains(PlantingPlus.ancientSeedsObject))
			{
				ObjectDB.instance.m_items.Add(PlantingPlus.ancientSeedsObject);
			}
		}
	}
}
