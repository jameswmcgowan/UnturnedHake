using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace UnturnedHake
{
	// Token: 0x0200000A RID: 10
	internal static class MethodUtil
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000336C File Offset: 0x0000156C
		internal unsafe static void Hook(MethodBase from, MethodBase to)
		{
			IntPtr address = MethodUtil.GetAddress(from);
			IntPtr address2 = MethodUtil.GetAddress(to);
			if (IntPtr.Size == 8)
			{
				byte* ptr = (byte*)address.ToPointer();
				*ptr = 73;
				ptr[1] = 187;
				*(long*)(ptr + 2) = address2.ToInt64();
				ptr[10] = 65;
				ptr[11] = byte.MaxValue;
				ptr[12] = 227;
				return;
			}
			if (IntPtr.Size == 4)
			{
				byte* ptr2 = (byte*)address.ToPointer();
				ptr2[1] = 187;
				*(long*)(ptr2 + 2) = (long)address2.ToInt32();
				ptr2[11] = byte.MaxValue;
				ptr2[12] = 227;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003404 File Offset: 0x00001604
		private static IntPtr GetAddress(MethodBase methodBase)
		{
			RuntimeHelpers.PrepareMethod(methodBase.MethodHandle);
			return methodBase.MethodHandle.GetFunctionPointer();
		}
	}
}
