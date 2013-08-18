
// CheckDecimal is used to for client-side validation of PlayerIndex form fields
function CheckPlayerIndex(playerIndexInput) 
{
    // var numbers = /^[0-9]+(\.[0-9]+)+$/;
    var playerIndexElement = document.getElementById(playerIndexInput.id);
    // alert(playerIndexElement.name.substring(11))
    var playerErrorElement = document.getElementById("PlayerError" + playerIndexElement.name.substring(11));
    //if (playerIndexInput.value.match(numbers)) {
    if (isNaN(parseFloat(playerIndexInput.value))) {
        // alert(playerIndexInput.value);
        playerIndexElement.style.borderColor = "#b03535";
        playerIndexElement.style.boxShadow = "0 0 5px #d45252";
        playerErrorElement.setAttribute("class", "player_hint_visible");
        playerErrorElement.innerHTML = "Enter value between -10.0 to 50.0";
        return false;
    }
    else if (parseFloat(playerIndexInput.value) < -10.0 || parseFloat(playerIndexInput.value) > 50.0) {
        var tempFloat = parseFloat(playerIndexInput.value).toFixed(1)
        playerIndexElement.style.borderColor = "#b03535";
        playerIndexElement.style.boxShadow = "0 0 5px #d45252";
        playerErrorElement.setAttribute("class", "player_hint_visible");
        playerErrorElement.innerHTML = "Enter value between -10.0 to 50.0";
        return false;
    }
    else if (playerIndexInput.value.indexOf(".") < 0) {
        tempFloat = parseFloat(playerIndexInput.value).toFixed(1)
        // alert(tempFloat.toString());
        playerIndexElement.value = tempFloat.toString();
        playerIndexElement.style.borderColor = "#aaa";
        playerIndexElement.style.removeProperty("box-shadow");
        playerErrorElement = document.getElementById("PlayerError" + playerIndexElement.name.substring(11));
        playerErrorElement.setAttribute("class", "player_hint_hidden");
        return true;
    }
    else 
    {
        tempFloat = parseFloat(playerIndexInput.value).toFixed(1);
        playerErrorElement = document.getElementById("PlayerError" + playerIndexElement.name.substring(11));
        playerIndexElement.value = tempFloat.toString();
        playerIndexElement.style.borderColor = "#aaa";
        playerIndexElement.style.removeProperty("box-shadow");
        playerErrorElement.setAttribute("class", "player_hint_hidden");
        return true;
    }
    
}

function CheckPlayerName(playerNameInput) {
    var alphaExp = /^[0-9a-zA-Z ]+$/;
    var playerNameElement = document.getElementById(playerNameInput.id);
    var playerErrorElement = document.getElementById("PlayerError" + playerNameElement.name.substring(10));
    if (playerNameInput.value.match(alphaExp)) {
        playerNameElement.style.borderColor = "#aaa";
        playerNameElement.style.removeProperty("box-shadow");
        // alert("PlayerError" + playerNameElement.name.substring(10));
        playerErrorElement.setAttribute("class", "player_hint_hidden");
        return true;
    }
    else {
        playerNameElement.style.borderColor = "#b03535";
        playerNameElement.style.boxShadow = "0 0 5px #d45252";
        playerErrorElement.setAttribute("class", "player_hint_visible");
        playerErrorElement.innerHTML = "Characters and numbers only please.";
        playerNameInput.focus();
        return false;
    }
}

function CheckGameName(gameNameInput) {
    var alphaExp = /^[0-9a-zA-Z _]+$/;
    var gameNameElement = document.getElementById(gameNameInput.id);
    var gameErrorElement = document.getElementById("nameDivStr");
    if (gameNameInput.value.match(alphaExp)) 
    {
        gameNameElement.style.borderColor = "#aaa";
        gameNameElement.style.removeProperty("box-shadow");
        gameErrorElement.setAttribute("class", "input_hint_hidden");
		
	}
    else {
        gameNameElement.style.borderColor = "#b03535";
        gameNameElement.style.boxShadow = "0 0 5px #d45252";
        gameErrorElement.setAttribute("class", "input_hint_visible");
        gameErrorElement.innerHTML = "Characters and numbers only please.";
		gameNameInput.focus();
		return false;
    }

    if (gameNameInput.value.length < 3) 
    {
        gameNameElement.style.borderColor = "#b03535";
        gameNameElement.style.boxShadow = "0 0 5px #d45252";
        gameErrorElement.setAttribute("class", "input_hint_visible");
        gameErrorElement.innerHTML = "Game name too short.";
        gameNameInput.focus();
        return false;
    }
    if (gameNameInput.value.length > 40) {
        gameNameElement.style.borderColor = "#b03535";
        gameNameElement.style.boxShadow = "0 0 5px #d45252";
        gameErrorElement.setAttribute("class", "input_hint_visible");
        gameErrorElement.innerHTML = "Game name too long.";
        gameNameInput.focus();
        return false;
    }

    return true;



}

function CheckNumberOfPlayers(numberOfPlayersInput) {
    var alphaExp = /^[0-9]+$/;
    var numberOfPlayersElement = document.getElementById(numberOfPlayersInput.id);
    var numberOfPlayersErrorElement = document.getElementById("numberOfPlayersDivStr");
    if (numberOfPlayersInput.value.match(alphaExp)) {
        numberOfPlayersElement.style.borderColor = "#aaa";
        numberOfPlayersElement.style.removeProperty("box-shadow");
        numberOfPlayersErrorElement.setAttribute("class", "input_hint_hidden");
    }
    else {
        numberOfPlayersElement.style.borderColor = "#b03535";
        numberOfPlayersElement.style.boxShadow = "0 0 5px #d45252";
        numberOfPlayersErrorElement.setAttribute("class", "input_hint_visible");
        numberOfPlayersErrorElement.innerHTML = "Numbers only please.";
        numberOfPlayersInput.focus();
        return false;
    }

    if (numberOfPlayersInput.value < 1) {
        numberOfPlayersElement.style.borderColor = "#b03535";
        numberOfPlayersElement.style.boxShadow = "0 0 5px #d45252";
        numberOfPlayersErrorElement.setAttribute("class", "input_hint_visible");
        numberOfPlayersErrorElement.innerHTML = "More than 0 players please.";
        numberOfPlayersInput.focus();
        return false;
    }
    if (numberOfPlayersInput.value > 120) {
        numberOfPlayersElement.style.borderColor = "#b03535";
        numberOfPlayersElement.style.boxShadow = "0 0 5px #d45252";
        numberOfPlayersErrorElement.setAttribute("class", "input_hint_visible");
        numberOfPlayersErrorElement.innerHTML = "120 is maximum number of players.";
        numberOfPlayersInput.focus();
        return false;
    }

    return true;

}


function CheckGameType(theInput) {
    var alphaExp = /^[0-4]+$/;
    var webElement = document.getElementById(theInput.id);
    var divElement = document.getElementById("gameTypeDivStr");
    if (theInput.value.match(alphaExp)) {
        webElement.style.borderColor = "#aaa";
        webElement.style.removeProperty("box-shadow");
        divElement.setAttribute("class", "input_hint_hidden");
    }
    else {
        webElement.style.borderColor = "#b03535";
        webElement.style.boxShadow = "0 0 5px #d45252";
        divElement.setAttribute("class", "input_hint_visible");
        divElement.innerHTML = "Select your Game Type.";
        theInput.focus();
        return false;
    }

    if (theInput.value < 1) {
        webElement.style.borderColor = "#b03535";
        webElement.style.boxShadow = "0 0 5px #d45252";
        divElement.setAttribute("class", "input_hint_visible");
        divElement.innerHTML = "I've done something wrong iyst";
        theInput.focus();
        return false;
    }
    if (theInput.value > 4) {
        webElement.style.borderColor = "#b03535";
        webElement.style.boxShadow = "0 0 5px #d45252";
        webElement.setAttribute("class", "input_hint_visible");
        divElement.innerHTML = "120 is maximum number of players.";
        theInput.focus();
        return false;
    }

    return true;

}


function CheckIsHandicapped(theInput) {
    var webElement = document.getElementById(theInput.id);
    var divElement = document.getElementById("isHandicappedDivStr");
    if (theInput.value == "true" || theInput.value == "false") {
        webElement.style.borderColor = "#aaa";
        webElement.style.removeProperty("box-shadow");
        divElement.setAttribute("class", "input_hint_hidden");
        return true;
    }
    else {
        webElement.style.borderColor = "#b03535";
        webElement.style.boxShadow = "0 0 5px #d45252";
        divElement.setAttribute("class", "input_hint_visible");
        divElement.innerHTML = "Select your Game Type.";
        theInput.focus();
        return false;
    }

    return false;


}

function CheckNumberOfRounds(theInput) {
    // alert("VALUE: " + theInput.value);
    var alphaExp = /^[0-9]+$/;
    var webElement = document.getElementById(theInput.id);
    var divElement = document.getElementById("numberOfRoundsDivStr");
    if (theInput.value.match(alphaExp)) {
        // alert("In First If");
        webElement.style.borderColor = "#aaa";
        webElement.style.removeProperty("box-shadow");
        divElement.setAttribute("class", "input_hint_hidden");
    }
    else {
        // alert("In Else");
        webElement.style.borderColor = "#b03535";
        webElement.style.boxShadow = "0 0 5px #d45252";
        divElement.setAttribute("class", "input_hint_visible");
        divElement.innerHTML = "Select the number of rounds in game.";
        theInput.focus();
        return false;
    }

    if (theInput.value < 1) {
        // alert("In less than 1");
        webElement.style.borderColor = "#b03535";
        webElement.style.boxShadow = "0 0 5px #d45252";
        divElement.setAttribute("class", "input_hint_visible");
        divElement.innerHTML = "Out of range, choose again.";
        theInput.focus();
        return false;
    }
    if (theInput.value > 4) {
        // alert("In greater than 4");
        webElement.style.borderColor = "#b03535";
        webElement.style.boxShadow = "0 0 5px #d45252";
        webElement.setAttribute("class", "input_hint_visible");
        divElement.innerHTML = "Out of range, choose again.";
        theInput.focus();
        return false;
    }

    return true;

}

function ValidateGameDetails() {

    var hasErrors = false;


    if (!CheckGameName(document.forms["create_game"]["Name"])) {
        hasErrors = true;
    }

    if (!CheckNumberOfPlayers(document.forms["create_game"]["NumberOfPlayersOrTeams"])) {
        hasErrors = true;
    }

    if (!CheckGameType(document.forms["create_game"]["GameTypeID"])) {
        hasErrors = true;
    }
    if (!CheckIsHandicapped(document.forms["create_game"]["IsHandicapped"])) {
        // alert("Check Is Handicapped");
        hasErrors = true;
    }
    if (!CheckNumberOfRounds(document.forms["create_game"]["NumberOfRounds"])) {
        // alert("Check Number of Rounds");
        hasErrors = true;
    }


    if (hasErrors == true) {
        return false;
    }
    else {
        return true;
    }


}





function CheckSlope(slopeInput) {
    // alert("IN CHECK SLOPE");
    var alphaExp = /^[0-9]+$/;
    var slopeElement = document.getElementById(slopeInput.id);
    // var numberOfPlayersErrorElement = document.getElementById("numberOfPlayersDivStr");
    if (slopeInput.value.match(alphaExp)) {
        if (slopeInput.value < 55 || slopeInput.value > 155) {
            alert("SLOPE OUT OF RANGE");
            return false;
        }
        else {
            alert("SLOPE IN RANGE - Looks good");
            return true;
        }   
    }
    else {
        alert("SLOPE BAD FORMAT");
        
        return false;
    }  

    return true;

}

function ValidateHandicaps() {
    // alert("IN VALIDATE HANDICAPS");
    var alphaExp = /^[0-9]+$/;
    var holes = new Array();
    var numberOfHolesInRoundElement = document.getElementById("HolesInRound");
    var handicapElement;
    var handicapInt;
    
    // fill an array with all of the handicaps and check values
    for (i = 1; i <= parseInt(numberOfHolesInRoundElement.value); i++) {
        handicapElement = document.getElementById("HandicapForHole" + i);
        // alert("ParseInt: " + parseInt(handicapElement.value));
        handicapInt = parseInt(handicapElement.value);
        if (handicapInt > 0 && handicapInt <= 18)
        {
            holes[i] = handicapElement.value;
        }
        else {
            alert("Failed range test for hole: " + i);
            return false;
        }
    }

        for (j = 1; j <= parseInt(numberOfHolesInRoundElement.value); j++)
        {
            for (k = 1; k <= holes.length; k++) {
                if (j != k) {
                    handicapElement = document.getElementById("HandicapForHole" + j);
                    if (handicapElement.value == holes[k]) {
                        alert("Duplicate Handicap Found");
                        alert("Hole " + j + " is the same as hole " + k);
                        return false;
                    }
                }
            }
        }

        return true;
    }



function ValidateRound() {

    // alert("IN VALIDATE ROUND");
    var hasErrors = false;


    if (!CheckSlope(document.forms["set_round"]["Slope"])) {
        alert("IN CHECK SLOPE");
        hasErrors = true;
    }

    if (!ValidateHandicaps()) {
        hasErrors = true;
    }


    if (hasErrors == true) {
        return false;
    }
    else {
        return true;
    }


}