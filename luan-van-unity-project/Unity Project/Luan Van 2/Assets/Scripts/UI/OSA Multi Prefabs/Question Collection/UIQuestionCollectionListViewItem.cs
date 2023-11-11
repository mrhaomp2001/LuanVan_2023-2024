using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

            imageGameIcon.sprite = spriteGameIcons[0];

            if (int.TryParse(questionCollectionModel.GameType, out int type))
            {
                if (type <= spriteGameIcons.Count)
                {
                    imageGameIcon.sprite = spriteGameIcons[type - 1];
                }
            }
        }

        public void GetQuestionsAndAnswers()
        {
            classroomController.GetQuestionsAndAnswers(questionCollectionModel.Id, int.Parse(questionCollectionModel.GameType));
        }
    }
}
