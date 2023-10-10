using Com.TheFallenGames.OSA.Demos.DifferentPrefabPerOrientation.Models;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LuanVan.OSA
{
    public class UITopicListViewItem : MonoBehaviour
    {
        [SerializeField] private Image imageAvatar;
        [SerializeField] private Image imageLikeUp;
        [SerializeField] private Image imageLikeDown;

        [SerializeField] private RectTransform containerTitle;
        [SerializeField] private RectTransform containerImage;
        [SerializeField] private TextMeshProUGUI textTitile;
        [SerializeField] private Image imageTopic;

        [SerializeField] private Color colorLikeChoice;
        [SerializeField] private Color colorUnLikeChoice;

        [SerializeField] private ClassroomController classroomController;
        [SerializeField] private ClassroomTopicController classroomTopicController;
        [SerializeField] private UITopicModel topicModel;

        public UITopicModel TopicModel { get => topicModel; set => topicModel = value; }

        public void ShowTopicComments()
        {
            classroomController.ShowTopicComments(topicModel);
        }

        public void UpdateLikeButtonColor()
        {

            imageLikeUp.color = colorUnLikeChoice;
            imageLikeDown.color = colorUnLikeChoice;

            if (topicModel.LikeStatus.Equals("1"))
            {
                imageLikeUp.color = colorLikeChoice;
            }

            if (topicModel.LikeStatus.Equals("-1"))
            {
                imageLikeDown.color = colorLikeChoice;
            }
        }

        public void UpdateTopicLikeStatus(string status)
        {
            if (status.Equals("1"))
            {
                if (topicModel.LikeStatus.Equals("-1"))
                {
                    topicModel.LikeCount++;
                }

                if (!topicModel.LikeStatus.Equals("1"))
                {
                    topicModel.LikeStatus = "1";
                    topicModel.LikeCount++;
                }
                else
                {
                    topicModel.LikeStatus = "0";
                    topicModel.LikeCount--;
                }
            }
            else if (status.Equals("-1"))
            {
                if (topicModel.LikeStatus.Equals("1"))
                {
                    topicModel.LikeCount--;
                }

                if (!topicModel.LikeStatus.Equals("-1"))
                {
                    topicModel.LikeStatus = "-1";
                    topicModel.LikeCount--;
                }
                else
                {
                    topicModel.LikeStatus = "0";
                    topicModel.LikeCount++;
                }
            }

            //topicModel.LikeStatus = status;
            UpdateLikeButtonColor();

            classroomController.UpdateTopicLikeStatus(topicModel.Id, topicModel.LikeStatus);
        }

        public void UpdateViews()
        {
            if (!topicModel.AvatarPath.Equals(""))
            {
                Davinci.get().load(GlobalSetting.Endpoint + topicModel.AvatarPath).into(imageAvatar).setFadeTime(0).start();
            }

            if (!topicModel.Title.Equals(""))
            {
                textTitile.text = topicModel.Title;
                containerTitle.gameObject.SetActive(true);
            }
            else
            {
                containerTitle.gameObject.SetActive(false);
            }

            if (!topicModel.ImagePath.Equals(""))
            {
                Davinci.get().load(GlobalSetting.Endpoint + topicModel.ImagePath).into(imageTopic).setFadeTime(0).start();
                containerImage.gameObject.SetActive(true);
            }
            else
            {
                containerImage.gameObject.SetActive(false);
            }

            classroomTopicController.CheckAndGetOldTopic(topicModel);
        }

        public void ShowTopicUtilitiesMenu()
        {
            classroomTopicController.ShowTopicUtilitiesMenu(topicModel);
        }
    }
}
