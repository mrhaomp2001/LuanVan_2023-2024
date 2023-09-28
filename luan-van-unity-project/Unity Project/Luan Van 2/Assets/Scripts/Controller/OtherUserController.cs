using Library;
using LuanVan.OSA;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OtherUserController : MonoBehaviour
{
    [Header("Stats: ")]
    [SerializeField] private int latestLoginUsersGetCount;

    [Header("Scripts: ")]
    [SerializeField] private Redirector redirector;

    [Header("OSAs: ")]
    [SerializeField] private UIMultiPrefabsOSA latestOnlineUserOSA;
    [SerializeField] private UIMultiPrefabsOSA friendOSA;
    [SerializeField] private UIMultiPrefabsOSA waitingFriensOSA;
    public void GetLatestLoginUsers()
    {
        StartCoroutine(GetLatestLoginUsersCoroutine());
    }

    private IEnumerator GetLatestLoginUsersCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/users/login" +
            "?user_id=" + GlobalSetting.LoginUser.Id +
            "&per_page=" + latestLoginUsersGetCount);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        GetLatestLoginResponse(res);
    }

    private void GetLatestLoginResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        var users = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"]["data"].Count; i++)
        {
            users.Add(new LatestOnlineUserItemModel()
            {
                LatestOnlineUserModel = new UILatestOnlineUserModel()
                {
                    CreatedAt = resToValue["data"]["data"][i]["created_at"],
                    Id = resToValue["data"]["data"][i]["id"],
                    Name = resToValue["data"]["data"][i]["name"],
                    UpdatedAt = resToValue["data"]["data"][i]["updated_at"],
                    Username = resToValue["data"]["data"][i]["username"],
                    AvatarPath = resToValue["data"]["data"][i]["avatar_path"] ?? "",
                    ContainerOSA = "latestUser",
                }
            });
        }

        latestOnlineUserOSA.Data.ResetItems(users);
    }

    public void GetFriends()
    {
        StartCoroutine(GetFriendsCoroutine());
    }

    private IEnumerator GetFriendsCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/user/friends" +
            "?user_id=" + GlobalSetting.LoginUser.Id);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);
        GetFriendsResponse(res);
    }

    private void GetFriendsResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        var users = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"].Count; i++)
        {
            users.Add(new LatestOnlineUserItemModel()
            {
                LatestOnlineUserModel = new UILatestOnlineUserModel()
                {
                    CreatedAt = resToValue["data"][i]["created_at"],
                    Id = resToValue["data"][i]["id"],
                    Name = resToValue["data"][i]["name"],
                    UpdatedAt = resToValue["data"][i]["updated_at"],
                    Username = resToValue["data"][i]["username"],
                    AvatarPath = resToValue["data"][i]["avatar_path"] ?? "",
                    ContainerOSA = "friend",
                }
            });
        }

        friendOSA.Data.ResetItems(users);
    }

    public void GetWaitingFriend()
    {
        redirector.Push("other_users.friend.waiting");
        StartCoroutine(GetWaitingFriendCoroutine());
    }

    public IEnumerator GetWaitingFriendCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/user/friends/waiting" +
    "?user_id=" + GlobalSetting.LoginUser.Id);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);
        GetWaitingFriendResponse(res);
    }

    public void GetWaitingFriendResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        var users = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"].Count; i++)
        {
            users.Add(new LatestOnlineUserItemModel()
            {
                LatestOnlineUserModel = new UILatestOnlineUserModel()
                {
                    CreatedAt = resToValue["data"][i]["created_at"],
                    Id = resToValue["data"][i]["id"],
                    Name = resToValue["data"][i]["name"],
                    UpdatedAt = resToValue["data"][i]["updated_at"],
                    Username = resToValue["data"][i]["username"],
                    AvatarPath = resToValue["data"][i]["avatar_path"] ?? "",
                    ContainerOSA = "waitingFriend",
                }
            });
        }
        waitingFriensOSA.Data.ResetItems(users);
    }

    public void UpdateFriendStatus(string otherId, string status)
    {
        StartCoroutine(UpdateFriendStatusCoroutine(otherId, status));
    }

    private IEnumerator UpdateFriendStatusCoroutine(string otherId, string status)
    {
        WWWForm body = new WWWForm();
        body.AddField("user_id", GlobalSetting.LoginUser.Id);
        body.AddField("other_id", otherId);
        body.AddField("status", status);

        for (int i = 0; i < waitingFriensOSA.Data.Count; i++)
        {
            if (waitingFriensOSA.Data[i] is LatestOnlineUserItemModel user)
            {
                if (user.LatestOnlineUserModel.Id.Equals(otherId))
                {
                    waitingFriensOSA.Data.RemoveOne(i);
                    break;
                }
            }
        }

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/user/friend/edit", body);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);
        UpdateFriendStatusResponse(res);
    }

    private void UpdateFriendStatusResponse(string res)
    {

    }
}

