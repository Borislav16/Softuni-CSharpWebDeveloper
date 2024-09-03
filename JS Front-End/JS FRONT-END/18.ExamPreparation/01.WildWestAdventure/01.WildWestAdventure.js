function WildAdventure(input) {
    const possesCount = Number(input.shift());
    const posses = {};

    for(let i = 0; i < possesCount; i++) {
        const [heroName, hp, bullets] = input.shift().split(' ');

        posses[heroName] = {
            hp: hp,
            bullets: bullets,
        };
    }

    const commands= {
        FireShot(heroName, target) {
            if(posses[heroName].bullets > 0) {
                console.log(`${heroName} has successfully hit ${target} and now has ${--posses[heroName].bullets} bullets!`);
            } else {
                console.log(`${heroName} doesn't have enough bullets to shoot at ${target}!`)
            }
        },
        TakeHit(heroName, damage, attacker ) {
            if(posses[heroName].hp - Number(damage) > 0) {
                posses[heroName].hp = posses[heroName].hp - Number(damage);
                console.log(`${heroName} took a hit for ${damage} HP from ${attacker} and now has ${posses[heroName].hp} HP!`)
            } else {
                console.log(`${heroName} was gunned down by ${attacker}!`);
                delete posses[heroName];
            }
        },
        Reload(heroName) {
            if(posses[heroName].bullets < 6) {
                console.log(`${heroName} reloaded ${6 - posses[heroName].bullets} bullets!`);
                posses[heroName].bullets = 6;
            } else {
                console.log(`${heroName}'s pistol is fully loaded!`);
            }
        },
        PatchUp(heroName, hp) {
            if(posses[heroName].hp < 100) {
                console.log(`${heroName} patched up and recovered ${hp} HP!`);
                posses[heroName].hp = Math.min(posses[heroName].hp + Number(hp), 100);
            } else {
                console.log(`${heroName} is in full health!`);
            }
        }
    };

    let command;
    while((command = input.shift()) != "Ride Off Into Sunset") {
        const [functionName, heroName, ...args] = command.split(' - ');
        commands[functionName](heroName, ...args);
    }

    Object.keys(posses)
        .forEach(heroName => {
            console.log(heroName);
            console.log(` HP: ${posses[heroName].hp}`);
            console.log(` Bullets: ${posses[heroName].bullets}`);
        })
}