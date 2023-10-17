using Library;
using LuanVan.OSA;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MainGameplayController : MonoBehaviour
{
    [System.Serializable]
    public struct QuestionCollection
    {
        public string name;
        public string id;
        public string classroomId;
        public string difficulty;
        public string gameType;
        public int questionsPerTime;
    }
    private WWWForm answeredQuestionBody;
    [SerializeField] private Redirector redirector;
    [SerializeField] private UIMultiPrefabsOSA questionCollectionOSA;
    [SerializeField] private List<QuestionCollection> questionCollections = new List<QuestionCollection>();

    public List<QuestionCollection> QuestionCollections { get => questionCollections; set => questionCollections = value; }
    public WWWForm AnsweredQuestionBody { get => answeredQuestionBody; set => answeredQuestionBody = value; }

    public void GetQuestionCollectionsResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        questionCollections.Clear();

        for (int i = 0; i < resToValue["data"]["question_collections_open"].Count; i++)
        {
            questionCollections.Add(new QuestionCollection()
            {
                classroomId = resToValue["data"]["question_collections_open"][i]["classroom_id"],
                difficulty = resToValue["data"]["question_collections_open"][i]["difficulty"],
                gameType = resToValue["data"]["question_collections_open"][i]["game_id"],
                id = resToValue["data"]["question_collections_open"][i]["id"],
                name = resToValue["data"]["question_collections_open"][i]["name"],
                questionsPerTime = resToValue["data"]["question_collections_open"][i]["questions_per_time"],
            });
        }
    }

    public void ShowQuestionCollections()
    {
        redirector.Push("classroom.question.collection");
        var collections = new List<BaseModel>();

        for (int i = 0; i < questionCollections.Count; i++)
        {
            collections.Add(new QuestionCollectionItemModel()
            {
                QuestionCollectionModel = new UIQuestionCollectionModel()
                {
                    ClassroomId = questionCollections[i].classroomId,
                    Difficulty = questionCollections[i].difficulty,
                    GameType = questionCollections[i].gameType,
                    Id = questionCollections[i].id,
                    Name = questionCollections[i].name,
                    QuestionsPerTime = questionCollections[i].questionsPerTime,
                }
            });
        }
        questionCollectionOSA.Data.ResetItems(collections);
    }

    public void SendAnsweredQuestions()
    {
        StartCoroutine(SendAnsweredQuestionsCoroutine());
    }

    private IEnumerator SendAnsweredQuestionsCoroutine()
    {
        answeredQuestionBody.AddField("user_id", GlobalSetting.LoginUser.Id);
        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/answered_question", answeredQuestionBody);

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
