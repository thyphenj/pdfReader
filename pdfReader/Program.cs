using System.Collections.Generic;
using System.IO;

namespace pdfReader
{
    class Program
    {
        static void Main()
        {
        //    var pdfBytes = File.ReadAllBytes("testfile.pdf");


        //}

        //static void FirstMain()
        //{
            var pdfs = new List<PDFobject>();

            var lines = File.ReadAllLines("testfile.pdf");

            int lineNo = 0;
            System.Console.WriteLine($"line 0 [{lines[lineNo]}]");
            if (!lines[0].ToUpper().Trim().StartsWith("%PDF-"))
            {
                System.Console.WriteLine("NOT A PDF");
                return;
            }

            while (lines[lineNo].ToUpper() != "%%EOF")
            {
                while (lines[lineNo][0] == '%' && lines[lineNo].ToUpper() != "%%EOF")
                {
                    lineNo++;
                }
                //should be a new object (nn 0 obj) OR xref OR trailer OR startxref

                string line = lines[lineNo++];
                if (line.ToLower() == "trailer")
                {
                    System.Console.WriteLine("TRAILER");
                    while (lines[lineNo] != ">>")
                    {
                        System.Console.WriteLine(lines[lineNo++]);
                    }
                    lineNo++;
                }
                else if (line.ToLower() == "xref")
                {
                    int skip = int.Parse(lines[lineNo++].Split(' ')[1]);
                    //System.Console.WriteLine($"XREF {skip}");
                    lineNo += skip;
                }
                else if (line.ToLower() == "startxref")
                {
                    System.Console.WriteLine("STARTXREF");
                    System.Console.WriteLine(lines[lineNo++]);
                }
                else
                {
                    pdfs.Add(new PDFobject() { Numero = int.Parse(line.Split(' ')[0]) });
                    while (lines[lineNo++].ToLower() != "endobj")
                    { }
                }
            }

            foreach (var x in pdfs)
            {
                System.Console.WriteLine(x);
            }

        }
    }
}
