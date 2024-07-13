function search() {
   let cities = document.querySelectorAll("li");
   let input = document.querySelector("input").value;
   let result = document.getElementById("result");
   let matches = 0;
   for(let city of cities){
      if(city.innerHTML.includes(input)){
         city.style = "";
         city.style.textDecoration = "";
         matches++;
         city.style = "bolt";
         city.style.textDecoration = "underline";
      }
   }
   result.textContent = `${matches} matches found`;
}
// judge 100/100
// function search() {

//    const towns = document.querySelectorAll('#towns li');
//    const input = document.getElementById('searchText').value;

//    let count = 0;

//    for (const town of towns) {

//        if (town.textContent.toLowerCase().includes(input.toLowerCase())) {
//            count++;

//            town.style.fontWeight = 'bold';
//            town.style.textDecoration = 'underline';
//        }

//    }

//    document.getElementById('result').textContent = `${count} matches found`;
// }
