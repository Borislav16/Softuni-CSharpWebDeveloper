function solve(numOne, numTwo){
    const factorial = (num) => {
        if(num === 1){
            return 1
        }
        
        return num * factorial(num - 1);

    } 
    first = factorial(numOne) ;
    second =factorial(numTwo)
    return (first / second).toFixed(2);
}

console.log(solve(5, 2));