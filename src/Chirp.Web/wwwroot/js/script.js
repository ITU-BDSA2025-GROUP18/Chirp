const firstPageButton = document.querySelector(".first-page")
const previousPageButton = document.querySelector(".previous-page")
const pageIndicator = document.querySelector(".page-indicator")
const nextPageButton = document.querySelector(".next-page")
const lastPageButton = document.querySelector(".last-page")

const pageFieldInput = document.querySelector(".page-field")

const searchParams = new URLSearchParams(window.location.search);
let currentPage = searchParams.get('page');

(currentPage == null) ? pageIndicator.innerHTML = "1" : pageIndicator.innerHTML = currentPage;

firstPageButton.addEventListener("click", () => {
    if (currentPage > 1) {
        document.location = "/?page=1"
    }
})

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

pageFieldInput.addEventListener('keypress', function (e) {
        if (e.key === 'Enter') {
            let pageint = parseInt(pageFieldInput.value);
            if (pageint >= 1 && Number.isInteger(pageint)) {
                document.location = `/?page=${pageint}`
            }

            // secret 🤫🤫🤫🤫
            if (pageFieldInput.value === "imma try it out") {
                let audio = new Audio('js/super-secret.mp3');
                audio.play()
            }
        }
    }
)

lastPageButton.addEventListener("click", () => {
    alert("Not yet implemented :P\n\nAnyways here is the bee movie:\n\n" +
        "According to all known laws of aviation, there is no way a bee should be able to fly.\n" +
        "Its wings are too small to get its fat little body off the ground.\n" +
        "The bee, of course, flies anyway because bees don't care what humans think is impossible.\n" +
        "Yellow, black. Yellow, black. Yellow, black. Yellow, black.\n" +
        "Ooh, black and yellow!\n" +
        "Let's shake it up a little.\n" +
        "Barry! Breakfast is ready!\n" +
        "Coming!\n" +
        "Hang on a second.\n" +
        "Hello?\n" +
        "Barry?\n" +
        "Adam?\n" +
        "Can you believe this is happening?\n" +
        "I can't.\n" +
        "I'll pick you up.\n" +
        "Looking sharp.\n" +
        "Use the stairs, Your father paid good money for those.\n" +
        "Sorry. I'm excited.\n" +
        "Here's the graduate.\n" +
        "We're very proud of you, son.\n" +
        "A perfect report card, all B's.\n" +
        "Very proud.\n" +
        "Ma! I got a thing going here.\n" +
        "You got lint on your fuzz.\n" +
        "Ow! That's me!\n" +
        "Wave to us! We'll be in row 118,000.\n" +
        "Bye!\n" +
        "Barry, I told you, stop flying in the house!\n" +
        "Hey, Adam.\n" +
        "Hey, Barry.\n" +
        "Is that fuzz gel?\n" +
        "A little. Special day, graduation.\n" +
        "Never thought I'd make it.\n" +
        "Three days grade school, three days high school.\n" +
        "Those were awkward.\n" +
        "Three days college. I'm glad I took a day and hitchhiked around The Hive.\n" +
        "You did come back different.\n" +
        "Hi, Barry. Artie, growing a mustache? Looks good.\n" +
        "Hear about Frankie?\n" +
        "Yeah.\n" +
        "You going to the funeral?\n" +
        "No, I'm not going.\n" +
        "Everybody knows, sting someone, you die.\n" +
        "Don't waste it on a squirrel.\n" +
        "Such a hothead.\n" +
        "I guess he could have just gotten out of the way.\n" +
        "I love this incorporating an amusement park into our day.\n" +
        "That's why we don't need vacations.\n" +
        "Boy, quite a bit of pomp under the circumstances.\n" +
        "Well, Adam, today we are men.\n" +
        "We are!\n" +
        "Bee-men.\n" +
        "Amen!\n" +
        "Hallelujah!\n" +
        "Students, faculty, distinguished bees,\n" +
        "please welcome Dean Buzzwell.\n" +
        "Welcome, New Hive City graduating class of 9:15.\n" +
        "That concludes our ceremonies And begins your career at Honex Industries!\n" +
        "Will we pick our job today?\n" +
        "I heard it's just orientation.\n" +
        "Heads up! Here we go.\n" +
        "Keep your hands and antennas inside the tram at all times.\n" +
        "Wonder what it'll be like?\n" +
        "A little scary.\n" +
        "Welcome to Honex, a division of Honesco and a part of the Hexagon Group.\n" +
        "This is it!\n" +
        "Wow.\n" +
        "Wow.\n" +
        "We know that you, as a bee, have worked your whole life to get to the point where you can work for your whole life.\n" +
        "Honey begins when our valiant Pollen Jocks bring the nectar to The Hive.\n" +
        "Our top-secret formula is automatically color-corrected, scent-adjusted and bubble-contoured into this soothing sweet syrup with its distinctive golden glow you know as... Honey!\n" +
        "That girl was hot.\n" +
        "She's my cousin!\n" +
        "She is?\n" +
        "Yes, we're all cousins.\n" +
        "Right. You're right.\n" +
        "At Honex, we constantly strive to improve every aspect of bee existence.\n" +
        "These bees are stress-testing a new helmet technology.\n" +
        "What do you think he makes?\n" +
        "Not enough.\n" +
        "Here we have our latest advancement, the Krelman.\n" +
        "What does that do?\n" +
        "Catches that little strand of honey that hangs after you pour it.\n" +
        "Saves us millions.\n" +
        "Can anyone work on the Krelman?\n" +
        "Of course. Most bee jobs are small ones.\n" +
        "But bees know that every small job, if it's done well, means a lot.\n" +
        "But choose carefully because you'll stay in the job you pick for the rest of your life.\n" +
        "The same job the rest of your life? I didn't know that.\n" +
        "What's the difference?\n" +
        "You'll be happy to know that bees, as a species, haven't had one day off in 27 million years.\n" +
        "So you'll just work us to death?\n" +
        "We'll sure try.\n" +
        "Wow! That blew my mind!\n" +
        "\"What's the difference?\"\n" +
        "How can you say that?\n" +
        "One job forever?\n" +
        "That's an insane choice to have to make.\n" +
        "I'm relieved. Now we only have to make one decision in life.\n" +
        "But, Adam, how could they never have told us that?\n" +
        "Why would you question anything? We're bees.\n" +
        "We're the most perfectly functioning society on Earth.\n" +
        "You ever think maybe things work a little too well here?\n" +
        "Like what? Give me one example.\n" +
        "I don't know. But you know what I'm talking about.\n" +
        "Please clear the gate. Royal Nectar Force on approach.\n" +
        "Wait a second. Check it out.\n" +
        "Hey, those are Pollen Jocks!\n" +
        "Wow.\n" +
        "I've never seen them this close.\n" +
        "They know what it's like outside The Hive.\n" +
        "Yeah, but some don't come back.\n" +
        "Hey, Jocks!\n" +
        "Hi, Jocks!\n" +
        "You guys did great!\n" +
        "You're monsters!\n" +
        "You're sky freaks! I love it! I love it!\n" +
        "I wonder where they were.\n" +
        "I don't know.\n" +
        "Their day's not planned.\n" +
        "Outside The Hive, flying who knows where, doing who knows what.\n" +
        "You can't just decide to be a Pollen Jock. You have to be bred for that.\n" +
        "Right.\n" +
        "Look. That's more pollen than you and I will see in a lifetime.\n" +
        "It's just a status symbol.\n" +
        "Bees make too much of it.\n" +
        "Perhaps. Unless you're wearing it and the ladies see you wearing it.\n" +
        "Those ladies?\n" +
        "Aren't they our cousins too?\n" +
        "Distant. Distant.\n" +
        "Look at these two.\n" +
        "Couple of Hive Harrys.\n" +
        "Let's have fun with them.\n" +
        "It must be dangerous being a Pollen Jock.\n" +
        "Yeah. Once a bear pinned me against a mushroom!\n" +
        "He had a paw on my throat, and with the other, he was slapping me!\n" +
        "Oh, my!\n" +
        "I never thought I'd knock him out.\n" +
        "What were you doing during this?\n" +
        "Trying to alert the authorities.\n" +
        "I can autograph that.\n" +
        "A little gusty out there today, wasn't it, comrades?\n" +
        "Yeah. Gusty.\n" +
        "We're hitting a sunflower patch six miles from here tomorrow.\n" +
        "Six miles, huh?\n" +
        "Barry!\n" +
        "A puddle jump for us, but maybe you're not up for it.\n" +
        "Maybe I am.\n" +
        "You are not!\n" +
        "We're going 0900 at J-Gate.\n" +
        "What do you think, buzzy-boy?\n" +
        "Are you bee enough?\n" +
        "I might be. It all depends on what 0900 means.\n" +
        "Hey, Honex!\n" +
        "Dad, you surprised me.\n" +
        "You decide what you're interested in?\n" +
        "Well, there's a lot of choices.\n" +
        "But you only get one.\n" +
        "Do you ever get bored doing the same job every day?\n" +
        "Son, let me tell you about stirring.\n" +
        "You grab that stick, and you just move it around, and you stir it around.\n" +
        "You get yourself into a rhythm.\n" +
        "It's a beautiful thing.\n" +
        "You know, Dad, the more I think about it,\n" +
        "maybe the honey field just isn't right for me.\n" +
        "You were thinking of what, making balloon animals?\n" +
        "That's a bad job for a guy with a stinger.\n" +
        "Janet, your son's not sure he wants to go into honey!\n" +
        "Barry, you are so funny sometimes.\n" +
        "I'm not trying to be funny.\n" +
        "You're not funny! You're going into honey. Our son, the stirrer!\n" +
        "You're gonna be a stirrer?\n" +
        "No one's listening to me!\n" +
        "Wait till you see the sticks I have.\n" +
        "I could say anything right now.\n" +
        "I'm gonna get an ant tattoo!\n" +
        "Let's open some honey and celebrate!\n" +
        "Maybe I'll pierce my thorax. Shave my antennae. Shack up with a grasshopper. Get a gold tooth and call everybody \"dawg\"!\n" +
        "I'm so proud.\n" +
        "We're starting work today!\n" +
        "Today's the day.\n" +
        "Come on! All the good jobs will be gone.\n" +
        "Yeah, right.\n" +
        "Pollen counting, stunt bee, pouring, stirrer, front desk, hair removal...\n" +
        "Is it still available?\n" +
        "Hang on. Two left!\n" +
        "One of them's yours! Congratulations!\n" +
        "Step to the side.\n" +
        "What'd you get?\n" +
        "Picking crud out. Stellar!\n" +
        "Wow!\n" +
        "Couple of newbies?\n" +
        "Yes, sir! Our first day! We are ready!\n" +
        "Make your choice.\n" +
        "You want to go first?\n" +
        "No, you go.\n" +
        "Oh, my. What's available?\n" +
        "Restroom attendant's open, not for the reason you think.\n" +
        "Any chance of getting the Krelman?\n" +
        "Sure, you're on.\n" +
        "I'm sorry, the Krelman just closed out.\n" +
        "Wax monkey's always open.\n" +
        "The Krelman opened up again.\n" +
        "What happened?\n" +
        "A bee died. Makes an opening. See? He's dead. Another dead one.\n" +
        "Deady. Deadified. Two more dead.\n" +
        "Dead from the neck up. Dead from the neck down. That's life!\n" +
        "Oh, this is so hard!\n" +
        "Heating, cooling, stunt bee, pourer, stirrer, humming, inspector number seven, lint coordinator, stripe supervisor, mite wrangler.\n" +
        "Barry, what do you think I should... Barry?\n" +
        "Barry!\n" +
        "All right, we've got the sunflower patch in quadrant nine...\n" +
        "What happened to you?\n" +
        "Where are you?\n" +
        "I'm going out.\n" +
        "Out? Out where?\n" +
        "Out there.\n" +
        "Oh, no!\n" +
        "I have to, before I go to work for the rest of my life.\n" +
        "You're gonna die! You're crazy! Hello?\n" +
        "Another call coming in.\n" +
        "If anyone's feeling brave, there's a Korean deli on 83rd that gets their roses today.\n" +
        "Hey, guys.\n" +
        "Look at that.\n" +
        "Isn't that the kid we saw yesterday?\n" +
        "Hold it, son, flight deck's restricted.\n" +
        "It's OK, Lou. We're gonna take him up.\n" +
        "Really? Feeling lucky, are you?\n" +
        "Sign here, here. Just initial that.\n" +
        "Thank you.\n" +
        "OK.\n" +
        "You got a rain advisory today, and as you all know, bees cannot fly in rain.\n" +
        "So be careful. As always, watch your brooms, hockey sticks, dogs, birds, bears and bats.\n" +
        "Also, I got a couple of reports of root beer being poured on us.\n" +
        "Murphy's in a home because of it, babbling like a cicada!\n" +
        "That's awful.\n" +
        "And a reminder for you rookies, bee law number one, absolutely no talking to humans!\n" +
        " All right, launch positions!\n" +
        "Buzz, buzz, buzz, buzz! Buzz, buzz, buzz, buzz! Buzz, buzz, buzz, buzz!\n" +
        "Black and yellow!\n" +
        "Hello!\n" +
        "You ready for this, hot shot?\n" +
        "Yeah. Yeah, bring it on.\n" +
        "Wind, check.\n" +
        "Antennae, check.\n" +
        "Nectar pack, check.\n" +
        "Wings, check.\n" +
        "Stinger, check.\n" +
        "Scared out of my shorts, check.\n" +
        "OK, ladies,\n" +
        "let's move it out!\n" +
        "Pound those petunias, you striped stem-suckers!\n" +
        "All of you, drain those flowers!\n" +
        "Wow! I'm out!\n" +
        "I can't believe I'm out!\n" +
        "So blue.\n" +
        "I feel so fast and free!\n" +
        "Box kite!\n" +
        "Wow!\n" +
        "Flowers!\n" +
        "This is Blue Leader, We have roses visual.\n" +
        "Bring it around 30 degrees and hold.\n" +
        "Roses!\n" +
        "30 degrees, roger. Bringing it around.\n" +
        "Stand to the side, kid.\n" +
        "It's got a bit of a kick.\n" +
        "That is one nectar collector!\n" +
        "Ever see pollination up close?\n" +
        "No, sir.\n" +
        "I pick up some pollen here, sprinkle it over here. Maybe a dash over there, a pinch on that one.\n" +
        "See that? It's a little bit of magic.\n" +
        "That's amazing. Why do we do that?\n" +
        "That's pollen power. More pollen, more flowers, more nectar, more honey for us.\n" +
        "Cool.\n" +
        "I'm picking up a lot of bright yellow, Could be daisies, Don't we need those?\n" +
        "Copy that visual.\n" +
        "Wait. One of these flowers seems to be on the move.\n" +
        "Say again? You're reporting a moving flower?\n" +
        "Affirmative.\n" +
        "That was on the line!\n" +
        "This is the coolest. What is it?\n" +
        "I don't know, but I'm loving this color.\n" +
        "It smells good.\n" +
        "Not like a flower, but I like it.\n" +
        "Yeah, fuzzy.\n" +
        "Chemical-y.\n" +
        "Careful, guys. It's a little grabby.\n" +
        "My sweet lord of bees!\n" +
        "Candy-brain, get off there!\n" +
        "Problem!\n" +
        "Guys!\n" +
        "This could be bad.\n" +
        "Affirmative.\n" +
        "Very close.\n" +
        "Gonna hurt.\n" +
        "Mama's little boy.\n" +
        "You are way out of position, rookie!\n" +
        "Coming in at you like a missile!\n" +
        "Help me!\n" +
        "I don't think these are ")
})
