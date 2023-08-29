using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LuanVan.OSA
{
    [System.Serializable]
    public class UIClassroomModel
    {
        [SerializeField] private string id;
        [SerializeField] private string name;
        [SerializeField] private string description;
        [SerializeField] private string createdAt;

        [SerializeField] private int itemIndexOSA;
        [SerializeField] private ClassroomItemViewsHolder viewsHolder;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string CreatedAt { get => createdAt; set => createdAt = value; }
        public int ItemIndexOSA { get => itemIndexOSA; set => itemIndexOSA = value; }
        public ClassroomItemViewsHolder ViewsHolder { get => viewsHolder; set => viewsHolder = value; }
    }
}
