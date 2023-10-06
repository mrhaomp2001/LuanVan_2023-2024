using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FooterMenuController : MonoBehaviour
{
    [SerializeField] private Redirector redirector;
    [SerializeField] private List<Image> imageBtnFooterMenus = new List<Image>();
    [SerializeField] private ClassroomController classroomController;
    [SerializeField] private PostController postController;
    [SerializeField] private OtherUserController otherUserController;
    [SerializeField] private ProfileController profileController;
    [SerializeField] private NotificationController notificationController;
    [SerializeField] private SettingMenuController settingMenuController;
    private void Start()
    {
        RedirectHome();
    }

    public void RedirectHome()
    {
        redirector.Pop();
        redirector.Push("home");

        foreach (var item in imageBtnFooterMenus)
        {
            var currentColor = item.color;
            currentColor.a = 0;
            item.color = currentColor;
        }
        var afterColor = imageBtnFooterMenus[0].color;
        afterColor.a = 1;
        imageBtnFooterMenus[0].color = afterColor;
    }

    public void RedirectClassroom()
    {

        if (GlobalSetting.LoginUser.Id.Equals(""))
        {
            redirector.Push("auth");
            return;
        }

        redirector.Pop();
        redirector.Push("classroom.user");

        foreach (var item in imageBtnFooterMenus)
        {
            var currentColor = item.color;
            currentColor.a = 0;
            item.color = currentColor;
        }
        var afterColor = imageBtnFooterMenus[0].color;
        afterColor.a = 1;
        imageBtnFooterMenus[1].color = afterColor;

        classroomController.GetUserClassrooms();

    }

    public void RedirectPost()
    {
        if (GlobalSetting.LoginUser.Id.Equals(""))
        {
            redirector.Push("auth");
            return;
        }

        redirector.Pop();
        redirector.Push("post");

        foreach (var item in imageBtnFooterMenus)
        {
            var currentColor = item.color;
            currentColor.a = 0;
            item.color = currentColor;
        }
        var afterColor = imageBtnFooterMenus[0].color;
        afterColor.a = 1;
        imageBtnFooterMenus[2].color = afterColor;

        if (postController.PostOSA.Data.Count <= 0)
        {
            postController.GetPosts();
        }
    }

    public void RedirectSetting()
    {
        if (GlobalSetting.LoginUser.Id.Equals(""))
        {
            redirector.Push("auth");
            return;
        }
        redirector.Pop();
        redirector.Push("setting");

        foreach (var item in imageBtnFooterMenus)
        {
            var currentColor = item.color;
            currentColor.a = 0;
            item.color = currentColor;
        }
        var afterColor = imageBtnFooterMenus[0].color;
        afterColor.a = 1;
        imageBtnFooterMenus[5].color = afterColor;

        settingMenuController.SetLoginInfo();
    }

    public void RedirectFriend()
    {
        if (GlobalSetting.LoginUser.Id.Equals(""))
        {
            redirector.Push("auth");
            return;
        }

        redirector.Pop();
        redirector.Push("other_users");

        foreach (var item in imageBtnFooterMenus)
        {
            var currentColor = item.color;
            currentColor.a = 0;
            item.color = currentColor;
        }
        var afterColor = imageBtnFooterMenus[0].color;
        afterColor.a = 1;
        imageBtnFooterMenus[3].color = afterColor;

        otherUserController.GetFriends();
    }    

    public void RedirectNotifications()
    {

        if (GlobalSetting.LoginUser.Id.Equals(""))
        {
            redirector.Push("auth");
            return;
        }

        foreach (var item in imageBtnFooterMenus)
        {
            var currentColor = item.color;
            currentColor.a = 0;
            item.color = currentColor;
        }
        var afterColor = imageBtnFooterMenus[4].color;
        afterColor.a = 1;
        imageBtnFooterMenus[5].color = afterColor;

        redirector.Push("notification");
        notificationController.GetNotifications();
    }
}
