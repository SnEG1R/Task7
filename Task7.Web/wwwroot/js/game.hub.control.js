const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/game-hub")
    .build();

hubConnection.on("GetConnectionInfo", function (gameInfoDto) {
    gameInfo = gameInfoDto;
    
    WriteNewField();
    removeLoader();
});

hubConnection.on("GetPlayingField", function (gameInfoDto) {
    gameInfo = gameInfoDto;
    WriteNewField();
});

function removeLoader() {
    let loader = document.querySelector('.loader');
    loader.remove();
}