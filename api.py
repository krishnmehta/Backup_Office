import requests
# import speech_recognition as sr
# import pyttsx3
from geopy.geocoders import Nominatim
def get_coordinates(city):
    geolocator = Nominatim(user_agent="geoapiExercises")
    location = geolocator.geocode(city)
    if location is not None:
        latitude = location.latitude
        longitude = location.longitude
        return latitude, longitude
    else:
        return None
def get_shabbat_timings(latitude, longitude):
    url = f"https://www.hebcal.com/shabbat?cfg=json&geo=pos&latitude={latitude}&longitude={longitude}"
    response = requests.get(url)
    data = response.json()
    candle_lighting = data["items"][0]["title"]
    havdalah = data["items"][3]["title"]
    return f"{candle_lighting}, {havdalah}."


while True:
    city = input("\nEnter the name of your city or exit to close the program: ")
    #i -= 1
    if city == "Exit" or city == "exit" or city == "EXIT":
        break
    coordinates = get_coordinates(city)
    if coordinates is not None:
        latitude, longitude = coordinates
        shabbat_timings = get_shabbat_timings(latitude,longitude)
        print(f"{shabbat_timings}")
        # speak(shabbat_timings)       
    else:
        print(f"Could not find coordinates for {city}")
        # speak(f"Could not find coordinates for {city}")