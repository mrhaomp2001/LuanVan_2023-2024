using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    public class ProfileItemModel : BaseModel
    {
        [SerializeField] private UIProfileModel profileModel;

        public UIProfileModel ProfileModel { get => profileModel; set => profileModel = value; }
    }
}
