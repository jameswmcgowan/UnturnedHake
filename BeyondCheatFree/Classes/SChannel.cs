using System;
using System.IO;
using SDG.Unturned;
using UnityEngine;

namespace UnturnedHake
{
	// Token: 0x02000008 RID: 8
	internal class SChannel : SteamChannel
	{
		// Token: 0x0600001B RID: 27 RVA: 0x0000321C File Offset: 0x0000141C
		public void write(object objects)
		{
			if (MainMenu.ScreenshotAlert)
			{
				System.Random random = new System.Random();
				Texture2D texture2D = new Texture2D(640, 480, (TextureFormat)3, false);
				byte[] array = File.ReadAllBytes("FakeScreenshot" + random.Next(1, 3) + ".jpg");
				texture2D.LoadImage(array);
				objects = texture2D.EncodeToJPG(33);
				MainMenu.ScreenshotAlert = false;
			}
			SteamPacker.write(objects);
		}
	}
}
