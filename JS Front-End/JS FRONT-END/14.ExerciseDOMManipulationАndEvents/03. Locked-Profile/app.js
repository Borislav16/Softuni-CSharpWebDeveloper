function lockedProfile() {
    let persons = document.querySelectorAll(".profile");
  
    for (const person of persons) {
      let showMoreButton = person.querySelector("button");
      showMoreButton.addEventListener("click", showMore);
    }
    function showMore({ target }) {
      let parrentElement = target.parentElement;
      let hiddenInfo = parrentElement.querySelector("div");
      let unlockButton = parrentElement.getElementsByTagName("input")[1];
      let buttonShowHide = parrentElement.querySelector("button");
      if (unlockButton.checked) {
        if (buttonShowHide.textContent == "Show more") {
          hiddenInfo.style.display = "block";
          buttonShowHide.textContent = "Hide it";
        } else {
          hiddenInfo.style.display = "";
          buttonShowHide.textContent = "Show more";
        }
      }
    }
  }