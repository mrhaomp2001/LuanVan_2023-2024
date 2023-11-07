using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSetting : MonoBehaviour
{
    private static string endpoint;
    private static UserModel loginUser;

    [SerializeField] private string endpointInit;
    [SerializeField] private UserModel loginUserInit;
    [SerializeField] private AuthController authController;
    [SerializeField] private RectTransform notLoginContainer;

    public static string Endpoint { get => endpoint; set => endpoint = value; }
    public static UserModel LoginUser { get => loginUser; set => loginUser = value; }

    private void Start()
    {
        endpoint = endpointInit;
        loginUser = loginUserInit;

        if (!GlobalSetting.loginUser.Username.Equals(""))
        {
            authController.AutoLogin(GlobalSetting.loginUser.Username, "password");
        }
        else
        {
            notLoginContainer.gameObject.SetActive(true);
        }


        Davinci.ClearAllCachedFiles();
    }
}
