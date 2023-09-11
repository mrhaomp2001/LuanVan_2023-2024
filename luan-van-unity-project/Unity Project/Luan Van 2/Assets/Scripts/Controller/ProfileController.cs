using Library;
using LuanVan.OSA;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ProfileController : MonoBehaviour
{
    [SerializeField] private int postGetCount;
    [SerializeField] private UIMultiPrefabsOSA profileOSA;
    [SerializeField] private UIProfileModel currentProfileModel;

    public void RefreshProfile()
    {
        GetUserProfile(currentProfileModel.Id);
    }

    public void GetUserProfile(string id)
    {
        StartCoroutine(GetUserProfileCoroutine(id));
    }

    private IEnumerator GetUserProfileCoroutine(string id)
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/user/info" +
            "?user_id=" + id);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);
        GetUserProfileResponse(res);
    }

    private void GetUserProfileResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        currentProfileModel = new UIProfileModel()
        {
            CreatedAt = resToValue["data"]["created_at"],
            Id = resToValue["data"]["id"],
            Name = resToValue["data"]["name"],
            UpdatedAt = resToValue["data"]["updated_at"],
            Username = resToValue["data"]["username"],
        };

        profileOSA.Data.ResetItems(new List<BaseModel>());

        profileOSA.Data.InsertOneAtStart(new ProfileItemModel()
        {
            ProfileModel = currentProfileModel,
        });

        GetUserProfilePosts();
    }

    private void GetUserProfilePosts()
    {
        StartCoroutine(GetUserProfilePostsCoroutine()); 
    }


    private IEnumerator GetUserProfilePostsCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/user/posts" +
            "?user_id=" + GlobalSetting.LoginUser.Id +
            "&other_user_id=" + currentProfileModel.Id +
            "&per_page=" + postGetCount);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);
        GetUserProfilePostsResponse(res);
    }

    private void GetUserProfilePostsResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        var posts = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"]["data"].Count; i++)
        {
            posts.Add(new PostItemModel()
            {
                PostModel = new UIPostModel()
                {
                    PostId = resToValue["data"]["data"][i]["id"],
                    Content = resToValue["data"]["data"][i]["content"],
                    CreateAt = resToValue["data"]["data"][i]["created_at"],
                    PosTemplateContent = resToValue["data"]["data"][i]["post_template"]["content"],
                    PosTemplateName = resToValue["data"]["data"][i]["post_template"]["name"],
                    PostTemplateId = resToValue["data"]["data"][i]["post_template"]["id"],
                    UserFullname = resToValue["data"]["data"][i]["user"]["name"],
                    Username = resToValue["data"]["data"][i]["user"]["username"],
                    UserId = resToValue["data"]["data"][i]["user"]["id"],
                    ThemeColor = resToValue["data"]["data"][i]["post_template"]["theme_color"],
                    LikeCount = (resToValue["data"]["data"][i]["post_likes_up"] - resToValue["data"]["data"][i]["post_likes_down"]),
                    LikeStatus = (resToValue["data"]["data"][i]["like_status"] != null) ? resToValue["data"]["data"][i]["like_status"]["like_status"].ToString() : "0",
                    CommentCount = resToValue["data"]["data"][i]["comment_count"],
                    PostStatus = resToValue["data"]["data"][i]["post_status_id"],
                }
            });
        }

        profileOSA.Data.InsertItems(1, posts);
    }
}
