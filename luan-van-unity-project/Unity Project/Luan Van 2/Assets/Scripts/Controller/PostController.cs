using Library;
using LuanVan.OSA;
using SimpleFileBrowser;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class PostController : MonoBehaviour
{
    [Header("Post Stats: ")]
    [SerializeField] private int postGetPerPage;
    [SerializeField] private string filterGetPosts;
    [Header("Scripts: ")]
    [SerializeField] private Redirector redirector;
    [SerializeField] private FooterNoticeController footerNoticeController;
    [SerializeField] private ResponseErrorChecker responseErrorChecker;
    [Header("OSAs: ")]
    [SerializeField] private UIMultiPrefabsOSA postOSA;
    [SerializeField] private UIMultiPrefabsOSA postFilterOSA;
    [SerializeField] private UIMultiPrefabsOSA postCommentOSA;
    [SerializeField] private UIMultiPrefabsOSA postTemplateOSA;
    [Header("UIs: ")]
    [Header("New Post: ")]
    [SerializeField] private TMP_InputField inputFieldNewPostContent;
    [SerializeField] private TMP_InputField inputFieldNewPostTitle;
    [SerializeField] private Toggle toggleVisibility;
    [SerializeField] private Image postImage;
    [SerializeField] private TextMeshProUGUI textPostImageButtonUI;
    [Header("Post Utilities:")]
    [SerializeField] private bool isEdit;
    [SerializeField] private UIPostTemplateModel templateModel = new UIPostTemplateModel();
    [SerializeField] private RectTransform containerUtilitiesMenu;
    [SerializeField] private RectTransform btnEditPost;
    [SerializeField] private RectTransform btnDeletePost;
    [Header("Comments: ")]
    [SerializeField] private int commentGetPerPage;
    [SerializeField] private TMP_InputField inputFieldNewCommentContent;
    [SerializeField] private UICommentModel currentCommentModelSelect;
    [SerializeField] private RectTransform containerUtilitiesCommentMenu;
    [SerializeField] private TMP_InputField inputFieldEditComment;
    [SerializeField] private RectTransform btnEditComment;
    [SerializeField] private RectTransform btnDeleteComment;
    [Header("Template: ")]
    [SerializeField] private string templateId;
    [SerializeField] private TextMeshProUGUI textTemplateName;
    [SerializeField] private TextMeshProUGUI textTemplateContentRules;

    [SerializeField] private UIPostModel currentPostSelect;

    public UIMultiPrefabsOSA PostOSA { get => postOSA; set => postOSA = value; }
    public UIPostModel CurrentPostSelect { get => currentPostSelect; set => currentPostSelect = value; }
    public UICommentModel CurrentCommentModelSelect { get => currentCommentModelSelect; set => currentCommentModelSelect = value; }

    public void GetPosts()
    {
        Davinci.ClearAllCachedFiles();
        StartCoroutine(GetPostsCoroutine());
    }

    public IEnumerator GetPostsCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/posts" +
            "?user_id=" + GlobalSetting.LoginUser.Id +
            "&per_page=" + postGetPerPage +
            "&filter=" + filterGetPosts);

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
                    AvatarPath = resToValue["data"]["data"][i]["user"]["avatar_path"],
                    UserId = resToValue["data"]["data"][i]["user"]["id"],
                    ThemeColor = resToValue["data"]["data"][i]["post_template"]["theme_color"],
                    LikeCount = (resToValue["data"]["data"][i]["post_likes_up"] - resToValue["data"]["data"][i]["post_likes_down"]),
                    LikeStatus = (resToValue["data"]["data"][i]["like_status"] != null) ? resToValue["data"]["data"][i]["like_status"]["like_status"].ToString() : "0",
                    CommentCount = resToValue["data"]["data"][i]["comment_count"],
                    PostStatus = resToValue["data"]["data"][i]["post_status_id"],
                    ImagePath = resToValue["data"]["data"][i]["image_path"],
                    PostTitle = resToValue["data"]["data"][i]["title"],
                    ContainerOSA = "post",
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

        if (templateId.Equals("2"))
        {
            if (inputFieldNewPostTitle.text.Equals(""))
            {
                footerNoticeController.SendAFooterMessage("Hãy nhập tiêu đề của bài đăng\nĐọc lại quy định viết bài để rõ hơn");
                return;
            }
        }

        if (templateId.Equals("3"))
        {
            if (inputFieldNewPostTitle.text.Equals(""))
            {
                footerNoticeController.SendAFooterMessage("Hãy nhập tiêu đề của bài đăng\nĐọc lại quy định viết bài để rõ hơn");
                return;
            }
            if (postImage.sprite == null)
            {
                footerNoticeController.SendAFooterMessage("Hãy đăng một bức ảnh minh họa\nĐọc lại quy định viết bài để rõ hơn");
                return;
            }
        }

        responseErrorChecker.SendRequest();

        if (isEdit)
        {
            StartCoroutine(UpdateAPostCoroutine());
        }
        else
        {
            StartCoroutine(UploadAPostCoroutine());
        }

    }

    public IEnumerator UploadAPostCoroutine()
    {
        WWWForm body = new WWWForm();

        body.AddField("content", inputFieldNewPostContent.text);
        body.AddField("title", inputFieldNewPostTitle.text);
        body.AddField("user_id", GlobalSetting.LoginUser.Id);
        body.AddField("post_template_id", templateId);
        body.AddField("post_status_id", (toggleVisibility.isOn) ? 2 : 1);

        if (postImage.sprite != null)
        {
            Debug.Log("here");
            byte[] textureBytes = null;
            textureBytes = GetTextureCopy(postImage.sprite.texture).EncodeToPNG();
            body.AddBinaryData("image", textureBytes, "image.png", "image/png");
        }

        footerNoticeController.SendAFooterMessage("Bài đăng của bạn đang được gửi");

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/posts", body);

        inputFieldNewPostContent.text = "";
        inputFieldNewPostTitle.text = "";

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }


        string res = request.downloadHandler.text;

        Debug.Log(res);

        JSONNode resToValues = JSONNode.Parse(res);
        if (resToValues["message"] != null)
        {
            responseErrorChecker.GetResponse(resToValues["message"][0][0]);
            Debug.Log(resToValues["message"][0][0]);
            yield break;
        }
        responseErrorChecker.GetResponse("");

        redirector.Pop();
        footerNoticeController.SendAFooterMessage("Bạn đã đăng bài thành công!");
    }

    public IEnumerator UpdateAPostCoroutine()
    {
        WWWForm body = new WWWForm();

        body.AddField("id", currentPostSelect.PostId);
        body.AddField("content", inputFieldNewPostContent.text);
        body.AddField("title", inputFieldNewPostTitle.text);
        body.AddField("post_template_id", templateId);
        body.AddField("post_status_id", (toggleVisibility.isOn) ? "2" : "1");

        if (postImage.sprite != null)
        {
            byte[] textureBytes = null;
            textureBytes = GetTextureCopy(postImage.sprite.texture).EncodeToPNG();
            body.AddBinaryData("image", textureBytes, "image.png", "image/png");
        }

        footerNoticeController.SendAFooterMessage("Bài đăng của bạn đang được sửa");

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/post/edit", body);

        currentPostSelect.Content = inputFieldNewPostContent.text;
        currentPostSelect.PostTitle = inputFieldNewPostTitle.text;

        currentPostSelect.ViewsHolder.textContent.text = currentPostSelect.Content;

        currentPostSelect.PostTemplateId = templateModel.Id;
        currentPostSelect.ThemeColor = templateModel.ThemeColor;
        currentPostSelect.PosTemplateName = templateModel.Name;
        currentPostSelect.PosTemplateContent = templateModel.Content;
        currentPostSelect.PostStatus = (toggleVisibility.isOn) ? "2" : "1";
        currentPostSelect.PosTemplateName = textTemplateName.text;
        currentPostSelect.ViewsHolder.MarkForRebuild();

        postOSA.ForceUpdateViewsHolderIfVisible(currentPostSelect.ItemIndexOSA);

        inputFieldNewPostContent.text = "";
        inputFieldNewPostTitle.text = "";

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        JSONNode resToValues = JSONNode.Parse(res);
        if (resToValues["message"] != null)
        {
            responseErrorChecker.GetResponse(resToValues["message"][0][0]);
            Debug.Log(resToValues["message"][0][0]);
            yield break;
        }
        responseErrorChecker.GetResponse("");

        redirector.Pop();
        footerNoticeController.SendAFooterMessage("Bạn đã sửa bài đăng thành công!");
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
        inputFieldNewPostTitle.text = "";

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
        inputFieldNewPostTitle.text = "";
        postImage.sprite = null;
        postImage.color = new Color(1, 1, 1, 0);
        redirector.Push("post.upload");
        textPostImageButtonUI.text = "Tải ảnh";
        textPostImageButtonUI.color = new Color(0, 0, 0);
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
            Debug.Log("ID: " + GlobalSetting.LoginUser.Id);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);
    }

    public void ShowPostUtilitiesMenu(UIPostModel postModel)
    {
        postImage.sprite = null;
        postImage.color = new Color(1, 1, 1, 0);

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
        inputFieldNewPostTitle.text = postModel.PostTitle;

        templateId = postModel.PostTemplateId;
        textTemplateName.text = postModel.PosTemplateName;
        textTemplateContentRules.text = postModel.PosTemplateContent;
        toggleVisibility.isOn = (postModel.PostStatus.Equals("2")) ? true : false;

        templateModel.ThemeColor = postModel.ThemeColor;

        if (!postModel.ImagePath.Equals(""))
        {
            postImage.color = new Color(1, 1, 1, 1);
            textPostImageButtonUI.text = "Hủy ảnh";
            textPostImageButtonUI.color = new Color(1, 0, 0);
        }
        else
        {
            textPostImageButtonUI.text = "Tải ảnh";
            textPostImageButtonUI.color = new Color(0, 0, 0);
        }

        Debug.Log("here 1");

    }

    public void ShowEditPostMenu()
    {
        containerUtilitiesMenu.gameObject.SetActive(false);
        if (!currentPostSelect.ImagePath.Equals(""))
        {
            Davinci.get().load(GlobalSetting.Endpoint + currentPostSelect.ImagePath).into(postImage).setFadeTime(0).start();
        }

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
            "&post_id=" + postModel.PostId +
            "&per_page=" + commentGetPerPage);


        responseErrorChecker.SendRequest();
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        JSONNode resToValues = JSONNode.Parse(res);
        if (resToValues["message"] != null)
        {
            responseErrorChecker.GetResponse(resToValues["message"][0][0]);
            Debug.Log(resToValues["message"][0][0]);
            yield break;
        }
        responseErrorChecker.GetResponse("");

        GetCommentsResponse(res);
    }

    public void GetCommentsResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        var commentModels = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"]["data"].Count; i++)
        {
            commentModels.Add(new CommentItemModel
            {
                CommentModel = new UICommentModel()
                {
                    Content = resToValue["data"]["data"][i]["content"],
                    CreatedAt = resToValue["data"]["data"][i]["created_at"],
                    Id = resToValue["data"]["data"][i]["id"],
                    PostId = resToValue["data"]["data"][i]["post_id"],
                    UserId = resToValue["data"]["data"][i]["user_id"],
                    UserFullName = resToValue["data"]["data"][i]["user"]["name"],
                    Username = resToValue["data"]["data"][i]["user"]["username"],
                    AvatarPath = resToValue["data"]["data"][i]["user"]["avatar_path"],
                    LikeCount = (resToValue["data"]["data"][i]["like_up"] - resToValue["data"]["data"][i]["like_down"]),
                    LikeStatus = (resToValue["data"]["data"][i]["like_status"]["like_status"]) ?? "0",
                }
            });
        }

        postCommentOSA.Data.InsertItems(1, commentModels);
    }

    public void CheckAndGetOldComment(UICommentModel commentModel)
    {
        if (commentModel.ViewsHolder.ItemIndex == (postCommentOSA.Data.Count - 1))
        {
            StartCoroutine(GetOldCommentsCoroutine(commentModel));
        }
    }

    public IEnumerator GetOldCommentsCoroutine(UICommentModel commentModel)
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/post/comments/old" +
            "?user_id=" + GlobalSetting.LoginUser.Id +
            "&post_id=" + commentModel.PostId +
            "&date=" + commentModel.CreatedAt +
            "&per_page=" + commentGetPerPage);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        GetOldCommentsResponse(res);
    }

    public void GetOldCommentsResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        var commentModels = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"]["data"].Count; i++)
        {
            bool isConflict = false;

            foreach (var item in postCommentOSA.Data)
            {
                if (item is CommentItemModel comment)
                {
                    if (comment.CommentModel.Id.Equals(resToValue["data"]["data"][i]["id"]))
                    {
                        isConflict = true;
                        break;
                    }
                }
            }

            if (!isConflict)
            {
                commentModels.Add(new CommentItemModel
                {
                    CommentModel = new UICommentModel()
                    {
                        Content = resToValue["data"]["data"][i]["content"],
                        CreatedAt = resToValue["data"]["data"][i]["created_at"],
                        Id = resToValue["data"]["data"][i]["id"],
                        PostId = resToValue["data"]["data"][i]["post_id"],
                        UserId = resToValue["data"]["data"][i]["user_id"],
                        UserFullName = resToValue["data"]["data"][i]["user"]["name"],
                        Username = resToValue["data"]["data"][i]["user"]["username"],
                        AvatarPath = resToValue["data"]["data"][i]["user"]["avatar_path"],

                        LikeCount = (resToValue["data"]["data"][i]["like_up"] - resToValue["data"]["data"][i]["like_down"]),
                        LikeStatus = (resToValue["data"]["data"][i]["like_status"]["like_status"]) ?? "0",
                    }
                });
            }
        }

        postCommentOSA.Data.InsertItemsAtEnd(commentModels);
    }

    public void RefreshComment()
    {
        ShowUIPostCommentAndGetComments(currentPostSelect);
    }

    public void SendComment()
    {
        if (inputFieldNewCommentContent.text.Equals(""))
        {
            footerNoticeController.SendAFooterMessage("Bình luận không được rỗng");
            return;
        }
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

        responseErrorChecker.SendRequest();
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }


        string res = request.downloadHandler.text;

        Debug.Log(res);

        JSONNode resToValues = JSONNode.Parse(res);
        if (resToValues["message"] != null)
        {
            responseErrorChecker.GetResponse(resToValues["message"][0][0]);
            Debug.Log(resToValues["message"][0][0]);
            yield break;
        }
        responseErrorChecker.GetResponse("");

        footerNoticeController.SendAFooterMessage("Gửi bình luận Thành công");

        post.PostModel.CommentCount++;
        postCommentOSA.ForceUpdateViewsHolderIfVisible(0);

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
                AvatarPath = resToValue["data"]["user"]["avatar_path"],
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

        btnDeleteComment.gameObject.SetActive(false);
        btnEditComment.gameObject.SetActive(false);

        if (commentModel.UserId.Equals(GlobalSetting.LoginUser.Id))
        {
            btnDeleteComment.gameObject.SetActive(true);
            btnEditComment.gameObject.SetActive(true);
        }

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
        if (inputFieldEditComment.text.Equals(""))
        {
            footerNoticeController.SendAFooterMessage("Bình luận không được rỗng");
            return;
        }

        StartCoroutine(EditCommentCoroutine());
    }

    public IEnumerator EditCommentCoroutine()
    {
        WWWForm body = new WWWForm();

        body.AddField("comment_id", currentCommentModelSelect.Id);
        body.AddField("content", inputFieldEditComment.text);
        body.AddField("comment_status_id", 1);


        inputFieldEditComment.text = "";

        footerNoticeController.SendAFooterMessage("Bình luận của bạn đang được sửa");

        var postModel = postCommentOSA.Data[0] as PostItemModel;

        containerUtilitiesCommentMenu.gameObject.SetActive(false);

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/post/comment/edit", body);

        responseErrorChecker.SendRequest();
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }


        string res = request.downloadHandler.text;

        Debug.Log(res);

        JSONNode resToValues = JSONNode.Parse(res);
        if (resToValues["message"] != null)
        {
            responseErrorChecker.GetResponse(resToValues["message"][0][0]);
            Debug.Log(resToValues["message"][0][0]);
            yield break;
        }
        footerNoticeController.SendAFooterMessage("Bạn đã sửa bình luận thành công!");

        currentCommentModelSelect.Content = inputFieldEditComment.text;

        postCommentOSA.ForceUpdateViewsHolderIfVisible(currentCommentModelSelect.ViewsHolder.ItemIndex);

        responseErrorChecker.GetResponse("");
    }

    public void CheckAndGetOldPosts(UIPostModel postModel)
    {
        if (postModel.ViewsHolder.ItemIndex == (postOSA.Data.Count - 1))
        {
            StartCoroutine(GetOldPostsCoroutine(postModel));
        }
    }

    public IEnumerator GetOldPostsCoroutine(UIPostModel postModel)
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/posts/old" +
            "?user_id=" + GlobalSetting.LoginUser.Id +
            "&per_page=" + postGetPerPage +
            "&date=" + postModel.CreateAt +
            "&filter=" + filterGetPosts);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        GetOldPostsResponse(res);
    }

    public void GetOldPostsResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        var posts = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"]["data"].Count; i++)
        {
            bool isConflict = false;

            foreach (var item in postOSA.Data)
            {
                if (item is PostItemModel post)
                {
                    if (post.PostModel.PostId.Equals(resToValue["data"]["data"][i]["id"]))
                    {
                        isConflict = true;
                        break;
                    }
                }
            }

            if (!isConflict)
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
                        AvatarPath = resToValue["data"]["data"][i]["user"]["avatar_path"],
                        Username = resToValue["data"]["data"][i]["user"]["username"],
                        UserId = resToValue["data"]["data"][i]["user"]["id"],
                        ThemeColor = resToValue["data"]["data"][i]["post_template"]["theme_color"],
                        LikeCount = (resToValue["data"]["data"][i]["post_likes_up"] - resToValue["data"]["data"][i]["post_likes_down"]),
                        LikeStatus = (resToValue["data"]["data"][i]["like_status"] != null) ? resToValue["data"]["data"][i]["like_status"]["like_status"].ToString() : "0",
                        CommentCount = resToValue["data"]["data"][i]["comment_count"],
                        PostStatus = resToValue["data"]["data"][i]["post_status_id"],
                        PostTitle = resToValue["data"]["data"][i]["title"],
                        ImagePath = resToValue["data"]["data"][i]["image_path"],
                        ContainerOSA = "post",
                    }
                });
            }
            //Debug.Log((resToValue["data"]["data"][i]["post_likes_up"] - resToValue["data"]["data"][i]["post_likes_down"]).ToString());
        }
        postOSA.Data.InsertItemsAtEnd(posts);
    }

    public void OpenFilePanel()
    {
        if (postImage.sprite == null)
        {
            FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));
            FileBrowser.SetDefaultFilter(".jpg");
            FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

            StartCoroutine(ShowLoadDialogCoroutine());
        }
        else
        {
            postImage.sprite = null;

            postImage.color = new Color(1, 1, 1, 0);

            textPostImageButtonUI.text = "Đăng ảnh";
            textPostImageButtonUI.color = new Color(0, 0, 0);

        }
    }
    IEnumerator ShowLoadDialogCoroutine()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: both, Allow multiple selection: true
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Load File", Submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Load Files and Folders", "Load");

        // Dialog is closed
        // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
            for (int i = 0; i < FileBrowser.Result.Length; i++)
            {
                Debug.Log(FileBrowser.Result[i]);

                Davinci.get().load("file://" + FileBrowser.Result[i]).into(postImage).setFadeTime(0).start();
            }

            //StartCoroutine(SetImageCoroutine(FileBrowser.Result[0]));
        }
    }
    private IEnumerator SetImageCoroutine(string path)
    {
        Texture2D texture;
        UnityWebRequest request = UnityWebRequestTexture.GetTexture("file://" + path);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            texture = DownloadHandlerTexture.GetContent(request);

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            postImage.sprite = sprite;

            postImage.color = new Color(1, 1, 1, 1);

            textPostImageButtonUI.text = "Hủy ảnh";
            textPostImageButtonUI.color = new Color(1, 0, 0);
        }
        else
        {
            Debug.LogError("Lỗi tải ảnh: " + request.error);
        }
    }
    Texture2D GetTextureCopy(Texture2D source)
    {
        //Create a RenderTexture
        RenderTexture rt = RenderTexture.GetTemporary(
                               source.width,
                               source.height,
                               0,
                               RenderTextureFormat.Default,
                               RenderTextureReadWrite.Linear
                           );

        //Copy source texture to the new render (RenderTexture) 
        Graphics.Blit(source, rt);

        //Store the active RenderTexture & activate new created one (rt)
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = rt;

        //Create new Texture2D and fill its pixels from rt and apply changes.
        Texture2D readableTexture = new Texture2D(source.width, source.height);
        readableTexture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        readableTexture.Apply();

        //activate the (previous) RenderTexture and release texture created with (GetTemporary( ) ..)
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(rt);

        return readableTexture;
    }

    public void GetFilterTemplate()
    {
        redirector.Push("post.filter");

        var template = new MultiItemModel();
        template.MultiModel.Type = "filter_template";

        template.MultiModel.PassedVariable["created_at"] = "";
        template.MultiModel.PassedVariable["content"] = "Xem tất cả";
        template.MultiModel.PassedVariable["id"] = "0";
        template.MultiModel.PassedVariable["name"] = "Xem tất cả";
        template.MultiModel.PassedVariable["theme_color"] = "#ffffff";

        postFilterOSA.Data.ResetItems(new List<BaseModel>()
        {
            template
        });

        StartCoroutine(GetFilterTemplateCoroutine());
    }

    private IEnumerator GetFilterTemplateCoroutine()
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
        GetFilterTemplateResponse(res);
    }

    private void GetFilterTemplateResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        List<BaseModel> templates = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"].Count; i++)
        {
            var template = new MultiItemModel();
            template.MultiModel.Type = "filter_template";

            template.MultiModel.PassedVariable["created_at"] = resToValue["data"][i]["created_at"];
            template.MultiModel.PassedVariable["content"] = resToValue["data"][i]["content"];
            template.MultiModel.PassedVariable["id"] = resToValue["data"][i]["id"];
            template.MultiModel.PassedVariable["name"] = resToValue["data"][i]["name"];
            template.MultiModel.PassedVariable["theme_color"] = resToValue["data"][i]["theme_color"];

            templates.Add(template);
        }

        postFilterOSA.Data.InsertItems(1, templates);
    }

    public void ChangeFilter(string value)
    {
        filterGetPosts = value;

        GetPosts();

        redirector.Pop();
    }
}
