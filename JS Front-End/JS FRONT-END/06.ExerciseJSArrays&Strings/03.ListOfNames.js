function sortAndPrint(arr) {
    arr.sort((e1, e2) => e1.localeCompare(e2));
    for (let i = 0; i < arr.length; i++) {
        console.log(`${i+1}.${arr[i]}`)
    }
}