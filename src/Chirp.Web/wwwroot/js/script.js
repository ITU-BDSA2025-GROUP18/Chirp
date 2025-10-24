const nextPageButton = document.querySelector(".next-page")
const firstPageButton = document.querySelector(".first-page")
const previousPageButton = document.querySelector(".previous-page")

let currentPage = 1;

nextPageButton.addEventListener("click", () => {
    currentPage++;
    console.log(currentPage);
})

previousPageButton.addEventListener("click", () => {
    if (currentPage > 1) {
        currentPage--
    }
    console.log(currentPage);
})

firstPageButton.addEventListener("click", () => {
    currentPage = 1;
})
