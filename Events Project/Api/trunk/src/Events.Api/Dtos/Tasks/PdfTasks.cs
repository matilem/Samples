using System;
using System.Collections.Generic;
using System.IO;
using Aafp.Events.Api.Models.Badges;
using Aafp.Events.Api.Tasks.Interfaces;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Aafp.Events.Api.Tasks
{
    public class PdfTasks : IPdfTasks
    {
        private PdfWriter writer;

        public byte[] GetPdf(IEnumerable<BadgeBase> badges)
        {
            byte[] pdfContentBytes;

            using (var pdfOutputStream = new MemoryStream())
            {
                // 288 = 4in x 72, 216 = 3in x 72
                var pdfDocument = new Document(new Rectangle(288f, 216f), 0f, 0f, 0f, 0f);
                writer = PdfWriter.GetInstance(pdfDocument, pdfOutputStream);
                ProduceContent(pdfDocument, badges);
                pdfContentBytes = pdfOutputStream.ToArray();
                writer.Close();
            }

            return pdfContentBytes;
        }

        private void ProduceContent(Document document, IEnumerable<BadgeBase> badges)
        {
            document.Open();

            try
            {
                document.Open();
                foreach (var badge in badges)
                {
                    if (badge is SessionBadge)
                        AddSessionBadge(document, (SessionBadge)badge);
                    else if (badge is RegistrantBadge)
                        AddRegistrantBadge(document, (RegistrantBadge)badge);
                    else if (badge is GuestBadge)
                        AddGuestBadge(document, (GuestBadge)badge);
                    document.NewPage();
                }
            }
            catch (Exception ex)
            {
                document.NewPage();
                document.Add(new Paragraph(ex.Message));
            }
            finally
            {
                document.Close();
            }
        }

        private void AddSessionBadge(Document document, SessionBadge badge)
        {
            // Top offset
            AddParagraph(document, " ", new Formatting { Leading = 48f, FontSize = 12f });

            // Session Code
            AddParagraph(document, badge.SessionCode, new Formatting { Leading = 15f, Alignment = "Center", Spacing = 6f, FontSize = 23, IsBold = true });

            // Session Name
            AddParagraph(document, badge.SessionName, new Formatting { Leading = 15f, Spacing = 12f, Alignment = "Center", FontSize = 13 });

            // Session Date
            if (badge.SessionDate != null && badge.SessionDate.Trim() != string.Empty)
            {
                AddParagraph(document, badge.SessionDate, new Formatting { Leading = 20f, Alignment = "Center", FontSize = 13 });
            }

            // Session Location
            if (badge.Location != null && badge.Location.Trim() != string.Empty)
            {
                AddParagraph(document, badge.Location, new Formatting { Leading = 15f, Spacing = 4f, Alignment = "Center", FontSize = 13, IsBold = true, IndentationLeft = 2, IndentationRight = 2});
            }

            // Fee
            AddParagraph(writer, badge.Fee, new Formatting { Spacing = 35f, IndentationLeft = 240f, FontSize = 10 });

            // Guest Name
            AddParagraph(writer, badge.AttendeeName, new Formatting { Spacing = 35f, IndentationLeft = 16f, FontSize = 13 });

            // Event Code
            AddParagraph(writer, badge.EventCode, new Formatting { Spacing = 20f, IndentationLeft = 16f, FontSize = 6 });

            // Attendy Type
            AddParagraph(writer, $"{badge.AttendeeName} - {badge.MemberId}", new Formatting { Spacing = 12f, IndentationLeft = 16f, FontSize = 6 });
        }

        private void AddRegistrantBadge(Document document, RegistrantBadge badge)
        {
            AddParagraph(document, " ", new Formatting { Leading = 7.5f, FontSize = 7.5f }); // dumb paragraph, allows the next paragraph to use Spacing

            // Nickname
            AddParagraph(document, badge.Nickname, new Formatting { Leading = 48f, Spacing = 55f, Alignment = "Center", IsBold = true, FontSize = 30f });

            // Full Name
            AddParagraph(document, badge.FullName, new Formatting { Leading = 15.5f, Spacing = 4f, Alignment = "Center", IsBold = true, FontSize = 16.5f });

            // Company
            if (badge.Company != null && badge.Company.Trim() != string.Empty)
            {
                AddParagraph(document, badge.Company, new Formatting { Leading = 14f, Spacing = 1f, Alignment = "Center", FontSize = 14f });
            }

            // Position
            if (badge.Position != null && badge.Position.Trim() != string.Empty)
            {
                AddParagraph(document, badge.Position, new Formatting { Leading = 14f, Spacing = 1f, Alignment = "Center", FontSize = 14f });
            }

            // Address
            AddParagraph(document, badge.Address, new Formatting { Leading = 14f, Spacing = 1f, Alignment = "Center", FontSize = 14f });

            // FAAFP
            if (badge.ShowFAAFP)
            {
                AddParagraph(document, "FAAFP", new Formatting { Leading = 13f, Spacing = 0f, Alignment = "Center", FontSize = 14f });
            }

            // Event Code
            AddParagraph(writer, badge.EventCode, new Formatting { Spacing = 20f, IndentationLeft = 16f, FontSize = 6 });

            AddParagraph(writer, $"{badge.AttendeeType} - {badge.MemberId}", new Formatting { Spacing = 12f, IndentationLeft = 16f, FontSize = 8f });
        }

        private static void AddGuestBadge(Document document, GuestBadge badge)
        {
            AddParagraph(document, " ", new Formatting { Leading = 7.5f, FontSize = 7.5f }); // dumb paragraph, allows the next paragraph to use Spacing

            // First Name
            AddParagraph(document, badge.Name, new Formatting { Leading = 26f, Spacing = 70f, Alignment = "Center", IsBold = true, FontSize = 26f });

            // Address
            AddParagraph(document, badge.Address, new Formatting { Leading = 16f, Spacing = 4f, Alignment = "Center", FontSize = 16f });
        }

        private static void AddParagraph(PdfWriter writer, string content, Formatting format)
        {
            writer.DirectContent.BeginText();
            writer.DirectContent.SetFontAndSize(GetBaseFont(), format.FontSize);
            writer.DirectContent.SetTextMatrix(format.IndentationLeft.Value, format.Spacing.Value);
            writer.DirectContent.ShowText(content);
            writer.DirectContent.EndText();
        }

        private static void AddParagraph(Document document, string content, Formatting format)
        {
            var paragraph = new Paragraph(format.Leading, content, GetFont(format.FontSize, format.IsBold));

            if (format.Spacing.HasValue)
            {
                paragraph.SpacingBefore = format.Spacing.Value;
            }

            if (format.IndentationLeft.HasValue)

            {
                paragraph.IndentationLeft = format.IndentationLeft.Value;
                
            }
            else
            {
                paragraph.IndentationLeft = 2;
            }

            if (format.IndentationRight.HasValue)
            {
                paragraph.IndentationRight = format.IndentationRight.Value;
            }
            else
            {
                paragraph.IndentationRight = 2;
            }

            if (format.HasAlignment)
            {
                paragraph.SetAlignment(format.Alignment);
            }

            document.Add(paragraph);
        }

        private static Font GetFont(float fontSize, bool isBold)
        {
            var font = GetFont(fontSize);
            if (isBold)
                font.SetStyle("bold");
            return font;
        }

        private static Font GetFont(float fontSize)
        {
            return FontFactory.GetFont("Arial", fontSize);
        }

        private static BaseFont GetBaseFont()
        {
            return BaseFont.CreateFont("Helvetica", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        }

        #region Nested type: Formatting

        private sealed class Formatting
        {
            public float Leading { get; set; }

            public float? IndentationLeft { get; set; }

            public float? IndentationRight { get; set; }

            public float? Spacing { get; set; }

            public string Alignment { get; set; }

            public float FontSize { get; set; }

            public bool IsBold { get; set; }

            public bool HasAlignment => !string.IsNullOrEmpty(Alignment);
        }

        #endregion
    }
}