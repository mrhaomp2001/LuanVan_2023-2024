using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    [System.Serializable]
    public class UIChatUserModel
    {
        [SerializeField] private int id;
        [SerializeField] private int senderId;
        [SerializeField] private int receiverId;
        [SerializeField] private string content;
        [SerializeField] private string createdAt;

        [SerializeField] private int otherId;
        [SerializeField] private string userFullname;
        [SerializeField] private string username;
        [SerializeField] private string avatarPath;

        [SerializeField] private ChatUserItemViewsHolder viewsHolder;

        public int Id { get => id; set => id = value; }
        public int SenderId { get => senderId; set => senderId = value; }
        public int ReceiverId { get => receiverId; set => receiverId = value; }
        public string Content { get => content; set => content = value; }
        public int OtherId { get => otherId; set => otherId = value; }
        public string UserFullname { get => userFullname; set => userFullname = value; }
        public string Username { get => username; set => username = value; }
        public string AvatarPath { get => avatarPath; set => avatarPath = value; }
        public string CreatedAt { get => createdAt; set => createdAt = value; }
        public ChatUserItemViewsHolder ViewsHolder { get => viewsHolder; set => viewsHolder = value; }
    }
}
