using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    [System.Serializable]
    public class UIMultiModel 
    {
        [SerializeField] private string type;
        [SerializeField] private Dictionary<string, string> passedVariable = new Dictionary<string, string>();
        [SerializeField] private MultiItemViewsHolder viewsHolder;

        public string Type { get => type; set => type = value; }
        public Dictionary<string, string> PassedVariable { get => passedVariable; set => passedVariable = value; }
        public MultiItemViewsHolder ViewsHolder { get => viewsHolder; set => viewsHolder = value; }
    }
}
