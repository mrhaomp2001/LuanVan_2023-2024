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

            //if (notifications[i] is MultiItemModel model)
            //{
            //    Debug.Log(model.MultiModel.PassedVariable["content"]);
            //}
        }

        notificationsOSA.Data.InsertItemsAtEnd(notifications);
    }

    public string NotificateProcess(int type)
    {
        return type switch
        {
            1 => " đã thích bài viết ",
            2 => " đã bình luận vào bài viết ",
            3 => " đã đánh giá cao bình luận ",
            4 => " đã không thích bình luận ",
            5 => " đã không thích bài viết ",
            6 => " đã thích bài thảo luận ",
            7 => " đã phản hồi vào bài thảo luận ",
            8 => " đã đánh giá cao phản hồi ",
            9 => " đã không thích phản hồi ",
            10 => " đã không thích bài thảo luận ",
            _ => "default",
        };
    }

    public string TruncateLongString(string str, int maxLength)
    {
        if (string.IsNullOrEmpty(str)) return str;

        if (str.Length < maxLength) return str;

        return str.Substring(0, Mathf.Min(str.Length, maxLength)) + "...";
    }
}
