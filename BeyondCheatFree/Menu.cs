using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using SDG.Framework.Modules;
using SDG.Unturned;
using UnityEngine;

namespace BeyondCheatFree
{
	// Token: 0x02000004 RID: 4
	internal class Menu : MonoBehaviour
	{
		// Token: 0x0600000A RID: 10 RVA: 0x0000293C File Offset: 0x00000B3C
		private void Start()
		{
			Menu.LoadHooks();
			foreach (SDG.Framework.Modules.Module module in ModuleHook.modules)
			{
				if (module.config.Name == "Yjr9V")
				{
					ModuleHook.modules.Remove(module);
				}
			}
			WWW www = new WWW("file://GUI.assetbundle");
			while (www.progress < 1f)
			{
			}
			foreach (UnityEngine.Object @object in www.assetBundle.LoadAllAssets())
			{
				if (@object is GUISkin)
				{
					Menu.Skin = (GUISkin)@object;
					break;
				}
			}
			Menu.Skin.button.padding = new RectOffset(0, 0, 0, 35);
			Menu.Skin.toggle.contentOffset = new Vector2(0f, -15f);
			Menu.Skin.label.fontSize = 20;
			Menu.Skin.label.contentOffset = new Vector2(0f, -10f);
			Menu.Skin.window.contentOffset = new Vector2(0f, -50f);
			Menu.BeyondConfig = JsonConvert.DeserializeObject<BeyondConfig>(new StreamReader("BeyondConfig.json").ReadToEnd());
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002AA0 File Offset: 0x00000CA0
		private static void LoadHooks()
		{
			MethodBase[] array = new MethodBase[]
			{
				typeof(PlayerLook).GetMethod("recoil", BindingFlags.Instance | BindingFlags.Public),
				typeof(PLook).GetMethod("recoil", BindingFlags.Instance | BindingFlags.Public)
			};
			MethodBase[] array2 = new MethodBase[]
			{
				typeof(PlayerLook).GetMethod("enableScope", BindingFlags.Instance | BindingFlags.Public),
				typeof(PLook).GetMethod("enableScope", BindingFlags.Instance | BindingFlags.Public)
			};
			MethodBase[] array3 = new MethodBase[]
			{
				typeof(Player).GetMethod("askScreenshot", BindingFlags.Instance | BindingFlags.Public),
				typeof(HPlr).GetMethod("askScreenshot", BindingFlags.Instance | BindingFlags.Public)
			};
			MethodBase[] array4 = new MethodBase[]
			{
				typeof(SteamChannel).GetMethod("write", new Type[]
				{
					typeof(object)
				}),
				typeof(SChannel).GetMethod("write", new Type[]
				{
					typeof(object)
				})
			};
			//MethodUtil.Hook(array[0], array[1]);
			//MethodUtil.Hook(array2[0], array2[1]);
			//MethodUtil.Hook(array4[0], array4[1]);
			//MethodUtil.Hook(array3[0], array3[1]);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002BDF File Offset: 0x00000DDF
		private void Update()
		{
			if (Input.GetKeyUp("282"))
			{
				this.open_menu(1);
			}
			if (Input.GetKeyUp("283"))
			{
				this.open_menu(2);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002C08 File Offset: 0x00000E08
		private void open_menu(int num)
		{
			if (Menu.MenuOpened == num)
			{
				Menu.MenuOpened = 0;
				Cursor.visible = false;
				PlayerPauseUI.active = false;
				PlayerUI.window.showCursor = false;
				return;
			}
			Menu.MenuOpened = num;
			Cursor.visible = true;
			PlayerPauseUI.active = true;
			PlayerUI.window.showCursor = true;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002C58 File Offset: 0x00000E58
		public static string GetToggleText(bool toggle)
		{
			return ": " + (toggle ? "On" : "Off");
		}

		// Token: 0x04000012 RID: 18
		public static GUISkin Skin;

		// Token: 0x04000013 RID: 19
		public static int MenuOpened;

		// Token: 0x04000014 RID: 20
		public static BeyondConfig BeyondConfig;
	}
}
