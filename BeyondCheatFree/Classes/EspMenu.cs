using System;
using HighlightingSystem;
using SDG.Unturned;
using UnityEngine;

namespace UnturnedHake
{
	// Token: 0x02000003 RID: 3
	internal class EspMenu : MonoBehaviour
	{
        MainMenu MAINMENU = new MainMenu();
		// Token: 0x06000005 RID: 5 RVA: 0x00002664 File Offset: 0x00000864
		private void OnGUI()
		{
			if (Menu.MenuOpened != 2)
			{
				return;
			}
			GUI.skin = Menu.Skin;
			GUILayout.BeginArea(new Rect((float)(Screen.width / 2 - 100), (float)(Screen.height / 2 - 250), 410f, 450f), "ESP menu", GUI.skin.GetStyle("window"));
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			GUILayout.Label(string.Format("ESP distance: {0}", Math.Round((double)EspMenu.EspDistance)), new GUILayoutOption[0]);
			GUILayout.Space(8f);
			EspMenu.EspDistance = GUILayout.HorizontalSlider(EspMenu.EspDistance, 10f, 1000f, new GUILayoutOption[0]);
			EspMenu.InfDist = GUILayout.Toggle(EspMenu.InfDist, "Infinite ESP distance", new GUILayoutOption[0]);
			GUILayout.Space(8f);
			EspMenu.ShowPlayers = GUILayout.Toggle(EspMenu.ShowPlayers, "Show players distance", new GUILayoutOption[0]);
            EspMenu.PlayerEspBoxes = GUILayout.Toggle(EspMenu.PlayerEspBoxes, "Boxes", new GUILayoutOption[0]);
            EspMenu.ShowZombies = GUILayout.Toggle(EspMenu.ShowZombies, "Show zombies distance", new GUILayoutOption[0]);
            EspMenu.ZombieEspBoxes = GUILayout.Toggle(EspMenu.ZombieEspBoxes, "Boxes", new GUILayoutOption[0]);
            EspMenu.ShowItems = GUILayout.Toggle(EspMenu.ShowItems, "Show items", new GUILayoutOption[0]);
			EspMenu.ShowVehicles = GUILayout.Toggle(EspMenu.ShowVehicles, "Show vehicles", new GUILayoutOption[0]);
			if (GUILayout.Button("Glow players" + Menu.GetToggleText(EspMenu.PlayersGlow), new GUILayoutOption[0]))
			{
				EspMenu.ToggleGlowPlayers();
			}
            if (GUILayout.Button("Glow zombies" + Menu.GetToggleText(EspMenu.ZombieGlow), new GUILayoutOption[0]))
            {
                EspMenu.ToggleGlowZombies();
            }
            if (GUILayout.Button("Disable Ballistics" + Menu.GetToggleText(EspMenu.ZombieGlow), new GUILayoutOption[0]))
            {
                MAINMENU.DisableBallistic();
            }
            GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002810 File Offset: 0x00000A10
		public static void ToggleGlowPlayers()
		{
			EspMenu.PlayersGlow = !EspMenu.PlayersGlow;
			foreach (Player player in FindObjectsOfType<Player>())
			{
				if (!(Player.player.name == player.name))
				{
					Highlighter highlighter = player.gameObject.GetComponent<Highlighter>();
					if (!EspMenu.PlayersGlow)
					{
						highlighter.ConstantOffImmediate();
					}
					else
					{
						if (highlighter == null)
						{
							highlighter = player.gameObject.AddComponent<Highlighter>();
						}
						highlighter.ConstantParams(Color.red);
						highlighter.OccluderOn();
						highlighter.SeeThroughOn();
						highlighter.ConstantOn();
					}
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000028A8 File Offset: 0x00000AA8
		public static void ToggleGlowZombies()
		{
			EspMenu.ZombieGlow = !EspMenu.ZombieGlow;
			foreach (Zombie zombie in FindObjectsOfType<Zombie>())
			{
				Highlighter highlighter = zombie.gameObject.GetComponent<Highlighter>();
				if (!EspMenu.ZombieGlow)
				{
					highlighter.ConstantOffImmediate();
				}
				else
				{
					if (highlighter == null)
					{
						highlighter = zombie.gameObject.AddComponent<Highlighter>();
					}
					highlighter.ConstantParams(Color.blue);
					highlighter.OccluderOn();
					highlighter.SeeThroughOn();
					highlighter.ConstantOn();
				}
			}
		}

		// Token: 0x04000008 RID: 8
		public static bool ShowPlayers;

        public static bool PlayerEspBoxes;

        public static bool ZombieEspBoxes;

        // Token: 0x04000009 RID: 9
        public static bool ShowZombies;

		// Token: 0x0400000A RID: 10
		public static bool ShowItems;

		// Token: 0x0400000B RID: 11
		public static bool ShowVehicles;

		// Token: 0x0400000C RID: 12
		public static bool InfDist;

		// Token: 0x0400000D RID: 13
		public static bool PlayersGlow;

		// Token: 0x0400000E RID: 14
		public static bool ZombieGlow;

		// Token: 0x0400000F RID: 15
		public static bool PlayersGlowAlert;

		// Token: 0x04000010 RID: 16
		public static bool ZombieGlowAlert;

		// Token: 0x04000011 RID: 17
		public static float EspDistance = 600f;
	}
}
