using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{

    public class AnswerItemModel : BaseModel
    {
        [SerializeField] private UIAnswerModel answerModel;

        public UIAnswerModel AnswerModel { get => answerModel; set => answerModel = value; }
    }
}
