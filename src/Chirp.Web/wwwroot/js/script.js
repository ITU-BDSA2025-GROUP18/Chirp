const firstPageButton = document.querySelector(".first-page")
const previousPageButton = document.querySelector(".previous-page")
const pageIndicator = document.querySelector(".page-indicator")
const nextPageButton = document.querySelector(".next-page")

const pageFieldInput = document.querySelector(".page-field")

const bgVideo = document.getElementById("bg-video")
const bgSource = document.getElementById("bg-source")
const title = document.getElementById("chirptitle")

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

pageFieldInput.addEventListener('keypress', function (e) {
        if (e.key === 'Enter') {
            let pageint = parseInt(pageFieldInput.value);
            if (pageint >= 1 && Number.isInteger(pageint)) {
                document.location = `?page=${pageint}`
            }

            // secret 🤫🤫🤫🤫
            if (pageFieldInput.value === "chirp of duty") {
                let audio = new Audio('../secret/super-secret.mp3#t=00:00:28');
                audio.play()

                bgSource.setAttribute("src", '../secret/bo2.mp4')
                bgSource.setAttribute('type', 'video/mp4')
                document.querySelector(".page").style.opacity = "90%";
                document.querySelector("div.page > h1").style.background = "#636363"
                document.querySelector("div.page > h1").style.borderTop = "5px solid #888"
                document.querySelector("div.page > h1").style.borderBottom = "5px solid #555"
                document.getElementById("Icon1").setAttribute("src", "../secret/cooler_chirp.png");
                title.innerHTML = "Chirp of Duty"
                title.style.textShadow = "0 3px 0 #222"
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
                title.innerHTML = "Written"
            } else if (pageFieldInput.value === "nintendo") {
                let audio = new Audio('../secret/miimusic.mp3');
                audio.loop = true;
                audio.play()

                bgSource.setAttribute("src", '../secret/bowling.mp4')
                bgSource.setAttribute('type', 'video/mp4')
                document.querySelector(".page").style.opacity = "90%";
                document.querySelector("div.page > h1").style.background = "#ffffff"
                document.querySelector("div.page > h1").style.borderTop = "5px solid #888"
                document.querySelector("div.page > h1").style.borderBottom = "5px solid #555"
                document.querySelector("div.page > h1").style.alignItems = "flex-end";
                document.querySelector("div.page > h1").style.padding = "0px 30px";
                document.querySelector("div.page > h1").style.textShadow = "none";
                document.getElementById("Icon1").setAttribute("src", "../secret/inetkun.gif");
                title.innerHTML = "Wii"
                title.style.color = "#8b8b8b"

                bgVideo.style.display = "block"
                bgVideo.playbackRate = 1.5;
                bgVideo.appendChild(bgSource)
                bgVideo.play()
            } else if (pageFieldInput.value === "aqua") {
                document.querySelectorAll("a").forEach(a => a.id = "frutfont")
                document.querySelectorAll("h2").forEach(a => a.id = "frutfont")
                document.querySelectorAll("h3").forEach(a => a.id = "frutfont")
                document.querySelectorAll("#Message").forEach(a => a.id = "frutfont")
                document.querySelectorAll("input").forEach(a => a.id = "frutfont")
                document.querySelectorAll(".follow-button").forEach(a => a.id = "frutfont")
                document.querySelectorAll("div").forEach(a => a.id = "frutfont")

                document.getElementById("Icon1").setAttribute("src", "../secret/frut-chirp.png")

                title.innerHTML = "Frutiger Chirp"
                title.style.textShadow = "0 3px 0 #3b76d4"

                document.querySelector("div.page > h1").style.background = "#89b6ff"
                document.querySelector("div.page > h1").style.borderTop = "5px solid #acccff"
                document.querySelector("div.page > h1").style.borderBottom = "5px solid #74a1e7"

                document.querySelector("div.page h2").style.color = "#daa20b"

                bgSource.setAttribute("src", "../secret/frut.mp4")
                bgSource.setAttribute('type', 'video/mp4')

                bgVideo.style.zoom = "10%"
                bgVideo.style.minWidth = "-webkit-fill-available"
                bgVideo.style.minHeight = "-webkit-fill-available"
                bgVideo.style.width = "unset"
                bgVideo.style.height = "unset"
                bgVideo.style.display = "block"
                bgVideo.appendChild(bgSource)
                bgVideo.play()
            } else if (pageFieldInput.value === "aqua") {
                document.querySelectorAll("a").forEach(a => a.id = "frutfont")
                document.querySelectorAll("h2").forEach(a => a.id = "frutfont")
                document.querySelectorAll("h3").forEach(a => a.id = "frutfont")
                document.querySelectorAll("#Message").forEach(a => a.id = "frutfont")
                document.querySelectorAll("input").forEach(a => a.id = "frutfont")
                document.querySelectorAll(".follow-button").forEach(a => a.id = "frutfont")
                document.querySelectorAll("div").forEach(a => a.id = "frutfont")

                document.getElementById("Icon1").setAttribute("src", "../secret/frut-chirp.png")

                title.innerHTML = "Frutiger Chirp"
                title.style.textShadow = "0 3px 0 #3b76d4"

                document.querySelector("div.page > h1").style.background = "#89b6ff"
                document.querySelector("div.page > h1").style.borderTop = "5px solid #acccff"
                document.querySelector("div.page > h1").style.borderBottom = "5px solid #74a1e7"

                document.querySelector("div.page h2").style.color = "#daa20b"

                bgSource.setAttribute("src", "../secret/frut.mp4")
                bgSource.setAttribute('type', 'video/mp4')

                bgVideo.style.zoom = "10%"
                bgVideo.style.minWidth = "-webkit-fill-available"
                bgVideo.style.minHeight = "-webkit-fill-available"
                bgVideo.style.width = "unset"
                bgVideo.style.height = "unset"
                bgVideo.style.display = "block"
                bgVideo.appendChild(bgSource)
                bgVideo.play()
            } else if (pageFieldInput.value === "ff") {
                document.getElementById("Icon1").setAttribute("src", "../secret/ff-logo.png")

                document.querySelectorAll("a").forEach(a => a.id = "finalfantasyfont")
                document.querySelectorAll("h2").forEach(a => a.id = "finalfantasyfont")
                document.querySelectorAll("h3").forEach(a => a.id = "finalfantasyfont")
                document.querySelectorAll("#Message").forEach(a => a.id = "finalfantasyfont")
                document.querySelectorAll("input").forEach(a => a.id = "finalfantasyfont")
                document.querySelectorAll(".follow-button").forEach(a => a.id = "finalfantasyfont")
                document.querySelectorAll("div").forEach(a => a.id = "finalfantasyfont")

                let audio = new Audio('../secret/ff-maintheme.mp3');
                audio.loop = true;
                audio.play()

                title.innerHTML = "Chirp"
                title.style.scale = "1.0"
                title.style.textShadow = "0 3px 0 #060606"
            }
        }
    }
)

