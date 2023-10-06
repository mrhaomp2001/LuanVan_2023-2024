using Library;
using LuanVan.OSA;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NotificationController : MonoBehaviour
{
    [SerializeField] private UIMultiPrefabsOSA notificationsOSA;

    public void GetNotifications()
    {
        notificationsOSA.Data.ResetItems(new List<BaseModel>());
        StartCoroutine(GetNotificationsCoroutine());
    }

    private IEnumerator GetNotificationsCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/notifications/users" +
            "?user_id=" + GlobalSetting.LoginUser.Id);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        GetNotificationsResponse(res);
    }
    private void GetNotificationsResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        List<BaseModel> notifications = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"].Count; i++)
        {
            Debug.Log(resToValue["data"][i]["notification_type"]["description"]);

            var notification = new UIMultiModel()
            {
                Type = "left_text"
            };

            notification.PassedVariable["content"] = "Place Holder";

            if (resToValue["data"][i]["notification_type"]["id"] == 1)
            {
                notification.PassedVariable["content"] = "<b>" + resToValue["data"][i]["sender"]["name"] + "</b>" + " đã thích bài viết của bạn";
            }

            if (resToValue["data"][i]["notification_type"]["id"] == 2)
            {
                notification.PassedVariable["content"] = "<b>" + resToValue["data"][i]["sender"]["name"] + "</b>" + " đã bình luận vào bài viết của bạn";
            }

            if (resToValue["data"][i]["notification_type"]["id"] == 3)
            {
                notification.PassedVariable["content"] = "<b>" + resToValue["data"][i]["sender"]["name"] + "</b>" + " đã đánh giá cao bình luận của bạn";
            }

            if (resToValue["data"][i]["notification_type"]["id"] == 4)
            {
                notification.PassedVariable["content"] = "<b>" + resToValue["data"][i]["sender"]["name"] + "</b>" + " đánh giá thấp bình luận của bạn";
            }

            notifications.Add(new MultiItemModel()
            {
                MultiModel = notification,
            });

            if (notifications[i] is MultiItemModel model)
            {
                Debug.Log(model.MultiModel.PassedVariable["content"]);
            }
        }

        notificationsOSA.Data.InsertItemsAtEnd(notifications);
    }
}
