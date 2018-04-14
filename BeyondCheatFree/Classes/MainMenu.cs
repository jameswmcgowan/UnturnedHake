using System;
using System.Reflection;
using System.Windows.Forms;
using SDG.Unturned;
using UnityEngine;



namespace UnturnedHake
{
    
	// Token: 0x02000005 RID: 5
	internal class MainMenu : MonoBehaviour
	{
        bool showing_menu = false;
        Esp ESPATTACK = new Esp();

        AIMBOTATTACK AIMBOTTAB = new AIMBOTATTACK();

        // Token: 0x06000010 RID: 16 RVA: 0x00002C7B File Offset: 0x00000E7B
        private void Start()
		{
			//MainMenu.SpreadAim = typeof(ItemGunAsset).GetField("spreadAim", BindingFlags.Instance | BindingFlags.Public);
			//MainMenu.SpreadHip = typeof(ItemGunAsset).GetField("spreadHip", BindingFlags.Instance | BindingFlags.Public);
		}

        // Token: 0x06000011 RID: 17 RVA: 0x00002CB4 File Offset: 0x00000EB4
        private void OnGUI()
		{
			if (Menu.MenuOpened != 1)
			{
				return;
			}
			GUI.skin = Menu.Skin;
			GUILayout.BeginArea(new Rect((float)(UnityEngine.Screen.width / 2 - 250), (float)(UnityEngine.Screen.height / 2 - 250), 520f, 450f), "Hake", GUI.skin.GetStyle("window"));
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			MainMenu.NoRecoil = GUILayout.Toggle(MainMenu.NoRecoil, "No recoil", new GUILayoutOption[0]);
			MainMenu.NoShake = GUILayout.Toggle(MainMenu.NoShake, "No shake", new GUILayoutOption[0]);
			MainMenu.NoSpread = GUILayout.Toggle(MainMenu.NoSpread, "No spread", new GUILayoutOption[0]);
			this._zoom = GUILayout.Toggle(this._zoom, "Enable FOV", new GUILayoutOption[0]);
			GUILayout.Space(50f);
            MainMenu.Aimbot_Enable = GUILayout.Toggle(MainMenu.Aimbot_Enable, "Enable Aimbot", new GUILayoutOption[0]);
            MainMenu.Aimbot_Players = GUILayout.Toggle(MainMenu.Aimbot_Players, "At Players", new GUILayoutOption[0]);
            MainMenu.Aimbot_Zombies = GUILayout.Toggle(MainMenu.Aimbot_Zombies, "At Zombies", new GUILayoutOption[0]);
            MainMenu.Aimbot_Animals = GUILayout.Toggle(MainMenu.Aimbot_Animals, "At Animals", new GUILayoutOption[0]);

            GUILayout.Label(MainMenu.WebText, new GUILayoutOption[0]);
            GUILayout.Label("Unturned Hake", new GUILayoutOption[0]);
            GUILayout.EndVertical();
            GUILayout.BeginVertical(new GUILayoutOption[0]);

            if (GUILayout.Button("Ballistic Force off", new GUILayoutOption[0]))
            {
                DisableBallistic();
            }
            if (GUILayout.Button("Set day", new GUILayoutOption[0]))
            {
                LightingManager.time = (uint)(LightingManager.cycle * LevelLighting.transition);
            }
            if (GUILayout.Button("Night vision: " + this._nightVision, new GUILayoutOption[0]))
            {
                this._nightVision++;
                if (this._nightVision > 3)
                {
                    this._nightVision = 0;
                }
                LevelLighting.vision = (ELightingVision)this._nightVision;
                LevelLighting.updateLighting();
                LevelLighting.updateLocal();
                PlayerLifeUI.updateGrayscale();
            }
            if (GUILayout.Button("No Respawn Timer", new GUILayoutOption[0]))
            {
                this.DisableTimers();
            }
            if (GUILayout.Button("No Fog" + Menu.GetToggleText(RenderSettings.fog), new GUILayoutOption[0]))
            {
                RenderSettings.fog = !RenderSettings.fog;
            }
            if (GUILayout.Button("No Rain", new GUILayoutOption[0]))
            {
                LevelLighting.rainyness = 0;
            }
            if (GUILayout.Button("Kill All Zombies", new GUILayoutOption[0]))
            {
                foreach (Zombie zombie in FindObjectsOfType<Zombie>())
                {
                    ZombieManager.sendZombieDead(zombie, new Vector3(0, 0, 0));
                }
            }

            GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002F38 File Offset: 0x00001138
		private void DisableTimers()
		{
			FieldInfo field = typeof(PlayerLife).GetField("TIMER_RESPAWN", BindingFlags.Static | BindingFlags.Public);
			if (field != null)
			{
				field.SetValue(null, 0f);
			}
			FieldInfo field2 = typeof(PlayerPauseUI).GetField("TIMER_LEAVE", BindingFlags.Static | BindingFlags.Public);
			if (field2 == null)
			{
				return;
			}
			field2.SetValue(null, 0f);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002F9C File Offset: 0x0000119C
		public void DisableBallistic()
		{
            FieldInfo field = typeof(ItemGunAsset).GetField("ballisticDrop", BindingFlags.Instance | BindingFlags.Public);
            FieldInfo field2 = typeof(ItemBarrelAsset).GetField("ballisticDrop", BindingFlags.Instance | BindingFlags.Public);
            field.SetValue((ItemGunAsset)Player.player.equipment.asset, 0);               
	        field2.SetValue((ItemBarrelAsset)Player.player.equipment.asset, 0);
        }

        // Token: 0x06000014 RID: 20 RVA: 0x00003028 File Offset: 0x00001228
        private void Update()
		{
            AIMING();
            DisableSprintGlitch();
			if (this._zoom)
			{
				if (Input.GetKeyUp(KeyCode.UpArrow))
				{
					this._zoomSize += 0.3f;
					if (this._zoomSize > 70f)
					{
						this._zoomSize = 70f;
					}
					OptionsSettings.fov = this._zoomSize;
				}
				else if (Input.GetKeyUp(KeyCode.DownArrow))
				{
					this._zoomSize -= 0.3f;
					OptionsSettings.fov = this._zoomSize;
				}
			}
			if (MainMenu.NoSpread)
			{
				this.DisableSpread();
			}
			if (MainMenu.NoShake)
			{
				Player.player.animator.viewSway = new Vector3(0f, 0f, 0f);
			}
            if (MainMenu.Aimbot_Enable)
            {
                //AIMBOTATTACK.enabled = true;
                //if (MainMenu.Aimbot_Players)
                //{
                //    AIMBOTATTACK.aim_players = true;
                //}
                //if (MainMenu.Aimbot_Zombies)
                //{
                //    AIMBOTATTACK.aim_zombies = true;
                //}
                //if (MainMenu.Aimbot_Animals)
                //{
                //    AIMBOTATTACK.aim_animals = true;
                //}
                //AIMBOTTAB.Update();
            }
            


        }

        bool resprinting = false;
        private void DisableSprintGlitch()
        {
            LightingManager.time = (uint)(LightingManager.cycle * LevelLighting.transition);

            if (Input.GetKeyDown(KeyCode.Mouse1) && Player.player.stance.stance == EPlayerStance.SPRINT)
            {
                Player.player.stance.stance = EPlayerStance.STAND;
                //resprinting = true;
            }
            
            //if (resprinting && Input.GetKeyUp(KeyCode.Mouse0))
            //{
            //    resprinting = false;
            //    //((UseableGun)Player.player.equipment.useable).isAiming = false;
            //    Player.player.stance.stance = EPlayerStance.SPRINT;
            //}
        }

		// Token: 0x06000015 RID: 21 RVA: 0x000030E0 File Offset: 0x000012E0
		private void DisableSpread()
		{
			if (this._spreadCache <= 0)
			{
				this._spreadCache = 30;
                FieldInfo SpreadAim = typeof(ItemGunAsset).GetField("spreadAim", BindingFlags.Instance | BindingFlags.Public);
                FieldInfo SpreadHip = typeof(ItemGunAsset).GetField("spreadHip", BindingFlags.Instance | BindingFlags.Public);
                SpreadAim.SetValue((ItemGunAsset)Player.player.equipment.asset, 0f);
                SpreadHip.SetValue((ItemGunAsset)Player.player.equipment.asset, 0f);


                FieldInfo recoilxmin = typeof(ItemGunAsset).GetField("recoilMin_x", BindingFlags.Instance | BindingFlags.Public);
                FieldInfo recoilymin = typeof(ItemGunAsset).GetField("recoilMin_y", BindingFlags.Instance | BindingFlags.Public);
                recoilxmin.SetValue((ItemGunAsset)Player.player.equipment.asset, 0f);
                recoilymin.SetValue((ItemGunAsset)Player.player.equipment.asset, 0f);
                FieldInfo recoilxmax = typeof(ItemGunAsset).GetField("recoilMax_x", BindingFlags.Instance | BindingFlags.Public);
                FieldInfo recoilyax = typeof(ItemGunAsset).GetField("recoilMax_y", BindingFlags.Instance | BindingFlags.Public);
                recoilxmax.SetValue((ItemGunAsset)Player.player.equipment.asset, 0f);
                recoilyax.SetValue((ItemGunAsset)Player.player.equipment.asset, 0f);

                this.DisableBallistic();
			}
			this._spreadCache--;
		}

        float temp_dist = 200;
        float distToPlayer;
        public string tempname;

        public void AIMING()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                foreach (Player player in ESPATTACK._players)
                {
                    
                    if (!(player == null))
                    {
                        tempname = player.name;

                        if (FriendList.Contains(tempname))
                        {
                            return;
                        }
                        else
                        { 
                            Vector3 position = player.transform.position;
                            distToPlayer = Vector3.Distance(position, ESPATTACK._pCamera.transform.position);
                            if (!(Player.player.name == player.name))
                            {
                                Vector3 vector = ESPATTACK._pCamera.WorldToScreenPoint(position);

                                if (vector.z >= 0f) vector.y = (float)UnityEngine.Screen.height - vector.y;

                                if (distToPlayer < temp_dist)
                                {
                                    temp_dist = distToPlayer;
                                }
                                else
                                {
                                    return;
                                }

                                Camera.main.transform.LookAt(vector);
                                float num4 = Camera.main.transform.localRotation.eulerAngles.x;
                                if (num4 <= 90f && num4 <= 270f)
                                {
                                    num4 = Camera.main.transform.localRotation.eulerAngles.x + 90f;
                                }
                                else if (num4 >= 270f && num4 <= 360f)
                                {
                                    num4 = Camera.main.transform.localRotation.eulerAngles.x - 270f;
                                }
                                typeof(ItemGunAsset).GetField("_pitch", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(player.look, num4);
                                typeof(ItemGunAsset).GetField("_yaw", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(player.look, player.transform.rotation.eulerAngles.y);

                            }
                        }
                    }
                }
            }
        }

        public void CreateFriendButtons()
        {
            foreach (Player player in ESPATTACK._players)
            {
                if (GUILayout.Button(player.name + (FriendList.Contains(player.name) ? "(Friend)" : "(Enemy)"), new GUILayoutOption[0]))
                {
                    if (FriendList.Contains(player.name))
                    {
                        FriendList.Replace(" " + player.name, "");
                    }
                    else
                    {
                    FriendList += " " + player.name;
                    }
                }
            }
        }
    

        public static bool Aimbot_Master;

        public static bool Aimbot_Enable;

        public static bool Aimbot_Players;

        public static bool Aimbot_Zombies;

        public static bool Aimbot_Animals;

        // Token: 0x04000015 RID: 21
        public static bool NoRecoil;

		// Token: 0x04000016 RID: 22
		public static bool NoShake;

		// Token: 0x04000017 RID: 23
		public static bool InteractRange;

		// Token: 0x04000018 RID: 24
		public static bool Scope20;

		// Token: 0x04000019 RID: 25
		public static bool NoSpread;

		// Token: 0x0400001A RID: 26
		public static bool ScreenshotAlert;

		// Token: 0x0400001B RID: 27
		public static bool IncreaseRange;

		// Token: 0x0400001C RID: 28
		public static bool IncreaseMeleeRange;

		// Token: 0x0400001D RID: 29
		public static string WebText = "b1g(large) unturned hack";

		// Token: 0x0400001E RID: 30
		public static int ShakeDelay;

		// Token: 0x0400001F RID: 31
		[Obfuscation]
		private int _nightVision;

		// Token: 0x04000020 RID: 32
		internal static FieldInfo SpreadAim;

		// Token: 0x04000021 RID: 33
		internal static FieldInfo SpreadHip;

		// Token: 0x04000022 RID: 34
		private int _spreadCache;

		// Token: 0x04000023 RID: 35
		private bool _zoom;

		// Token: 0x04000024 RID: 36
		private float _zoomSize;

        public string FriendList;
	}
}
