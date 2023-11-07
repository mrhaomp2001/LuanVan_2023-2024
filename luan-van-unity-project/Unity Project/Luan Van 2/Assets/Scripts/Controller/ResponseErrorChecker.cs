using Library;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResponseErrorChecker : MonoBehaviour
{
    [SerializeField] private RectTransform loadingContainer;
    [SerializeField] private RectTransform noticeContainer;
    [SerializeField] private TextMeshProUGUI textNotice;
    public void SendRequest()
    {
        noticeContainer.gameObject.SetActive(false);
        loadingContainer.gameObject.SetActive(true);
    }

    public void GetResponse(string notice)
    {
        if (notice != "")
        {
            noticeContainer.gameObject.SetActive(true);
            textNotice.text = notice;

            return;
        }

        loadingContainer.gameObject.SetActive(false);
    }
}
