function reveal(words, template) {
    let wordsAsArr = words.split(", ");
    let templateAsArr = template.split(" ");

    for (let word of wordsAsArr) {
        for (let i = 0; i < templateAsArr.length; i++) {
            if(templateAsArr[i].includes("*") && templateAsArr[i].length === word.length){
                templateAsArr[i]=word;
            }
        }
    }
    console.log(templateAsArr.join(" "))
}
