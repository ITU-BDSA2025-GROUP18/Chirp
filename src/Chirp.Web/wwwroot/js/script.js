const nextPageButton = document.querySelector(".next-page")
const firstPageButton = document.querySelector(".first-page")

let currentPage = 1;

nextPageButton.addEventListener("click", () => {
    currentPage++;
    console.log(currentPage);
})

firstPageButton.addEventListener("click", () => {
    currentPage = 1;
})
