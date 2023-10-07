using LuanVan.OSA;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LuanVan.OSA
{
    public class UICommentListViewItem : MonoBehaviour
    {
        [SerializeField] private Image imageAvatar;
        [SerializeField] private Image imageLikeUp;
        [SerializeField] private Image imageLikeDown;

        [SerializeField] private Color colorLikeChoice;
        [SerializeField] private Color colorUnLikeChoice;

        [SerializeField] private Redirector redirector;
        [SerializeField] private ProfileController profileController;
        [SerializeField] private PostController postController;
        [SerializeField] private UICommentModel commentModel;

        public UICommentModel CommentModel { get => commentModel; set => commentModel = value; }

        public void UpdateViews()
        {
            if (!commentModel.AvatarPath.Equals(""))
            {
                Davinci.get().load(GlobalSetting.Endpoint + commentModel.AvatarPath).into(imageAvatar).setFadeTime(0).start();
            }
        }

        public void UpdateLikeButtonColor()
        {

            imageLikeUp.color = colorUnLikeChoice;
            imageLikeDown.color = colorUnLikeChoice;

            if (commentModel.LikeStatus.Equals("1"))
            {
                imageLikeUp.color = colorLikeChoice;
            }

            if (commentModel.LikeStatus.Equals("-1"))
            {
                imageLikeDown.color = colorLikeChoice;
            }
        }


        public void LikeComment()
        {
            if (commentModel.LikeStatus.Equals("-1"))
            {
                commentModel.LikeCount++;
            }

            if (!commentModel.LikeStatus.Equals("1"))
            {
                commentModel.LikeStatus = "1";
                commentModel.LikeCount++;
            }
            else
            {
                commentModel.LikeStatus = "0";
                commentModel.LikeCount--;
            }

            UpdateLikeButtonColor();

            commentModel.ViewsHolder.textLikeCount.text = commentModel.LikeCount.ToString();
            postController.UpdateOrCreateCommentLikeStatus(commentModel);
        }

        public void DislikeComment()
        {
            if (commentModel.LikeStatus.Equals("1"))
            {
                commentModel.LikeCount--;
            }

            if (!commentModel.LikeStatus.Equals("-1"))
            {
                commentModel.LikeStatus = "-1";
                commentModel.LikeCount--;
            }
            else
            {
                commentModel.LikeStatus = "0";
                commentModel.LikeCount++;
            }

            UpdateLikeButtonColor();

            commentModel.ViewsHolder.textLikeCount.text = commentModel.LikeCount.ToString();
            postController.UpdateOrCreateCommentLikeStatus(commentModel);

        }

        public void ShowCommentUtilitiesMenu()
        {
            postController.ShowCommentUtilitiesMenu(commentModel);
        }

        public void CheckAndGetOldComment()
        {
            postController.CheckAndGetOldComment(commentModel);
        }

        public void ShowProfile()
        {
            redirector.Push("profile");
            profileController.GetUserProfile(commentModel.UserId);
        }
    }
}
