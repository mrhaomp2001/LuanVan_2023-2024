using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{

    public class ClassroomInfoItemModel : BaseModel
    {
        [SerializeField] private UIClassroomInfoModel classroomInfoModel;

        public UIClassroomInfoModel ClassroomInfoModel { get => classroomInfoModel; set => classroomInfoModel = value; }
    }
}
