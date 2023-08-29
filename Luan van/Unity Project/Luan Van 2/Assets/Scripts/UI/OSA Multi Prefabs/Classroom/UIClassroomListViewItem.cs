using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    public class UIClassroomListViewItem : MonoBehaviour
    {
        [SerializeField] private ClassroomController classroomController;
        [SerializeField] private UIClassroomModel classroomModel;

        public UIClassroomModel ClassroomModel { get => classroomModel; set => classroomModel = value; }

        /// <summary>
        /// call in button get questions and answers
        /// </summary>
        public void GetQuestionsAndAnswers()
        {
            classroomController.GetQuestionsAndAnswers(classroomModel);
        }
    }
}
