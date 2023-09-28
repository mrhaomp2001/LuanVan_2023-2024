using Library;
using LuanVan.OSA;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StudyDocumentController : MonoBehaviour
{
    public struct StudyDocument
    {
        public string id;
        public string classroomId;
        public string content;
        public string createdAt;
        public string imagePath;
        public int page;
    }
    [SerializeField] private string currentClassroomSelectId;
    [SerializeField] private int currentDocumentIndex;

    [SerializeField] private List<StudyDocument> studyDocuments = new List<StudyDocument>();
    [SerializeField] private Sprite spriteDefaultDocument;

    [Header("Scripts: ")]
    [SerializeField] private Redirector redirector;

    [Header("UIs: ")]
    [SerializeField] private TMP_InputField inputFieldTurnPage;
    [SerializeField] private TextMeshProUGUI textContent;
    [SerializeField] private TextMeshProUGUI textPageNumber;
    [SerializeField] private Image imageDocument;

    public void GetClassroomDocuments(string classroomId)
    {
        redirector.Push("classroom.document");
        currentClassroomSelectId = classroomId;
        StartCoroutine(GetClassroomDocumentsCoroutine(classroomId));
    }


    private IEnumerator GetClassroomDocumentsCoroutine(string classroomId)
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/classroom/documents" +
            "?classroom_id=" + classroomId +
            "&user_id=" + GlobalSetting.LoginUser.Id);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);
        GetClassroomDocumentsResponse(res);
    }

    private void GetClassroomDocumentsResponse(string res)
    {
        studyDocuments.Clear();

        var resToValue = JSONNode.Parse(res);
        for (int i = 0; i < resToValue["data"].Count; i++)
        {
            studyDocuments.Add(new StudyDocument()
            {
                classroomId = resToValue["data"][i]["classroom_id"],
                content = resToValue["data"][i]["content"],
                createdAt = resToValue["data"][i]["created_at"],
                id = resToValue["data"][i]["id"],
                page = resToValue["data"][i]["page"],
                imagePath = resToValue["data"][i]["image_path"],
            });
        }

        LoadDocument(0);
    }


    public void LoadDocumentPage()
    {
        if (int.TryParse(inputFieldTurnPage.text, out int index))
        {
            LoadDocument(index - 1);
        }
        inputFieldTurnPage.text = (currentDocumentIndex + 1).ToString();
    }

    public void LoadNextDocument()
    {
        if (currentDocumentIndex + 1 >= 0 && currentDocumentIndex + 1 < studyDocuments.Count)
        {
            currentDocumentIndex++;

            textPageNumber.text = (currentDocumentIndex + 1).ToString() + "/" + studyDocuments.Count.ToString();
            textContent.text = studyDocuments[currentDocumentIndex].content;

            LoadDocument(currentDocumentIndex);
        }
    }

    public void LoadPreviousDocument()
    {
        if (currentDocumentIndex - 1 >= 0 && currentDocumentIndex - 1 <= studyDocuments.Count)
        {
            currentDocumentIndex--;

            textPageNumber.text = (currentDocumentIndex + 1).ToString() + "/" + studyDocuments.Count.ToString();
            textContent.text = studyDocuments[currentDocumentIndex].content;

            LoadDocument(currentDocumentIndex);
        }
    }

    public void LoadDocument(int index)
    {
        if (index >= 0 && index < studyDocuments.Count)
        {
            currentDocumentIndex = index;

            inputFieldTurnPage.text = (index + 1).ToString();

            textPageNumber.text = (index + 1).ToString() + "/" + studyDocuments.Count.ToString();
            textContent.text = studyDocuments[currentDocumentIndex].content;
            if (!studyDocuments[currentDocumentIndex].imagePath.Equals(""))
            {
                Davinci.get().load(GlobalSetting.Endpoint + studyDocuments[currentDocumentIndex].imagePath).into(imageDocument).setFadeTime(0).start();
            }
            else
            {
                imageDocument.sprite = spriteDefaultDocument;
            }
        }
    }
}
