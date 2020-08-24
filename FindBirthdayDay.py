import datetime
import calendar


def findDay(date):
    born = datetime.datetime.strptime(date, '%m %d %Y')
    day = born.weekday()
    return calendar.day_name[day]


date = '01 02 1999'
print(findDay(date))
