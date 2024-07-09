function print(word, text) {
    let newWordsArr = text.toLowerCase().split(" ");

    for (let currentWordToCheck of newWordsArr) {
        if (currentWordToCheck === word.toLowerCase()) {
            console.log(word);
            return;
        }

    }
    console.log(`${word} not found!`)
}
