from geopy.geocoders import Nominatim
from geopy.exc import GeocoderTimedOut
import csv

with open('path_to_input_csv','r') as csvinput:
    with open('path_to_new_output_csv', 'w') as csvoutput:
        spamreader = csv.reader(csvinput, delimiter=',', quotechar='|')
        writer = csv.writer(csvoutput, lineterminator='\n')
        for row in spamreader:
            geolocator = Nominatim()
            try:
                location = geolocator.geocode(row[7] + ", Toronto") #I'm using Toronto, change your city respectively
                try:
                    row.append(location.latitude)
                    row.append(location.longitude)
                    writer.writerow(row)
                except(AttributeError):
                    print("Error with location")
            except GeocoderTimedOut as e:
                print("Error: geocode failed on input")