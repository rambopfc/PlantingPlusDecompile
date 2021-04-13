using HarmonyLib;

namespace PlantingPlus
{
	[HarmonyPatch(typeof(Pickable), "SetPicked")]
	public static class FixPickableTime
	{
		public class PickState
		{
			public long picked_time;

			public bool picked;
		}

		[HarmonyPrefix]
		public static void Prefix(bool picked, ZNetView ___m_nview, bool ___m_picked, ref PickState __state)
		{
			__state = new PickState();
			__state.picked_time = ___m_nview.GetZDO().GetLong("picked_time", 0L);
			__state.picked = ___m_picked;
		}

		[HarmonyPostfix]
		public static void Postfix(bool picked, ZNetView ___m_nview, bool ___m_picked, ref PickState __state)
		{
			if (__state != null && __state.picked == ___m_picked && ___m_nview.IsOwner())
			{
				___m_nview.GetZDO().Set("picked_time", __state.picked_time);
			}
		}
	}
}
