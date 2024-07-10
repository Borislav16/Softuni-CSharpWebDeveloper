function printAddress(input) {
    let addressBook = input.reduce((acc, curr) => {
        const [name, address] = curr.split(":");
        acc[name] = address;
        return acc;
    }, {});

    Object.entries(addressBook)
        .sort()
        .forEach(([key, value]) => {
            console.log(`${key} -> ${value}`);
        });
}