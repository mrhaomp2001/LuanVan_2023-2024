using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    public class UIQuestionListViewItem : MonoBehaviour
    {
        [SerializeField] private UIQuestionModel questionModel;

        public UIQuestionModel QuestionModel { get => questionModel; set => questionModel = value; }
    }
}
