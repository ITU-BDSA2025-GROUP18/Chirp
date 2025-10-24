const nextPageButton = document.querySelector(".next-page")
const firstPageButton = document.querySelector(".first-page")
const previousPageButton = document.querySelector(".previous-page")
const pageIndicator = document.querySelector(".page-indicator")

const searchParams = new URLSearchParams(window.location.search);
let currentPage = searchParams.get('page');

(currentPage == null) ? pageIndicator.innerHTML = "1" : pageIndicator.innerHTML = currentPage;

nextPageButton.addEventListener("click", () => {
    if (searchParams.get('page') == null) {
        currentPage += 2;
    } else {
        currentPage++;
    }
    document.location = `/?page=${currentPage}`
})

previousPageButton.addEventListener("click", () => {
    if (currentPage > 1) {
        currentPage--
        document.location = `/?page=${currentPage}`
    }
})

firstPageButton.addEventListener("click", () => {
    if (currentPage > 1) {
        document.location = "/?page=1"
    }
})
