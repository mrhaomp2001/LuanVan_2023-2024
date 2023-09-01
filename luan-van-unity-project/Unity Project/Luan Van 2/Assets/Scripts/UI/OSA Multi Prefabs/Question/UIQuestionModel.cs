using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LuanVan.OSA
{
    [System.Serializable]
    public class UIQuestionModel
    {
        [SerializeField] private string id;
        [SerializeField] private string classroomId;
        [SerializeField] private string content;
        [SerializeField] private string createdAt;
        [SerializeField] private int itemIndexOSA;
        [SerializeField] private QuestionItemViewsHolder viewsHolder;

        public string Id { get => id; set => id = value; }
        public string ClassroomId { get => classroomId; set => classroomId = value; }
        public string Content { get => content; set => content = value; }
        public string CreatedAt { get => createdAt; set => createdAt = value; }
        public int ItemIndexOSA { get => itemIndexOSA; set => itemIndexOSA = value; }
        public QuestionItemViewsHolder ViewsHolder { get => viewsHolder; set => viewsHolder = value; }
    }
}
