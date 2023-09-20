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
    }
}
