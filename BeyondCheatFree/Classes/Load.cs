using System;
using SDG.Framework.Modules;
using UnityEngine;

namespace UnturnedHake
{
	// Token: 0x0200000B RID: 11
	public class Load : IModuleNexus
	{
		/// <summary>
		/// </summary>
		// Token: 0x06000022 RID: 34 RVA: 0x0000342A File Offset: 0x0000162A
		public void initialize()
		{
			GameObject gameObject = new GameObject();
            
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			gameObject.AddComponent<Menu>();
			gameObject.AddComponent<MainMenu>();
			gameObject.AddComponent<EspMenu>();
			gameObject.AddComponent<Esp>();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003452 File Offset: 0x00001652
		public void shutdown()
		{
		}
	}
}
