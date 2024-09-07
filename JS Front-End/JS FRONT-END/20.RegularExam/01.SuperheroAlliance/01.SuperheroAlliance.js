function solve(input) {
    const count = input.shift();
    const heroes = {};

    for(let i = 0; i < count; i++) {
        const [name, powers, energy] = input.shift().split('-');
        heroes[name] = {
            powers: powers.split(','),
            energy: Number(energy),
        };
    }

    let command = input.shift();
    while(command != 'Evil Defeated!') {
        command = command.split(' * ');

        if(command[0] == 'Use Power') {
            if(heroes[command[1]].powers.includes(command[2])
            && heroes[command[1]].energy - Number(command[3]) > 0) {

                heroes[command[1]].energy -= Number(command[3]);
                console.log(`${command[1]} has used ${command[2]} and now has ${heroes[command[1]].energy} energy!`);
            } else {
                console.log(`${command[1]} is unable to use ${command[2]} or lacks energy!`);
            }
        } else if (command[0] == 'Train') {
            if(heroes[command[1]].energy < 100) {
                if(heroes[command[1]].energy + Number(command[2]) > 100) {

                    console.log(`${command[1]} has trained and gained ${100 - heroes[command[1]].energy} energy!`);
                    heroes[command[1]].energy = 100;
                } else {
                    console.log(`${command[1]} has trained and gained ${command[2]} energy!`);
                    heroes[command[1]].energy += Number(command[2]);
                }
            } else {
                console.log(`${command[1]} is already at full energy!`);
            }
        } else if (command[0] == 'Learn') {
            if(heroes[command[1]].powers.includes(command[2])) {
                console.log(`${command[1]} already knows ${command[2]}.`);
            } else {
                heroes[command[1]].powers.push(command[2])
                console.log(`${command[1]} has learned ${command[2]}!`);
            }
        }

        command = input.shift();
    }

    Object.keys(heroes)
        .forEach(hero => {
            console.log(`Superhero: ${hero}`);
            console.log(`- Superpowers: ${heroes[hero].powers.join(', ')}`);
            console.log(`- Energy: ${heroes[hero].energy}`);
        });
}

solve([
    "2",
    "Iron Man-Repulsor Beams,Flight-20",
    "Thor-Lightning Strike,Hammer Throw-100",
    "Train * Thor * 20",
    "Use Power * Iron Man * Repulsor Beams * 30",
    "Evil Defeated!"
]

);