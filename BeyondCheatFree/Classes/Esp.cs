using System;
using HighlightingSystem;
using SDG.Unturned;
using UnityEngine;

namespace BeyondCheatFree
{
	// Token: 0x02000002 RID: 2
	internal class Esp : MonoBehaviour
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private void Start()
		{
			Esp._lineMaterial = new Material(Shader.Find("Hidden/Internal-Colored"))
			{
				hideFlags = (HideFlags)61
			};
			Esp._lineMaterial.SetInt("_SrcBlend", 5);
			Esp._lineMaterial.SetInt("_DstBlend", 10);
			Esp._lineMaterial.SetInt("_Cull", 0);
			Esp._lineMaterial.SetInt("_ZWrite", 0);
			this._players = FindObjectsOfType<Player>();
			this._zombies = FindObjectsOfType<Zombie>();
			this._items = FindObjectsOfType<InteractableItem>();
			this._vehicles = FindObjectsOfType<InteractableVehicle>();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E8 File Offset: 0x000002E8
		private void FindObjects()
		{
			this._players = FindObjectsOfType<Player>();
			if (EspMenu.PlayersGlow)
			{
				foreach (Player player in this._players)
				{
					if (!(Player.player.name == player.name) && player.gameObject.GetComponent<Highlighter>() == null)
					{
						Highlighter highlighter = player.gameObject.AddComponent<Highlighter>();
						highlighter.ConstantParams(Color.red);
						highlighter.OccluderOn();
						highlighter.SeeThroughOn();
						highlighter.ConstantOn();
					}
				}
			}
			this._zombies = FindObjectsOfType<Zombie>();
			if (EspMenu.ZombieGlow)
			{
				foreach (Zombie zombie in this._zombies)
				{
					if (zombie.gameObject.GetComponent<Highlighter>() == null)
					{
						Highlighter highlighter2 = zombie.gameObject.AddComponent<Highlighter>();
						highlighter2.ConstantParams(Color.blue);
						highlighter2.OccluderOn();
						highlighter2.SeeThroughOn();
						highlighter2.ConstantOn();
					}
				}
			}
			if (EspMenu.ShowItems)
			{
				this._items = FindObjectsOfType<InteractableItem>();
			}
			if (EspMenu.ShowVehicles)
			{
				this._vehicles = FindObjectsOfType<InteractableVehicle>();
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002200 File Offset: 0x00000400
		private void OnGUI()
		{
			if (this._pCamera == null)
			{
				this._pCamera = Camera.main;
			}
			if (this._espCache <= 0)
			{
				this.FindObjects();
				this._espCache = 200;
			}
			if (EspMenu.ShowPlayers)
			{
				foreach (Player player in this._players)
				{
					if (!(player == null))
					{
						Vector3 position = player.transform.position;
						float num = Vector3.Distance(position, this._pCamera.transform.position);
						if ((num <= EspMenu.EspDistance || EspMenu.InfDist) && !player.life.isDead && !(Player.player.name == player.name))
						{
							Vector3 vector = this._pCamera.WorldToScreenPoint(position);
							if (vector.z >= 0f)
							{
								vector.y = (float)Screen.height - vector.y;
								if (EspMenu.ShowPlayers)
								{
									GUI.Label(new Rect(vector.x, vector.y, 150f, 60f), string.Format("<size=12><color=#ff0000ff>{0}</color></size>", Math.Round((double)num)));
								}
							}
						}
					}
				}
			}
			if (EspMenu.ShowZombies)
			{
				foreach (Zombie zombie in this._zombies)
				{
					if (!(zombie == null))
					{
						Vector3 position2 = zombie.transform.position;
						float num2 = Vector3.Distance(position2, this._pCamera.transform.position);
						if ((num2 <= EspMenu.EspDistance || EspMenu.InfDist) && !zombie.isDead)
						{
							Vector3 vector2 = this._pCamera.WorldToScreenPoint(position2);
							if (vector2.z >= 0f)
							{
								vector2.y = (float)Screen.height - vector2.y;
								if (EspMenu.ShowZombies)
								{
									GUI.Label(new Rect(vector2.x, vector2.y, 150f, 60f), string.Format("<size=12><color=#0000ffff>{0}</color></size>\n<size=12>{1}</size>", zombie.name, Math.Round((double)num2)));
								}
							}
						}
					}
				}
			}
			if (EspMenu.ShowItems)
			{
				foreach (InteractableItem interactableItem in this._items)
				{
					try
					{
						if (!(interactableItem == null))
						{
							Vector3 position3 = interactableItem.transform.position;
							float num3 = Vector3.Distance(position3, this._pCamera.transform.position);
							if (num3 <= EspMenu.EspDistance || EspMenu.InfDist)
							{
								Vector3 vector3 = this._pCamera.WorldToScreenPoint(position3);
								if (vector3.z >= 0f)
								{
									vector3.y = (float)Screen.height - vector3.y;
									GUI.Label(new Rect(vector3.x, vector3.y, 150f, 60f), string.Format("<size=12><color=#00ff00ff>{0}</color></size>\n<size=12>{1}</size>", interactableItem.asset.itemName, Math.Round((double)num3)));
								}
							}
						}
					}
					catch (Exception)
					{
					}
				}
			}
			if (EspMenu.ShowVehicles)
			{
				foreach (InteractableVehicle interactableVehicle in this._vehicles)
				{
					try
					{
						if (!(interactableVehicle == null))
						{
							Vector3 position4 = interactableVehicle.transform.position;
							float num4 = Vector3.Distance(position4, this._pCamera.transform.position);
							if (num4 <= EspMenu.EspDistance || EspMenu.InfDist)
							{
								Vector3 vector4 = this._pCamera.WorldToScreenPoint(position4);
								if (vector4.z >= 0f)
								{
									vector4.y = (float)Screen.height - vector4.y;
									GUI.Label(new Rect(vector4.x, vector4.y, 150f, 60f), string.Format("<size=12><color=#c0c0c0ff>{0}</color></size>\n<size=12>{1}</size>", interactableVehicle.asset.vehicleName, Math.Round((double)num4)));
								}
							}
						}
					}
					catch (Exception)
					{
					}
				}
			}
			this._espCache--;
		}

		// Token: 0x04000001 RID: 1
		private static Material _lineMaterial;

		// Token: 0x04000002 RID: 2
		private int _espCache = 200;

		// Token: 0x04000003 RID: 3
		private InteractableItem[] _items;

		// Token: 0x04000004 RID: 4
		private Camera _pCamera;

		// Token: 0x04000005 RID: 5
		private Player[] _players;

		// Token: 0x04000006 RID: 6
		private InteractableVehicle[] _vehicles;

		// Token: 0x04000007 RID: 7
		private Zombie[] _zombies;
	}
}
