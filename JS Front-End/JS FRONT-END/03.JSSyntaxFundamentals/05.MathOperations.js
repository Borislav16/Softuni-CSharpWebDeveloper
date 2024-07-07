function solve(first, second, symbol){
    switch(symbol)
    {
        case '+': console.log(first + second); break;
        case '*': console.log(first * second); break;
        case '%': console.log(first % second); break;
        case '/': console.log(first / second); break;
        case '-': console.log(first - second); break;
        case '**': console.log(first ** second); break;
    }
}

solve(5, 5, '*');
solve(10, 5, '/');