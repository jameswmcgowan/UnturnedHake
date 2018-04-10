using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using UnityEngine;
using SDG.Unturned;
using System.Runtime.InteropServices;

namespace BeyondCheatFree
{
    public class AimbotType
    {
        

        public string name;
        public string dmg;

        public AimbotType(string name, string dmg)
        {
            this.name = name;
            this.dmg = dmg;
        }
    }
    public class menu_Aimbot
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);

        bool isOn;

        Rect window_Main = new Rect(10, 10, 200, 200);

        DateTime lastUpdate;
        DateTime lastAim;

        SteamPlayer[] players;
        Zombie[] zombies;
        Animal[] animals;

        public static bool enabled = false;
        public static bool aim_players = true;
        public static bool ignore_friends = true;
        public static bool ignore_admins = true;
        public static bool ignore_walls = true;
        bool use_gun_distance = true;
        public static bool aim_zombies = false;
        public static bool aim_animals = false;
        float distance = 200f;
        int aim_update = 20;
        int at_pos = 0;

        List<AimbotType> at = new List<AimbotType>();

        public bool getIsOn()
        {
            return isOn;
        }

        public void setIsOn(bool a)
        {
            isOn = a;
        }

        public void toggleOn()
        {
            isOn = !isOn;
        }

        public void Start()
        {
            isOn = false;
            at.Add(new AimbotType("Head", "Skull"));
            at.Add(new AimbotType("Torso", "Spine"));
        }

        public void Update()
        {
            if (enabled)
            {
                if (lastUpdate == null || (DateTime.Now - lastUpdate).TotalMilliseconds >= 1000)
                {
                    update_Information();
                    lastUpdate = DateTime.Now;
                }
                if (lastAim == null || (DateTime.Now - lastAim).TotalMilliseconds >= aim_update)
                {
                    update_Aim();
                    lastAim = DateTime.Now;
                }
            }
        }

        void update_Aim()
        {
            GameObject obj = null;

            if (aim_zombies)
            {
                Zombie z = getNZombie();
                if (z != null)
                {
                    if (obj == null)
                    {
                        obj = z.gameObject;
                    }
                    else
                    {
                        if (MiscFunctions.getDistance(z.transform.position) < MiscFunctions.getDistance(obj.transform.position))
                        {
                            obj = z.gameObject;
                        }
                    }
                }
            }

            if (aim_players)
            {
                Player p = getNPlayer();
                if (p != null)
                {
                    if (obj == null)
                    {
                        obj = p.gameObject;
                    }
                    else
                    {
                        if (MiscFunctions.getDistance(p.transform.position) < MiscFunctions.getDistance(obj.transform.position))
                        {
                            obj = p.gameObject;
                        }
                    }
                }
            }

            if (aim_animals)
            {
                Animal a = getNAnimal();
                if (a != null)
                {
                    if (obj == null)
                    {
                        obj = a.gameObject;
                    }
                    else
                    {
                        if (MiscFunctions.getDistance(a.transform.position) < MiscFunctions.getDistance(obj.transform.position))
                        {
                            obj = a.gameObject;
                        }
                    }
                }
            }

            if (obj != null)
            {
                aim(obj);
            }

        }

        void aim(GameObject obj)
        {
            if (GetAsyncKeyState(1) == -32768)
            {
                Vector3 skullPosition = getAimPosition(obj.transform);
                MiscFunctions.getLocalPlayer().transform.LookAt(skullPosition);
                MiscFunctions.getLocalPlayer().transform.eulerAngles = new Vector3(0f, MiscFunctions.getLocalPlayer().transform.rotation.eulerAngles.y, 0f);
                Camera.main.transform.LookAt(skullPosition);
                float num4 = Camera.main.transform.localRotation.eulerAngles.x;
                if (num4 <= 90f && num4 <= 270f)
                {
                    num4 = Camera.main.transform.localRotation.eulerAngles.x + 90f;
                }
                else if (num4 >= 270f && num4 <= 360f)
                {
                    num4 = Camera.main.transform.localRotation.eulerAngles.x - 270f;
                }
                MiscFunctions.getLocalPlayer().look.GetType().GetField("_pitch", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(MiscFunctions.getLocalPlayer().look, num4);
                MiscFunctions.getLocalPlayer().look.GetType().GetField("_yaw", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(MiscFunctions.getLocalPlayer().look, MiscFunctions.getLocalPlayer().transform.rotation.eulerAngles.y);
            }
        }

        public Vector3 getAimPosition(Transform parent)
        {
            Transform[] componentsInChildren = parent.GetComponentsInChildren<Transform>();
            if (componentsInChildren != null)
            {
                Transform[] array = componentsInChildren;
                for (int i = 0; i < array.Length; i++)
                {
                    Transform tr = array[i];
                    if (tr.name.Trim() == at[at_pos].dmg)
                    {
                        return tr.position + new Vector3(0f, 0.4f, 0f);
                    }
                }
            }
            return Vector3.zero;
        }

        void update_Information()
        {
            players = Provider.clients.ToArray();
            List<Zombie> temp = new List<Zombie>();
            for (int i = 0; i < ZombieManager.regions.Length; i++)
            {
                temp.AddRange(ZombieManager.regions[i].zombies);
            }
            zombies = temp.ToArray();
            animals = AnimalManager.animals.ToArray();
        }

        Animal getNAnimal()
        {
            Animal a = null;
            for (int i = 0; i < animals.Length; i++)
            {
                if (!animals[i].isDead && correctDist(animals[i].transform.position))
                {
                    if (a == null)
                    {
                        if (ignore_walls)
                        {
                            a = animals[i];
                        }
                        else
                        {
                            if (MiscFunctions.noWall(animals[i].transform))
                            {
                                a = animals[i];
                            }
                        }
                    }
                    else
                    {
                        if (ignore_walls)
                        {
                            if (MiscFunctions.getDistance(a.transform.position) > MiscFunctions.getDistance(animals[i].transform.position))
                            {
                                a = animals[i];
                            }
                        }
                        else
                        {
                            if (MiscFunctions.noWall(animals[i].transform))
                            {
                                if (MiscFunctions.getDistance(a.transform.position) > MiscFunctions.getDistance(animals[i].transform.position))
                                {
                                    a = animals[i];
                                }
                            }
                        }
                    }
                }
            }
            return a;
        }

        Zombie getNZombie()
        {
            Zombie z = null;
            for (int i = 0; i < zombies.Length; i++)
            {
                if (!zombies[i].isDead && correctDist(zombies[i].transform.position))
                {
                    if (z == null)
                    {
                        if (ignore_walls)
                        {
                            z = zombies[i];
                        }
                        else
                        {
                            if (MiscFunctions.noWall(zombies[i].transform))
                            {
                                z = zombies[i];
                            }
                        }
                    }
                    else
                    {
                        if (ignore_walls)
                        {
                            if (MiscFunctions.getDistance(z.transform.position) > MiscFunctions.getDistance(zombies[i].transform.position))
                            {
                                z = zombies[i];
                            }
                        }
                        else
                        {
                            if (MiscFunctions.noWall(zombies[i].transform))
                            {
                                if (MiscFunctions.getDistance(z.transform.position) > MiscFunctions.getDistance(zombies[i].transform.position))
                                {
                                    z = zombies[i];
                                }
                            }
                        }
                    }
                }
            }
            return z;
        }

        bool canAttack(SteamPlayer p)
        {
                  if (ignore_admins)
                  {
                       if (!p.isAdmin)
                       {
                           return true;
                       }
                       else
                       {
                           return false;
                       }
                  }
                  else
                  {
                      return true;
                  }

        }

        bool correctDist(Vector3 pos)
        {
            if (use_gun_distance)
            {
                if (MiscFunctions.getDistance(pos) <= ((ItemWeaponAsset)MiscFunctions.getLocalPlayer().equipment.asset).range)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (MiscFunctions.getDistance(pos) <= distance)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        Player getNPlayer()
        {
            Player p = null;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].playerID.steamID != Provider.client && players[i].player.life != null && !players[i].player.life.isDead && canAttack(players[i]) && correctDist(players[i].player.transform.position))
                {
                    if (p == null)
                    {
                        if (ignore_walls)
                        {
                            p = players[i].player;
                        }
                        else
                        {
                            if (MiscFunctions.noWall(players[i].player.transform))
                            {
                                p = players[i].player;
                            }
                        }
                    }
                    else
                    {
                        if (ignore_walls)
                        {
                            if (MiscFunctions.getDistance(p.transform.position) > MiscFunctions.getDistance(players[i].player.transform.position))
                            {
                                p = players[i].player;
                            }
                        }
                        else
                        {
                            if (MiscFunctions.noWall(players[i].player.transform))
                            {
                                if (MiscFunctions.getDistance(p.transform.position) > MiscFunctions.getDistance(players[i].player.transform.position))
                                {
                                    p = players[i].player;
                                }
                            }
                        }
                    }
                }
            }
            return p;
        }
    }
}
