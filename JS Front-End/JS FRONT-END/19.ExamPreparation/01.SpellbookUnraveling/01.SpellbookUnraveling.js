function solve(input) {
    let spell = input.shift();

    let command = input.shift();
    while(command != 'End') {

        command = command.split('!');
        if(command[0] == 'RemoveEven') {
            let result = '';

            for (let i = 0; i < spell.length; i++) {
                if (i % 2 === 0) {  // Even index
                    result += spell[i];
                }
            }

            spell = result;

            console.log(spell);
        } else if (command[0] == 'TakePart'){
            spell = spell.slice(Number(command[1]), command[2]);

            console.log(spell);
        } else if (command[0] == 'Reverse') {
            let subStr = command[1];
            let startIndex = spell.indexOf(subStr);
            if (startIndex !== -1) {
                let endIndex = startIndex + subStr.length;
                spell = spell.slice(0, startIndex) + spell.slice(endIndex);
        
                let reverseSubstring = subStr.split('').reverse().join('');
        
                spell += reverseSubstring;
        
                console.log(spell);
            } else {
                console.log('Error');
            }
        }

        command = input.shift();
    }
        
    console.log(`The concealed spell is: ${spell}`);
}

solve((["hZwemtroiui5tfone1haGnanbvcaploL2u2a2n2i2m", 
    "TakePart!31!42",
    "RemoveEven",
    "Reverse!anim",
    "Reverse!sad",
    "End"])
);