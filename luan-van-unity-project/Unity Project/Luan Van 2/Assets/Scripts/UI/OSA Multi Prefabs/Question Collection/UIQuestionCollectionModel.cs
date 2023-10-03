using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LuanVan.OSA
{
    [System.Serializable]
    public class UIQuestionCollectionModel 
    {
        [SerializeField] private string id;
        [SerializeField] private string classroomId;
        [SerializeField] private string name;
        [SerializeField] private string difficulty;
        [SerializeField] private string gameType;
        [SerializeField] private int questionsPerTime;
        [SerializeField] private QuestionCollectionItemViewsHolder viewsHolder;

        public string Id { get => id; set => id = value; }
        public string ClassroomId { get => classroomId; set => classroomId = value; }
        public string Name { get => name; set => name = value; }
        public string Difficulty { get => difficulty; set => difficulty = value; }
        public string GameType { get => gameType; set => gameType = value; }
        public int QuestionsPerTime { get => questionsPerTime; set => questionsPerTime = value; }
        public QuestionCollectionItemViewsHolder ViewsHolder { get => viewsHolder; set => viewsHolder = value; }
    } 
}
