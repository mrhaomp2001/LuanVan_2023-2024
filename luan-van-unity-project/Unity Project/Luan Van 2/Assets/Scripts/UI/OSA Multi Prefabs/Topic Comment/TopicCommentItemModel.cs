using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{

    public class TopicCommentItemModel : BaseModel
    {










        [SerializeField] private UITopicCommentModel topicCommentModel;

        public UITopicCommentModel TopicCommentModel { get => topicCommentModel; set => topicCommentModel = value; }
    }
}
