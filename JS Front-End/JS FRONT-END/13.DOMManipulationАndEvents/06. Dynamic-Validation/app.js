function validate() {
    let emailInput = document.querySelector("#email");
    emailInput.addEventListener("change", (e) => {
      let regEx = /^[a-z\-\.]+@([a-z-]+\.)+[a-z-]{2,}$/;
      if (regEx.test(e.target.value)) {
        e.target.classList.remove("error");
      } else {
        e.target.classList.add("error");
      }
    });
  }