using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LuanVan.OSA
{

    public class UITopicCommentListViewItem : MonoBehaviour
    {
        [SerializeField] private Image imageLikeUp;
        [SerializeField] private Image imageLikeDown;

        [SerializeField] private Color colorLikeChoice;
        [SerializeField] private Color colorUnLikeChoice;

        [SerializeField] private ClassroomController classroomController;
        [SerializeField] private ClassroomTopicController classroomTopicController;
        [SerializeField] private UITopicCommentModel topicCommentModel;

        public UITopicCommentModel TopicCommentModel { get => topicCommentModel; set => topicCommentModel = value; }
        public void UpdateLikeButtonColor()
        {

            imageLikeUp.color = colorUnLikeChoice;
            imageLikeDown.color = colorUnLikeChoice;

            if (topicCommentModel.LikeStatus.Equals("1"))
            {
                imageLikeUp.color = colorLikeChoice;
            }

            if (topicCommentModel.LikeStatus.Equals("-1"))
            {
                imageLikeDown.color = colorLikeChoice;
            }
        }

        public void UpdateTopicCommentLikeStatus(string status)
        {
            if (status.Equals("1"))
            {
                if (topicCommentModel.LikeStatus.Equals("-1"))
                {
                    topicCommentModel.LikeCount++;
                }

                if (!topicCommentModel.LikeStatus.Equals("1"))
                {
                    topicCommentModel.LikeStatus = "1";
                    topicCommentModel.LikeCount++;
                }
                else
                {
                    topicCommentModel.LikeStatus = "0";
                    topicCommentModel.LikeCount--;
                }
            }
            else if (status.Equals("-1"))
            {
                if (topicCommentModel.LikeStatus.Equals("1"))
                {
                    topicCommentModel.LikeCount--;
                }

                if (!topicCommentModel.LikeStatus.Equals("-1"))
                {
                    topicCommentModel.LikeStatus = "-1";
                    topicCommentModel.LikeCount--;
                }
                else
                {
                    topicCommentModel.LikeStatus = "0";
                    topicCommentModel.LikeCount++;
                }
            }

            UpdateLikeButtonColor();
            classroomController.UpdateTopicCommentLikeStatus(topicCommentModel.Id, topicCommentModel.LikeStatus, topicCommentModel.ViewsHolder.ItemIndex);
        }

        public void UpdateViews()
        {
            classroomTopicController.CheckAndGetOldTopicComments(topicCommentModel);
        }

        public void ShowUICommentUtilitiesMenu()
        {
            classroomTopicController.ShowUICommentUtilitiesMenu(topicCommentModel);
        }
    }
}
