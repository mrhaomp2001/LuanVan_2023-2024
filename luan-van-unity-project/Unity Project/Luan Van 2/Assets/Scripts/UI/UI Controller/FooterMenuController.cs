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
        redirector.Pop();
        redirector.Push("classroom");

        foreach (var item in imageBtnFooterMenus)
        {
            var currentColor = item.color;
            currentColor.a = 0;
            item.color = currentColor;
        }
        var afterColor = imageBtnFooterMenus[0].color;
        afterColor.a = 1;
        imageBtnFooterMenus[1].color = afterColor;

        if (classroomController.ClassroomOSA.Data.Count <= 0)
        {
            classroomController.GetClassrooms();
        }
    }

    public void RedirectPost()
    {
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
        redirector.Push("auth");
    }

    public void RedirectFriend()
    {
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

        otherUserController.GetLatestLoginUsers();
    }    

    public void RedirectProfile()
    {
        redirector.Push("profile");

        profileController.GetUserProfile(GlobalSetting.LoginUser.Id);

        //foreach (var item in imageBtnFooterMenus)
        //{
        //    var currentColor = item.color;
        //    currentColor.a = 0;
        //    item.color = currentColor;
        //}
        //var afterColor = imageBtnFooterMenus[0].color;
        //afterColor.a = 1;
        //imageBtnFooterMenus[4].color = afterColor;

    }
}
