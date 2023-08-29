using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{

    public class UIAnswerListViewItem : MonoBehaviour
    {
        [SerializeField] private UIAnswerModel answerModel;
        [SerializeField] private ClassroomController classroomController;

        public UIAnswerModel AnswerModel { get => answerModel; set => answerModel = value; }

        public void SelectAnswer()
        {
            classroomController.SelectAnswer(answerModel);
        }
    }
}
