﻿using Library;
using LuanVan.OSA;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class AuthController : MonoBehaviour
{
    [Header("Scripts: ")]
    [SerializeField] private Redirector redirector;
    [SerializeField] private FooterNoticeController footerNoticeController;
    [SerializeField] private SocketManager socketManager;
    [SerializeField] private HomeController homeController;
    [SerializeField] private ResponseErrorChecker responseErrorChecker;
    [Header("UIs: ")]
    [SerializeField] private TMP_InputField inputFieldUsernameLogin;
    [SerializeField] private TMP_InputField inputFieldPasswordLogin;
    [SerializeField] private Button btnQuitLogin, btnQuitRegister;
    [SerializeField] private RectTransform notLoginContainer;
    [SerializeField] private RectTransform imageNewMessageNotice;
    [SerializeField] private List<RectTransform> imageNewFriendsNotice;

    [Header(" --- ")]
    [SerializeField] private TMP_InputField inputFieldNameRegister;
    [SerializeField] private TMP_InputField inputFieldUsernameRegister;
    [SerializeField] private TMP_InputField inputFieldPasswordRegister;
    [SerializeField] private TMP_InputField inputFieldPasswordConfirmRegister;
    [SerializeField] private TMP_InputField inputFieldEndpoint;

    public void Login()
    {
        if (inputFieldUsernameLogin.text.Length < 5)
        {
            footerNoticeController.SendAFooterMessage("Tài khoản phải dài hơn 4 ký tự");
            return;
        }

        if (inputFieldPasswordLogin.text.Length < 5)
        {
            footerNoticeController.SendAFooterMessage("mật khẩu phải dài hơn 4 ký tự");
            return;
        }

        StartCoroutine(LoginCoroutine());
    }

    private IEnumerator LoginCoroutine()
    {
        GlobalSetting.Endpoint = inputFieldEndpoint.text;
        btnQuitLogin.interactable = false;
        btnQuitRegister.interactable = false;

        responseErrorChecker.SendRequest();

        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/login" +
            "?username=" + inputFieldUsernameLogin.text +
            "&password=" + inputFieldPasswordLogin.text);

        PlayerPrefs.SetString("password", inputFieldPasswordLogin.text);
        PlayerPrefs.Save();

        inputFieldPasswordLogin.text = "";

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            responseErrorChecker.GetResponse("Đã xảy ra lỗi!\nHãy kiểm tra kết nối mạng của bạn");
            yield break;
        }

        btnQuitLogin.interactable = true;
        btnQuitRegister.interactable = true;

        string res = request.downloadHandler.text;

        Debug.Log(res);

        var resToValue = JSONNode.Parse(res);


        if (resToValue["message"] == null)
        {
            redirector.Pop();
        }

        LoginResponse(res);
    }

    public void AutoLogin(string username, string password)
    {
        StartCoroutine(AutoLoginCoroutine(username, password));
    }

    private IEnumerator AutoLoginCoroutine(string username, string password)
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/login" +
            "?username=" + username +
            "&password=" + password);
        

        inputFieldPasswordLogin.text = "";

        responseErrorChecker.SendRequest();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            responseErrorChecker.GetResponse("Hãy kiểm tra kết nối mạng của bạn");
            yield break;
        }


        string res = request.downloadHandler.text;

        Debug.Log(res);

        LoginResponse(res);
    }

    private void LoginResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        if (resToValue["message"] != null)
        {
            responseErrorChecker.GetResponse(resToValue["message"]);
            return;
        }

        responseErrorChecker.GetResponse("");
        notLoginContainer.gameObject.SetActive(false);

        GlobalSetting.LoginUser.Username = resToValue["data"]["username"];
        GlobalSetting.LoginUser.Name = resToValue["data"]["name"];
        GlobalSetting.LoginUser.Id = resToValue["data"]["id"];
        GlobalSetting.LoginUser.CreatedAt = resToValue["data"]["created_at"];
        GlobalSetting.LoginUser.UpdatedAt = resToValue["data"]["updated_at"];
        GlobalSetting.LoginUser.AvatarPath = resToValue["data"]["avatar_path"];

        homeController.GetInfomations();

        if (resToValue["has_new_message"])
        {
            imageNewMessageNotice.gameObject.SetActive(true);
        }

        if (resToValue["has_new_friends"])
        {
            foreach (var item in imageNewFriendsNotice)
            {
                item.gameObject.SetActive(true);
            }
        }

        socketManager.Connect();

        PlayerPrefs.SetString("username", GlobalSetting.LoginUser.Username);
        PlayerPrefs.SetString("endpoint", GlobalSetting.Endpoint);
        PlayerPrefs.Save();
    }

    public void Logout()
    {
        notLoginContainer.gameObject.SetActive(true);

        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.Save();

        GlobalSetting.LoginUser.Username = "";
        GlobalSetting.LoginUser.Name = "";
        GlobalSetting.LoginUser.Id = "";
        GlobalSetting.LoginUser.CreatedAt = "";
        GlobalSetting.LoginUser.UpdatedAt = "";
        GlobalSetting.LoginUser.AvatarPath = "";
        redirector.Pop();
        redirector.Push("home");
        homeController.Logout();
    }


    public void Register()
    {
        if (inputFieldNameRegister.text.Length < 1)
        {
            footerNoticeController.SendAFooterMessage("Tên phải dài hơn 0 ký tự");
            return;
        }

        if (inputFieldUsernameRegister.text.Length < 5)
        {
            footerNoticeController.SendAFooterMessage("Tên đăng nhập phải dài hơn 4 ký tự");
            return;
        }

        if (inputFieldPasswordRegister.text.Length < 5)
        {
            footerNoticeController.SendAFooterMessage("Mật khẩu phải dài hơn 4 ký tự");
            return;
        }

        if (!inputFieldPasswordRegister.text.Equals(inputFieldPasswordConfirmRegister.text))
        {
            footerNoticeController.SendAFooterMessage("Mật khẩu nhập lại sai");
            return;
        }


        StartCoroutine(RegisterCoroutine());
    }

    public IEnumerator RegisterCoroutine()
    {
        btnQuitLogin.interactable = false;
        btnQuitRegister.interactable = false;

        WWWForm body = new WWWForm();

        body.AddField("name", inputFieldNameRegister.text);
        body.AddField("username", inputFieldUsernameRegister.text);
        body.AddField("password", inputFieldPasswordRegister.text);

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/register", body);

        PlayerPrefs.SetString("password", inputFieldPasswordRegister.text);
        PlayerPrefs.Save();

        inputFieldPasswordRegister.text = "";
        inputFieldPasswordConfirmRegister.text = "";

        responseErrorChecker.SendRequest();
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }


        btnQuitLogin.interactable = true;
        btnQuitRegister.interactable = true;

        string res = request.downloadHandler.text;

        Debug.Log(res);
        var resToValue = JSONNode.Parse(res);

        if (resToValue["message"] != null)
        {
            responseErrorChecker.GetResponse(resToValue["message"]);
            yield break;
        }
        responseErrorChecker.GetResponse("");

        redirector.Pop();

        LoginResponse(res);

    }
}
