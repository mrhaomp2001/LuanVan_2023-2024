using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    [System.Serializable]
    public class UIMessageModel 
    {
        [SerializeField] private int id;
        [SerializeField] private int senderId;
        [SerializeField] private int receiverId;
        [SerializeField] private string content;
        [SerializeField] private string createdAt;

        [SerializeField] private string senderUsername;
        [SerializeField] private string senderFullName;

        [SerializeField] private string receiverUsername;
        [SerializeField] private string receiverFullName;

        [SerializeField] private MessageItemViewsHolder viewsHolder;

        public int Id { get => id; set => id = value; }
        public int SenderId { get => senderId; set => senderId = value; }
        public int ReceiverId { get => receiverId; set => receiverId = value; }
        public string Content { get => content; set => content = value; }
        public string CreatedAt { get => createdAt; set => createdAt = value; }
        public string SenderUsername { get => senderUsername; set => senderUsername = value; }
        public string SenderFullName { get => senderFullName; set => senderFullName = value; }
        public string ReceiverUsername { get => receiverUsername; set => receiverUsername = value; }
        public string ReceiverFullName { get => receiverFullName; set => receiverFullName = value; }
        public MessageItemViewsHolder ViewsHolder { get => viewsHolder; set => viewsHolder = value; }
    }
}
