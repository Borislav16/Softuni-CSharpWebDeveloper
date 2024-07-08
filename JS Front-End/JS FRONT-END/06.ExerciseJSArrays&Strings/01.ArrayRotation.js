function rotation(arr, rotations) {
    rotations = rotations % arr.length;
    if (rotations < 0) {
        rotations += arr.length;
    }

    for (let i = 0; i < rotations; i++) {

        let firstElement = arr.shift();
        arr.push(firstElement);
    }

    console.log(arr.join(' '));
}

rotation([51, 47, 32, 61, 21], 2);