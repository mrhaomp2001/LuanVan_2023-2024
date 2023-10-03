using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    public class QuestionCollectionItemModel : BaseModel
    {
        [SerializeField] private UIQuestionCollectionModel questionCollectionModel;

        public UIQuestionCollectionModel QuestionCollectionModel { get => questionCollectionModel; set => questionCollectionModel = value; }
    }
}
