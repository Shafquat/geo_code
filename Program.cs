using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace geo_code
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Queries excel sheet */
            var excel = new LinqToExcel.ExcelQueryFactory();
            excel.FileName = @"C:\Users\sarefeen\Documents\Visual Studio 2010\Projects\geo_code\Parking_Tags_Data_2014_3.csv";

            var sheet = from x in excel.Worksheet("Parking_Tags_Data_2014_3")
                        select x;

            foreach (var a in sheet)
            {
                var tag_number_masked = a["tag_number_masked"];
                var date_of_infraction = a["date_of_infraction"].ToString().Substring(4,2) + "/" + a["date_of_infraction"].ToString().Substring(6,2) + "/" + a["date_of_infraction"].ToString().Substring(0,4);
                var infraction_code = a["infraction_code"];
                var infraction_description = a["infraction_description"];
                var set_fine_amount = a["set_fine_amount"];
                var time_of_infraction = a["time_of_infraction"].ToString().Insert(2, ":");
                var location1 = a["location1"];
                var location2 = a["location2"];
                var location3 = a["location3"];
                var location4 = a["location4"];
                var province = a["province"];

                System.Threading.Thread.Sleep(200); //used to delay API so Exception does not occur
                XmlDocument xDoc = new XmlDocument();
                var address = location2 + ", Toronto";
                var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(address));
                xDoc.Load(requestUri);
                try
                {
                    if (location3 == "")
                    {
                        var lat = xDoc.SelectSingleNode("/GeocodeResponse/result/geometry/location/lat").InnerText;
                        var longitude = xDoc.SelectSingleNode("/GeocodeResponse/result/geometry/location/lng").InnerText;
                        var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}", tag_number_masked, date_of_infraction,
                            infraction_code, infraction_description, set_fine_amount, time_of_infraction, location1, location2, location3,
                            location4, province, lat, longitude);
                        Console.WriteLine(newLine);
                        using (var destination = File.AppendText(@"C:\Users\sarefeen\Documents\Visual Studio 2010\Projects\geo_code\Parking_Tags_Data3.csv"))
                        {
                            destination.WriteLine(newLine);
                        }
                    }
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine("No location discovered");
                }
            }
        }
    }
}
