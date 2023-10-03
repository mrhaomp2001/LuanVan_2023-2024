using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace LuanVan.OSA
{

    public class UIQuestionCollectionListViewItem : MonoBehaviour
    {
        [SerializeField] private List<Sprite> spriteGameIcons = new List<Sprite>();
        [SerializeField] private Image imageGameIcon;
        [SerializeField] private TextMeshProUGUI textName;
        [SerializeField] private TextMeshProUGUI textInfo;
        [SerializeField] private ClassroomController classroomController;

        [SerializeField] private UIQuestionCollectionModel questionCollectionModel;

        public UIQuestionCollectionModel QuestionCollectionModel { get => questionCollectionModel; set => questionCollectionModel = value; }

        public void UpdateViews()
        {
            textName.text = questionCollectionModel.Name;
            textInfo.text = "Độ khó: " + questionCollectionModel.Difficulty + "\n";
            textInfo.text += "Số câu hỏi mỗi lần: " + questionCollectionModel.QuestionsPerTime.ToString();

            if (questionCollectionModel.GameType.Equals("1"))
            {
                imageGameIcon.sprite = spriteGameIcons[0];
            }
            if (questionCollectionModel.GameType.Equals("2"))
            {
                imageGameIcon.sprite = spriteGameIcons[1];
            }
        }

        public void GetQuestionsAndAnswers()
        {
            classroomController.GetQuestionsAndAnswers(questionCollectionModel.Id, int.Parse(questionCollectionModel.GameType));
        }
    }
}
