#Anthony Jordan

import random

number = random.randint(1,15)
chances = 0

print("Pick a number between 1-15:")
while chances < 5:
    guess = int(input("Enter your number:"))

    if guess == number:
        print("Yay! you picked the correct number.")
        exit()
    elif guess< number:
        print("Your guess was too low, try again!:")
    else:
        print("Your guess was too high, try again!")
    chances+=1
    print("Chances Left: ", 5-chances)
print("You lose! The correct number is : ", number)