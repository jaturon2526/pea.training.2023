using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Licensing.Base;
using iText.Licensing.Base.Licensefile;
using J2N.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PEA.Training._2023
{
    internal class Program
    {
        static void Main()
        {
            try
            {
                LicenseKey.LoadLicenseFile(new System.IO.FileInfo(@"../../itext.demo.license.20231014.json"));

                PdfFont pdfFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA, PdfEncodings.UTF8, PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
                PdfFont regular = PdfFontFactory.CreateFont(@"../../fonts/Sarabun-Regular.ttf", PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
                PdfFont bold = PdfFontFactory.CreateFont(@"../../fonts/Sarabun-Bold.ttf", PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
                PdfFont italic = PdfFontFactory.CreateFont(@"../../fonts/Sarabun-Italic.ttf", PdfEncodings.IDENTITY_H, PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);

                WriterProperties writerProperties = new WriterProperties();
                writerProperties.SetPdfVersion(PdfVersion.PDF_2_0);
                writerProperties.SetStandardEncryption(Encoding.UTF8.GetBytes("1234"),
                    Encoding.UTF8.GetBytes("4321"), 
                    EncryptionConstants.ALLOW_PRINTING | EncryptionConstants.ALLOW_COPY | EncryptionConstants.ALLOW_MODIFY_CONTENTS, 
                    EncryptionConstants.ENCRYPTION_AES_128);

                PdfDocument pdf = new PdfDocument(new PdfWriter(@"../../hello.pdf", writerProperties));
                //pdf.SetDefaultPageSize(new PageSize(400, 400));

                Document doc = new Document(pdf);
                doc.SetFont(regular).SetFontSize(16f);

                Paragraph paragraph = new Paragraph("Hello");
                paragraph.SetFontSize(22f);
                paragraph.SetItalic();
                paragraph.SetTextAlignment(TextAlignment.CENTER);

                Paragraph paragraph1 = new Paragraph("New Line");
                paragraph1.SetFontSize(30);
                paragraph1.SetFirstLineIndent(10).SetMarginTop(50f);

                Paragraph paragraph2 = new Paragraph("Line 3")
                    .SetFontSize(16)
                    .SetBold()
                    .SetUnderline()
                    .SetTextAlignment(TextAlignment.RIGHT);

                Paragraph paragraph3 = new Paragraph("สวัสดี คุณทดสอบ อ่านไทย")
                    .SetFont(regular)
                    .SetFontSize(16)
                    .SetWidth(30f);

                Text text1 = new Text("เรียน").SetFont(regular).SetFontSize(16).SetFontColor(ColorConstants.RED);
                Text text2 = new Text("คุณสมหมาย").SetFont(bold).SetFontSize(20);
                Text text3 = new Text("นามสมมุติ").SetFont(italic).SetFontSize(14);
                Text text4 = new Text("หนึ่ง ทิ้ง ทั้ง").SetFont(regular).SetFontSize(20);

                Paragraph paragraph4 = new Paragraph()
                    .Add(text1)
                    .Add(text2)
                    .Add(text3)
                    .Add(text4);

                doc.Add(paragraph);
                doc.Add(paragraph1);
                doc.Add(paragraph2);
                doc.Add(paragraph3);
                doc.Add(paragraph4);

                doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

                doc.Add(new Paragraph("New Page / หน้าใหม่"));

                float[] columnWidths = { 1, 5, 5 };
                Table table = new Table(UnitValue.CreatePercentArray(columnWidths))
                    .UseAllAvailableWidth();

                Cell cell = new Cell(1, 3)
                .Add(new Paragraph("This is a header"))
                .SetFont(regular)
                .SetFontSize(13)
                .SetFontColor(DeviceGray.WHITE)
                .SetBackgroundColor(DeviceGray.BLACK)
                .SetTextAlignment(TextAlignment.CENTER);

                table.AddHeaderCell(cell);

                for (int i = 0; i < 2; i++)
                {
                    Cell[] headerFooter =
                    {
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("#")),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Key")),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Value"))
                };

                    foreach (Cell hfCell in headerFooter)
                    {
                        if (i == 0)
                        {
                            table.AddHeaderCell(hfCell);
                        }
                        else
                        {
                            table.AddFooterCell(hfCell);
                        }
                    }
                }

                Color eventRowColor = ColorConstants.BLUE;

                for (int counter = 0; counter < 5; counter++)
                {
                    Cell cell1 = new Cell().SetTextAlignment(TextAlignment.RIGHT).Add(new Paragraph("คีย์ " + (counter + 1)));

                    if (counter % 2 == 0)
                        cell1.SetFontColor(eventRowColor);

                    table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph((counter + 1).ToString())));
                    table.AddCell(cell1);
                    table.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("แวลู่ " + (counter + 1))));
                }

                doc.Add(table);

                ImageData imageData = ImageDataFactory.Create(@"..\..\images\iText_400x400.jpg");
                Image image = new Image(imageData)
                    .ScaleAbsolute(100, 100)
                    .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                    .SetMarginTop(50)
                    .SetMarginBottom(20);
                doc.Add(image);

                doc.Add(new Paragraph("under image"));

                Image image1 = new Image(imageData)
                    .SetFixedPosition(50, 100)
                    .ScaleAbsolute(300, 100);
                doc.Add(image1);

                float[] columnWidth = { 20, 20, 50 };
                Table table1 = new Table(UnitValue.CreatePointArray(columnWidth))
                    .UseAllAvailableWidth();

                Cell hdCell = new Cell(1, 3).Add(new Paragraph("Header"))
                    .SetTextAlignment(TextAlignment.CENTER);
                table1.AddHeaderCell(hdCell);

                Cell cell2 = new Cell().Add(new Paragraph("คอลัมน์ 1"));
                Cell cell3 = new Cell().Add(new Paragraph("คอลัมน์ 2"));
                Cell cell4 = new Cell().Add(new Paragraph("คอลัมน์ 3"));

                table1.AddCell(cell2).AddCell(cell3).AddCell(cell4);

                Image image2 = new Image(imageData).ScaleAbsolute(100, 100)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);

                Cell imageCell = new Cell(1, 3).Add(image2);
                table1.AddCell(imageCell);
                doc.Add(table1);

                //for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
                //{
                //    Rectangle pageSize = pdf.GetPage(i).GetPageSize();
                //    float x = pageSize.GetWidth() / 2;
                //    float y = pageSize.GetTop() - 20;
                //    doc.ShowTextAligned(new Paragraph(i.ToString()), x, y, i, TextAlignment.LEFT, VerticalAlignment.BOTTOM, 0);
                //}

                pdf.Close();
            }catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
        }
    }
}
