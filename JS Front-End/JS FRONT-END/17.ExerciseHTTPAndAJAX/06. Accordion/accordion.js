async function solution() {
    let urlArticleList = "http://localhost:3030/jsonstore/advanced/articles/list";
    let urlArticleDetails =
      "http://localhost:3030/jsonstore/advanced/articles/details/";
    let mainElement = document.getElementById("main");
  
    try {
      //get list of articles and their ids
      const listGetResponse = await fetch(urlArticleList);
      let listData = await listGetResponse.json();
      listData.forEach(async (article) => {
        //for each article create a div accordion
        let accordionDivElement = document.createElement("div");
        accordionDivElement.classList.add("accordion");
        //div head within the div accordion
        let headDivElement = document.createElement("div");
        headDivElement.classList.add("head");
        //span with title within the div head
        let spanTitleElement = document.createElement("span");
        spanTitleElement.textContent = article.title;
        //button within the div head
        let buttonElement = document.createElement("button");
        buttonElement.classList.add('button');
        buttonElement.id = article._id;
        buttonElement.textContent = "More";
        headDivElement.appendChild(spanTitleElement);
        headDivElement.appendChild(buttonElement);
        //create extra div element
        let extraDivElement = document.createElement("div");
        extraDivElement.classList.add("extra");
        //create paragraph within the extra div
        let descriptionElement = document.createElement("p");
        //get response for details of said id
        try {
          const detailsGetResponse = await fetch(urlArticleDetails + article._id);
          let detailsData = await detailsGetResponse.json();
          descriptionElement.textContent = detailsData.content;
        } catch (error) {
          console.log(error);
        }
        //append all children accordingly
        extraDivElement.appendChild(descriptionElement);
        accordionDivElement.appendChild(headDivElement);
        accordionDivElement.appendChild(extraDivElement);
        mainElement.appendChild(accordionDivElement);
  
        //attach event listener to the button
        buttonElement.addEventListener('click', async function(){
          if(buttonElement.textContent === 'More'){
              extraDivElement.style.display = 'inline-block';
              buttonElement.textContent = 'Less';
          } else{
              extraDivElement.style.display = 'none';
              buttonElement.textContent = 'More';
          }
        })
      });
    } catch (error) {
      console.log(error);
    }
  }
  
  solution();