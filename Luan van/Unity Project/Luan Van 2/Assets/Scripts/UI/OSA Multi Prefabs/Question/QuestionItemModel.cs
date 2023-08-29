using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    public class QuestionItemModel : BaseModel
    {
        [SerializeField] private UIQuestionModel questionModel;

        public UIQuestionModel QuestionModel { get => questionModel; set => questionModel = value; }
    }
}
