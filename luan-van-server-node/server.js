require("dotenv").config();
const axios = require("axios");
var express = require("express");
var app = express();

var http = require("http").createServer(app);
var socketIO = require("socket.io")(http, {
  cors: {
    origin: "*",
  },
});

var users = [];

socketIO.on("connection", function (socket) {
  socket.on("connected", function (data) {
    users[data.userId] = socket.id;

    console.log("---");
    users.forEach((element) => {
      console.log(" - " + element + "\n");
    });
    console.log("---");
  });

  socket.on("sendEvent", async function (data) {
    socketIO.to(users[data.receiver_id]).emit("messageReceived", data);

    axios
      .post(process.env.ENDPOINT + "messages", data)
      .then((response) => {
        console.log("Response:", response.data);
      })
      .catch((error) => {
        console.error("Error:", error);
      });
  });
});

app.get("/", (req, res) => {
  res.send("Hello, world!");
});

http.listen(process.env.PORT || 3000, function () {
  console.log("Server is started at port: " + process.env.PORT);
});
