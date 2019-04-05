using System;
using System.Collections.Generic;
using System.Text;
using MonoMod;

namespace RoR2{
	class ItemCatalog
	{
		private static extern void orig_RegisterItem(ItemIndex itemIndex, ItemDef itemDef);
		[MonoModPublic]
		public static void RegisterItem(ItemIndex itemIndex, ItemDef itemDef)
		{
			orig_RegisterItem(itemIndex, itemDef);
		}
	}
}
