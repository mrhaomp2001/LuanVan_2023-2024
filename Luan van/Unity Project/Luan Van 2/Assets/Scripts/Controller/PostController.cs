using Library;
using LuanVan.OSA;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class PostController : MonoBehaviour
{
    [Header("Scripts: ")]
    [SerializeField] private Redirector redirector;
    [SerializeField] private FooterNoticeController footerNoticeController;
    [Header("OSAs: ")]
    [SerializeField] private UIMultiPrefabsOSA postOSA;
    [SerializeField] private UIMultiPrefabsOSA postTemplateOSA;
    [Header("UIs: ")]
    [Header("New Post: ")]
    [SerializeField] private TMP_InputField inputFieldNewPostContent;
    [Header("Post Utilities:")]
    [SerializeField] private bool isEdit;
    [SerializeField] private UIPostTemplateModel templateModel;
    [SerializeField] private RectTransform containerUtilitiesMenu;
    [SerializeField] private RectTransform btnEditPost;
    [SerializeField] private RectTransform btnDeletePost;
    private UIPostModel postNeedEdit;
    [Header("Template: ")]
    [SerializeField] private string templateId;
    [SerializeField] private TextMeshProUGUI textTemplateName;
    [SerializeField] private TextMeshProUGUI textTemplateContentRules;
    public void GetPosts()
    {
        StartCoroutine(GetPostsCoroutine());
    }

    public IEnumerator GetPostsCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/posts" +
            "?user_id=" + GlobalSetting.LoginUser.Id);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        GetPostsResponse(res);
    }

    public void GetPostsResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        var posts = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"].Count; i++)
        {
            posts.Add(new PostItemModel()
            {
                PostModel = new UIPostModel()
                {
                    PostId = resToValue["data"][i]["id"],
                    Content = resToValue["data"][i]["content"],
                    CreateAt = resToValue["data"][i]["created_at"],
                    PosTemplateContent = resToValue["data"][i]["post_template"]["content"],
                    PosTemplateName = resToValue["data"][i]["post_template"]["name"],
                    PostTemplateId = resToValue["data"][i]["post_template"]["id"],
                    UserFullname = resToValue["data"][i]["user"]["name"],
                    Username = resToValue["data"][i]["user"]["username"],
                    UserId = resToValue["data"][i]["user"]["id"],
                    ThemeColor = resToValue["data"][i]["post_template"]["theme_color"],
                    LikeCount = (resToValue["data"][i]["post_likes_up"] - resToValue["data"][i]["post_likes_down"]),
                    LikeStatus = (resToValue["data"][i]["like_status"] != null) ? resToValue["data"][i]["like_status"]["like_status"].ToString() : "0",
                }
            });
        }

        postOSA.Data.ResetItems(posts);
    }

    public void UpdateOrCreatePost()
    {
        if (templateId.Equals("0"))
        {
            footerNoticeController.SendAFooterMessage("Hãy chọn 1 mẫu bài đăng");
            return;
        }
        if (inputFieldNewPostContent.text.Equals(""))
        {
            footerNoticeController.SendAFooterMessage("Hãy nhập nội dung cho bài đăng");
            return;
        }

        if (isEdit)
        {
            StartCoroutine(UpdateAPostCoroutine());
        }
        else
        {
            StartCoroutine(UploadAPostCoroutine());
        }

        redirector.Pop();
    }

    public IEnumerator UploadAPostCoroutine()
    {
        WWWForm body = new WWWForm();

        body.AddField("content", inputFieldNewPostContent.text);
        body.AddField("user_id", GlobalSetting.LoginUser.Id);
        body.AddField("post_template_id", templateId);

        footerNoticeController.SendAFooterMessage("Bài đăng của bạn đang được gửi");

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/posts", body);

        inputFieldNewPostContent.text = "";

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        footerNoticeController.SendAFooterMessage("Bạn đã đăng bài thành công!");

        string res = request.downloadHandler.text;

        Debug.Log(res);

    }

    public IEnumerator UpdateAPostCoroutine()
    {
        WWWForm body = new WWWForm();

        body.AddField("id", postNeedEdit.PostId);
        body.AddField("content", inputFieldNewPostContent.text);
        body.AddField("post_template_id", templateId);

        footerNoticeController.SendAFooterMessage("Bài đăng của bạn đang được sửa");

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/post/edit", body);

        postNeedEdit.Content = inputFieldNewPostContent.text;

        postNeedEdit.ViewsHolder.textContent.text = postNeedEdit.Content;

        postNeedEdit.PostTemplateId = templateModel.Id;
        postNeedEdit.ThemeColor = templateModel.ThemeColor;
        postNeedEdit.PosTemplateName = templateModel.Name;
        postNeedEdit.PosTemplateContent = templateModel.Content;
        postNeedEdit.ViewsHolder.MarkForRebuild();
        postOSA.ScheduleForceRebuildLayout();

        inputFieldNewPostContent.text = "";

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        footerNoticeController.SendAFooterMessage("Bạn đã sửa bài đăng thành công!");

        string res = request.downloadHandler.text;


        Debug.Log(res);
    }

    public void ShowUIUploadNewPostOrEditPost()
    {
        isEdit = false;
        templateId = "0";
        textTemplateName.text = "Hãy chọn 1 mãu bài đăng";
        textTemplateContentRules.text = "Hãy chọn 1 mẫu bài đăng để xem quy định của mẫu đó.";
        inputFieldNewPostContent.text = "";
        redirector.Push("post.upload");
    }

    public void ShowUISelectPostTemplate()
    {
        redirector.Push("post.upload.template");
        GetTemplates();
    }

    public void GetTemplates()
    {
        StartCoroutine(GetTemplatesCoroutine());
    }

    public IEnumerator GetTemplatesCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/post-templates");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        GetTemplatesResponse(res);
    }

    public void GetTemplatesResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        List<BaseModel> templates = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"].Count; i++)
        {
            templates.Add(new PostTemplateItemModel()
            {
                PostTemplateModel = new UIPostTemplateModel()
                {
                    CreateAt = resToValue["data"][i]["created_at"],
                    Content = resToValue["data"][i]["content"],
                    Id = resToValue["data"][i]["id"],
                    Name = resToValue["data"][i]["name"],
                    ThemeColor = resToValue["data"][i]["theme_color"],
                }
            });
        }

        postTemplateOSA.Data.ResetItems(templates);
    }

    public void SelectTemplate(UIPostTemplateModel postTemplateModel)
    {
        templateId = postTemplateModel.Id;
        textTemplateName.text = postTemplateModel.Name;
        textTemplateContentRules.text = postTemplateModel.Content;
        templateModel = postTemplateModel;
        redirector.Pop();
    }

    public void UpdateOrCreateLikeStatus(UIPostModel postModel)
    {
        StartCoroutine(UpdateOrCreateLikeStatusCoroutine(postModel));
    }

    public IEnumerator UpdateOrCreateLikeStatusCoroutine(UIPostModel postModel)
    {
        WWWForm body = new WWWForm();

        body.AddField("user_id", GlobalSetting.LoginUser.Id);
        body.AddField("post_id", postModel.PostId);
        body.AddField("like_status", postModel.LikeStatus);

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/post/like", body);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

    }

    public void ShowPostUtilitiesMenu(UIPostModel postModel)
    {
        containerUtilitiesMenu.gameObject.SetActive(true);
        btnDeletePost.gameObject.SetActive(false);
        btnEditPost.gameObject.SetActive(false);

        if (postModel.UserId.Equals(GlobalSetting.LoginUser.Id))
        {
            btnDeletePost.gameObject.SetActive(true);
            btnEditPost.gameObject.SetActive(true);
        }

        isEdit = true;
        postNeedEdit = postModel;

        inputFieldNewPostContent.text = postModel.Content;

        templateId = postModel.PostTemplateId;
        textTemplateName.text = postModel.PosTemplateName;
        textTemplateContentRules.text = postModel.PosTemplateContent;
    }

    public void ShowEditPostMenu()
    {
        containerUtilitiesMenu.gameObject.SetActive(false);
        redirector.Push("post.upload");
    }
}
