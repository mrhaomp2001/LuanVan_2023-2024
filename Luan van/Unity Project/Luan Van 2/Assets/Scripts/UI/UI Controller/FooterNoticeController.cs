using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class FooterNoticeController : MonoBehaviour
{
    [SerializeField] private RectTransform imageFooterNoticeBackground;
    [SerializeField] private TextMeshProUGUI textFooterNoticeContent;

    public void SendAFooterMessage(string message)
    {
        LeanTween.cancel(imageFooterNoticeBackground);
        LeanTween.cancel(textFooterNoticeContent.rectTransform);

        textFooterNoticeContent.text = message;

        var color = textFooterNoticeContent.color;
        var fadeoutColor = color;
        fadeoutColor.a = 0;

        LeanTween.alpha(imageFooterNoticeBackground, 1, 0.5f).setOnComplete(() =>
        {
            LeanTween.alpha(imageFooterNoticeBackground, 0, 1f).setDelay(2f);
        });

        LeanTweenExt.LeanAlphaText(textFooterNoticeContent, 1, 0.5f).setOnComplete(() =>
        {
            LeanTweenExt.LeanAlphaText(textFooterNoticeContent, 0, 1f).setDelay(2f);
        });
    }
}
