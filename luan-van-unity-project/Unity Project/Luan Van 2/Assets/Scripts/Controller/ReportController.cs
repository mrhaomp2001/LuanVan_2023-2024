using Library;
using LuanVan.OSA;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ReportController : MonoBehaviour
{
    [SerializeField] private int reportTypeId;
    [SerializeField] private Redirector redirector;
    [SerializeField] private PostController postController;
    [Header("UIs:")]
    [SerializeField] private RectTransform postUtilitiesPopupMenu;
    [SerializeField] private TextMeshProUGUI textReportTypeName;
    [SerializeField] private TMP_InputField inputFieldContentReport;
    [Header("OSAs: ")]
    [SerializeField] private UIMultiPrefabsOSA reportTypesOSA;

    public void ShowUIReport()
    {
        postUtilitiesPopupMenu.gameObject.SetActive(false);
        redirector.Push("report");
    }

    public void GetReportTypes()
    {
        redirector.Push("report.type");
        StartCoroutine(GetReportTypesCoroutine());
    }

    private IEnumerator GetReportTypesCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/report/types");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);
        GetReportTypesResponse(res);
    }

    private void GetReportTypesResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        List<BaseModel> reportTypes = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"]["data"].Count; i++)
        {
            var reportType = new MultiItemModel();
            reportType.MultiModel.Type = "report_type";

            reportType.MultiModel.PassedVariable["id"] = resToValue["data"]["data"][i]["id"];
            reportType.MultiModel.PassedVariable["name"] = resToValue["data"]["data"][i]["name"];
            reportType.MultiModel.PassedVariable["description"] = resToValue["data"]["data"][i]["description"];
            reportType.MultiModel.PassedVariable["created_at"] = resToValue["data"]["data"][i]["created_at"];

            reportTypes.Add(reportType);
        }

        reportTypesOSA.Data.ResetItems(reportTypes);
    }

    public void SetReportType(UIMultiModel reportTypeData)
    {
        int.TryParse(reportTypeData.PassedVariable["id"], out int id);

        reportTypeId = id;
        textReportTypeName.text = reportTypeData.PassedVariable["name"];
        redirector.Pop();
    }

    public void SendReport()
    {
        // Check Input here pls!
        StartCoroutine(SendReportCoroutine());
    }

    private IEnumerator SendReportCoroutine()
    {
        WWWForm body = new WWWForm();
        body.AddField("user_id", GlobalSetting.LoginUser.Id);
        body.AddField("report_type_id", reportTypeId);
        body.AddField("model_id", postController.CurrentPostSelect.PostId);
        body.AddField("model_type", "post");
        body.AddField("content", inputFieldContentReport.text);

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/reports", body);

        inputFieldContentReport.text = "";

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);
    }
}
