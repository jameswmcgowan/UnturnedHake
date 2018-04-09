using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;
using Steamworks;

namespace Payload
{
    public class MiscFunctions
    {
        public static bool noWall(Transform ver, float distance = Mathf.Infinity)
        {
            return !Physics.Linecast(Camera.main.transform.position, ver.transform.position, RayMasks.DAMAGE_CLIENT);
        }

        public static Player getLocalPlayer()
        {
            return Player.player;
        }

        public static float getDistance(Vector3 point)
        {
            return Vector3.Distance(Camera.main.transform.position, point);
        }

        public static bool getLookingAt(out RaycastHit result, float distance = Mathf.Infinity)
        {
            return Physics.Raycast(getLocalPlayer().look.aim.position, getLocalPlayer().look.aim.forward, out result, distance);
        }

        public static bool aContains(Array a, object b)
        {
            return Array.IndexOf(a, b) > -1;
        }

        public static ulong getPlayerID(Player p)
        {
            return Array.Find(Provider.clients.ToArray(), a => a.player == p).playerID.steamID.m_SteamID;
        }

        public static SteamPlayer getSteamPlayer(ulong id)
        {
            return Array.Find(Provider.clients.ToArray(), a => a.playerID.steamID.m_SteamID == id);
        }

        public static SteamPlayer getSteamPlayer(string name)
        {
            return Array.Find(Provider.clients.ToArray(), a => a.playerID.playerName == name || a.playerID.nickName == name || a.player.transform.name == name);
        }

        public static SteamPlayer getSteamPlayer(Player p)
        {
            return Array.Find(Provider.clients.ToArray(), a => a.player == p);
        }

        public static SteamPlayer getSteamSemi(string name)
        {
            return Array.Find(Provider.clients.ToArray(), a => a.player.transform.name.Contains(name));
        }

        public static SteamPlayer getSteamPlayer(CSteamID id)
        {
            return Array.Find(Provider.clients.ToArray(), a => a.playerID.steamID == id);
        }

        public static Player getPlayer(string name)
        {
            return Array.Find(Provider.clients.ToArray(), a => a.playerID.playerName == name || a.playerID.nickName == name || a.player.transform.name == name).player;
        }
    }
}
