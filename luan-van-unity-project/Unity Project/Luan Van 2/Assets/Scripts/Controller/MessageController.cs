using Library;
using LuanVan.OSA;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MessageController : MonoBehaviour
{
    [SerializeField] private Redirector redirector;
    [SerializeField] private UIChatUserModel currentChatUserModel;
    [SerializeField] private UIMultiPrefabsOSA chatUserOSA;
    [SerializeField] private UIMultiPrefabsOSA chatOSA;
    public void GetChatUsers()
    {
        StartCoroutine(GetChatUsersCoroutine());
    }
    private IEnumerator GetChatUsersCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/messages/latest" +
            "?user_id=" + GlobalSetting.LoginUser.Id);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);
        GetChatUsersRespose(res);
    }

    private void GetChatUsersRespose(string res)
    {
        var resToValues = JSONNode.Parse(res);

        var listChatUser = new List<BaseModel>();

        for (int i = 0; i < resToValues["data"].Count; i++)
        {
            var chatUser = new ChatUserItemModel()
            {
                ChatUserModel = new UIChatUserModel()
                {
                    Id = resToValues["data"][i]["id"],
                    ReceiverId = resToValues["data"][i]["receiver_id"],
                    SenderId = resToValues["data"][i]["sender_id"],
                    Content = resToValues["data"][i]["content"],
                    CreatedAt = resToValues["data"][i]["created_at"],

                    UserFullname = resToValues["data"][i]["other_user"]["name"],
                    Username = resToValues["data"][i]["other_user"]["username"],
                    OtherId = resToValues["data"][i]["other_user"]["id"],
                    AvatarPath = resToValues["data"][i]["other_user"]["avatar_path"],
                }
            };

            listChatUser.Add(chatUser);
        }

        chatUserOSA.Data.ResetItems(listChatUser);
    }

    public void GetMessages(UIChatUserModel chatUserModel)
    {
        currentChatUserModel = chatUserModel;

        redirector.Push("chat");

        StartCoroutine(GetMessagesCoroutine());
    }

    private IEnumerator GetMessagesCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/messages" +
            "?sender_id=" + GlobalSetting.LoginUser.Id +
            "&receiver_id=" + currentChatUserModel.OtherId);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        GetMessagesResponse(res);
    }

    private void GetMessagesResponse(string res)
    {
        var resToValues = JSONNode.Parse(res);

        var listMessage = new List<BaseModel>();

        for (int i = 0; i < resToValues["data"].Count; i++)
        {
            var message = new MessageItemModel()
            {
                MessageModel = new UIMessageModel()
                {
                    Content = resToValues["data"][i]["content"],
                    CreatedAt = resToValues["data"][i]["created_at"],
                    Id = resToValues["data"][i]["id"],
                    ReceiverFullName = "",
                    ReceiverId = resToValues["data"][i]["receiver_id"],
                    ReceiverUsername = "",
                    SenderFullName = "",
                    SenderId = resToValues["data"][i]["sender_id"],
                    SenderUsername = "",
                }
            };

            listMessage.Add(message);
        }

        chatOSA.Data.ResetItems(listMessage);
        if (chatOSA.Data.Count > 1)
        {
            chatOSA.ScrollTo(chatOSA.Data.Count - 1);
        }
    }
}
