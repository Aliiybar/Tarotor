// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let max_selection = 10;
let selected_cards = [];
let tarot;
function draw() {
    $(".cardContainer").empty();
    var shuffledCards = shuffle(tarot.cards)
    shuffledCards.forEach(card => {
        $(".cardContainer").append(`<div class="cardItem">
                        <img src="${tarot.back_path}" data-index="${card.order_num}" height="100" class="cardItemContent" />
                    </div>`);
    })
}

$(".cardContainer").on("click", ".cardItemContent", function () {
    if (selected_cards.length >= max_selection) {
        $("#selectedCard").empty();
        $("#selectedCard").append(`<h5>Maksimum kart seçimine ulaştınız</h5>`);
        $("#cardModalLabel").text("UYARI")
        $("#cardModal").modal('show');
    } else {

        var card = tarot.cards.find(k => k.order_num == this.getAttribute("data-index"));
        if (selected_cards.find(p => p.order_num == card.order_num)) {
            $("#selectedCard").empty();
            $("#selectedCard").append(`<h5>Bu Kart Secilmis</h5>`);
            $("#cardModalLabel").text("UYARI")
            $("#cardModal").modal('show');
        } else {
            $("#selectedCard").empty();
            $("#selectedCard").append(`<img src="${card.path}" height="300"/>`);
            $("#cardModalLabel").text(card.card_name)
            $("#cardModal").modal('show');

            $(this).attr("src", card.path);
            selected_cards.push(card);
            $(".selectedCards").append(`<div class="cardItem">
                        <img src="${card.path}" data-index="${card.order_num}" height="100" class="cardItemContent"/>
                    </div>`);
        }
    }
})
$(".selectedCards").on("click", ".cardItemContent", function () {

    var card = tarot.cards.find(k => k.order_num == this.getAttribute("data-index"));
    $("#selectedCard").empty();
    $("#selectedCard").append(`<img src="${card.path}" height="300"/>`);
    $("#cardModalLabel").text(card.card_name)
    $("#cardModal").modal('show');


})

function shuffle(array) {
    var currentIndex = array.length, temporaryValue, randomIndex;

    // While there remain elements to shuffle...
    while (0 !== currentIndex) {

        // Pick a remaining element...
        randomIndex = Math.floor(Math.random() * currentIndex);
        currentIndex -= 1;

        // And swap it with the current element.
        temporaryValue = array[currentIndex];
        array[currentIndex] = array[randomIndex];
        array[randomIndex] = temporaryValue;
    }

    return array;
}
function getCardData() {
    $.ajax({
        dataType: "json",
        url: "/json/cards.json",

        success: (data) => {
            tarot = data;
            draw();
        }
    });
}

getCardData();

