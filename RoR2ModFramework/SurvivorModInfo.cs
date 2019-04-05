using System;
using RoR2;
using UnityEngine;

namespace RoR2ModFramework
{
    // Token: 0x02000004 RID: 4
    public class SurvivorModInfo
    {
        // Token: 0x06000009 RID: 9 RVA: 0x00002310 File Offset: 0x00000510
        public SurvivorDef RegisterModSurvivor()
        {
            this.characterObject = UnityEngine.Object.Instantiate<GameObject>(BodyCatalog.FindBodyPrefab(this.bodyPrefabString));
            this.characterObject.SetActive(false);
            UnityEngine.Object.DontDestroyOnLoad(this.characterObject);
            return new SurvivorDef
            {
                bodyPrefab = this.characterObject,
                displayPrefab = Resources.Load<GameObject>(this.portraitPrefabString),
                descriptionToken = this.descriptionTokenString,
                primaryColor = new Color(this.usedColor.r, this.usedColor.g, this.usedColor.b),
                unlockableName = this.unlockableNameString
            };
        }

        // Token: 0x04000005 RID: 5
        public string bodyPrefabString;

        // Token: 0x04000006 RID: 6
        public string portraitPrefabString;

        // Token: 0x04000007 RID: 7
        public string descriptionTokenString;

        // Token: 0x04000008 RID: 8
        public Color usedColor;

        // Token: 0x04000009 RID: 9
        public int toReplace = -1;

        // Token: 0x0400000A RID: 10
        public string unlockableNameString = "";

        // Token: 0x0400000B RID: 11
        public GameObject characterObject;
    }
}
