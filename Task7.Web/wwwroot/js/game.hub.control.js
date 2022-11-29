const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/game-hub")
    .build();

hubConnection.on("GetConnectionInfo", function (gameInfoDto) {
    console.log(gameInfo)
    gameInfo = gameInfoDto;

    showTurningPlayer();
    writeNewField();
    removeLoader();
});

hubConnection.on("GetPlayingField", function (gameInfoDto) {
    gameInfo = gameInfoDto;
    writeNewField();
});

hubConnection.on("GetWinnerPlayer", function (winnerPlayerName, isWin) {
    let winnerInfoContainer = document.querySelector('.winner-info-container');
    document.querySelector('.turning-player').remove();

    winnerInfoContainer.innerHTML = winnerInfoContainer.innerHTML
        .replace("{winner}", `${winnerPlayerName}`)
        .replace("{win}", isWin ? "You won!" : "You lost!");

    winnerInfoContainer.style.display = 'flex';
});

function removeLoader() {
    let loader = document.querySelector('.loader');
    loader.remove();
}