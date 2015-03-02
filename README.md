This project is meant to take in addresses and convert them into latitude and longitude.
I'm using both Python and C# independently to extract data from a CSV file.

Python (install via pip):
pip install geopy
https://github.com/geopy/geopy

C# (install via nuget):
Install-Package LinqToExcel
http://www.codeproject.com/Articles/659643/Csharp-Query-Excel-and-CSV-Files-Using-LinqToExcel

Not using (due to outdated version of visual studio):
Install-Package Geocoding.net
https://github.com/chadly/Geocoding.net


The addresses are in the 8th column of the CSV. Adjust row[7] accordingly for your CSV file.