using Library;
using LuanVan.OSA;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class AuthController : MonoBehaviour
{
    [Header("Scripts: ")]
    [SerializeField] private Redirector redirector;
    [SerializeField] private FooterNoticeController footerNoticeController;
    [Header("UIs: ")]
    [SerializeField] private TMP_InputField inputFieldUsernameLogin;
    [SerializeField] private TMP_InputField inputFieldPasswordLogin;
    [SerializeField] private Button btnQuitLogin, btnQuitRegister;
    [Header(" --- ")]
    [SerializeField] private TMP_InputField inputFieldNameRegister;
    [SerializeField] private TMP_InputField inputFieldUsernameRegister;
    [SerializeField] private TMP_InputField inputFieldPasswordRegister;
    [SerializeField] private TMP_InputField inputFieldPasswordConfirmRegister;

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
        btnQuitLogin.interactable = false;
        btnQuitRegister.interactable = false;

        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/login" +
            "?username=" + inputFieldUsernameLogin.text +
            "&password=" + inputFieldPasswordLogin.text);

        inputFieldPasswordLogin.text = "";

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

        LoginResponse(res);
    }

    private void LoginResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);
        GlobalSetting.LoginUser.Username = resToValue["data"]["username"];
        GlobalSetting.LoginUser.Name = resToValue["data"]["name"];
        GlobalSetting.LoginUser.Id = resToValue["data"]["id"];

        redirector.Pop();
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

        inputFieldPasswordRegister.text = "";
        inputFieldPasswordConfirmRegister.text = "";

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

        LoginResponse(res);
        
    }
}
