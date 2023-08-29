using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    [System.Serializable]
    public class UIPostTemplateModel
    {
        [SerializeField] private string id;
        [SerializeField] private string name;
        [SerializeField] private string content;
        [SerializeField] private string createAt;

        [SerializeField] private int itemIndexOSA;
        [SerializeField] private PostTemplateItemViewsHolder viewsHolder;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Content { get => content; set => content = value; }
        public string CreateAt { get => createAt; set => createAt = value; }
        public int ItemIndexOSA { get => itemIndexOSA; set => itemIndexOSA = value; }
        public PostTemplateItemViewsHolder ViewsHolder { get => viewsHolder; set => viewsHolder = value; }
    }
}
