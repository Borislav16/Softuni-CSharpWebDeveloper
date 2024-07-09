function sum(number) {
    let sumOdd = 0;
    let sumEven = 0;
    while (number > 0) {
        let currentDigit = number % 10;
        if (currentDigit % 2 == 0) {
            sumEven += currentDigit;
        } else {
            sumOdd += currentDigit;
        }
        number = Math.trunc(number / 10);
    }
    console.log("Odd sum = " + sumOdd + ", Even sum = " + sumEven);

}