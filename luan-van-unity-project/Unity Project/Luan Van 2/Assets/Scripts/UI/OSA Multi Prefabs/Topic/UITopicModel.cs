using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    [System.Serializable]
    public class UITopicModel
    {
        [SerializeField] private string id;
        [SerializeField] private string title;
        [SerializeField] private string content;
        [SerializeField] private string userId;
        [SerializeField] private string createAt;
        [SerializeField] private string imagePath;

        [SerializeField] private string username;
        [SerializeField] private string userFullname;
        [SerializeField] private string avatarPath;

        [SerializeField] private int commentCount;

        [SerializeField] private int likeCount;
        [SerializeField] private string likeStatus;

        [SerializeField] private string topicStatus;

        [SerializeField] private TopicItemViewsHolder viewsHolder;

        public string Id { get => id; set => id = value; }
        public string Content { get => content; set => content = value; }
        public string UserId { get => userId; set => userId = value; }
        public string CreateAt { get => createAt; set => createAt = value; }
        public string Username { get => username; set => username = value; }
        public string UserFullname { get => userFullname; set => userFullname = value; }
        public int CommentCount { get => commentCount; set => commentCount = value; }
        public int LikeCount { get => likeCount; set => likeCount = value; }
        public string LikeStatus { get => likeStatus; set => likeStatus = value; }
        public string TopicStatus { get => topicStatus; set => topicStatus = value; }
        public TopicItemViewsHolder ViewsHolder { get => viewsHolder; set => viewsHolder = value; }
        public string AvatarPath { get => avatarPath; set => avatarPath = value; }
        public string Title { get => title; set => title = value; }
        public string ImagePath { get => imagePath; set => imagePath = value; }
    }
}
