using System;
using System.Reflection;
using SDG.Unturned;
using UnityEngine;

namespace UnturnedHake
{
    // Token: 0x02000009 RID: 9
    internal class PLook : PlayerLook
    {
        // Token: 0x0600001D RID: 29 RVA: 0x00003290 File Offset: 0x00001490
        public void recoil(float x, float y, float h, float v)
        {
            if (MainMenu.NoRecoil)
            {
                return;
            }
            ////this._yaw += x;
            //FieldInfo _yaw2 = typeof(PlayerLook).GetField("_yaw", BindingFlags.NonPublic | BindingFlags.Instance);
            //_yaw2.SetValue(player.look, (int)_yaw2.GetValue(player.look) + x);
            
            ////this._pitch -= y;
            //FieldInfo _pitch2 = typeof(PlayerLook).GetField("_pitch", BindingFlags.NonPublic | BindingFlags.Instance);
            //_pitch2.SetValue(player.look, (int)_pitch2.GetValue(player.look) - y);

            ////this.recoil_x += x * h;
            //FieldInfo recoil_x2 = typeof(PlayerLook).GetField("recoil_x", BindingFlags.NonPublic | BindingFlags.Instance);
            //recoil_x2.SetValue(player.look, (int)recoil_x2.GetValue(player.look) + x * h);

            ////this.recoil_y += y * v;
            //FieldInfo recoil_y2 = typeof(PlayerLook).GetField("recoil_y", BindingFlags.NonPublic | BindingFlags.Instance);
            //recoil_y2.SetValue(player.look, (int)recoil_y2.GetValue(player.look) + y * v);

            //FieldInfo lastRecoil2 = typeof(PlayerLook).GetField("lastRecoil", BindingFlags.NonPublic | BindingFlags.Instance);

            //if ((double)(Time.realtimeSinceStartup - (float)lastRecoil2.GetValue(player.look)) < 0.2)
            //{
            //    //this.recoil_x *= 0.6f;
            //    recoil_x2.SetValue(player.look, (int)recoil_x2.GetValue(player.look) * 0.6f);

            //    //this.recoil_y *= 0.6f;
            //    recoil_y2.SetValue(player.look, (int)recoil_y2.GetValue(player.look) * 0.6f);
            //}
            ////this.lastRecoil = Time.realtimeSinceStartup;
            //lastRecoil2.SetValue(player.look, Time.realtimeSinceStartup);
        }

        // Token: 0x0600001E RID: 30 RVA: 0x00003329 File Offset: 0x00001529
        public void enableScope(float zoom, ELightingVision vision)
        {
            base.scopeCamera.fieldOfView = (MainMenu.Scope20 ? 3f : zoom);
            base.scopeCamera.enabled = (vision == 0);
            base.enableScope(zoom, vision);

            //this.scopeCamera.fieldOfView = zoom;
            //this._isScopeActive = true;
            //this.scopeVision = vision;
            //this.scopeCamera.enabled = (this.scopeCamera.targetTexture != null && this.scopeVision == ELightingVision.NONE);
        }
    }
}
