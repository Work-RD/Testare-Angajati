using System;
using System.IO;
using System.Runtime.InteropServices;
namespace Testare_Angajati
{
	public class RawPrinterHelper
	{
		[StructLayout(LayoutKind.Sequential)]
		public class DOCINFOA
		{
			[MarshalAs(UnmanagedType.LPStr)]
			public string pDocName;
			[MarshalAs(UnmanagedType.LPStr)]
			public string pOutputFile;
			[MarshalAs(UnmanagedType.LPStr)]
			public string pDataType;
		}
		[DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "OpenPrinterA", SetLastError = true)]
		public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);
		[DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		public static extern bool ClosePrinter(IntPtr hPrinter);
		[DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "StartDocPrinterA", SetLastError = true)]
		public static extern bool StartDocPrinter(IntPtr hPrinter, int level, [MarshalAs(UnmanagedType.LPStruct)] [In] RawPrinterHelper.DOCINFOA di);
		[DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		public static extern bool EndDocPrinter(IntPtr hPrinter);
		[DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		public static extern bool StartPagePrinter(IntPtr hPrinter);
		[DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		public static extern bool EndPagePrinter(IntPtr hPrinter);
		[DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);
		public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, int dwCount)
		{
			int num = 0;
			IntPtr hPrinter = new IntPtr(0);
			RawPrinterHelper.DOCINFOA dOCINFOA = new RawPrinterHelper.DOCINFOA();
			bool flag = false;
			dOCINFOA.pDocName = "LabelBarcode";
			dOCINFOA.pDataType = "RAW";
			bool flag2 = RawPrinterHelper.OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero);
			if (flag2)
			{
				bool flag3 = RawPrinterHelper.StartDocPrinter(hPrinter, 1, dOCINFOA);
				if (flag3)
				{
					bool flag4 = RawPrinterHelper.StartPagePrinter(hPrinter);
					if (flag4)
					{
						flag = RawPrinterHelper.WritePrinter(hPrinter, pBytes, dwCount, out num);
						RawPrinterHelper.EndPagePrinter(hPrinter);
					}
					RawPrinterHelper.EndDocPrinter(hPrinter);
				}
				RawPrinterHelper.ClosePrinter(hPrinter);
			}
			bool flag5 = !flag;
			if (flag5)
			{
				Marshal.GetLastWin32Error();
			}
			return flag;
		}
		public static bool SendFileToPrinter(string szPrinterName, string szFileName)
		{
			FileStream fileStream = new FileStream(szFileName, FileMode.Open);
			BinaryReader binaryReader = new BinaryReader(fileStream);
			byte[] array = new byte[fileStream.Length];
			IntPtr intPtr = new IntPtr(0);
			int num = Convert.ToInt32(fileStream.Length);
			byte[] source = binaryReader.ReadBytes(num);
			IntPtr intPtr2 = Marshal.AllocCoTaskMem(num);
			Marshal.Copy(source, 0, intPtr2, num);
			bool result = RawPrinterHelper.SendBytesToPrinter(szPrinterName, intPtr2, num);
			Marshal.FreeCoTaskMem(intPtr2);
			fileStream.Close();
			return result;
		}
		public static bool SendStringToPrinter(string szPrinterName, string szString)
		{
			int length = szString.Length;
			IntPtr intPtr = Marshal.StringToCoTaskMemAnsi(szString);
			RawPrinterHelper.SendBytesToPrinter(szPrinterName, intPtr, length);
			Marshal.FreeCoTaskMem(intPtr);
			return true;
		}
	}
}
