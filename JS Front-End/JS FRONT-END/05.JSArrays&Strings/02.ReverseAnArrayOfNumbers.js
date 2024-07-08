function reverse(range, array){
    let a = [];
    for(let i = range- 1;i >= 0; i--){
        a.unshift(array[i]);
    }
    a.reverse()
    console.log(a.join(" "));
}

reverse(3, [10, 20, 30, 40, 50])
