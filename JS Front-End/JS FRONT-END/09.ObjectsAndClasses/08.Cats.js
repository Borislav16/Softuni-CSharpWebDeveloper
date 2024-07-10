function solve(input) {
    class Cat {
        constructor(name, age) {
            this.name = name;
            this.age = age;
        }

        meow() {
            console.log(`${this.name}, age ${this.age} says Meow`)
        }
    }

    for (let element of input) {
        let info = element.split(' ');
        let name = info[0];
        let age = info[1];
        let cat = new Cat(name, age);
        cat.meow();
    }
}

solve(['Mellow 2', 'Tom 5']);