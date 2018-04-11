using System;
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
            this.recoil(x, y, h, v);
            //this._yaw += x;
            //this._pitch -= y;
            //this.recoil_x += x * h;
            //this.recoil_y += y * v;
            //if ((double)(Time.realtimeSinceStartup - this.lastRecoil) < 0.2)
            //{
            //	this.recoil_x *= 0.6f;
            //	this.recoil_y *= 0.6f;
            //}
            //this.lastRecoil = Time.realtimeSinceStartup;
        }

        // Token: 0x0600001E RID: 30 RVA: 0x00003329 File Offset: 0x00001529
        public void enableScope(float zoom, ELightingVision vision)
        {
            base.scopeCamera.fieldOfView = (MainMenu.Scope20 ? 3f : zoom);
            base.scopeCamera.enabled = (vision == 0);
            this.enableScope(zoom, vision);
        }
    }
}
