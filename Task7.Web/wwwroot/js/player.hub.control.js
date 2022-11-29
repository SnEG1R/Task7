const playerHub = new signalR.HubConnectionBuilder()
    .withUrl("/player-hub")
    .build();

playerHub.on("GetPlayerName", function (player) {
    playerName = player;
});