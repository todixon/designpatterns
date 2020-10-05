using System;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml;

using LoadFromSpreadsheet.Models;

using Newtonsoft.Json;

using Serilog;


using OfficeOpenXml;
using System.Linq;
using EPPlus.DataExtractor;
using System.Data.SqlClient;
using Dapper;

namespace LoadFromSpreadsheet
{
    public partial class Parser : IParser
    {
        private readonly Application _appsettings;


        public Parser(Application appsettings)
        {
            _appsettings = appsettings;
        }


        public void Parse()
        {
            string first = "", last = "";

            try
            {
                Log.Logger.Information($"Processing file");
                using SqlConnection db = new SqlConnection(_appsettings.ImportDB);
                // Verify appsettings location for spreadsheet before starting!
                using ExcelPackage package = new ExcelPackage(new FileInfo(_appsettings.ImportSpreadsheet));

                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                int columns = worksheet.Dimension.End.Column;

                var contactlist = worksheet
                    .Extract<Spreadsheet>()
                    .WithProperty(p => p.Id, "A")
                    .WithProperty(p => p.FirstName, "B")
                    .WithProperty(p => p.LastName, "C")
                    .WithProperty(p => p.PhoneNumber, "D")
                    .WithProperty(p => p.Address, "E")
                    .WithProperty(p => p.City, "F")
                    .WithProperty(p => p.State, "G")
                    .WithProperty(p => p.Zip, "H")
                    .WithProperty(p => p.EmailAddress, "I")
                    .WithProperty(p => p.Company, "J")
                    .GetData(1, worksheet.Dimension.End.Row)
                    .ToList();

                int count = 1;
                foreach (var c in contactlist)
                {
                    if (count == 1)
                    {
                        count++;
                        continue;
                    }

                    Log.Information(@$"Processing {count}: {c.FirstName} {c.LastName}");

                    var result = db.Query<Spreadsheet>($@"select * from Contacts where firstname like '{first}' and lastname like '{last}' ");

                    foreach (var a in result)
                    {
                        //if (a.Id == 0)
                        //{
                        //    // db.Execute($@"Update address set ifg_npn='{c.ContactNPN}' where contactid = '{a.ContactId}'");
                        //}
                    }

                    count++;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error parsing file");
            }

        }
    }

}
