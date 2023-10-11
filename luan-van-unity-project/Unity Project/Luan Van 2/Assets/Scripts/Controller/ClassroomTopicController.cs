using Library;
using LuanVan.OSA;
using SimpleFileBrowser;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ClassroomTopicController : MonoBehaviour
{
    [Header("Stats: ")]
    [SerializeField] private int topicGetCount;
    [SerializeField] private int topicCommentGetCount;
    [SerializeField] private bool isEdit;
    [SerializeField] private UITopicModel currentTopicModel;
    [SerializeField] private UITopicCommentModel currentCommentModel;
    [Header("Scripts: ")]
    [SerializeField] private Redirector redirector;
    [SerializeField] private ClassroomController classroomController;
    [Header("OSAs: ")]
    [SerializeField] private UIMultiPrefabsOSA classroomInfoOSA;
    [SerializeField] private UIMultiPrefabsOSA topicCommentOSA;
    [Header("UIs: ")]
    [SerializeField] private RectTransform topicUtilitiesMenuContainer;
    [SerializeField] private RectTransform topicCommentUtilitiesMenuContainer;
    [SerializeField] private RectTransform topicCommentEditContainer;
    [SerializeField] private TMP_InputField inputFieldCommentEditContent;
    [SerializeField] private RectTransform btnEditTopic;
    [SerializeField] private RectTransform btnDeleteTopic;
    [SerializeField] private Image imageTopic;
    [SerializeField] private TextMeshProUGUI textButtonUploadImage;
    [SerializeField] private TMP_InputField inputFieldTitle;
    [SerializeField] private TMP_InputField inputFieldContent;
    [SerializeField] private RectTransform btnEditComment;
    [SerializeField] private RectTransform btnDeleteComment;

    public UITopicCommentModel CurrentCommentModel { get => currentCommentModel; set => currentCommentModel = value; }
    public UITopicModel CurrentTopicModel { get => currentTopicModel; set => currentTopicModel = value; }

    public void CheckAndGetOldTopic(UITopicModel topicModel)
    {
        if (topicModel.ViewsHolder.ItemIndex == (classroomInfoOSA.Data.Count - 1))
        {
            StartCoroutine(GetOldTopicCoroutine(topicModel.CreateAt));
        }
    }

    private IEnumerator GetOldTopicCoroutine(string date)
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/classrooms/topics/old" +
            "?user_id=" + GlobalSetting.LoginUser.Id +
            "&classroom_id=" + classroomController.CurrentClassroomModel.Id +
            "&per_page=" + topicGetCount +
            "&date=" + date);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        GetOldTopicRespose(res);
    }

    public void GetOldTopicRespose(string res)
    {
        var resToValue = JSONNode.Parse(res);
        var topics = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"]["data"].Count; i++)
        {
            bool isConflict = false;

            foreach (var item in classroomInfoOSA.Data)
            {
                if (item is TopicItemModel topic)
                {
                    if (topic.TopicModel.Id.Equals(resToValue["data"]["data"][i]["id"]))
                    {
                        isConflict = true;
                        break;
                    }
                }
            }

            if (!isConflict)
            {
                topics.Add(new TopicItemModel()
                {
                    TopicModel = new UITopicModel()
                    {
                        Id = resToValue["data"]["data"][i]["id"],
                        CommentCount = resToValue["data"]["data"][i]["comment_count"],
                        Content = resToValue["data"]["data"][i]["content"],
                        CreateAt = resToValue["data"]["data"][i]["created_at"],
                        LikeCount = (resToValue["data"]["data"][i]["like_up"] - resToValue["data"]["data"][i]["like_down"]),
                        LikeStatus = resToValue["data"]["data"][i]["like_status"]["like_status"] ?? "",
                        UserFullname = resToValue["data"]["data"][i]["user"]["name"],
                        UserId = resToValue["data"]["data"][i]["user"]["id"],
                        Username = resToValue["data"]["data"][i]["user"]["username"],
                        AvatarPath = resToValue["data"]["data"][i]["user"]["avatar_path"],
                        TopicStatus = resToValue["data"]["data"][i]["topic_status_id"],
                        ImagePath = resToValue["data"]["data"][i]["image_path"],
                        Title = resToValue["data"]["data"][i]["title"],
                    }
                });
            }

        }

        classroomInfoOSA.Data.InsertItemsAtEnd(topics);
    }

    public void CheckAndGetOldTopicComments(UITopicCommentModel topicCommentModel)
    {
        if (topicCommentModel.ViewsHolder.ItemIndex == (topicCommentOSA.Data.Count - 1))
        {
            StartCoroutine(GetOldTopicCommentsCoroutine(topicCommentModel.CreatedAt));
        }
    }

    private IEnumerator GetOldTopicCommentsCoroutine(string date)
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/classrooms/topics/comments/old" +
            "?user_id=" + GlobalSetting.LoginUser.Id +
            "&classroom_topic_id=" + classroomController.CurrentTopicSellect.Id +
            "&per_page=" + topicCommentGetCount +
            "&date=" + date);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        GetTopicOldCommentsRespose(res);
    }

    private void GetTopicOldCommentsRespose(string res)
    {
        var resToValue = JSONNode.Parse(res);

        var comments = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"]["data"].Count; i++)
        {

            bool isConflict = false;

            foreach (var item in topicCommentOSA.Data)
            {
                if (item is TopicCommentItemModel comment)
                {
                    if (comment.TopicCommentModel.Id.Equals(resToValue["data"]["data"][i]["id"]))
                    {
                        isConflict = true;
                        break;
                    }
                }
            }

            if (!isConflict)
            {
                comments.Add(new TopicCommentItemModel()
                {
                    TopicCommentModel = new UITopicCommentModel()
                    {
                        Content = resToValue["data"]["data"][i]["content"],
                        CreatedAt = resToValue["data"]["data"][i]["created_at"],
                        Id = resToValue["data"]["data"][i]["id"],
                        LikeCount = (resToValue["data"]["data"][i]["like_up"] - resToValue["data"]["data"][i]["like_down"]),
                        LikeStatus = resToValue["data"]["data"][i]["like_status"]["like_status"] ?? "",
                        TopicId = resToValue["data"]["data"][i]["classroom_topic_id"],
                        UserFullName = resToValue["data"]["data"][i]["user"]["name"],
                        UserId = resToValue["data"]["data"][i]["user"]["id"],
                        Username = resToValue["data"]["data"][i]["user"]["username"],
                    }
                });

            }
        }

        topicCommentOSA.Data.InsertItemsAtEnd(comments);
    }

    public void ShowUICreateTopic()
    {
        imageTopic.sprite = null;
        imageTopic.color = new Color(1f, 1f, 1f, 0f);

        inputFieldContent.text = "";
        inputFieldTitle.text = "";

        textButtonUploadImage.text = "Tải ảnh";
        textButtonUploadImage.color = Color.black;

        isEdit = false;

        redirector.Push("classroom.topic.create");
    }

    public void ShowTopicUtilitiesMenu(UITopicModel topicModel)
    {
        btnEditTopic.gameObject.SetActive(false);
        btnDeleteTopic.gameObject.SetActive(false);

        if (topicModel.UserId.Equals(GlobalSetting.LoginUser.Id))
        {
            btnEditTopic.gameObject.SetActive(true);
            btnDeleteTopic.gameObject.SetActive(true);
        }

        topicUtilitiesMenuContainer.gameObject.SetActive(true);
        currentTopicModel = topicModel;
    }

    public void ShowUIEditTopic()
    {
        topicUtilitiesMenuContainer.gameObject.SetActive(false);
        isEdit = true;

        if (!currentTopicModel.ImagePath.Equals(""))
        {
            Davinci.get().load(GlobalSetting.Endpoint + currentTopicModel.ImagePath).into(imageTopic).setFadeTime(0).start();
            imageTopic.color = new Color(1f, 1f, 1f, 1f);

            textButtonUploadImage.text = "Xóa ảnh";
            textButtonUploadImage.color = Color.red;
        }
        else
        {
            imageTopic.sprite = null;
            imageTopic.color = new Color(1f, 1f, 1f, 0f);

            textButtonUploadImage.text = "Tải ảnh";
            textButtonUploadImage.color = Color.black;

        }

        inputFieldContent.text = currentTopicModel.Content;
        inputFieldTitle.text = currentTopicModel.Title ?? "";

        redirector.Push("classroom.topic.create");
    }

    public void CreateTopic()
    {
        // check the inputs here!
        if (!isEdit)
        {
            StartCoroutine(CreateTopicCoroutine());
        }
        else
        {
            StartCoroutine(UptadeTopicCoroutine());
        }
    }

    private IEnumerator CreateTopicCoroutine()
    {
        WWWForm body = new WWWForm();

        body.AddField("user_id", GlobalSetting.LoginUser.Id);
        body.AddField("classroom_id", classroomController.CurrentClassroomModel.Id);
        body.AddField("topic_status_id", "1");
        body.AddField("title", inputFieldTitle.text);
        body.AddField("content", inputFieldContent.text);

        if (imageTopic.sprite != null)
        {
            byte[] textureBytes = null;
            textureBytes = GetTextureCopy(imageTopic.sprite.texture).EncodeToPNG();
            body.AddBinaryData("image", textureBytes, "image.png", "image/png");
        }


        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/classrooms/topics", body);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            Debug.Log("ID: " + GlobalSetting.LoginUser.Id);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        CreateTopicReponse(res);
    }

    private IEnumerator UptadeTopicCoroutine()
    {
        WWWForm body = new WWWForm();

        body.AddField("id", currentTopicModel.Id);
        body.AddField("topic_status_id", "1");
        body.AddField("title", inputFieldTitle.text);
        body.AddField("content", inputFieldContent.text);

        if (imageTopic.sprite != null)
        {
            byte[] textureBytes = null;
            textureBytes = GetTextureCopy(imageTopic.sprite.texture).EncodeToPNG();
            body.AddBinaryData("image", textureBytes, "image.png", "image/png");
        }


        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/classrooms/topics/edit", body);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            Debug.Log("ID: " + GlobalSetting.LoginUser.Id);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        CreateTopicReponse(res);
    }

    public void DeleteTopic()
    {
        StartCoroutine(DeleteTopicCoroutine());
    }

    public IEnumerator DeleteTopicCoroutine()
    {
        WWWForm body = new WWWForm();

        body.AddField("id", currentTopicModel.Id);
        body.AddField("topic_status_id", "2");
        body.AddField("content", currentTopicModel.Content);

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/classrooms/topics/edit", body);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            Debug.Log("ID: " + GlobalSetting.LoginUser.Id);
            yield break;
        }

        topicUtilitiesMenuContainer.gameObject.SetActive(false);


        string res = request.downloadHandler.text;

        Debug.Log(res);

        CreateTopicReponse(res);
    }

    private void CreateTopicReponse(string res)
    {
        redirector.Pop();
        classroomController.GetClassroomInfo(classroomController.CurrentClassroomModel);
    }

    public void ShowUICommentUtilitiesMenu(UITopicCommentModel topicCommentModel)
    {
        currentCommentModel = topicCommentModel;
        topicCommentUtilitiesMenuContainer.gameObject.SetActive(true);

        btnEditComment.gameObject.SetActive(false);
        btnDeleteComment.gameObject.SetActive(false);

        if (topicCommentModel.Id.Equals(GlobalSetting.LoginUser.Id))
        {
            btnEditComment.gameObject.SetActive(true);
            btnDeleteComment.gameObject.SetActive(true);
        }
    }

    public void ShowUIEditComment()
    {
        topicCommentUtilitiesMenuContainer.gameObject.SetActive(false);
        topicCommentEditContainer.gameObject.SetActive(true);
        inputFieldCommentEditContent.text = currentCommentModel.Content;
    }


    public void UpdateComment()
    {
        StartCoroutine(UpdateCommentCoroutine());
    }

    private IEnumerator UpdateCommentCoroutine()
    {
        WWWForm body = new WWWForm();

        body.AddField("id", currentCommentModel.Id);
        body.AddField("content", inputFieldCommentEditContent.text);
        body.AddField("topic_comment_status_id", "1");

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/classrooms/topics/comments/edit", body);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            Debug.Log("ID: " + GlobalSetting.LoginUser.Id);
            yield break;
        }

        topicCommentEditContainer.gameObject.SetActive(false);

        classroomController.ShowTopicComments(classroomController.CurrentTopicSellect);

        string res = request.downloadHandler.text;

        Debug.Log(res);
    }

    public void DeleteComment()
    {
        StartCoroutine(DeleteCommentCoroutine());
    }

    private IEnumerator DeleteCommentCoroutine()
    {
        WWWForm body = new WWWForm();

        body.AddField("id", currentCommentModel.Id);
        body.AddField("content", currentCommentModel.Content);
        body.AddField("topic_comment_status_id", "2");

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/classrooms/topics/comments/edit", body);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            Debug.Log("ID: " + GlobalSetting.LoginUser.Id);
            yield break;
        }

        topicCommentEditContainer.gameObject.SetActive(false);
        classroomController.CurrentTopicSellect.CommentCount--;
        classroomController.ShowTopicComments(classroomController.CurrentTopicSellect);

        topicCommentUtilitiesMenuContainer.gameObject.SetActive(false);

        for (int i = 0; i < classroomInfoOSA.Data.Count; i++)
        {
            if (classroomInfoOSA.Data[i] is TopicItemModel topic)
            {
                if (topic.TopicModel.Id.Equals(classroomController.CurrentTopicSellect.Id))
                {
                    classroomInfoOSA.ForceUpdateViewsHolderIfVisible(i);
                    break;
                }
            }
        }

        classroomController.TopicCommentOSA.ForceUpdateViewsHolderIfVisible(0);

        string res = request.downloadHandler.text;

        Debug.Log(res);
    }

    public void OpenFilePanel()
    {
        if (imageTopic.sprite == null)
        {
            FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));
            FileBrowser.SetDefaultFilter(".jpg");
            FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

            textButtonUploadImage.text = "Xóa ảnh";
            textButtonUploadImage.color = Color.red;

            StartCoroutine(ShowLoadDialogCoroutine());
        }
        else
        {
            imageTopic.sprite = null;

            textButtonUploadImage.text = "Tải ảnh";
            textButtonUploadImage.color = Color.black;

            imageTopic.color = new Color(1, 1, 1, 0);
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

                Davinci.get().load("file://" + FileBrowser.Result[i]).into(imageTopic).setFadeTime(0).start();
                imageTopic.color = new Color(1f, 1f, 1f, 1f);
            }

            //StartCoroutine(SetImageCoroutine(FileBrowser.Result[0]));
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
    
}
