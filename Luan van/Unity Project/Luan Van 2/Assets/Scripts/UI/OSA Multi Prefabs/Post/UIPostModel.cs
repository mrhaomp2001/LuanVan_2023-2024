using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    [System.Serializable]
    public class UIPostModel 
    {
        [SerializeField] private string postId;
        [SerializeField] private string content;
        [SerializeField] private string userId;
        [SerializeField] private string postTemplateId;
        [SerializeField] private string createAt;

        [SerializeField] private string username;
        [SerializeField] private string userFullname;

        [SerializeField] private string posTemplateName;
        [SerializeField] private string posTemplateContent;

        [SerializeField] private int likeCount;
        [SerializeField] private string likeStatus;

        [SerializeField] private string themeColor;

        [SerializeField] private int itemIndexOSA;
        [SerializeField] private PostItemViewsHolder viewsHolder;

        public string PostId { get => postId; set => postId = value; }
        public string Content { get => content; set => content = value; }
        public string UserId { get => userId; set => userId = value; }
        public string PostTemplateId { get => postTemplateId; set => postTemplateId = value; }
        public string CreateAt { get => createAt; set => createAt = value; }
        public string Username { get => username; set => username = value; }
        public string UserFullname { get => userFullname; set => userFullname = value; }
        public string PosTemplateName { get => posTemplateName; set => posTemplateName = value; }
        public string PosTemplateContent { get => posTemplateContent; set => posTemplateContent = value; }
        public int ItemIndexOSA { get => itemIndexOSA; set => itemIndexOSA = value; }
        public PostItemViewsHolder ViewsHolder { get => viewsHolder; set => viewsHolder = value; }
        public string ThemeColor { get => themeColor; set => themeColor = value; }
        public int LikeCount { get => likeCount; set => likeCount = value; }

        public string LikeStatus { get => likeStatus; set => likeStatus = value; }
    }
}
