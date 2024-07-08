function sort(arr) {

    let sortedArr = [...arr].sort((a, b) => a - b);
    let printArr = [];

    let initialLenght  = sortedArr.length;
    for (let i = 0; i < initialLenght; i++) {
        if (i % 2 === 0) {
            printArr.push(sortedArr.shift());
        } else {
            printArr.push(sortedArr.pop());
        }
    }
    return printArr;
}

sort([1, 65, 3, 52, 48, 63, 31, -3, 18, 56]);