function toggle() {
    let button = document.getElementsByClassName("button")[0];
    let extra = document.getElementById("extra")
    let buttonText = button.textContent;

    if(buttonText == "More"){
        extra.style.display = "block";
        button.textContent = "Less";    
    }else{
        extra.style.display = "none";
        button.textContent = "More"
    }
}