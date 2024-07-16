function solve() {
   let buttonsAdd = Array.from(document.querySelectorAll(".add-product"));
   let textarea = document.querySelector("textarea");
   let shoppingCart = {};
 
   buttonsAdd.forEach((button) => {
     button.addEventListener("click", addProductToCart);
   });
 
   let checkoutBtn = document.querySelector(".checkout");
   checkoutBtn.addEventListener("click", checkOutCart);
 
   function addProductToCart(e) {
     let currProductDiv = e.target.parentElement.parentElement;
     let productName =
       currProductDiv.querySelector(".product-title").textContent;
     let price = Number(
       currProductDiv.querySelector(".product-line-price").textContent
     ).toFixed(2);
     if (!shoppingCart[productName]) {
       shoppingCart[productName] = {
         name: productName,
         price,
         quantity: 0,
       };
     }
     shoppingCart[productName].quantity++;
     textarea.value += `Added ${productName} for ${price} to the cart.\n`;
   }
 
   function checkOutCart() {
     buttonsAdd.forEach((btn) => (btn.disabled = true));
     checkoutBtn.disabled = true;
     let products = Object.keys(shoppingCart).join(", ");
     let totalPrice = Object.values(shoppingCart).reduce(
       (acc, curr) => (acc += curr.price * curr.quantity),
       0
     );
     textarea.value += `You bought ${products} for ${totalPrice.toFixed(2)}.`;
   }
 }