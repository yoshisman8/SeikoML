using System;
using System.Collections.Generic;
using System.Linq;
using RoR2ModFramework;
using UnityEngine;
using MonoMod;

namespace RoR2
{
	class patch_SurvivorCatalog
	{
		public static SurvivorIndex[] idealSurvivorOrder;
		public static int survivorMaxCount;
		private static extern void orig_cctor();
		[MonoModConstructor]
		private static void cctor()
		{
            orig_cctor();
            idealSurvivorOrder = BaseFramework.BuildIdealOrder(idealSurvivorOrder);
		}


        [MonoModIgnore,MonoModPublic]
        public extern static void RegisterSurvivor(SurvivorIndex survivorIndex, SurvivorDef survivorDef);

		public static SurvivorDef GetSurvivorDef(SurvivorIndex survivorIndex){
			if (survivorIndex < 0 || survivorIndex > (SurvivorIndex)SurvivorCatalog.survivorDefs.Count())
			{
				return null;
			}
			return SurvivorCatalog.survivorDefs[(int)survivorIndex];
		}

		[SystemInitializer(new Type[]
		{
			typeof(BodyCatalog)
		})]
		[MonoModReplace]
		private static void Init(){
			SurvivorCatalog.survivorDefs = new SurvivorDef[BaseFramework.SurvivorCount];
			SurvivorCatalog.RegisterSurvivor(SurvivorIndex.Commando, new SurvivorDef
			{
				bodyPrefab = BodyCatalog.FindBodyPrefab("CommandoBody"),
				displayPrefab = Resources.Load<GameObject>("Prefabs/CharacterDisplays/CommandoDisplay"),
				descriptionToken = "COMMANDO_DESCRIPTION",
				primaryColor = new Color(0.929411769f, 0.5882353f, 0.07058824f)
			});
			SurvivorCatalog.RegisterSurvivor(SurvivorIndex.Huntress, new SurvivorDef
			{
				bodyPrefab = BodyCatalog.FindBodyPrefab("HuntressBody"),
				displayPrefab = Resources.Load<GameObject>("Prefabs/CharacterDisplays/HuntressDisplay"),
				primaryColor = new Color(0.8352941f, 0.235294119f, 0.235294119f),
				descriptionToken = "HUNTRESS_DESCRIPTION",
				unlockableName = "Characters.Huntress"
			});
			SurvivorCatalog.RegisterSurvivor(SurvivorIndex.Toolbot, new SurvivorDef
			{
				bodyPrefab = BodyCatalog.FindBodyPrefab("ToolbotBody"),
				displayPrefab = Resources.Load<GameObject>("Prefabs/CharacterDisplays/ToolbotDisplay"),
				descriptionToken = "TOOLBOT_DESCRIPTION",
				primaryColor = new Color(0.827451f, 0.768627465f, 0.3137255f),
				unlockableName = "Characters.Toolbot"
			});
			SurvivorCatalog.RegisterSurvivor(SurvivorIndex.Engineer, new SurvivorDef
			{
				bodyPrefab = BodyCatalog.FindBodyPrefab("EngiBody"),
				displayPrefab = Resources.Load<GameObject>("Prefabs/CharacterDisplays/EngiDisplay"),
				descriptionToken = "ENGI_DESCRIPTION",
				primaryColor = new Color(0.372549027f, 0.8862745f, 0.5254902f),
				unlockableName = "Characters.Engineer"
			});
			SurvivorCatalog.RegisterSurvivor(SurvivorIndex.Mage, new SurvivorDef
			{
				bodyPrefab = BodyCatalog.FindBodyPrefab("MageBody"),
				displayPrefab = Resources.Load<GameObject>("Prefabs/CharacterDisplays/MageDisplay"),
				descriptionToken = "MAGE_DESCRIPTION",
				primaryColor = new Color(0.968627453f, 0.75686276f, 0.992156863f),
				unlockableName = "Characters.Mage"
			});
			SurvivorCatalog.RegisterSurvivor(SurvivorIndex.Merc, new SurvivorDef
			{
				bodyPrefab = BodyCatalog.FindBodyPrefab("MercBody"),
				displayPrefab = Resources.Load<GameObject>("Prefabs/CharacterDisplays/MercDisplay"),
				descriptionToken = "MERC_DESCRIPTION",
				primaryColor = new Color(0.423529416f, 0.819607854f, 0.917647064f),
				unlockableName = "Characters.Mercenary"
			});
			BaseFramework.addSurvivors();
			for (SurvivorIndex survivorIndex = 0; survivorIndex < (SurvivorIndex)SurvivorCatalog.survivorDefs.Count(); survivorIndex++)
			{
				if (SurvivorCatalog.survivorDefs[(int)survivorIndex] == null)
				{
					Debug.LogWarningFormat("Unregistered survivor {0}!", new object[]
					{
						Enum.GetName(typeof(SurvivorIndex), survivorIndex)
					});
				}
			}
			SurvivorCatalog._allSurvivorDefs = (from v in SurvivorCatalog.survivorDefs
												where v != null
												select v).ToArray<SurvivorDef>();
			ViewablesCatalog.Node node = new ViewablesCatalog.Node("Survivors", true, null);
			using (IEnumerator<SurvivorDef> enumerator = SurvivorCatalog.allSurvivorDefs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
                    SurvivorDef survivor = new SurvivorDef();
                    survivor = enumerator.Current;
                    ViewablesCatalog.Node survivorEntryNode = new ViewablesCatalog.Node(survivor.survivorIndex.ToString(), false, node);
                    survivorEntryNode.shouldShowUnviewed = ((UserProfile userProfile) => !userProfile.HasViewedViewable(survivorEntryNode.fullName) && userProfile.HasSurvivorUnlocked(survivor.survivorIndex) && !string.IsNullOrEmpty(survivor.unlockableName));
                }
			}
			ViewablesCatalog.AddNodeToRoot(node);
		}
	}
}
