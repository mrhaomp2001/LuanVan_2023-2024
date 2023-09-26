using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LuanVan.OSA
{
    public class UIPostListViewItem : MonoBehaviour
    {
        [SerializeField] private Image imageLikeUp;
        [SerializeField] private Image imageLikeDown;

        [SerializeField] private Color colorLikeChoice;
        [SerializeField] private Color colorUnLikeChoice;

        [SerializeField] private Redirector redirector;
        [SerializeField] private PostController postController;
        [SerializeField] private ProfileController profileController;

        [SerializeField] private UIPostModel postModel;
        public UIPostModel PostModel { get => postModel; set => postModel = value; }

        public void UpdateLikeButtonColor()
        {
            imageLikeUp.color = colorUnLikeChoice;
            imageLikeDown.color = colorUnLikeChoice;

            if (PostModel.LikeStatus.Equals("1"))
            {
                imageLikeUp.color = colorLikeChoice;
            }

            if (PostModel.LikeStatus.Equals("-1"))
            {
                imageLikeDown.color = colorLikeChoice;
            }
        }

        public void LikePost()
        {
            if (PostModel.LikeStatus.Equals("-1"))
            {
                postModel.LikeCount++;
            }

            if (!PostModel.LikeStatus.Equals("1"))
            {
                PostModel.LikeStatus = "1";
                postModel.LikeCount++;
            }
            else
            {
                PostModel.LikeStatus = "0";
                postModel.LikeCount--;
            }

            UpdateLikeButtonColor();

            postModel.ViewsHolder.textLikeCount.text = postModel.LikeCount.ToString();

            postController.UpdateOrCreateLikeStatus(postModel);
        }

        public void DislikePost()
        {
            if (PostModel.LikeStatus.Equals("1"))
            {
                postModel.LikeCount--;
            }

            if (!PostModel.LikeStatus.Equals("-1"))
            {
                PostModel.LikeStatus = "-1";
                postModel.LikeCount--;
            }
            else
            {
                PostModel.LikeStatus = "0";
                postModel.LikeCount++;
            }

            UpdateLikeButtonColor();

            postModel.ViewsHolder.textLikeCount.text = postModel.LikeCount.ToString();

            postController.UpdateOrCreateLikeStatus(postModel);
        }

        public void ShowUUtilitiesMenu()
        {
            postController.ShowPostUtilitiesMenu(postModel);
        }

        public void ShowUIPostCommentAndGetComments()
        {
            postController.ShowUIPostCommentAndGetComments(postModel);
        }

        public void CheckAndGetOldPosts()
        {
            if (postModel.ContainerOSA.Equals("post"))
            {
                postController.CheckAndGetOldPosts(postModel);
            }

            if (postModel.ContainerOSA.Equals("profile"))
            {
                profileController.CheckAndGetOldPosts(postModel);
            }
            
        }

        public void ShowProfile()
        {
            redirector.Push("profile");
            profileController.GetUserProfile(postModel.UserId);
        }
    }
}
