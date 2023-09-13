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
                }
            });
        }

        latestOnlineUserOSA.Data.ResetItems(users);
    }

    public void GetFriends()
    {
        if (friendOSA.Data.Count <= 0)
        {
            StartCoroutine(GetFriendsCoroutine());
        }
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
                }
            });
        }

        friendOSA.Data.ResetItems(users);
    }
}

