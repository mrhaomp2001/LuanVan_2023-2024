using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

namespace LuanVan.OSA
{
    public class UIMessageListViewItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textname;
        [SerializeField] private TextMeshProUGUI textContent;
        [SerializeField] private TextMeshProUGUI textDate;
        [SerializeField] private Image imageMessageBackground;
        [SerializeField] private Color senderColor, receiverColor;

        [SerializeField] private VerticalLayoutGroup messageLayout;
        [SerializeField] private VerticalLayoutGroup messageHeaderLayout;

        [SerializeField] private MessageController messageController;
        [SerializeField] private UIMultiPrefabsOSA chatOSA;

        [SerializeField] private UIMessageModel messageModel;

        public UIMessageModel MessageModel { get => messageModel; set => messageModel = value; }

        public void UpdateViews()
        {
            textContent.text = messageModel.Content;
            textname.text = messageModel.SenderFullName;

            if (messageModel.SenderId.ToString() == GlobalSetting.LoginUser.Id)
            {
                messageLayout.childAlignment = TextAnchor.UpperRight;
                messageHeaderLayout.childAlignment = TextAnchor.UpperRight;
                imageMessageBackground.color = senderColor;
            }
            else
            {
                messageLayout.childAlignment = TextAnchor.UpperLeft;
                messageHeaderLayout.childAlignment = TextAnchor.UpperLeft;
                imageMessageBackground.color = receiverColor;
            }

            if (DateTime.TryParse(messageModel.CreatedAt, out DateTime createDate)) { }

            TimeSpan timeSpanCreateDate = DateTime.Now - createDate;

            textDate.text = "Vừa xong";

            if (timeSpanCreateDate.TotalMinutes >= 1)
            {
                textDate.text = timeSpanCreateDate.Minutes.ToString() + " phút trước";
            }

            if (timeSpanCreateDate.TotalHours >= 1)
            {
                textDate.text = timeSpanCreateDate.Hours.ToString() + " giờ trước";
            }

            if (timeSpanCreateDate.TotalDays >= 1 && timeSpanCreateDate.TotalDays <= 3)
            {
                textDate.text = timeSpanCreateDate.Days.ToString() + " ngày trước";
            }

            if (timeSpanCreateDate.TotalDays > 3)
            {
                textDate.text = createDate.ToString("yyyy-MM-dd HH:mm");
            }

            if (chatOSA.Data.Count > 1)
            {
                if (messageModel.ViewsHolder.ItemIndex == (chatOSA.Data.Count - 1))
                {
                    messageController.IsEndOfMessages = true;
                }
                else
                {
                    messageController.IsEndOfMessages = false;
                }
            }
        }
    }
}
