function addItem() {
    debugger;
    let newItemText = document.querySelector("#newItemText").value;
    let newLi = document.createElement("li");
    newLi.textContent = newItemText;
    let deleteAnchor = document.createElement("a");
    deleteAnchor.href = "#";
    deleteAnchor.text = "[Delete]";
    deleteAnchor.addEventListener("click", () => {
      deleteAnchor.parentElement.remove();
    });
    newLi.appendChild(deleteAnchor);
    document.querySelector("#items").appendChild(newLi);
  }