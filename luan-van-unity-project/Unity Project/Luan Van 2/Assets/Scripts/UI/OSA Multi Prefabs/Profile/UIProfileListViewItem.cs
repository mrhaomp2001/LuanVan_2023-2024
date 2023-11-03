using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LuanVan.OSA
{
    [System.Serializable]
    public class UIProfileListViewItem : MonoBehaviour
    {
        [SerializeField] private Image imageAvatar;
        [SerializeField] private RectTransform messageAndFriendLayout; 
        [SerializeField] private OtherUserController otherUserController;
        [SerializeField] private ProfileController profileController;
        [SerializeField] private UIProfileModel profileModel;

        public UIProfileModel ProfileModel { get => profileModel; set => profileModel = value; }

        public void UpdateFriendStatus()
        {
            string status = "";

            if (profileModel.FriendStatusToOther.Equals("1") || profileModel.FriendStatusToOther.Equals(""))
            {
                // chưa là bạn, cần kết bạn
                status = "2";
                profileModel.FriendStatusToOther = "2";
                profileModel.FriendStatusToSelf = "3";

                profileController.UpdateViewHolder();
                otherUserController.UpdateFriendStatus(profileModel.Id, status);
                return;
            }

            if (profileModel.FriendStatusToOther.Equals("2"))
            {
                if (profileModel.FriendStatusToSelf.Equals("2"))
                {
                    // đã là bạn, cần hủy kết bạn
                    status = "1";
                    profileModel.FriendStatusToSelf = "1";
                    profileModel.FriendStatusToOther = "1";

                    profileController.UpdateViewHolder();
                    otherUserController.UpdateFriendStatus(profileModel.Id, status);
                    return;
                }

                if (profileModel.FriendStatusToSelf.Equals("3"))
                {
                    // đã gửi lời mời, cần hủy lời mời
                    status = "1";
                    profileModel.FriendStatusToOther = "1";
                    profileModel.FriendStatusToSelf = "1";

                    profileController.UpdateViewHolder();
                    otherUserController.UpdateFriendStatus(profileModel.Id, status);
                    return;
                }
            }

            if (profileModel.FriendStatusToOther.Equals("3"))
            {
                status = "2";
                profileModel.FriendStatusToSelf = "2";
                profileModel.FriendStatusToOther = "2";

                profileController.UpdateViewHolder();
                otherUserController.UpdateFriendStatus(profileModel.Id, status);
                return;
            }
        }

        public void UpdateViews()
        {
            if (!profileModel.AvatarPath.Equals(""))
            {
                Davinci.get().load(GlobalSetting.Endpoint + profileModel.AvatarPath).into(imageAvatar).setFadeTime(0).start();
            }

            if (profileModel.Id == GlobalSetting.LoginUser.Id)
            {
                messageAndFriendLayout.gameObject.SetActive(false);
            }
            else
            {
                messageAndFriendLayout.gameObject.SetActive(true);
            }
        }
    }
}
