using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using IslamicPOS.Core.Barcoding.Models;

namespace IslamicPOS.Infrastructure.Services
{
    public class PrintService
    {
        public async Task PrintLabel(byte[] labelContent, PrinterSettings settings)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                await PrintWindowsLabel(labelContent, settings);
            }
            else
            {
                await PrintUnixLabel(labelContent, settings);
            }
        }

        private async Task PrintWindowsLabel(byte[] labelContent, PrinterSettings settings)
        {
            var printDocument = new PrintDocument
            {
                PrinterSettings = new System.Drawing.Printing.PrinterSettings
                {
                    PrinterName = settings.PrinterName,
                    Copies = (short)settings.Copies
                }
            };

            // Set custom paper size if specified
            if (!string.IsNullOrEmpty(settings.PaperSize))
            {
                var paperSize = new PaperSize(settings.PaperSize, 
                    (int)(settings.Width * 100), 
                    (int)(settings.Height * 100));
                printDocument.DefaultPageSettings.PaperSize = paperSize;
            }

            // Handle direct thermal printing if needed
            if (settings.DirectThermalPrinting)
            {
                // Add thermal printer specific commands
                var escCommands = GenerateThermalPrinterCommands(labelContent);
                await RawPrint(settings.PrinterName, escCommands);
            }
            else
            {
                // Standard printing
                using var stream = new System.IO.MemoryStream(labelContent);
                var image = System.Drawing.Image.FromStream(stream);
                printDocument.PrintPage += (sender, e) =>
                {
                    e.Graphics.DrawImage(image, e.PageBounds);
                };
                printDocument.Print();
            }
        }

        private async Task PrintUnixLabel(byte[] labelContent, PrinterSettings settings)
        {
            // Implementation for Unix-based systems using CUPS
            var tempFile = System.IO.Path.GetTempFileName();
            await System.IO.File.WriteAllBytesAsync(tempFile, labelContent);

            var processInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "lp",
                Arguments = $"-d {settings.PrinterName} {tempFile}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            var process = System.Diagnostics.Process.Start(processInfo);
            await process.WaitForExitAsync();
            System.IO.File.Delete(tempFile);
        }

        private byte[] GenerateThermalPrinterCommands(byte[] imageData)
        {
            // Add printer-specific commands (e.g., ESC/POS commands)
            var commands = new System.Collections.Generic.List<byte>();

            // Initialize printer
            commands.AddRange(new byte[] { 0x1B, 0x40 }); // ESC @

            // Set print mode
            commands.AddRange(new byte[] { 0x1B, 0x21, 0x00 }); // ESC ! 0

            // Add image data
            commands.AddRange(imageData);

            // Cut paper
            commands.AddRange(new byte[] { 0x1D, 0x56, 0x41, 0x10 }); // GS V A 16

            return commands.ToArray();
        }

        private async Task RawPrint(string printerName, byte[] data)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Windows raw printing implementation
                using var printStream = new RawPrinterHelper(printerName);
                await printStream.WriteAsync(data);
            }
            else
            {
                // Unix raw printing implementation
                var tempFile = System.IO.Path.GetTempFileName();
                await System.IO.File.WriteAllBytesAsync(tempFile, data);

                var processInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "lp",
                    Arguments = $"-d {printerName} -o raw {tempFile}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                var process = System.Diagnostics.Process.Start(processInfo);
                await process.WaitForExitAsync();
                System.IO.File.Delete(tempFile);
            }
        }
    }

    // Helper class for Windows raw printing
    public class RawPrinterHelper : IDisposable
    {
        private readonly IntPtr printerHandle;
        private readonly string printerName;

        public RawPrinterHelper(string printer)
        {
            printerName = printer;
            OpenPrinter(printer, out printerHandle, IntPtr.Zero);
        }

        public async Task WriteAsync(byte[] data)
        {
            var docInfo = new DOCINFO
            {
                pDocName = "Raw Print Job",
                pDataType = "RAW"
            };

            if (StartDocPrinter(printerHandle, 1, docInfo))
            {
                try
                {
                    await Task.Run(() =>
                    {
                        int bytesWritten;
                        StartPagePrinter(printerHandle);
                        WritePrinter(printerHandle, data, data.Length, out bytesWritten);
                        EndPagePrinter(printerHandle);
                    });
                }
                finally
                {
                    EndDocPrinter(printerHandle);
                }
            }
        }

        public void Dispose()
        {
            if (printerHandle != IntPtr.Zero)
            {
                ClosePrinter(printerHandle);
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private class DOCINFO
        {
            [MarshalAs(UnmanagedType.LPStr)] public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)] public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)] public string pDataType;
        }

        [DllImport("winspool.drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFO di);

        [DllImport("winspool.drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool WritePrinter(IntPtr hPrinter, byte[] pBytes, Int32 dwCount, out Int32 dwWritten);
    }
}