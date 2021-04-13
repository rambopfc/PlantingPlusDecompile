using System.Threading;
using UnityEngine;

namespace PlantingPlus
{
	public static class ModLoader
	{
		public static GameObject Load;

		public static void Main(string[] args)
		{
			InitThreading();
		}

		public static void InitThreading()
		{
			new Thread((ThreadStart)delegate
			{
				Thread.Sleep(10000);
				Init();
			}).Start();
		}

		public static void Init()
		{
			Load = new GameObject("PlantingPlus");
			Load.AddComponent<PlantingPlus>();
			Object.DontDestroyOnLoad(Load);
		}
	}
}
