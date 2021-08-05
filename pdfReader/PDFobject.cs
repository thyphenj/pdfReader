using System;
namespace pdfReader
{
    public class PDFobject
    {
        public int Numero { get; set; }
        public int Generation { get; set; }

        public PDFObjectType Type { get; set; }

        public PDFobject()
        {

        }

        public override string ToString()
        {
            return $"{Numero} {Generation}";
        }
    }
}
