using iText.Kernel.Pdf;
using iText.Licensing.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PEA.Training._2023.PdfReader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LicenseKey.LoadLicenseFile(new System.IO.FileInfo(@"..\..\itext.demo.license.20231014.json"));

            
        }
    }
}
