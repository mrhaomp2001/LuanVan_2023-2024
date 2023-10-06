using LuanVan.OSA;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private Image imageAvatar;
    public void SetLoginInfo()
    {
        textName.text = GlobalSetting.LoginUser.Name;
        if (GlobalSetting.LoginUser.AvatarPath != "")
        {
            Davinci.get().load(GlobalSetting.Endpoint + GlobalSetting.LoginUser.AvatarPath).into(imageAvatar).setFadeTime(0).start();
        }
        else
        {

        }

    }
}
