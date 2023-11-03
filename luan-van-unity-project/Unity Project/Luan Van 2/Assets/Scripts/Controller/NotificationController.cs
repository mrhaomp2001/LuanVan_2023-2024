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

            notification.PassedVariable["content"] = "<b>" + resToValue["data"][i]["sender"]["name"] + "</b>" + NotificateProcess(resToValue["data"][i]["notification_type"]["id"]) + "<b>" + TruncateLongString(resToValue["data"][i]["model"]["content"], 20) + "</b>";

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

    public string NotificateProcess(int type)
    {
        switch (type)
        {
            case 1:
                return " đã thích bài viết ";
            case 2:
                return " đã bình luận vào bài viết ";
            case 3:
                return " đã đánh giá cao bình luận ";
            case 4:
                return " đánh giá thấp bình luận ";
            default:
                return "default";
        }
    }

    public string TruncateLongString(string str, int maxLength)
    {
        if (string.IsNullOrEmpty(str)) return str;

        return str.Substring(0, Mathf.Min(str.Length, maxLength)) + "...";
    }
}
