using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LuanVan.OSA
{

    public class UIChatUserListViewItem : MonoBehaviour
    {
        [SerializeField] private Sprite spriteDefault;
        [SerializeField] private Image imageAvatar;
        [SerializeField] private TextMeshProUGUI textName;
        [SerializeField] private TextMeshProUGUI textContent;
        [SerializeField] private TextMeshProUGUI textDate;
        [SerializeField] private UIChatUserModel chatUserModel;

        [SerializeField] private MessageController messageController;

        public UIChatUserModel ChatUserModel { get => chatUserModel; set => chatUserModel = value; }

        public void UpdateViews()
        {
            if (DateTime.TryParse(chatUserModel.CreatedAt, out DateTime createDate)) { }

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

            textName.text = chatUserModel.UserFullname;
            textContent.text = chatUserModel.Content;

            imageAvatar.sprite = spriteDefault;

            if (chatUserModel.AvatarPath != "")
            {
                Davinci.get().load(GlobalSetting.Endpoint + chatUserModel.AvatarPath).into(imageAvatar).setFadeTime(0).start();
            }
        }

        public void GetMessage()
        {
            messageController.GetMessages(chatUserModel);
        }
    }
}
