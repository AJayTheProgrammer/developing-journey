# Anthony Jordan

#Get the proper imports
import requests
from bs4 import BeautifulSoup

#Initialize the url, request, text using an html parser, and the data using
#the find all method of the div tags and getting the numbers
url = "https://www.worldometers.info/coronavirus/"
r = requests.get(url)
s = BeautifulSoup(r.text, "html.parser")
data = s.find_all("div", class_="maincounter-number")

#Prints all the info
print("Worldwide Cases: ", data[0].text.strip())
print("Total Deaths: ", data[1].text.strip())
print("Total Recovered: ", data[2].text.strip())