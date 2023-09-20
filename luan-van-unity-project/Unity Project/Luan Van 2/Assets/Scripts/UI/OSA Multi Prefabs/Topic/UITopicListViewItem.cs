using Com.TheFallenGames.OSA.Demos.DifferentPrefabPerOrientation.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LuanVan.OSA
{
    public class UITopicListViewItem : MonoBehaviour
    {
        [SerializeField] private Image imageLikeUp;
        [SerializeField] private Image imageLikeDown;

        [SerializeField] private Color colorLikeChoice;
        [SerializeField] private Color colorUnLikeChoice;

        [SerializeField] private ClassroomController classroomController;
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
    }
}
