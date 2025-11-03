const firstPageButton = document.querySelector(".first-page")
const previousPageButton = document.querySelector(".previous-page")
const pageIndicator = document.querySelector(".page-indicator")
const nextPageButton = document.querySelector(".next-page")
const lastPageButton = document.querySelector(".last-page")

const pageFieldInput = document.querySelector(".page-field")

const bgVideo = document.getElementById("bg-video")
const bgSource = document.getElementById("bg-source")

const searchParams = new URLSearchParams(window.location.search);
let currentPage = searchParams.get('page');

(currentPage == null) ? pageIndicator.innerHTML = "Page 1" : pageIndicator.innerHTML = "Page " + currentPage;

firstPageButton.addEventListener("click", () => {
    if (currentPage > 1) {
        document.location = "?page=1"
    }
})

nextPageButton.addEventListener("click", () => {
    if (searchParams.get('page') == null) {
        currentPage += 2;
    } else {
        currentPage++;
    }
    document.location = `?page=${currentPage}`
})

previousPageButton.addEventListener("click", () => {
    if (currentPage > 1) {
        currentPage--
        document.location = `?page=${currentPage}`
    }
})

lastPageButton.addEventListener("click", () => {
    alert("Not yet implemented.")
})

pageFieldInput.addEventListener('keypress', function (e) {
        if (e.key === 'Enter') {
            let pageint = parseInt(pageFieldInput.value);
            if (pageint >= 1 && Number.isInteger(pageint)) {
                document.location = `?page=${pageint}`
            }

            // secret 🤫🤫🤫🤫
            if (pageFieldInput.value === "imma try it out") {
                let audio = new Audio('../secret/super-secret.mp3');
                audio.play()

                bgSource.setAttribute("src", '../secret/bo2.mp4')
                bgSource.setAttribute('type', 'video/mp4')
                document.querySelector("div.page > h1").style.background = "#636363"
                document.getElementById("Icon1").innerHTML = `<img src="../secret/cooler_chirp.png" alt="Cooler chirp xD"/>Chirp of Duty`

                bgVideo.style.display = "block"
                bgVideo.playbackRate = 1.5;
                bgVideo.appendChild(bgSource)
                bgVideo.play()
            } else if (pageFieldInput.value === "okay") {
                bgSource.setAttribute("src", '../secret/OKAY.mp4')
                bgSource.setAttribute('type', 'video/mp4')

                bgVideo.style.display = "block"
                bgVideo.appendChild(bgSource)
                bgVideo.play()
            } else if (pageFieldInput.value === "written") {
                document.getElementById("Icon1").innerHTML = "Written"
            }
        }
    }
)

