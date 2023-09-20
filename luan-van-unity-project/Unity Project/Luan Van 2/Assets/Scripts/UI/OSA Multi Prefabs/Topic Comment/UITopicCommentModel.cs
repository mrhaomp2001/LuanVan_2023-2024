using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    [System.Serializable]
    public class UITopicCommentModel
    {
        [SerializeField] private string id;
        [SerializeField] private string userId;
        [SerializeField] private string topicId;
        [SerializeField] private string content;
        [SerializeField] private string createdAt;
        [SerializeField] private int likeCount;
        [SerializeField] private string username;
        [SerializeField] private string userFullName;
        [SerializeField] private string likeStatus;

        [SerializeField] private TopicCommentItemViewsHolder viewsHolder;

        public string Id { get => id; set => id = value; }
        public string UserId { get => userId; set => userId = value; }
        public string TopicId { get => topicId; set => topicId = value; }
        public string Content { get => content; set => content = value; }
        public string CreatedAt { get => createdAt; set => createdAt = value; }
        public int LikeCount { get => likeCount; set => likeCount = value; }
        public string Username { get => username; set => username = value; }
        public string UserFullName { get => userFullName; set => userFullName = value; }
        public string LikeStatus { get => likeStatus; set => likeStatus = value; }
        public TopicCommentItemViewsHolder ViewsHolder { get => viewsHolder; set => viewsHolder = value; }
    }
}
