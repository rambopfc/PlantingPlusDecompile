using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace PlantingPlus
{
	[HarmonyPatch(typeof(Player), "RemovePiece")]
	public static class Player_RemovePiece_Hook
	{
		[HarmonyPrefix]
		public static bool Prefix(Player __instance, ZSyncAnimation ___m_zanim, ref bool __result)
		{
			if (__instance.GetRightItem().m_shared.m_name == "$item_cultivator")
			{
				if (Physics.Raycast(GameCamera.instance.transform.position, GameCamera.instance.transform.forward, out var hitInfo, 50f, LayerMask.GetMask("item", "piece_nonsolid", "terrain")) && Vector3.Distance(hitInfo.point, __instance.m_eye.position) < __instance.m_maxPlaceDistance)
				{
					Pickable pickable = hitInfo.collider.GetComponentInParent<Pickable>();
					if (pickable == null)
					{
						float num = 999999f;
						ZNetView zNetView = null;
						foreach (KeyValuePair<ZDO, ZNetView> item in typeof(ZNetScene).GetField("m_instances", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(ZNetScene.instance) as Dictionary<ZDO, ZNetView>)
						{
							if ((bool)item.Value.gameObject.GetComponent<Pickable>())
							{
								float num2 = Mathf.Sqrt((item.Key.GetPosition().x - hitInfo.point.x) * (item.Key.GetPosition().x - hitInfo.point.x) + (item.Key.GetPosition().z - hitInfo.point.z) * (item.Key.GetPosition().z - hitInfo.point.z));
								if ((double)num2 <= 0.5 && num2 <= num)
								{
									num = num2;
									zNetView = item.Value;
								}
							}
						}
						if ((bool)zNetView)
						{
							pickable = zNetView.gameObject.GetComponent<Pickable>();
						}
					}
					if (pickable != null)
					{
						ZNetView component = pickable.GetComponent<ZNetView>();
						if (component == null)
						{
							__result = false;
							return false;
						}
						component.ClaimOwnership();
						__instance.m_removeEffects.Create(pickable.transform.position, Quaternion.identity);
						ZNetScene.instance.Destroy(pickable.gameObject);
						ItemDrop.ItemData rightItem = __instance.GetRightItem();
						if (rightItem != null)
						{
							__instance.FaceLookDirection();
							___m_zanim.SetTrigger(rightItem.m_shared.m_attack.m_attackAnimation);
						}
						__result = true;
						return false;
					}
				}
				__result = false;
				return false;
			}
			return true;
		}
	}
}
