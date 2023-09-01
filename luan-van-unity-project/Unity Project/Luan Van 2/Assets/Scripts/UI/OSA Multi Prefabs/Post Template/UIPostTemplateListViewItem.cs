using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{

    public class UIPostTemplateListViewItem : MonoBehaviour
    {
        [SerializeField] private PostController postController;
        [SerializeField] private UIPostTemplateModel postTemplateModel;

        public UIPostTemplateModel PostTemplateModel { get => postTemplateModel; set => postTemplateModel = value; }

        public void SelectTemplate()
        {
            postController.SelectTemplate(postTemplateModel);
        }
    }
}
