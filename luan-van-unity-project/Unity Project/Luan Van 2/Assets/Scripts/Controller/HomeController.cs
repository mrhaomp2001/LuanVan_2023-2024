using Library;
using LuanVan.OSA;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HomeController : MonoBehaviour
{
    [SerializeField] private NotificationController notificationController;
    [SerializeField] private ResponseErrorChecker responseErrorChecker;
    [SerializeField] private UIMultiPrefabsOSA homeOSA;

    public void GetInfomations()
    {
        homeOSA.Data.ResetItems(new List<BaseModel>());
        StartCoroutine(GetInfomationsCoroutine());
    }

    public void Logout()
    {
        homeOSA.Data.ResetItems(new List<BaseModel>());
    }

    public IEnumerator GetInfomationsCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/home" +
            "?user_id=" + GlobalSetting.LoginUser.Id);

        responseErrorChecker.SendRequest();
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        var resToValue = JSONNode.Parse(res);

        if (resToValue["message"] != null)
        {
            responseErrorChecker.GetResponse(resToValue["message"]);
            yield break;
        }

        responseErrorChecker.GetResponse("");
        GetInfomationsResponse(res);
    }

    public void GetInfomationsResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        var itemLabel = new UIMultiModel()
        {
            Type = "border_less_center_text"
        };

        itemLabel.PassedVariable["content"] = "<b>Tin tức hệ thống</b>";

        var item = new MultiItemModel()
        {
            MultiModel = itemLabel
        };

        homeOSA.Data.InsertOneAtStart(item);

        List<BaseModel> system_notifications = new List<BaseModel>();

        for (int i = 0; i < resToValue["system_notifications"].Count; i++)
        {

            var notification = new UIMultiModel()
            {
                Type = "left_text"
            };

            notification.PassedVariable["content"] = resToValue["system_notifications"][i]["content"];

            system_notifications.Add(new MultiItemModel()
            {
                MultiModel = notification,
            });
        }

        homeOSA.Data.InsertItemsAtEnd(system_notifications);

        // new label

        itemLabel = new UIMultiModel()
        {
            Type = "border_less_center_text"
        };

        itemLabel.PassedVariable["content"] = "<b>Thông báo của bạn</b>";


        item = new MultiItemModel()
        {
            MultiModel = itemLabel
        };

        homeOSA.Data.InsertOneAtEnd(item);

        List<BaseModel> notifications = new List<BaseModel>();

        for (int i = 0; i < resToValue["notifications"].Count; i++)
        {
            //Debug.Log(resToValue["notifications"][i]["notification_type"]["description"]);

            var notification = new UIMultiModel()
            {
                Type = "left_text"
            };

            notification.PassedVariable["content"] = "<b>"
                + resToValue["notifications"][i]["sender"]["name"]
                + "</b>"
                + notificationController.NotificateProcess(resToValue["notifications"][i]["notification_type"]["id"])
                + "<b>"
                + notificationController.TruncateLongString(resToValue["notifications"][i]["model"]["content"], 24)
                + "</b>";

            notifications.Add(new MultiItemModel()
            {
                MultiModel = notification,
            });
        }

        homeOSA.Data.InsertItemsAtEnd(notifications);

        // New Label

        itemLabel = new UIMultiModel()
        {
            Type = "border_less_center_text"
        };

        itemLabel.PassedVariable["content"] = "<b>Bài viết mới nhất</b>";

        item = new MultiItemModel()
        {
            MultiModel = itemLabel
        };

        homeOSA.Data.InsertOneAtEnd(item);

        var posts = new List<BaseModel>();

        for (int i = 0; i < resToValue["posts"].Count; i++)
        {
            posts.Add(new PostItemModel()
            {
                PostModel = new UIPostModel()
                {
                    PostId = resToValue["posts"][i]["id"],
                    Content = resToValue["posts"][i]["content"],
                    CreateAt = resToValue["posts"][i]["created_at"],
                    PosTemplateContent = resToValue["posts"][i]["post_template"]["content"],
                    PosTemplateName = resToValue["posts"][i]["post_template"]["name"],
                    PostTemplateId = resToValue["posts"][i]["post_template"]["id"],
                    UserFullname = resToValue["posts"][i]["user"]["name"],
                    Username = resToValue["posts"][i]["user"]["username"],
                    AvatarPath = resToValue["posts"][i]["user"]["avatar_path"],
                    UserId = resToValue["posts"][i]["user"]["id"],
                    ThemeColor = resToValue["posts"][i]["post_template"]["theme_color"],
                    LikeCount = (resToValue["posts"][i]["post_likes_up"] - resToValue["posts"][i]["post_likes_down"]),
                    LikeStatus = (resToValue["posts"][i]["like_status"] != null) ? resToValue["posts"][i]["like_status"]["like_status"].ToString() : "0",
                    CommentCount = resToValue["posts"][i]["comment_count"],
                    PostStatus = resToValue["posts"][i]["post_status_id"],
                    ImagePath = resToValue["posts"][i]["image_path"],
                    PostTitle = resToValue["posts"][i]["title"],

                    ContainerOSA = "profile",
                }
            });
        }
        homeOSA.Data.InsertItemsAtEnd(posts);
    }


}
