const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/game-hub")
    .build();

hubConnection.on("GetConnectionInfo", function (gameInfoDto) {
    if (gameInfo.gameStatus === 'Completed') {
        if (window.confirm("The second player left the game!")) {
            document.location.href = '/';
        } else {
            document.location.href = '/';
        }
    }

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

    winnerInfoContainer.innerHTML = winnerInfoContainer.innerHTML
        .replace("{winner}", `${winnerPlayerName}`)
        .replace("{win}", isWin ? "You won!" : "You lost!");

    winnerInfoContainer.style.display = 'flex';
});

hubConnection.on("GetAllGame", function (games) {
    GetAllGame(games.games);
});

function removeLoader() {
    let loader = document.querySelector('.loader');
    loader.remove();
}