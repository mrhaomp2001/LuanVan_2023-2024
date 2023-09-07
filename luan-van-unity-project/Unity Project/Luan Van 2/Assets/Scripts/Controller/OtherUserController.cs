using LuanVan.OSA;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Library;

public class OtherUserController : MonoBehaviour
{
    [SerializeField] private int latestLoginUsersGetCount;
    [SerializeField] private Redirector redirector;
    [SerializeField] private UIMultiPrefabsOSA latestOnlineUserOSA;
    public void GetLatestLoginUsers()
    {
        StartCoroutine(GetLatestLoginUsersCoroutine());
    }

    private IEnumerator GetLatestLoginUsersCoroutine()
    { 
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/users/login"+ 
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
}

