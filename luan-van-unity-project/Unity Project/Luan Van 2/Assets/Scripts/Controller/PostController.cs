using Library;
using LuanVan.OSA;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class PostController : MonoBehaviour
{
    [Header("Post Stats: ")]
    [SerializeField] private int page;
    [SerializeField] private int postGetPerPage;
    [Header("Scripts: ")]
    [SerializeField] private Redirector redirector;
    [SerializeField] private FooterNoticeController footerNoticeController;
    [Header("OSAs: ")]
    [SerializeField] private UIMultiPrefabsOSA postOSA;
    [SerializeField] private UIMultiPrefabsOSA postCommentOSA;
    [SerializeField] private UIMultiPrefabsOSA postTemplateOSA;
    [Header("UIs: ")]
    [Header("New Post: ")]
    [SerializeField] private TMP_InputField inputFieldNewPostContent;
    [SerializeField] private Toggle toggleVisibility;
    [Header("Post Utilities:")]
    [SerializeField] private bool isEdit;
    [SerializeField] private UIPostTemplateModel templateModel = new UIPostTemplateModel();
    [SerializeField] private RectTransform containerUtilitiesMenu;
    [SerializeField] private RectTransform btnEditPost;
    [SerializeField] private RectTransform btnDeletePost;
    [Header("Comments: ")]
    [SerializeField] private TMP_InputField inputFieldNewCommentContent;
    [SerializeField] private UICommentModel currentCommentModelSelect;
    [SerializeField] private RectTransform containerUtilitiesCommentMenu;
    [SerializeField] private TMP_InputField inputFieldEditComment;
    [Header("Template: ")]
    [SerializeField] private string templateId;
    [SerializeField] private TextMeshProUGUI textTemplateName;
    [SerializeField] private TextMeshProUGUI textTemplateContentRules;

    private UIPostModel currentPostSelect;
    public void GetPosts()
    {
        StartCoroutine(GetPostsCoroutine());
    }

    public IEnumerator GetPostsCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/posts" +
            "?user_id=" + GlobalSetting.LoginUser.Id +
            "&page=" + page +
            "&per_page=" + postGetPerPage);

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

        for (int i = 0; i < resToValue["data"]["data"].Count; i++)
        {
            posts.Add(new PostItemModel()
            {
                PostModel = new UIPostModel()
                {
                    PostId = resToValue["data"]["data"][i]["id"],
                    Content = resToValue["data"]["data"][i]["content"],
                    CreateAt = resToValue["data"]["data"][i]["created_at"],
                    PosTemplateContent = resToValue["data"]["data"][i]["post_template"]["content"],
                    PosTemplateName = resToValue["data"]["data"][i]["post_template"]["name"],
                    PostTemplateId = resToValue["data"]["data"][i]["post_template"]["id"],
                    UserFullname = resToValue["data"]["data"][i]["user"]["name"],
                    Username = resToValue["data"]["data"][i]["user"]["username"],
                    UserId = resToValue["data"]["data"][i]["user"]["id"],
                    ThemeColor = resToValue["data"]["data"][i]["post_template"]["theme_color"],
                    LikeCount = (resToValue["data"]["data"][i]["post_likes_up"] - resToValue["data"]["data"][i]["post_likes_down"]),
                    LikeStatus = (resToValue["data"]["data"][i]["like_status"] != null) ? resToValue["data"]["data"][i]["like_status"]["like_status"].ToString() : "0",
                    CommentCount = resToValue["data"]["data"][i]["comment_count"],
                    PostStatus = resToValue["data"]["data"][i]["post_status_id"],
                }
            });
            //Debug.Log((resToValue["data"]["data"][i]["post_likes_up"] - resToValue["data"]["data"][i]["post_likes_down"]).ToString());
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
        body.AddField("post_status_id", (toggleVisibility.isOn) ? 2 : 1);

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

        body.AddField("id", currentPostSelect.PostId);
        body.AddField("content", inputFieldNewPostContent.text);
        body.AddField("post_template_id", templateId);
        body.AddField("post_status_id", (toggleVisibility.isOn) ? "2" : "1");


        footerNoticeController.SendAFooterMessage("Bài đăng của bạn đang được sửa");

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/post/edit", body);

        currentPostSelect.Content = inputFieldNewPostContent.text;

        currentPostSelect.ViewsHolder.textContent.text = currentPostSelect.Content;

        currentPostSelect.PostTemplateId = templateModel.Id;
        currentPostSelect.ThemeColor = templateModel.ThemeColor;
        currentPostSelect.PosTemplateName = templateModel.Name;
        currentPostSelect.PosTemplateContent = templateModel.Content;
        currentPostSelect.PostStatus = (toggleVisibility.isOn) ? "2" : "1";
        currentPostSelect.ViewsHolder.MarkForRebuild();

        postOSA.ForceUpdateViewsHolderIfVisible(currentPostSelect.ItemIndexOSA);

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

    public void DeletePost()
    {
        StartCoroutine(DeleteAPostCoroutine());
    }

    public IEnumerator DeleteAPostCoroutine()
    {
        WWWForm body = new WWWForm();

        body.AddField("id", currentPostSelect.PostId);
        body.AddField("content", inputFieldNewPostContent.text);
        body.AddField("post_template_id", templateId);
        body.AddField("post_status_id", 3);

        footerNoticeController.SendAFooterMessage("Bài đăng của bạn đang được xóa");

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/post/edit", body);

        currentPostSelect.Content = inputFieldNewPostContent.text;

        currentPostSelect.ViewsHolder.textContent.text = currentPostSelect.Content;

        currentPostSelect.PostTemplateId = templateModel.Id;
        currentPostSelect.ThemeColor = templateModel.ThemeColor;
        currentPostSelect.PosTemplateName = templateModel.Name;
        currentPostSelect.PosTemplateContent = templateModel.Content;

        foreach (var postInPostOSA in postOSA.Data)
        {
            if (!(postInPostOSA is PostItemModel postNeedEdit))
            {
                continue;
            }

            if (postNeedEdit.PostModel.PostId.Equals(currentPostSelect.PostId))
            {
                postOSA.Data.RemoveOne(postInPostOSA.id);
                break;
            }
        }

        currentPostSelect.ViewsHolder.MarkForRebuild();

        postOSA.ForceUpdateViewsHolderIfVisible(currentPostSelect.ItemIndexOSA);

        inputFieldNewPostContent.text = "";

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        footerNoticeController.SendAFooterMessage("Bạn đã xóa bài đăng thành công!");

        string res = request.downloadHandler.text;


        Debug.Log(res);
    }

    public void ShowUIUploadNewPostOrEditPost()
    {
        isEdit = false;
        toggleVisibility.isOn = false;
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
        foreach (var item in postOSA.Data)
        {
            if (item is PostItemModel post)
            {
                if (post.PostModel.PostId.Equals(postModel.PostId))
                {
                    postOSA.ForceUpdateViewsHolderIfVisible(item.id);
                    break;
                }
            }
        }
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
        currentPostSelect = postModel;

        inputFieldNewPostContent.text = postModel.Content;

        templateId = postModel.PostTemplateId;
        textTemplateName.text = postModel.PosTemplateName;
        textTemplateContentRules.text = postModel.PosTemplateContent;
        toggleVisibility.isOn = (postModel.PostStatus.Equals("2")) ? true : false;

        templateModel.ThemeColor = postModel.ThemeColor;
    }

    public void ShowEditPostMenu()
    {
        containerUtilitiesMenu.gameObject.SetActive(false);
        redirector.Push("post.upload");
    }

    public void ShowUIPostCommentAndGetComments(UIPostModel postModel)
    {
        currentPostSelect = postModel;
        postCommentOSA.Data.ResetItems(new List<BaseModel>());
        redirector.Push("post.comment");

        postCommentOSA.Data.InsertOneAtStart(new PostItemModel()
        {
            PostModel = postModel,
        });

        StartCoroutine(GetCommentsCoroutine(postModel));
    }

    public IEnumerator GetCommentsCoroutine(UIPostModel postModel)
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/post/comments" +
            "?user_id=" + GlobalSetting.LoginUser.Id +
            "&post_id=" + postModel.PostId);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        GetCommentsResponse(res);
    }

    public void GetCommentsResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        var commentModels = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"].Count; i++)
        {
            commentModels.Add(new CommentItemModel
            {
                CommentModel = new UICommentModel()
                {
                    Content = resToValue["data"][i]["content"],
                    CreatedAt = resToValue["data"][i]["created_at"],
                    Id = resToValue["data"][i]["id"],
                    PostId = resToValue["data"][i]["post_id"],
                    UserId = resToValue["data"][i]["user_id"],
                    UserFullName = resToValue["data"][i]["user"]["name"],
                    Username = resToValue["data"][i]["user"]["username"],
                    LikeCount = (resToValue["data"][i]["like_up"] - resToValue["data"][i]["like_down"]),
                    LikeStatus = (resToValue["data"][i]["like_status"]["like_status"]) ?? "0",
                }
            });
        }

        postCommentOSA.Data.InsertItems(1, commentModels);
    }

    public void SendComment()
    {
        StartCoroutine(SendCommentCoroutine());
    }

    public IEnumerator SendCommentCoroutine()
    {
        WWWForm body = new WWWForm();

        body.AddField("user_id", GlobalSetting.LoginUser.Id);
        body.AddField("post_id", currentPostSelect.PostId);
        body.AddField("comment_status_id", 1);
        body.AddField("content", inputFieldNewCommentContent.text);

        inputFieldNewCommentContent.text = "";

        var post = postCommentOSA.Data[0] as PostItemModel;

        post.PostModel.CommentCount++;
        postCommentOSA.ForceUpdateViewsHolderIfVisible(0);

        foreach (var postInPostOSA in postOSA.Data)
        {
            if (postInPostOSA is PostItemModel postNeedEdit)
            {
                if (postNeedEdit.PostModel.PostId.Equals(post.PostModel.PostId))
                {
                    postOSA.ForceUpdateViewsHolderIfVisible(postInPostOSA.id);
                    break;
                }
            }
        }

        footerNoticeController.SendAFooterMessage("Đang gửi bình luận của bạn");


        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/post/comments", body);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        footerNoticeController.SendAFooterMessage("Gửi bình luận Thành công");

        string res = request.downloadHandler.text;

        Debug.Log(res);

        var resToValue = JSONNode.Parse(res);

        var comment = new CommentItemModel
        {
            CommentModel = new UICommentModel()
            {
                Content = resToValue["data"]["content"],
                CreatedAt = resToValue["data"]["created_at"],
                Id = resToValue["data"]["id"],
                PostId = resToValue["data"]["post_id"],
                UserId = resToValue["data"]["user_id"],
                UserFullName = resToValue["data"]["user"]["name"],
                Username = resToValue["data"]["user"]["username"],
                LikeCount = 0,
                LikeStatus = "0",
            }
        };

        postCommentOSA.Data.InsertOne(1, comment);


    }

    public void UpdateOrCreateCommentLikeStatus(UICommentModel commentModel)
    {
        StartCoroutine(UpdateOrCreateCommentLikeStatusCoroutine(commentModel));
    }

    public IEnumerator UpdateOrCreateCommentLikeStatusCoroutine(UICommentModel commentModel)
    {
        WWWForm body = new WWWForm();

        body.AddField("user_id", GlobalSetting.LoginUser.Id);
        body.AddField("comment_id", commentModel.Id);
        body.AddField("like_status", commentModel.LikeStatus);

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/post/comment/like", body);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);
    }

    public void ShowCommentUtilitiesMenu(UICommentModel commentModel)
    {
        currentCommentModelSelect = commentModel;

        containerUtilitiesCommentMenu.gameObject.SetActive(true);

        inputFieldEditComment.text = currentCommentModelSelect.Content;
    }

    public void DeleteComment()
    {
        StartCoroutine(DeleteCommentCoroutine());
    }
    public IEnumerator DeleteCommentCoroutine()
    {
        WWWForm body = new WWWForm();

        body.AddField("comment_id", currentCommentModelSelect.Id);
        body.AddField("content", currentCommentModelSelect.Content);
        body.AddField("comment_status_id", 2);

        footerNoticeController.SendAFooterMessage("Bình luận của bạn đang được xóa");

        var postModel = postCommentOSA.Data[0] as PostItemModel;

        postModel.PostModel.CommentCount--;
        postCommentOSA.ForceUpdateViewsHolderIfVisible(0);

        foreach (var item in postOSA.Data)
        {
            if (item is PostItemModel post)
            {
                if (post.PostModel.PostId.Equals(postModel.PostModel.PostId))
                {
                    postOSA.ForceUpdateViewsHolderIfVisible(item.id);
                    break;
                }
            }
        }

        postCommentOSA.Data.RemoveOne(currentCommentModelSelect.ViewsHolder.ItemIndex);

        containerUtilitiesCommentMenu.gameObject.SetActive(false);

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/post/comment/edit", body);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        footerNoticeController.SendAFooterMessage("Bạn đã xóa bình luận thành công!");

        string res = request.downloadHandler.text;

        Debug.Log(res);
    }    

    public void EditComment()
    {
        StartCoroutine(EditCommentCoroutine());
    }

    public IEnumerator EditCommentCoroutine()
    {
        WWWForm body = new WWWForm();

        body.AddField("comment_id", currentCommentModelSelect.Id);
        body.AddField("content", inputFieldEditComment.text);
        body.AddField("comment_status_id", 1);

        currentCommentModelSelect.Content = inputFieldEditComment.text;

        postCommentOSA.ForceUpdateViewsHolderIfVisible(currentCommentModelSelect.ViewsHolder.ItemIndex);

        inputFieldEditComment.text = "";

        footerNoticeController.SendAFooterMessage("Bình luận của bạn đang được sửa");

        var postModel = postCommentOSA.Data[0] as PostItemModel;

        containerUtilitiesCommentMenu.gameObject.SetActive(false);

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/post/comment/edit", body);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        footerNoticeController.SendAFooterMessage("Bạn đã sửa bình luận thành công!");

        string res = request.downloadHandler.text;

        Debug.Log(res);
    }
}
