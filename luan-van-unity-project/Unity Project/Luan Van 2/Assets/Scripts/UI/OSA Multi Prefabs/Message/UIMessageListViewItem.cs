using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace LuanVan.OSA
{
    public class UIMessageListViewItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textContent;
        [SerializeField] private UIMessageModel messageModel;

        public UIMessageModel MessageModel { get => messageModel; set => messageModel = value; }

        public void UpdateViews()
        {
            textContent.text = messageModel.Content;
        }
    }
}
