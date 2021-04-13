using System.Collections.Generic;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using PlantingPlus.Properties;
using UnityEngine;

namespace PlantingPlus
{
	[BepInPlugin("com.bkeyes93.PlantingPlus", "PlantingPlus", "1.4.5")]
	public class PlantingPlus : BaseUnityPlugin
	{
		public const string MODNAME = "PlantingPlus";

		public const string AUTHOR = "bkeyes93";

		public const string GUID = "com.bkeyes93.PlantingPlus";

		public const string VERSION = "1.4.5";

		public static GameObject raspberryBushObject;

		public static GameObject blueberryBushObject;

		public static GameObject cloudberryBushObject;

		public static GameObject pickableMushroomObject;

		public static GameObject pickableYellowMushroomObject;

		public static GameObject pickableBlueMushroomObject;

		public static GameObject pickableThistleObject;

		public static GameObject pickableDandelionObject;

		public static ItemDrop raspberryItem;

		public static ItemDrop blueberriesItem;

		public static ItemDrop cloudberryItem;

		public static ItemDrop cultivatorItem;

		public static ItemDrop mushroomItem;

		public static ItemDrop yellowMushroomItem;

		public static ItemDrop blueMushroomItem;

		public static ItemDrop thistleItem;

		public static ItemDrop dandelionItem;

		public static GameObject placeWoodPoleVfx;

		public static GameObject buildCultivatorSfx;

		public static Piece raspberryBushPiece;

		public static Piece blueberryBushPiece;

		public static Piece cloudberryBushPiece;

		public static Piece pickableMushroomPiece;

		public static Piece pickableYellowMushroomPiece;

		public static Piece pickableBlueMushroomPiece;

		public static Piece pickableThistlePiece;

		public static Piece pickableDandelionPiece;

		public static GameObject birchConeObject;

		public static GameObject oakSeedsObject;

		public static GameObject ancientSeedsObject;

		public static GameObject birchTreeSaplingObject;

		public static GameObject oakTreeSaplingObject;

		public static GameObject birchTree1Object;

		public static GameObject birchTree2Object;

		public static GameObject oakTree1Object;

		public static GameObject swampTree1Object;

		public static GameObject pineTreeSaplingObject;

		public static GameObject beechTreeSaplingObject;

		public static GameObject swampTreeSaplingObject;

		public static bool isCloningPrefab;

		public static ConfigEntry<bool> modEnabled;

		public static ConfigEntry<int> nexusID;

		public static ConfigEntry<bool> enableOtherResources;

		public static ConfigEntry<bool> resourcesSpawnEmpty;

		public static ConfigEntry<bool> requireCultivation;

		public static ConfigEntry<bool> placeAnywhere;

		public static ConfigEntry<bool> enforceBiomes;

		public static ConfigEntry<bool> alternateIcons;

		public static ConfigEntry<bool> enableCustomRespawnTimes;

		public static ConfigEntry<bool> enableCustomResourceAmounts;

		public static ConfigEntry<int> raspberryCost;

		public static ConfigEntry<int> blueberryCost;

		public static ConfigEntry<int> cloudberryCost;

		public static ConfigEntry<int> mushroomCost;

		public static ConfigEntry<int> yellowMushroomCost;

		public static ConfigEntry<int> blueMushroomCost;

		public static ConfigEntry<int> thistleCost;

		public static ConfigEntry<int> dandelionCost;

		public static ConfigEntry<int> birchCost;

		public static ConfigEntry<int> oakCost;

		public static ConfigEntry<int> ancientCost;

		public static ConfigEntry<int> raspberryRespawnTime;

		public static ConfigEntry<int> blueberryRespawnTime;

		public static ConfigEntry<int> cloudberryRespawnTime;

		public static ConfigEntry<int> mushroomRespawnTime;

		public static ConfigEntry<int> yellowMushroomRespawnTime;

		public static ConfigEntry<int> blueMushroomRespawnTime;

		public static ConfigEntry<int> thistleRespawnTime;

		public static ConfigEntry<int> dandelionRespawnTime;

		public static ConfigEntry<float> birchGrowthTime;

		public static ConfigEntry<float> oakGrowthTime;

		public static ConfigEntry<float> ancientGrowthTime;

		public static ConfigEntry<float> birchMinScale;

		public static ConfigEntry<float> birchMaxScale;

		public static ConfigEntry<float> oakMinScale;

		public static ConfigEntry<float> oakMaxScale;

		public static ConfigEntry<float> ancientMinScale;

		public static ConfigEntry<float> ancientMaxScale;

		public static ConfigEntry<int> raspberryResourceAmount;

		public static ConfigEntry<int> blueberryResourceAmount;

		public static ConfigEntry<int> cloudberryResourceAmount;

		public static ConfigEntry<int> mushroomResourceAmount;

		public static ConfigEntry<int> yellowMushroomResourceAmount;

		public static ConfigEntry<int> blueMushroomResourceAmount;

		public static ConfigEntry<int> thistleResourceAmount;

		public static ConfigEntry<int> dandelionResourceAmount;

		public void Awake()
		{
			modEnabled = base.Config.Bind("General", "Enabled", defaultValue: true, "Enable this mod");
			nexusID = base.Config.Bind("General", "NexusID", 274, "Nexus mod ID for updates");
			enableOtherResources = base.Config.Bind("General", "EnableOtherResources", defaultValue: true, "Enable planting resources other than berries");
			resourcesSpawnEmpty = base.Config.Bind("General", "ResourcesSpawnEmpty", defaultValue: false, "Pickable resources will spawn empty rather than full");
			requireCultivation = base.Config.Bind("General", "RequireCultivation", defaultValue: false, "Pickable resources can only be planted on cultivated ground");
			placeAnywhere = base.Config.Bind("General", "PlaceAnywhere", defaultValue: false, "Allow resources to be placed anywhere. This will only apply to bushes and trees");
			enforceBiomes = base.Config.Bind("General", "EnforceBiomes", defaultValue: false, "Only allow resources to be placed in their respective biome");
			alternateIcons = base.Config.Bind("General", "AlternateIcons", defaultValue: false, "User berry icons in the cultivator menu rather than the default ones");
			enableCustomRespawnTimes = base.Config.Bind("General", "EnableCustomRespawnTimes", defaultValue: true, "Enable custom respawn times for pickable resources");
			enableCustomResourceAmounts = base.Config.Bind("General", "EnableCustomResourceAmounts", defaultValue: true, "Enable custom resource amounts for pickable resources");
			raspberryCost = base.Config.Bind("General", "RaspberryCost", 10, "Number of raspberries required to place a raspberry bush");
			blueberryCost = base.Config.Bind("General", "BlueberryCost", 10, "Number of blueberries required to place a blueberry bush");
			cloudberryCost = base.Config.Bind("General", "CloudberryCost", 10, "Number of cloudberries required to place a cloudberry bush");
			mushroomCost = base.Config.Bind("General", "MushroomCost", 5, "Number of mushrooms required to place a pickable mushroom spawner");
			yellowMushroomCost = base.Config.Bind("General", "YellowMushroomCost", 5, "Number of yellow mushrooms required to place a pickable yellow mushroom spawner");
			blueMushroomCost = base.Config.Bind("General", "BlueMushroomCost", 5, "Number of blue mushrooms required to place a pickable blue mushroom spawner");
			thistleCost = base.Config.Bind("General", "ThistleCost", 5, "Number of thistle required to place a pickable thistle spawner");
			dandelionCost = base.Config.Bind("General", "DandelionCost", 5, "Number of dandelion required to place a pickable dandelion spawner");
			birchCost = base.Config.Bind("General", "BirchCost", 1, "Number of birch cones required to place a birch sapling");
			oakCost = base.Config.Bind("General", "OakCost", 1, "Number of oak seeds required to place an oak sapling");
			ancientCost = base.Config.Bind("General", "AncientCost", 1, "Number of ancient seeds required to place an ancient sapling");
			raspberryRespawnTime = base.Config.Bind("General", "RaspberryRespawnTime", 300, "Number of minutes it takes for a raspberry bush to respawn berries");
			blueberryRespawnTime = base.Config.Bind("General", "BlueberryRespawnTime", 300, "Number of minutes it takes for a blueberry bush to respawn berries");
			cloudberryRespawnTime = base.Config.Bind("General", "CloudberryRespawnTime", 300, "Number of minutes it takes for a cloudberry bush to respawn berries");
			mushroomRespawnTime = base.Config.Bind("General", "MushroomRespawnTime", 240, "Number of minutes it takes for mushrooms to respawn");
			yellowMushroomRespawnTime = base.Config.Bind("General", "YellowMushroomRespawnTime", 240, "Number of minutes it takes for yellow mushrooms to respawn");
			blueMushroomRespawnTime = base.Config.Bind("General", "BlueMushroomRespawnTime", 240, "Number of minutes it takes for blue mushrooms to respawn");
			thistleRespawnTime = base.Config.Bind("General", "ThistleRespawnTime", 240, "Number of minutes it takes for thistle to respawn");
			dandelionRespawnTime = base.Config.Bind("General", "DandelionRespawnTime", 240, "Number of minutes it takes for dandelion to respawn");
			birchGrowthTime = base.Config.Bind("General", "BirchGrowthTime", 3000f, "Number of seconds it takes for a birch tree to grow from a birch sapling (will take at least 10 seconds after planting to grow)");
			oakGrowthTime = base.Config.Bind("General", "OakGrowthTime", 3000f, "Number of seconds it takes for an oak tree to grow from an oak sapling (will take at least 10 seconds after planting to grow)");
			ancientGrowthTime = base.Config.Bind("General", "AncientGrowthTime", 3000f, "Number of seconds it takes for an ancient tree to grow from an ancient sapling (will take at least 10 seconds after planting to grow)");
			raspberryResourceAmount = base.Config.Bind("General", "RaspberryResourceAmount", 1, "Number of berries a raspberry bush will spawn");
			blueberryResourceAmount = base.Config.Bind("General", "BlueberryResourceAmount", 1, "Number of berries a blueberry bush will spawn");
			cloudberryResourceAmount = base.Config.Bind("General", "CloudberryResourceAmount", 1, "Number of berries a cloudberry bush will spawn");
			mushroomResourceAmount = base.Config.Bind("General", "MushroomResourceAmount", 1, "Number of mushrooms a pickable mushroom spawner will spawn");
			yellowMushroomResourceAmount = base.Config.Bind("General", "YellowMushroomResourceAmount", 1, "Number of yellow mushrooms a pickable yellow mushroom spawner will spawn");
			blueMushroomResourceAmount = base.Config.Bind("General", "BlueMushroomResourceAmount", 1, "Number of blue mushrooms a pickable blue mushroom spawner will spawn");
			thistleResourceAmount = base.Config.Bind("General", "ThistleResourceAmount", 1, "Number of thistle a pickable thistle spawner will spawn");
			dandelionResourceAmount = base.Config.Bind("General", "DandelionResourceAmount", 1, "Number of dandelion a pickable dandelion spawner will spawn");
			birchMinScale = base.Config.Bind("General", "BirchMinScale", 0.5f, "The minimum scaling factor used to scale a birch tree upon growth");
			birchMaxScale = base.Config.Bind("General", "BirchMaxScale", 2f, "The maximum scaling factor used to scale a birch tree upon growth");
			oakMinScale = base.Config.Bind("General", "OakMinScale", 0.5f, "The minimum scaling factor used to scale an oak tree upon growth");
			oakMaxScale = base.Config.Bind("General", "OakMaxScale", 2f, "The maximum scaling factor used to scale an oak tree upon growth");
			ancientMinScale = base.Config.Bind("General", "AncientMinScale", 0.5f, "The minimum scaling factor used to scale an ancient tree upon growth");
			ancientMaxScale = base.Config.Bind("General", "AncientMaxScale", 2f, "The maximum scaling factor used to scale an ancient tree upon growth");
			Object.DontDestroyOnLoad(this);
			if (!modEnabled.Value)
			{
				base.enabled = false;
			}
			else
			{
				new Harmony("com.bkeyes93.PlantingPlus").PatchAll();
			}
		}

		public void Update()
		{
			Object[] array = UnityEngine.Resources.FindObjectsOfTypeAll(typeof(GameObject));
			for (int i = 0; i < array.Length; i++)
			{
				GameObject gameObject = (GameObject)array[i];
				if (gameObject.name == "RaspberryBush")
				{
					raspberryBushObject = gameObject;
				}
				if (gameObject.name == "BlueberryBush")
				{
					blueberryBushObject = gameObject;
				}
				if (gameObject.name == "CloudberryBush")
				{
					cloudberryBushObject = gameObject;
				}
				if (enableOtherResources.Value)
				{
					if (gameObject.name == "Pickable_Mushroom")
					{
						pickableMushroomObject = gameObject;
					}
					if (gameObject.name == "Pickable_Mushroom_yellow")
					{
						pickableYellowMushroomObject = gameObject;
					}
					if (gameObject.name == "Pickable_Mushroom_blue")
					{
						pickableBlueMushroomObject = gameObject;
					}
					if (gameObject.name == "Pickable_Thistle")
					{
						pickableThistleObject = gameObject;
					}
					if (gameObject.name == "Pickable_Dandelion")
					{
						pickableDandelionObject = gameObject;
					}
					if (gameObject.name == "Oak1")
					{
						oakTree1Object = gameObject;
					}
					if (gameObject.name == "Birch1")
					{
						birchTree1Object = gameObject;
					}
					if (gameObject.name == "Birch2")
					{
						birchTree2Object = gameObject;
					}
					if (gameObject.name == "SwampTree1")
					{
						swampTree1Object = gameObject;
					}
					if (gameObject.name == "PineTree_Sapling")
					{
						pineTreeSaplingObject = gameObject;
					}
					if (gameObject.name == "Beech_Sapling")
					{
						beechTreeSaplingObject = gameObject;
					}
				}
				if (raspberryBushObject != null && blueberryBushObject != null && cloudberryBushObject != null && ((pickableMushroomObject != null && pickableYellowMushroomObject != null && pickableBlueMushroomObject != null && pickableThistleObject != null && pickableDandelionObject != null && oakTree1Object != null && birchTree1Object != null && birchTree2Object != null && swampTree1Object != null && pineTreeSaplingObject != null && beechTreeSaplingObject != null) || !enableOtherResources.Value))
				{
					break;
				}
			}
			if (raspberryBushObject == null || blueberryBushObject == null || cloudberryBushObject == null || (enableOtherResources.Value && (pickableMushroomObject == null || pickableYellowMushroomObject == null || pickableBlueMushroomObject == null || pickableThistleObject == null || pickableDandelionObject == null || oakTree1Object == null || birchTree1Object == null || birchTree2Object == null || swampTree1Object == null || pineTreeSaplingObject == null || beechTreeSaplingObject == null)))
			{
				return;
			}
			if (enableOtherResources.Value)
			{
				if (birchTreeSaplingObject == null)
				{
					isCloningPrefab = true;
					birchTreeSaplingObject = Object.Instantiate(pineTreeSaplingObject);
					birchTreeSaplingObject.name = "Birch_Sapling";
					Object.DontDestroyOnLoad(birchTreeSaplingObject);
					birchTreeSaplingObject.hideFlags = HideFlags.HideInHierarchy;
					isCloningPrefab = false;
				}
				if (oakTreeSaplingObject == null)
				{
					isCloningPrefab = true;
					oakTreeSaplingObject = Object.Instantiate(beechTreeSaplingObject);
					oakTreeSaplingObject.name = "Oak_Sapling";
					Object.DontDestroyOnLoad(oakTreeSaplingObject);
					oakTreeSaplingObject.hideFlags = HideFlags.HideInHierarchy;
					isCloningPrefab = false;
				}
				if (swampTreeSaplingObject == null)
				{
					isCloningPrefab = true;
					swampTreeSaplingObject = Object.Instantiate(pineTreeSaplingObject);
					swampTreeSaplingObject.name = "Ancient_Sapling";
					Object.DontDestroyOnLoad(swampTreeSaplingObject);
					swampTreeSaplingObject.hideFlags = HideFlags.HideInHierarchy;
					isCloningPrefab = false;
				}
			}
			if (enableCustomRespawnTimes.Value)
			{
				raspberryBushObject.GetComponent<Pickable>().m_respawnTimeMinutes = raspberryRespawnTime.Value;
				blueberryBushObject.GetComponent<Pickable>().m_respawnTimeMinutes = blueberryRespawnTime.Value;
				cloudberryBushObject.GetComponent<Pickable>().m_respawnTimeMinutes = cloudberryRespawnTime.Value;
				if (enableOtherResources.Value)
				{
					pickableMushroomObject.GetComponent<Pickable>().m_respawnTimeMinutes = mushroomRespawnTime.Value;
					pickableYellowMushroomObject.GetComponent<Pickable>().m_respawnTimeMinutes = yellowMushroomRespawnTime.Value;
					pickableBlueMushroomObject.GetComponent<Pickable>().m_respawnTimeMinutes = blueMushroomRespawnTime.Value;
					pickableThistleObject.GetComponent<Pickable>().m_respawnTimeMinutes = thistleRespawnTime.Value;
					pickableDandelionObject.GetComponent<Pickable>().m_respawnTimeMinutes = dandelionRespawnTime.Value;
				}
			}
			if (enableCustomResourceAmounts.Value)
			{
				raspberryBushObject.GetComponent<Pickable>().m_amount = raspberryResourceAmount.Value;
				blueberryBushObject.GetComponent<Pickable>().m_amount = blueberryResourceAmount.Value;
				cloudberryBushObject.GetComponent<Pickable>().m_amount = cloudberryResourceAmount.Value;
				if (enableOtherResources.Value)
				{
					pickableMushroomObject.GetComponent<Pickable>().m_amount = mushroomResourceAmount.Value;
					pickableYellowMushroomObject.GetComponent<Pickable>().m_amount = yellowMushroomResourceAmount.Value;
					pickableBlueMushroomObject.GetComponent<Pickable>().m_amount = blueMushroomResourceAmount.Value;
					pickableThistleObject.GetComponent<Pickable>().m_amount = thistleResourceAmount.Value;
					pickableDandelionObject.GetComponent<Pickable>().m_amount = dandelionResourceAmount.Value;
				}
			}
			if (ObjectDB.instance == null)
			{
				return;
			}
			List<GameObject>.Enumerator enumerator = ObjectDB.instance.m_items.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetComponent<ItemDrop>().name == "Raspberry")
				{
					raspberryItem = enumerator.Current.GetComponent<ItemDrop>();
				}
				if (enumerator.Current.GetComponent<ItemDrop>().name == "Blueberries")
				{
					blueberriesItem = enumerator.Current.GetComponent<ItemDrop>();
				}
				if (enumerator.Current.GetComponent<ItemDrop>().name == "Cloudberry")
				{
					cloudberryItem = enumerator.Current.GetComponent<ItemDrop>();
				}
				if (enumerator.Current.GetComponent<ItemDrop>().name == "Cultivator")
				{
					cultivatorItem = enumerator.Current.GetComponent<ItemDrop>();
				}
				if (enableOtherResources.Value)
				{
					if (enumerator.Current.GetComponent<ItemDrop>().name == "Mushroom")
					{
						mushroomItem = enumerator.Current.GetComponent<ItemDrop>();
					}
					if (enumerator.Current.GetComponent<ItemDrop>().name == "MushroomYellow")
					{
						yellowMushroomItem = enumerator.Current.GetComponent<ItemDrop>();
					}
					if (enumerator.Current.GetComponent<ItemDrop>().name == "MushroomBlue")
					{
						blueMushroomItem = enumerator.Current.GetComponent<ItemDrop>();
					}
					if (enumerator.Current.GetComponent<ItemDrop>().name == "Thistle")
					{
						thistleItem = enumerator.Current.GetComponent<ItemDrop>();
					}
					if (enumerator.Current.GetComponent<ItemDrop>().name == "Dandelion")
					{
						dandelionItem = enumerator.Current.GetComponent<ItemDrop>();
					}
				}
				if (raspberryItem != null && blueberriesItem != null && cloudberryItem != null && cultivatorItem != null && ((mushroomItem != null && yellowMushroomItem != null && blueMushroomItem != null && thistleItem != null && dandelionItem != null) || !enableOtherResources.Value))
				{
					break;
				}
			}
			if (raspberryItem == null || blueberriesItem == null || cloudberryItem == null || cultivatorItem == null || (enableOtherResources.Value && (mushroomItem == null || yellowMushroomItem == null || blueMushroomItem == null || thistleItem == null || dandelionItem == null)) || (enableOtherResources.Value && (birchTreeSaplingObject == null || oakTreeSaplingObject == null || swampTreeSaplingObject == null || birchConeObject == null || oakSeedsObject == null || ancientSeedsObject == null)))
			{
				return;
			}
			raspberryBushPiece = raspberryBushObject.AddComponent<Piece>();
			blueberryBushPiece = blueberryBushObject.AddComponent<Piece>();
			cloudberryBushPiece = cloudberryBushObject.AddComponent<Piece>();
			raspberryBushPiece.m_name = "Raspberry Bush";
			raspberryBushPiece.m_description = "Plant raspberries to grow raspberry bushes";
			raspberryBushPiece.m_category = Piece.PieceCategory.Misc;
			raspberryBushPiece.m_cultivatedGroundOnly = requireCultivation.Value;
			raspberryBushPiece.m_groundOnly = !placeAnywhere.Value;
			raspberryBushPiece.m_groundPiece = !placeAnywhere.Value;
			blueberryBushPiece.m_name = "Blueberry Bush";
			blueberryBushPiece.m_description = "Plant blueberries to grow blueberry bushes";
			blueberryBushPiece.m_category = Piece.PieceCategory.Misc;
			blueberryBushPiece.m_cultivatedGroundOnly = requireCultivation.Value;
			blueberryBushPiece.m_groundOnly = !placeAnywhere.Value;
			blueberryBushPiece.m_groundPiece = !placeAnywhere.Value;
			cloudberryBushPiece.m_name = "Cloudberry Bush";
			cloudberryBushPiece.m_description = "Plant cloudberries to grow cloudberry bushes";
			cloudberryBushPiece.m_category = Piece.PieceCategory.Misc;
			cloudberryBushPiece.m_cultivatedGroundOnly = requireCultivation.Value;
			cloudberryBushPiece.m_groundOnly = !placeAnywhere.Value;
			cloudberryBushPiece.m_groundPiece = !placeAnywhere.Value;
			if (enableOtherResources.Value)
			{
				pickableMushroomPiece = pickableMushroomObject.AddComponent<Piece>();
				pickableYellowMushroomPiece = pickableYellowMushroomObject.AddComponent<Piece>();
				pickableBlueMushroomPiece = pickableBlueMushroomObject.AddComponent<Piece>();
				pickableMushroomPiece.m_name = "Pickable Mushrooms";
				pickableMushroomPiece.m_description = "Plant mushrooms to grow more pickable mushrooms";
				pickableMushroomPiece.m_category = Piece.PieceCategory.Misc;
				pickableMushroomPiece.m_cultivatedGroundOnly = requireCultivation.Value;
				pickableMushroomPiece.m_groundOnly = true;
				pickableMushroomPiece.m_groundPiece = true;
				pickableYellowMushroomPiece.m_name = "Pickable Yellow Mushrooms";
				pickableYellowMushroomPiece.m_description = "Plant yellow mushrooms to grow more pickable yellow mushrooms";
				pickableYellowMushroomPiece.m_category = Piece.PieceCategory.Misc;
				pickableYellowMushroomPiece.m_cultivatedGroundOnly = requireCultivation.Value;
				pickableYellowMushroomPiece.m_groundOnly = true;
				pickableYellowMushroomPiece.m_groundPiece = true;
				pickableBlueMushroomPiece.m_name = "Pickable Blue Mushrooms";
				pickableBlueMushroomPiece.m_description = "Plant blue mushrooms to grow more pickable blue mushrooms";
				pickableBlueMushroomPiece.m_category = Piece.PieceCategory.Misc;
				pickableBlueMushroomPiece.m_cultivatedGroundOnly = requireCultivation.Value;
				pickableBlueMushroomPiece.m_groundOnly = true;
				pickableBlueMushroomPiece.m_groundPiece = true;
				pickableThistlePiece = pickableThistleObject.AddComponent<Piece>();
				pickableThistlePiece.m_name = "Pickable Thistle";
				pickableThistlePiece.m_description = "Plant thistle to grow more pickable thistle";
				pickableThistlePiece.m_category = Piece.PieceCategory.Misc;
				pickableThistlePiece.m_cultivatedGroundOnly = requireCultivation.Value;
				pickableThistlePiece.m_groundOnly = true;
				pickableThistlePiece.m_groundPiece = true;
				pickableDandelionPiece = pickableDandelionObject.AddComponent<Piece>();
				pickableDandelionPiece.m_name = "Pickable Dandelion";
				pickableDandelionPiece.m_description = "Plant dandelion to grow more pickable dandelion";
				pickableDandelionPiece.m_category = Piece.PieceCategory.Misc;
				pickableDandelionPiece.m_cultivatedGroundOnly = requireCultivation.Value;
				pickableDandelionPiece.m_groundOnly = true;
				pickableDandelionPiece.m_groundPiece = true;
				birchTreeSaplingObject.GetComponent<Piece>().m_name = "Birch Sapling";
				oakTreeSaplingObject.GetComponent<Piece>().m_name = "Oak Sapling";
				swampTreeSaplingObject.GetComponent<Piece>().m_name = "Ancient Sapling";
				birchTreeSaplingObject.GetComponent<Piece>().m_groundOnly = !placeAnywhere.Value;
				birchTreeSaplingObject.GetComponent<Piece>().m_groundPiece = !placeAnywhere.Value;
				birchTreeSaplingObject.GetComponent<Piece>().m_cultivatedGroundOnly = requireCultivation.Value;
				oakTreeSaplingObject.GetComponent<Piece>().m_groundOnly = !placeAnywhere.Value;
				oakTreeSaplingObject.GetComponent<Piece>().m_groundPiece = !placeAnywhere.Value;
				oakTreeSaplingObject.GetComponent<Piece>().m_cultivatedGroundOnly = requireCultivation.Value;
				swampTreeSaplingObject.GetComponent<Piece>().m_groundOnly = !placeAnywhere.Value;
				swampTreeSaplingObject.GetComponent<Piece>().m_groundPiece = !placeAnywhere.Value;
				swampTreeSaplingObject.GetComponent<Piece>().m_cultivatedGroundOnly = requireCultivation.Value;
				birchTreeSaplingObject.GetComponent<Plant>().m_name = "Birch Sapling";
				oakTreeSaplingObject.GetComponent<Plant>().m_name = "Oak Sapling";
				swampTreeSaplingObject.GetComponent<Plant>().m_name = "Ancient Sapling";
				birchTreeSaplingObject.GetComponent<Plant>().m_grownPrefabs = new GameObject[2]
				{
					birchTree1Object,
					birchTree2Object
				};
				oakTreeSaplingObject.GetComponent<Plant>().m_grownPrefabs = new GameObject[1]
				{
					oakTree1Object
				};
				swampTreeSaplingObject.GetComponent<Plant>().m_grownPrefabs = new GameObject[1]
				{
					swampTree1Object
				};
				birchTreeSaplingObject.GetComponent<Plant>().m_growTime = birchGrowthTime.Value;
				birchTreeSaplingObject.GetComponent<Plant>().m_growTimeMax = birchGrowthTime.Value;
				oakTreeSaplingObject.GetComponent<Plant>().m_growTime = oakGrowthTime.Value;
				oakTreeSaplingObject.GetComponent<Plant>().m_growTimeMax = oakGrowthTime.Value;
				swampTreeSaplingObject.GetComponent<Plant>().m_growTime = ancientGrowthTime.Value;
				swampTreeSaplingObject.GetComponent<Plant>().m_growTimeMax = ancientGrowthTime.Value;
				birchTreeSaplingObject.GetComponent<Plant>().m_needCultivatedGround = requireCultivation.Value;
				oakTreeSaplingObject.GetComponent<Plant>().m_needCultivatedGround = requireCultivation.Value;
				swampTreeSaplingObject.GetComponent<Plant>().m_needCultivatedGround = requireCultivation.Value;
				if (placeAnywhere.Value)
				{
					typeof(Plant).GetField("m_roofMask", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, 4);
					typeof(Plant).GetField("m_spaceMask", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, 4);
				}
				birchTreeSaplingObject.GetComponent<Plant>().m_minScale = birchMinScale.Value;
				birchTreeSaplingObject.GetComponent<Plant>().m_maxScale = birchMaxScale.Value;
				oakTreeSaplingObject.GetComponent<Plant>().m_minScale = oakMinScale.Value;
				oakTreeSaplingObject.GetComponent<Plant>().m_maxScale = oakMaxScale.Value;
				swampTreeSaplingObject.GetComponent<Plant>().m_minScale = ancientMinScale.Value;
				swampTreeSaplingObject.GetComponent<Plant>().m_maxScale = ancientMaxScale.Value;
			}
			if (enforceBiomes.Value)
			{
				raspberryBushPiece.m_onlyInBiome = Heightmap.Biome.Meadows;
				blueberryBushPiece.m_onlyInBiome = Heightmap.Biome.BlackForest;
				cloudberryBushPiece.m_onlyInBiome = Heightmap.Biome.Plains;
				if (enableOtherResources.Value)
				{
					pickableMushroomPiece.m_onlyInBiome = (Heightmap.Biome)9;
					pickableYellowMushroomPiece.m_onlyInBiome = (Heightmap.Biome)10;
					pickableBlueMushroomPiece.m_onlyInBiome = (Heightmap.Biome)10;
					pickableThistlePiece.m_onlyInBiome = (Heightmap.Biome)10;
					pickableDandelionPiece.m_onlyInBiome = Heightmap.Biome.Meadows;
					birchTreeSaplingObject.GetComponent<Piece>().m_onlyInBiome = (Heightmap.Biome)17;
					oakTreeSaplingObject.GetComponent<Piece>().m_onlyInBiome = (Heightmap.Biome)17;
					swampTreeSaplingObject.GetComponent<Piece>().m_onlyInBiome = Heightmap.Biome.Swamp;
					birchTreeSaplingObject.GetComponent<Plant>().m_biome = (Heightmap.Biome)17;
					oakTreeSaplingObject.GetComponent<Plant>().m_biome = (Heightmap.Biome)17;
					swampTreeSaplingObject.GetComponent<Plant>().m_biome = Heightmap.Biome.Swamp;
				}
			}
			else if (enableOtherResources.Value)
			{
				birchTreeSaplingObject.GetComponent<Piece>().m_onlyInBiome = Heightmap.Biome.None;
				oakTreeSaplingObject.GetComponent<Piece>().m_onlyInBiome = Heightmap.Biome.None;
				swampTreeSaplingObject.GetComponent<Piece>().m_onlyInBiome = Heightmap.Biome.None;
				birchTreeSaplingObject.GetComponent<Plant>().m_biome = (Heightmap.Biome)895;
				oakTreeSaplingObject.GetComponent<Plant>().m_biome = (Heightmap.Biome)895;
				swampTreeSaplingObject.GetComponent<Plant>().m_biome = (Heightmap.Biome)895;
			}
			raspberryBushPiece.m_resources = new Piece.Requirement[1];
			raspberryBushPiece.m_resources[0] = new Piece.Requirement();
			raspberryBushPiece.m_resources[0].m_resItem = raspberryItem;
			raspberryBushPiece.m_resources[0].m_amount = raspberryCost.Value;
			raspberryBushPiece.m_resources[0].m_recover = false;
			blueberryBushPiece.m_resources = new Piece.Requirement[1];
			blueberryBushPiece.m_resources[0] = new Piece.Requirement();
			blueberryBushPiece.m_resources[0].m_resItem = blueberriesItem;
			blueberryBushPiece.m_resources[0].m_amount = blueberryCost.Value;
			blueberryBushPiece.m_resources[0].m_recover = false;
			cloudberryBushPiece.m_resources = new Piece.Requirement[1];
			cloudberryBushPiece.m_resources[0] = new Piece.Requirement();
			cloudberryBushPiece.m_resources[0].m_resItem = cloudberryItem;
			cloudberryBushPiece.m_resources[0].m_amount = cloudberryCost.Value;
			cloudberryBushPiece.m_resources[0].m_recover = false;
			if (enableOtherResources.Value)
			{
				pickableMushroomPiece.m_resources = new Piece.Requirement[1];
				pickableMushroomPiece.m_resources[0] = new Piece.Requirement();
				pickableMushroomPiece.m_resources[0].m_resItem = mushroomItem;
				pickableMushroomPiece.m_resources[0].m_amount = mushroomCost.Value;
				pickableMushroomPiece.m_resources[0].m_recover = false;
				pickableYellowMushroomPiece.m_resources = new Piece.Requirement[1];
				pickableYellowMushroomPiece.m_resources[0] = new Piece.Requirement();
				pickableYellowMushroomPiece.m_resources[0].m_resItem = yellowMushroomItem;
				pickableYellowMushroomPiece.m_resources[0].m_amount = yellowMushroomCost.Value;
				pickableYellowMushroomPiece.m_resources[0].m_recover = false;
				pickableBlueMushroomPiece.m_resources = new Piece.Requirement[1];
				pickableBlueMushroomPiece.m_resources[0] = new Piece.Requirement();
				pickableBlueMushroomPiece.m_resources[0].m_resItem = blueMushroomItem;
				pickableBlueMushroomPiece.m_resources[0].m_amount = blueMushroomCost.Value;
				pickableBlueMushroomPiece.m_resources[0].m_recover = false;
				pickableThistlePiece.m_resources = new Piece.Requirement[1];
				pickableThistlePiece.m_resources[0] = new Piece.Requirement();
				pickableThistlePiece.m_resources[0].m_resItem = thistleItem;
				pickableThistlePiece.m_resources[0].m_amount = thistleCost.Value;
				pickableThistlePiece.m_resources[0].m_recover = false;
				pickableDandelionPiece.m_resources = new Piece.Requirement[1];
				pickableDandelionPiece.m_resources[0] = new Piece.Requirement();
				pickableDandelionPiece.m_resources[0].m_resItem = dandelionItem;
				pickableDandelionPiece.m_resources[0].m_amount = dandelionCost.Value;
				pickableDandelionPiece.m_resources[0].m_recover = false;
				birchTreeSaplingObject.GetComponent<Piece>().m_resources[0] = new Piece.Requirement();
				birchTreeSaplingObject.GetComponent<Piece>().m_resources[0].m_resItem = birchConeObject.GetComponent<ItemDrop>();
				birchTreeSaplingObject.GetComponent<Piece>().m_resources[0].m_amount = birchCost.Value;
				oakTreeSaplingObject.GetComponent<Piece>().m_resources[0] = new Piece.Requirement();
				oakTreeSaplingObject.GetComponent<Piece>().m_resources[0].m_resItem = oakSeedsObject.GetComponent<ItemDrop>();
				oakTreeSaplingObject.GetComponent<Piece>().m_resources[0].m_amount = oakCost.Value;
				swampTreeSaplingObject.GetComponent<Piece>().m_resources[0] = new Piece.Requirement();
				swampTreeSaplingObject.GetComponent<Piece>().m_resources[0].m_resItem = ancientSeedsObject.GetComponent<ItemDrop>();
				swampTreeSaplingObject.GetComponent<Piece>().m_resources[0].m_amount = ancientCost.Value;
				DropTable.DropData item = default(DropTable.DropData);
				item.m_item = birchConeObject;
				item.m_stackMin = 1;
				item.m_stackMax = 5;
				birchTree1Object.GetComponent<TreeBase>().m_dropWhenDestroyed.m_drops.Add(item);
				birchTree2Object.GetComponent<TreeBase>().m_dropWhenDestroyed.m_drops.Add(item);
				DropTable.DropData item2 = default(DropTable.DropData);
				item2.m_item = oakSeedsObject;
				item2.m_stackMin = 1;
				item2.m_stackMax = 5;
				oakTree1Object.GetComponent<TreeBase>().m_dropWhenDestroyed.m_drops.Add(item2);
				DropTable.DropData item3 = default(DropTable.DropData);
				item3.m_item = ancientSeedsObject;
				item3.m_stackMin = 1;
				item3.m_stackMax = 5;
				swampTree1Object.GetComponent<TreeBase>().m_dropWhenDestroyed.m_drops.Add(item3);
				birchTree1Object.GetComponent<TreeBase>().m_dropWhenDestroyed.m_dropChance = 1f;
				birchTree1Object.GetComponent<TreeBase>().m_dropWhenDestroyed.m_oneOfEach = true;
				birchTree1Object.GetComponent<TreeBase>().m_dropWhenDestroyed.m_dropMin = 1;
				birchTree1Object.GetComponent<TreeBase>().m_dropWhenDestroyed.m_dropMax = 3;
				birchTree2Object.GetComponent<TreeBase>().m_dropWhenDestroyed.m_dropChance = 1f;
				birchTree2Object.GetComponent<TreeBase>().m_dropWhenDestroyed.m_oneOfEach = true;
				birchTree2Object.GetComponent<TreeBase>().m_dropWhenDestroyed.m_dropMin = 1;
				birchTree2Object.GetComponent<TreeBase>().m_dropWhenDestroyed.m_dropMax = 3;
				oakTree1Object.GetComponent<TreeBase>().m_dropWhenDestroyed.m_dropChance = 1f;
				oakTree1Object.GetComponent<TreeBase>().m_dropWhenDestroyed.m_oneOfEach = true;
				oakTree1Object.GetComponent<TreeBase>().m_dropWhenDestroyed.m_dropMin = 1;
				oakTree1Object.GetComponent<TreeBase>().m_dropWhenDestroyed.m_dropMax = 3;
				swampTree1Object.GetComponent<TreeBase>().m_dropWhenDestroyed.m_dropChance = 1f;
				swampTree1Object.GetComponent<TreeBase>().m_dropWhenDestroyed.m_oneOfEach = true;
				swampTree1Object.GetComponent<TreeBase>().m_dropWhenDestroyed.m_dropMin = 1;
				swampTree1Object.GetComponent<TreeBase>().m_dropWhenDestroyed.m_dropMax = 3;
			}
			array = UnityEngine.Resources.FindObjectsOfTypeAll(typeof(GameObject));
			for (int i = 0; i < array.Length; i++)
			{
				GameObject gameObject2 = (GameObject)array[i];
				if (gameObject2.name == "vfx_Place_wood_pole")
				{
					placeWoodPoleVfx = gameObject2;
				}
				if (gameObject2.name == "sfx_build_cultivator")
				{
					buildCultivatorSfx = gameObject2;
				}
				if (placeWoodPoleVfx != null && buildCultivatorSfx != null)
				{
					break;
				}
			}
			if (!(placeWoodPoleVfx == null) && !(buildCultivatorSfx == null))
			{
				raspberryBushPiece.m_placeEffect.m_effectPrefabs = new EffectList.EffectData[2];
				raspberryBushPiece.m_placeEffect.m_effectPrefabs[0] = new EffectList.EffectData();
				raspberryBushPiece.m_placeEffect.m_effectPrefabs[0].m_prefab = placeWoodPoleVfx;
				raspberryBushPiece.m_placeEffect.m_effectPrefabs[0].m_enabled = true;
				raspberryBushPiece.m_placeEffect.m_effectPrefabs[1] = new EffectList.EffectData();
				raspberryBushPiece.m_placeEffect.m_effectPrefabs[1].m_prefab = buildCultivatorSfx;
				raspberryBushPiece.m_placeEffect.m_effectPrefabs[1].m_enabled = true;
				blueberryBushPiece.m_placeEffect.m_effectPrefabs = new EffectList.EffectData[2];
				blueberryBushPiece.m_placeEffect.m_effectPrefabs[0] = new EffectList.EffectData();
				blueberryBushPiece.m_placeEffect.m_effectPrefabs[0].m_prefab = placeWoodPoleVfx;
				blueberryBushPiece.m_placeEffect.m_effectPrefabs[0].m_enabled = true;
				blueberryBushPiece.m_placeEffect.m_effectPrefabs[1] = new EffectList.EffectData();
				blueberryBushPiece.m_placeEffect.m_effectPrefabs[1].m_prefab = buildCultivatorSfx;
				blueberryBushPiece.m_placeEffect.m_effectPrefabs[1].m_enabled = true;
				cloudberryBushPiece.m_placeEffect.m_effectPrefabs = new EffectList.EffectData[2];
				cloudberryBushPiece.m_placeEffect.m_effectPrefabs[0] = new EffectList.EffectData();
				cloudberryBushPiece.m_placeEffect.m_effectPrefabs[0].m_prefab = placeWoodPoleVfx;
				cloudberryBushPiece.m_placeEffect.m_effectPrefabs[0].m_enabled = true;
				cloudberryBushPiece.m_placeEffect.m_effectPrefabs[1] = new EffectList.EffectData();
				cloudberryBushPiece.m_placeEffect.m_effectPrefabs[1].m_prefab = buildCultivatorSfx;
				cloudberryBushPiece.m_placeEffect.m_effectPrefabs[1].m_enabled = true;
				if (enableOtherResources.Value)
				{
					pickableMushroomPiece.m_placeEffect.m_effectPrefabs = new EffectList.EffectData[2];
					pickableMushroomPiece.m_placeEffect.m_effectPrefabs[0] = new EffectList.EffectData();
					pickableMushroomPiece.m_placeEffect.m_effectPrefabs[0].m_prefab = placeWoodPoleVfx;
					pickableMushroomPiece.m_placeEffect.m_effectPrefabs[0].m_enabled = true;
					pickableMushroomPiece.m_placeEffect.m_effectPrefabs[1] = new EffectList.EffectData();
					pickableMushroomPiece.m_placeEffect.m_effectPrefabs[1].m_prefab = buildCultivatorSfx;
					pickableMushroomPiece.m_placeEffect.m_effectPrefabs[1].m_enabled = true;
					pickableYellowMushroomPiece.m_placeEffect.m_effectPrefabs = new EffectList.EffectData[2];
					pickableYellowMushroomPiece.m_placeEffect.m_effectPrefabs[0] = new EffectList.EffectData();
					pickableYellowMushroomPiece.m_placeEffect.m_effectPrefabs[0].m_prefab = placeWoodPoleVfx;
					pickableYellowMushroomPiece.m_placeEffect.m_effectPrefabs[0].m_enabled = true;
					pickableYellowMushroomPiece.m_placeEffect.m_effectPrefabs[1] = new EffectList.EffectData();
					pickableYellowMushroomPiece.m_placeEffect.m_effectPrefabs[1].m_prefab = buildCultivatorSfx;
					pickableYellowMushroomPiece.m_placeEffect.m_effectPrefabs[1].m_enabled = true;
					pickableBlueMushroomPiece.m_placeEffect.m_effectPrefabs = new EffectList.EffectData[2];
					pickableBlueMushroomPiece.m_placeEffect.m_effectPrefabs[0] = new EffectList.EffectData();
					pickableBlueMushroomPiece.m_placeEffect.m_effectPrefabs[0].m_prefab = placeWoodPoleVfx;
					pickableBlueMushroomPiece.m_placeEffect.m_effectPrefabs[0].m_enabled = true;
					pickableBlueMushroomPiece.m_placeEffect.m_effectPrefabs[1] = new EffectList.EffectData();
					pickableBlueMushroomPiece.m_placeEffect.m_effectPrefabs[1].m_prefab = buildCultivatorSfx;
					pickableBlueMushroomPiece.m_placeEffect.m_effectPrefabs[1].m_enabled = true;
					pickableThistlePiece.m_placeEffect.m_effectPrefabs = new EffectList.EffectData[2];
					pickableThistlePiece.m_placeEffect.m_effectPrefabs[0] = new EffectList.EffectData();
					pickableThistlePiece.m_placeEffect.m_effectPrefabs[0].m_prefab = placeWoodPoleVfx;
					pickableThistlePiece.m_placeEffect.m_effectPrefabs[0].m_enabled = true;
					pickableThistlePiece.m_placeEffect.m_effectPrefabs[1] = new EffectList.EffectData();
					pickableThistlePiece.m_placeEffect.m_effectPrefabs[1].m_prefab = buildCultivatorSfx;
					pickableThistlePiece.m_placeEffect.m_effectPrefabs[1].m_enabled = true;
					pickableDandelionPiece.m_placeEffect.m_effectPrefabs = new EffectList.EffectData[2];
					pickableDandelionPiece.m_placeEffect.m_effectPrefabs[0] = new EffectList.EffectData();
					pickableDandelionPiece.m_placeEffect.m_effectPrefabs[0].m_prefab = placeWoodPoleVfx;
					pickableDandelionPiece.m_placeEffect.m_effectPrefabs[0].m_enabled = true;
					pickableDandelionPiece.m_placeEffect.m_effectPrefabs[1] = new EffectList.EffectData();
					pickableDandelionPiece.m_placeEffect.m_effectPrefabs[1].m_prefab = buildCultivatorSfx;
					pickableDandelionPiece.m_placeEffect.m_effectPrefabs[1].m_enabled = true;
				}
				if (!alternateIcons.Value)
				{
					Texture2D texture2D = new Texture2D(64, 64, TextureFormat.ARGB32, mipChain: false);
					texture2D.LoadImage(global::PlantingPlus.Properties.Resources.raspberryBushPieceIcon);
					texture2D.Apply();
					raspberryBushPiece.m_icon = Sprite.Create(texture2D, new Rect(0f, 0f, 64f, 64f), new Vector2(0f, 0f));
					Texture2D texture2D2 = new Texture2D(64, 64, TextureFormat.ARGB32, mipChain: false);
					texture2D2.LoadImage(global::PlantingPlus.Properties.Resources.blueberryBushPieceIcon);
					texture2D2.Apply();
					blueberryBushPiece.m_icon = Sprite.Create(texture2D2, new Rect(0f, 0f, 64f, 64f), new Vector2(0f, 0f));
					Texture2D texture2D3 = new Texture2D(64, 64, TextureFormat.ARGB32, mipChain: false);
					texture2D3.LoadImage(global::PlantingPlus.Properties.Resources.cloudberryBushPieceIcon);
					texture2D3.Apply();
					cloudberryBushPiece.m_icon = Sprite.Create(texture2D3, new Rect(0f, 0f, 64f, 64f), new Vector2(0f, 0f));
				}
				else
				{
					raspberryBushPiece.m_icon = raspberryItem.m_itemData.GetIcon();
					blueberryBushPiece.m_icon = blueberriesItem.m_itemData.GetIcon();
					cloudberryBushPiece.m_icon = cloudberryItem.m_itemData.GetIcon();
				}
				if (enableOtherResources.Value)
				{
					pickableMushroomPiece.m_icon = mushroomItem.m_itemData.GetIcon();
					pickableYellowMushroomPiece.m_icon = yellowMushroomItem.m_itemData.GetIcon();
					pickableBlueMushroomPiece.m_icon = blueMushroomItem.m_itemData.GetIcon();
					pickableThistlePiece.m_icon = thistleItem.m_itemData.GetIcon();
					pickableDandelionPiece.m_icon = dandelionItem.m_itemData.GetIcon();
				}
				cultivatorItem.m_itemData.m_shared.m_buildPieces.m_pieces.Add(raspberryBushObject);
				cultivatorItem.m_itemData.m_shared.m_buildPieces.m_pieces.Add(blueberryBushObject);
				cultivatorItem.m_itemData.m_shared.m_buildPieces.m_pieces.Add(cloudberryBushObject);
				if (enableOtherResources.Value)
				{
					cultivatorItem.m_itemData.m_shared.m_buildPieces.m_pieces.Add(pickableMushroomObject);
					cultivatorItem.m_itemData.m_shared.m_buildPieces.m_pieces.Add(pickableYellowMushroomObject);
					cultivatorItem.m_itemData.m_shared.m_buildPieces.m_pieces.Add(pickableBlueMushroomObject);
					cultivatorItem.m_itemData.m_shared.m_buildPieces.m_pieces.Add(pickableThistleObject);
					cultivatorItem.m_itemData.m_shared.m_buildPieces.m_pieces.Add(pickableDandelionObject);
					cultivatorItem.m_itemData.m_shared.m_buildPieces.m_pieces.Add(birchTreeSaplingObject);
					cultivatorItem.m_itemData.m_shared.m_buildPieces.m_pieces.Add(oakTreeSaplingObject);
					cultivatorItem.m_itemData.m_shared.m_buildPieces.m_pieces.Add(swampTreeSaplingObject);
				}
				cultivatorItem.m_itemData.m_shared.m_buildPieces.m_canRemovePieces = true;
				Debug.Log("[Planting+ v1.4.5 finished loading]");
				base.enabled = false;
			}
		}
	}
}
