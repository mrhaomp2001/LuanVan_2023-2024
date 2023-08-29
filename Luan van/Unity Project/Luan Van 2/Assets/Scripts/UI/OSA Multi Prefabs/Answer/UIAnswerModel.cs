using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    [System.Serializable]
    public class UIAnswerModel
    {
        [SerializeField] private string id;
        [SerializeField] private string questionId;
        [SerializeField] private string content;
        [SerializeField] private string createdAt;
        [SerializeField] private bool isCorrect;

        [SerializeField] private int itemIndexOSA;
        [SerializeField] private AnswerItemViewsHolder viewsHolder;

        public string Id { get => id; set => id = value; }
        public string QuestionId { get => questionId; set => questionId = value; }
        public string Content { get => content; set => content = value; }
        public string CreatedAt { get => createdAt; set => createdAt = value; }
        public int ItemIndexOSA { get => itemIndexOSA; set => itemIndexOSA = value; }
        public AnswerItemViewsHolder ViewsHolder { get => viewsHolder; set => viewsHolder = value; }
        public bool IsCorrect { get => isCorrect; set => isCorrect = value; }
    }
}
