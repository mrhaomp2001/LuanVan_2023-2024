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

        classroomController.GetClassrooms();
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

        postController.GetPosts();
    }

    public void RedirectSetting()
    {
        redirector.Push("auth");
    }
}
