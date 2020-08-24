#Anthony Jordan
# initialize the SENTENCE as a string type.
test_string = "There are 3 dogs and 2 cats by the street"


# printing the original string value to show back to user for comparison.
print("The original string : " + test_string)


# using List comprehension + isdigit() +split() to get the numbers from the sentence.
res = [int(i) for i in test_string.split() if i.isdigit()]


# print the numbers found in the sentence
print("The numbers list is : " + str(res))
