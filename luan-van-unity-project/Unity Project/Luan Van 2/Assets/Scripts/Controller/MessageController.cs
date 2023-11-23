using Library;
using LuanVan.OSA;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class MessageController : MonoBehaviour
{
    [SerializeField] private Redirector redirector;
    [SerializeField] private TextMeshProUGUI userFullnameHeader;
    [SerializeField] private TMP_InputField inputFieldChatMessage;
    [SerializeField] private RectTransform imageNewMessageNotice;

    [SerializeField] private UIMultiPrefabsOSA chatUserOSA;
    [SerializeField] private UIMultiPrefabsOSA chatOSA;
    [SerializeField] private bool isEndOfMessages;
    [SerializeField] private ResponseErrorChecker responseErrorChecker;
    [SerializeField] private FooterNoticeController footerNoticeController;

    [SerializeField] private SocketManager socketManager;
    [SerializeField] private UIChatUserModel currentChatUserModel;

    public bool IsEndOfMessages { get => isEndOfMessages; set => isEndOfMessages = value; }

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
        userFullnameHeader.text = chatUserModel.UserFullname;

        redirector.Push("chat");
        chatOSA.Data.ResetItems(new List<BaseModel>());
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

            if (message.MessageModel.ReceiverId.ToString() == GlobalSetting.LoginUser.Id)
            {
                message.MessageModel.ReceiverFullName = GlobalSetting.LoginUser.Name;
                message.MessageModel.ReceiverUsername = GlobalSetting.LoginUser.Username;

                message.MessageModel.SenderFullName = currentChatUserModel.UserFullname;
                message.MessageModel.SenderUsername = currentChatUserModel.Username;
            }
            else
            {
                message.MessageModel.ReceiverFullName = currentChatUserModel.UserFullname;
                message.MessageModel.ReceiverUsername = currentChatUserModel.Username;

                message.MessageModel.SenderFullName = GlobalSetting.LoginUser.Name;
                message.MessageModel.SenderUsername = GlobalSetting.LoginUser.Username;
            }

            listMessage.Add(message);
        }

        chatOSA.Data.ResetItems(listMessage);
        if (chatOSA.Data.Count > 1)
        {
            chatOSA.ScrollTo(chatOSA.Data.Count - 1);
        }
        isEndOfMessages = true;
    }

    public void ReceiveMessage(string res)
    {
        var resToValues = JSONNode.Parse(res);

        bool isScrollToEnd = isEndOfMessages;

        var message = new MessageItemModel()
        {
            MessageModel = new UIMessageModel()
            {
                Content = resToValues["data"]["content"],
                CreatedAt = resToValues["data"]["created_at"],
                Id = resToValues["data"]["id"],
                ReceiverFullName = resToValues["data"]["receiver"]["name"],
                ReceiverId = resToValues["data"]["receiver_id"],
                ReceiverUsername = resToValues["data"]["receiver"]["username"],
                SenderFullName = resToValues["data"]["sender"]["name"],
                SenderId = resToValues["data"]["sender_id"],
                SenderUsername = resToValues["data"]["sender"]["username"],
            }
        };

        if (message.MessageModel.SenderId == currentChatUserModel.OtherId)
        {
            chatOSA.Data.InsertOneAtEnd(message);

            if (isScrollToEnd)
            {
                if (chatOSA.Data.Count > 1)
                {
                    chatOSA.ScrollTo(chatOSA.Data.Count - 1);
                }
            }
        }

        imageNewMessageNotice.gameObject.SetActive(true);

        GetChatUsers();
    }

    public void SendChatMessage()
    {
        if (string.IsNullOrWhiteSpace(inputFieldChatMessage.text))
        {
            footerNoticeController.SendAFooterMessage("Tin nhắn không được rỗng");
            return;
        }

        if (inputFieldChatMessage.text.Length > 128)
        {
            footerNoticeController.SendAFooterMessage("Tin nhắn không được dàu hơn 128");
            return;
        }

        var chatReq = new
        {
            sender_id = GlobalSetting.LoginUser.Id,
            receiver_id = currentChatUserModel.OtherId,
            content = inputFieldChatMessage.text,
        };

        bool isScrollToEnd = isEndOfMessages;

        var message = new MessageItemModel()
        {
            MessageModel = new UIMessageModel()
            {
                Content = inputFieldChatMessage.text,
                CreatedAt = DateTime.Now.ToString(),
                Id = 0,
                ReceiverFullName = currentChatUserModel.Username,
                ReceiverId = currentChatUserModel.OtherId,
                ReceiverUsername = currentChatUserModel.UserFullname,
                SenderFullName = GlobalSetting.LoginUser.Name,
                SenderId = int.Parse(GlobalSetting.LoginUser.Id),
                SenderUsername = GlobalSetting.LoginUser.Username,
            }
        };

        inputFieldChatMessage.text = "";

        //responseErrorChecker.SendRequest();

        socketManager.Socket.Emit("sendEvent", (res) =>
        {
            JSONNode resToValues = JSONNode.Parse(res.GetValue().GetRawText());
            Debug.Log(resToValues["message"]);
            socketManager.Socket.ExecuteInUnityUpdateThread(() =>
            {

                //if (resToValues["message"] != null)
                //{
                //    responseErrorChecker.GetResponse(resToValues["message"]);
                //    Debug.Log(resToValues["message"]);
                //    return;
                //}
                //responseErrorChecker.GetResponse("");

                GetChatUsers();
            });
        }, chatReq);


        chatOSA.Data.InsertOneAtEnd(message);

        if (isScrollToEnd)
        {
            if (chatOSA.Data.Count > 1)
            {
                chatOSA.ScrollTo(chatOSA.Data.Count - 1);
            }
        }
    }
}
