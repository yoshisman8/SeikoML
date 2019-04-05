using System;
using RoR2;

namespace RoR2ModFramework
{
    // Token: 0x02000006 RID: 6
    public class ItemModInfo
    {
        // Token: 0x0600000C RID: 12 RVA: 0x000023B0 File Offset: 0x000005B0
        public ItemDef RegisterModItem()
        {
            return new ItemDef
            {
                tier = this.itemTier,
                pickupModelPath = this.modelPathString,
                pickupIconPath = this.iconPathString,
                nameToken = this.nameTokenString,
                pickupToken = this.pickupTokenString,
                descriptionToken = this.descriptionTokenString,
                addressToken = "",
                unlockableName = this.unlockableNameString
            };
        }

        // Token: 0x0400000C RID: 12
        public string descriptionTokenString;

        // Token: 0x0400000D RID: 13
        public int toReplace = -1;

        // Token: 0x0400000E RID: 14
        public string unlockableNameString = "";

        // Token: 0x0400000F RID: 15
        public string modelPathString;

        // Token: 0x04000010 RID: 16
        public string nameTokenString;

        // Token: 0x04000011 RID: 17
        public string pickupTokenString;

        // Token: 0x04000012 RID: 18
        public ItemTier itemTier;

        // Token: 0x04000013 RID: 19
        public string iconPathString;
    }
}
