using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfReaderAutoFill
{
    public class Class1
    {
        public static void SavePDFdata(string emptyTemplateName, string FileName, DataTable dtResults)
        {

            using (Stream pdfStream = new FileStream(emptyTemplateName, FileMode.Open))
            using (Stream newpdfStream = new FileStream(FileName, FileMode.Create, FileAccess.ReadWrite))
            {
                PdfReader pdfReader = new PdfReader(pdfStream);
                PdfStamper pdfStamper = new PdfStamper(pdfReader, newpdfStream);
                AcroFields acroFields = pdfStamper.AcroFields;
                foreach (KeyValuePair<string, iTextSharp.text.pdf.AcroFields.Item> pair in pdfReader.AcroFields.Fields)
                {
                    foreach (DataRow row in dtResults.Rows)
                    {
                        if (pair.Key == row.ItemArray[0].ToString())
                        {
                            acroFields.SetField(pair.Key, row.ItemArray[1].ToString());
                        }
                    }
                }
                pdfStamper.Close();
            }
        }
    }
}
