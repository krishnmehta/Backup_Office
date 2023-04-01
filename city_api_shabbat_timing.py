import requests
import speech_recognition as sr
import pyttsx3
from geopy.geocoders import Nominatim
# def get_coordinates(city):
#     geolocator = Nominatim(user_agent="geoapiExercises")
#     location = geolocator.geocode(city)
#     if location is not None:
#         latitude = location.latitude
#         longitude = location.longitude
#         return latitude, longitude
#     else:
#         return None
def get_shabbat_timings(city):
    url = f"https://www.hebcal.com/shabbat?cfg=json&city={city}&M=on"
    response = requests.get(url)
    data = response.json()
    candle_lighting = data["items"][0]["title"]
    havdalah = data["items"][3]["title"]
    return f"{candle_lighting}, {havdalah}."
i=6
while i>0:
    # text = listen()
    #text = text.lower()
    city = input("Enter the name of your city: ")
    i -= 1
    #coordinates = get_coordinates(city)
    
    #latitude, longitude = coordinates
    shabbat_timings = get_shabbat_timings(city)
    print(f"{shabbat_timings}")
    print()
        # speak(shabbat_timings)
    

