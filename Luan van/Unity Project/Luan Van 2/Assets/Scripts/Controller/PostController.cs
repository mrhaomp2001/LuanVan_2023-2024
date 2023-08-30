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
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/posts?user-id=1");

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
                }
            });
        }

        postOSA.Data.ResetItems(posts);
    }

    public void UploadAPost()
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
        StartCoroutine(UploadAPostCoroutine());

        redirector.Pop();
    }

    public IEnumerator UploadAPostCoroutine()
    {
        WWWForm body = new WWWForm();

        body.AddField("content", inputFieldNewPostContent.text);
        body.AddField("user_id", "1");
        body.AddField("post_template_id", templateId);

        footerNoticeController.SendAFooterMessage("Bài đăng của bạn đang được xử lý");

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

    public void ShowUIUploadNewPost()
    {
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

        redirector.Pop();
    }
}
