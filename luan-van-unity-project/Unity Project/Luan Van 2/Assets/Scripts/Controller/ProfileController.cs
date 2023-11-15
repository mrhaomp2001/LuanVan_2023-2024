using Library;
using LuanVan.OSA;
using SimpleFileBrowser;
using System.Collections;
using System.Collections.Generic;
using Test;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ProfileController : MonoBehaviour
{
    [SerializeField] private int postGetCount;
    [SerializeField] private Image imageAvatar;
    [SerializeField] private FooterNoticeController footerNoticeController;
    [SerializeField] private ResponseErrorChecker responseErrorChecker;
    [SerializeField] private Redirector redirector;
    [SerializeField] private SocketManager socketManager;
    [SerializeField] private UIMultiPrefabsOSA profileOSA;
    [SerializeField] private UIProfileModel currentProfileModel;

    [Header("UIs: ")]
    [SerializeField] private TMP_InputField inputFieldNameChange;
    [SerializeField] private TMP_InputField inputFieldPasswordChange;
    [SerializeField] private List<RectTransform> imageNewFriendsNotice;

    public void RefreshProfile()
    {
        GetUserProfile(currentProfileModel.Id);
    }

    public void GetUserProfile(string id)
    {
        profileOSA.Data.ResetItems(new List<BaseModel>());

        StartCoroutine(GetUserProfileCoroutine(id));
    }

    private IEnumerator GetUserProfileCoroutine(string id)
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/user/info" +
            "?user_id=" + GlobalSetting.LoginUser.Id +
            "&profile_user_id=" + id);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);
        GetUserProfileResponse(res);
    }

    private void GetUserProfileResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        currentProfileModel = new UIProfileModel()
        {
            CreatedAt = resToValue["data"]["created_at"],
            Id = resToValue["data"]["id"],
            Name = resToValue["data"]["name"],
            UpdatedAt = resToValue["data"]["updated_at"],
            Username = resToValue["data"]["username"],
            AvatarPath = resToValue["data"]["avatar_path"],
            FriendStatusToSelf = resToValue["data"]["friend_to_user"]["friend_status_id"] ?? "",
            FriendStatusToOther = resToValue["data"]["friend_to_other"]["friend_status_id"] ?? "",
        };

        profileOSA.Data.InsertOneAtStart(new ProfileItemModel()
        {
            ProfileModel = currentProfileModel,
        });

        GetUserProfilePosts();
    }

    private void GetUserProfilePosts()
    {
        StartCoroutine(GetUserProfilePostsCoroutine());
    }

    private IEnumerator GetUserProfilePostsCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/user/posts" +
            "?user_id=" + GlobalSetting.LoginUser.Id +
            "&other_user_id=" + currentProfileModel.Id +
            "&per_page=" + postGetCount);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);
        GetUserProfilePostsResponse(res);
    }

    private void GetUserProfilePostsResponse(string res)
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

                    ContainerOSA = "profile",
                }
            });
        }

        profileOSA.Data.InsertItems(1, posts);
    }

    public void CheckAndGetOldPosts(UIPostModel postModel)
    {
        if (postModel.ViewsHolder.ItemIndex == (profileOSA.Data.Count - 1))
        {
            StartCoroutine(GetUserOldPostsCoroutine(postModel));
        }
    }

    public IEnumerator GetUserOldPostsCoroutine(UIPostModel postModel)
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/user/posts/old" +
            "?user_id=" + GlobalSetting.LoginUser.Id +
            "&other_user_id=" + currentProfileModel.Id +
            "&per_page=" + postGetCount +
            "&date=" + postModel.CreateAt);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        CheckAndGetOldPostsResponse(res);
    }

    public void CheckAndGetOldPostsResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        var posts = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"]["data"].Count; i++)
        {
            bool isConflict = false;

            foreach (var item in profileOSA.Data)
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

                        ContainerOSA = "profile",
                    }
                });
            }
        }

        profileOSA.Data.InsertItemsAtEnd(posts);
    }

    public void UpdateViewHolder()
    {
        profileOSA.ForceUpdateViewsHolderIfVisible(0);
    }

    public void GetLoginProfile()
    {
        redirector.Push("profile");
        GetUserProfile(GlobalSetting.LoginUser.Id);
    }

    public void ShowUIUpdateAvatar()
    {
        imageAvatar.sprite = null;
        imageAvatar.color = new Color(1f, 1f, 1f, 0f);
        redirector.Push("profile.avatar");
    }

    public void OpenFilePanel()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));
        FileBrowser.SetDefaultFilter(".jpg");
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

        StartCoroutine(ShowLoadDialogCoroutine());
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: both, Allow multiple selection: true
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Load File", Submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Load File", "Load");

        // Dialog is closed
        // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
            for (int i = 0; i < FileBrowser.Result.Length; i++)
            {
                Debug.Log(FileBrowser.Result[i]);
                Davinci.get().load("file://" + FileBrowser.Result[i]).into(imageAvatar).setFadeTime(0).start();
                imageAvatar.color = new Color(1f, 1f, 1f, 1f);
            }

        }
    }

    public void UpdateAvatar()
    {
        // check input here
        if (imageAvatar.sprite == null)
        {
            footerNoticeController.SendAFooterMessage("hãy tải lên hình ảnh");
            return;
        }

        StartCoroutine(UpdateAvatarCoroutine());
    }

    public IEnumerator UpdateAvatarCoroutine()
    {
        WWWForm body = new WWWForm();

        body.AddField("user_id", GlobalSetting.LoginUser.Id);

        if (imageAvatar.sprite != null)
        {
            byte[] textureBytes = null;
            textureBytes = GetTextureCopy(imageAvatar.sprite.texture).EncodeToPNG();
            body.AddBinaryData("image", textureBytes, "image.png", "image/png");
        }

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/user/avatar/edit", body);
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
            responseErrorChecker.GetResponse(resToValues["message"]);
            Debug.Log(resToValues["message"]);
            redirector.Pop();

            yield break;
        }
        responseErrorChecker.GetResponse("");

        redirector.Pop();
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

    public void ShowUIUpdateName()
    {
        redirector.Push("profile.name");
    }

    public void UpdateName()
    {
        StartCoroutine(UpdateNameCoroutine());
    }

    private IEnumerator UpdateNameCoroutine()
    {
        WWWForm body = new WWWForm();

        body.AddField("user_id", GlobalSetting.LoginUser.Id);
        body.AddField("name", inputFieldNameChange.text);
        body.AddField("password", inputFieldPasswordChange.text);


        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/users/info/edit", body);
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
            responseErrorChecker.GetResponse(resToValues["message"]);
            Debug.Log(resToValues["message"]);
            redirector.Pop();
            inputFieldPasswordChange.text = "";

            inputFieldNameChange.text = "";

            yield break;
        }

        responseErrorChecker.GetResponse("");
    }

    public void SendFriendRequestSocket()
    {
        var data = new
        {
            sender_id = GlobalSetting.LoginUser.Id,
            receiver_id = currentProfileModel.Id,
        };

        socketManager.Socket.Emit("friend_request_send", (res) =>
        {
            socketManager.Socket.ExecuteInUnityUpdateThread(() =>
            {
                
            });
        }, data);

    }

    public void NoticeNewFriends()
    {
        imageNewFriendsNotice.ForEach(friend => { friend.gameObject.SetActive(true); });

    }
}
