function subtract() {
    let first = document.getElementById("firstNumber").value; 
    let second = document.getElementById("secondNumber").value; 
    let result = document.getElementById("result");
    result.innerText = Number(first) - Number(second);
}