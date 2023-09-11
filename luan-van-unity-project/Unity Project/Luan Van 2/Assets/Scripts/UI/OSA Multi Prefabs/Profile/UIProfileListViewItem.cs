using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LuanVan.OSA
{
    [System.Serializable]
    public class UIProfileListViewItem : MonoBehaviour
    {
        [SerializeField] private UIProfileModel profileModel;

        public UIProfileModel ProfileModel { get => profileModel; set => profileModel = value; }
    }
}
