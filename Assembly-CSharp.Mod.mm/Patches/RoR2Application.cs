#pragma warning disable CS0626 // Method, operator, or accessor is marked external and has no attributes on it

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Facepunch.Steamworks;
using Rewired;
using RoR2.ConVar;
using RoR2.Networking;
using RoR2ModFramework;
using SteamAPIValidator;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zio.FileSystems;
using MonoMod;

namespace RoR2
{
	class patch_RoR2Application : RoR2Application
	{
		// Redefine the fields so that we can access them without any restrictions.
		public static int hardMaxPlayers;
		public static int maxPlayers;
		public static int maxLocalPlayers;
		public static bool isModded;
		public static string steamBuildId;
		public static string messageForDevelopers;

		// Set our custom values after the game has set its original values.
		private static extern void orig_cctor();
		[MonoModConstructor]
		private static void cctor()
		{
			orig_cctor();

			hardMaxPlayers = 32;
			maxPlayers = 32;
			maxLocalPlayers = 32;
			isModded = true;
			messageForDevelopers = "Don't worry about it at all! Just keep being awesome and doing what you do, we all appreciate and love that you put your time, effort, and money into making such a wonderful game for us all to enjoy. By the way, thank you for adding this. We were looking to do it ourselves until you made it!";
		}

		public static string GetBuildId(){
			if (isModded){
				return steamBuildId + "MOD";
			}
			return steamBuildId;
		}

		private extern void orig_Awake();
		private void Awake()
		{
			if (RoR2Application.maxPlayers != 4)
			{
				RoR2Application.isModded = true;
			}
			this.stopwatch.Start();
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			if (RoR2Application.instance)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			RoR2Application.instance = this;
			if (!this.loaded)
			{
				BaseFramework.Begin();
				this.OnLoad();
				this.loaded = true;
			}
		}
	}
}
