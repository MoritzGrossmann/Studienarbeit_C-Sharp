using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.Domain.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Buchungssystem.PdfCreator
{
    public class PdfCreator
    {
        private Stage _stage;

        private List<Booking> _bookings;

        public PdfCreator(Stage stage, List<Booking> bookings)
        {
            _stage = stage;
            _bookings = bookings;
        }

        private decimal Price => _bookings.Where(b => b.Status == BookingStatus.Paid).Sum(b => b.Price);

        private String Header => $"Tagesabrechnung vom {_stage.Begin.ToShortDateString()}";

        public void GeneratePdf()
        {
            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream($"{_stage.Begin.ToShortTimeString()}.pdf", FileMode.CreateNew));

            doc.Open();
            doc.Add(new Paragraph(Header));

            doc.Close();
        }
    }
}
