using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ParticipantsAutomationProgrammingApplication.Entities;
using System.Xml;
using System.Xml.Linq;

namespace ParticipantsAutomationProgrammingApplication.Services
{
    public class ParticipantService
    {
        string path = "C:/exc/ParticipantsAutomationProgrammingApplication/ParticipantsData/";
        string xml = "ParticipantsDatatest.xml";
        string excel = "participants.xlsx";

        public void AddPerson(Person person)
        {

            string? personName = "";
            string? personEmail = "";
            List<Person> personList = new List<Person>();
            while (personEmail != "quit" || personName != "quit")
            {

                person.Id = Guid.NewGuid();
                Console.WriteLine("Please enter name of participant:");
                personName = Console.ReadLine();

                Console.WriteLine("Please enter participant email:");
                personEmail = Console.ReadLine();

                if (personName == "quit" || personEmail == "quit")
                    break;

                if (!string.IsNullOrEmpty(personName) || !string.IsNullOrEmpty(personEmail))
                {
                    personList.Add(new Person()
                    {
                        Id = person.Id,
                        Name = personName,
                        Email = personEmail
                    });
                }
                else
                {
                    Console.WriteLine("Please rewrite, you left an empty field.");
                }
            }

            foreach (var participant in personList)
            {
                _writeToXMLDocument(participant);
            }
        }

        public void GetAllParticipants()
        {
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(path + excel, false))
            {
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                string text;
                foreach (Row r in sheetData.Elements<Row>())
                {
                    foreach (Cell c in r.Elements<Cell>())
                    {
                        text = c.CellValue.Text;
                        Console.Write(text + " ");
                    }
                }
                Console.WriteLine();
                Console.ReadKey();
            }
        }

        private void _writeToXMLDocument(Person person)
        {
            if (!File.Exists(path + xml))
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = "\t";
                settings.CloseOutput = true;
                settings.OmitXmlDeclaration = true;
                using (XmlWriter writer = XmlWriter.Create(path, settings))
                {
                    writer.WriteStartElement("Participants");
                    writer.WriteStartElement("Participant");
                    writer.WriteElementString("Id", person.Id.ToString());
                    writer.WriteElementString("Name", person.Name);
                    writer.WriteElementString("Email", person.Email);
                    writer.WriteEndElement();
                    writer.Flush();
                }
            }
            else
            {
                XDocument xDocument = XDocument.Load(path + xml);
                XElement root = xDocument.Element("Participants");
                IEnumerable<XElement> rows = root.Descendants("Participant");
                XElement firstRow = rows.First();
                firstRow.AddBeforeSelf(
                   new XElement("Student",
                   new XElement("Id", person.Id),
                   new XElement("Name", person.Name),
                   new XElement("Email", person.Email)));
                xDocument.Save(path);
            }
        }
    }
}
