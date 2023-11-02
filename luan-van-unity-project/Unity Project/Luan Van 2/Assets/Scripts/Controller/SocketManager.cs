using Library;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SocketIOUnity;

public class SocketManager : MonoBehaviour
{
    [SerializeField] private string endpoint;

    private SocketIOUnity socket;
    public SocketIOUnity Socket { get => socket; set => socket = value; }

    public void Connect()
    {
        socket =
        new SocketIOUnity(new Uri(endpoint), new SocketIOOptions
        {
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket,
        })
        {
            JsonSerializer = new NewtonsoftJsonSerializer(),

        };
        socket.unityThreadScope = UnityThreadScope.Update;


        /// reserved socketio events area
        socket.OnConnected += (sender, e) =>
        {

            socket.ExecuteInUnityUpdateThread(() =>
            {

                var loginData = new
                {
                    userId = GlobalSetting.LoginUser.Id
                };

                socket.Emit("connected", (response) =>
                {

                    string text = response.GetValue<string>();

                    Debug.Log(text);
                }, loginData);

            });
        };
        socket.OnPing += (sender, e) =>
        {
            //Debug.Log("Ping");
        };
        socket.OnPong += (sender, e) =>
        {
            //Debug.Log("Pong: " + e.TotalMilliseconds);
        };
        socket.OnDisconnected += (sender, e) =>
        {
            Debug.Log("disconnect: " + e);
        };
        socket.OnReconnectAttempt += (sender, e) =>
        {
            Debug.Log($"{DateTime.Now} Reconnecting: attempt = {e}");
        };
        /// end reserved socketio events area
        /// 
        socket.Connect();

        socket.OnUnityThread("messageReceived", (res) =>
        {
            var resToValues = JSONNode.Parse(res.GetValue().GetRawText());
            Debug.Log(res.GetValue().GetRawText());
            Debug.Log(resToValues["data"]["content"]);
            Debug.Log(resToValues["data"]["sender"]["name"]);
        });
    }

    private void OnApplicationQuit()
    {
        socket?.Disconnect();
        socket?.Dispose();
    }
}
