using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    public class ClassroomItemModel : BaseModel
    {
        [SerializeField] private UIClassroomModel classroomModel;

        public UIClassroomModel ClassroomModel { get => classroomModel; set => classroomModel = value; }
    }
}
