function attachGradientEvents() {
    let hoverElement = document.getElementById("gradient");
    let resultDiv = document.getElementById("result");
    hoverElement.addEventListener("mouseout", () => (resultDiv.textContent = ""));
    hoverElement.addEventListener("mousemove", (e) => {
      let percent = e.offsetX / e.target.clientWidth;
      percent = Math.trunc(percent * 100);
      resultDiv.textContent = percent + "%";
    });
  }