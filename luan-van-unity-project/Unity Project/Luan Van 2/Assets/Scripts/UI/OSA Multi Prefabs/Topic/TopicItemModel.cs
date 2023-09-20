using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    public class TopicItemModel : BaseModel
    {
        [SerializeField] private UITopicModel topicModel;

        public UITopicModel TopicModel { get => topicModel; set => topicModel = value; }
    }
}
