num = 1234
answer = 0


while(num > 0):
    remainder = num%10
    answer = (answer*10)+remainder
    num = num//10

print(answer)