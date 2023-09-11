using Library;
using LuanVan.OSA;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ClassroomController : MonoBehaviour
{
    [System.Serializable]
    public struct Question
    {
        public string id;
        public string classroomId;
        public string content;
        public string createdAt;
    }

    [System.Serializable]
    public struct Answer
    {
        public string id;
        public string questionId;
        public string content;
        public string createdAt;
        public bool isCorrect;
    }

    [System.Serializable]
    public struct QuestionWithAnswers
    {
        public Question question;
        public List<Answer> answers;
    }


    [Header("OSAs: ")]
    [SerializeField] private UIMultiPrefabsOSA classroomOSA;
    [SerializeField] private UIMultiPrefabsOSA playOSA;

    [Header("Scripts: ")]
    [SerializeField] private Redirector redirector;

    [Header("Classrooms: ")]
    [SerializeField] private int page;
    [SerializeField] private int perPage;

    [Header("Questions: ")]
    [SerializeField] private int questionAmount;

    [Header("Play Stats: ")]
    [SerializeField] private int currentQuestion;
    [SerializeField] private int correctAnswersCount;
    [SerializeField] private UIAnswerModel currentAnswerSelect;
    [SerializeField] private List<QuestionWithAnswers> listOfCurrentQuestions = new List<QuestionWithAnswers>();
    [SerializeField] private float playTime;
    [SerializeField] private bool isStartPlayTimer;
    [Header("UIs Play: ")]
    [SerializeField] private Button buttonCheckAnswer;
    [SerializeField] private Slider sliderProgress;
    [SerializeField] private RectTransform uiCorrectNotice, uiWrongNotice;
    [SerializeField] private RectTransform uiComplete;
    [SerializeField] private Color colorSelectAnswer;
    [SerializeField] private Color colorNonSelectAnswer;
    [SerializeField] private TextMeshProUGUI textPlayTime;
    [SerializeField] private TextMeshProUGUI textAccuracy;

    public UIMultiPrefabsOSA ClassroomOSA { get => classroomOSA; set => classroomOSA = value; }

    private void Update()
    {
        if (isStartPlayTimer)
        {
            playTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// call in button get classroom
    /// </summary>
    public void GetClassrooms()
    {
        StartCoroutine(GetClassroomCoroutine());
    }

    public IEnumerator GetClassroomCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/classrooms" +
            "?page=" + page +
            "&per_page=" + perPage);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        GetClassroomResponse(res);
    }

    public void GetClassroomResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        var listClassroom = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"]["data"].Count; i++)
        {

            listClassroom.Add(new ClassroomItemModel()
            {
                ClassroomModel = new UIClassroomModel()
                {
                    CreatedAt = resToValue["data"]["data"][i]["created_at"],
                    Description = resToValue["data"]["data"][i]["description"],
                    Id = resToValue["data"]["data"][i]["id"],
                    Name = resToValue["data"]["data"][i]["name"],
                    AvatarPath = resToValue["data"]["data"][i]["avatar_path"],
                    ThemeColor = resToValue["data"]["data"][i]["theme_color"],
                }
            });
        }

        classroomOSA.Data.ResetItems(listClassroom);
    }

    public void GetQuestionsAndAnswers(UIClassroomModel classroomModel)
    {
        playOSA.Data.ResetItems(new List<BaseModel>());
        redirector.Push("play");

        currentQuestion = 0;
        correctAnswersCount = 0;
        sliderProgress.maxValue = questionAmount;
        sliderProgress.value = currentQuestion;

        StartCoroutine(GetQuestionsAndAnswersCoroutine(classroomModel));
    }

    public IEnumerator GetQuestionsAndAnswersCoroutine(UIClassroomModel classroomModel)
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/questions" +
            "?class=" + classroomModel.Id +
            "&amount=" + questionAmount);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        var resToValue = JSONNode.Parse(res);

        listOfCurrentQuestions.Clear();

        for (int i = 0; i < resToValue["data"].Count; i++)
        {
            var questionRes = new Question()
            {
                classroomId = resToValue["data"][i]["classroom_id"],
                content = resToValue["data"][i]["content"],
                createdAt = resToValue["data"][i]["created_at"],
                id = resToValue["data"][i]["id"]
            };

            List<Answer> answersRes = new List<Answer>();

            for (int j = 0; j < resToValue["data"][i]["answers_in_random_order"].Count; j++)
            {
                answersRes.Add(new Answer()
                {
                    content = resToValue["data"][i]["answers_in_random_order"][j]["content"],
                    createdAt = resToValue["data"][i]["answers_in_random_order"][j]["created_at"],
                    id = resToValue["data"][i]["answers_in_random_order"][j]["id"],
                    isCorrect = resToValue["data"][i]["answers_in_random_order"][j]["is_correct"] == 1,
                    questionId = resToValue["data"][i]["answers_in_random_order"][j]["question_id"],
                });
            }

            listOfCurrentQuestions.Add(new QuestionWithAnswers()
            {
                answers = answersRes,
                question = questionRes,
            });
        }

        List<BaseModel> answerItemModels = new List<BaseModel>();

        foreach (var answer in listOfCurrentQuestions[0].answers)
        {
            answerItemModels.Add(new AnswerItemModel()
            {
                AnswerModel = new UIAnswerModel()
                {
                    Content = answer.content,
                    CreatedAt = answer.createdAt,
                    Id = answer.id,
                    QuestionId = answer.questionId,
                    IsCorrect = answer.isCorrect,
                }
            });
        }

        playOSA.Data.ResetItems(answerItemModels);

        playOSA.Data.InsertOneAtStart(new QuestionItemModel()
        {
            QuestionModel = new UIQuestionModel()
            {
                Content = listOfCurrentQuestions[0].question.content,
                ClassroomId = listOfCurrentQuestions[0].question.classroomId,
                CreatedAt = listOfCurrentQuestions[0].question.createdAt,
                Id = listOfCurrentQuestions[0].question.id,
            }
        });
        playOSA.ScheduleForceRebuildLayout();

        playTime = 0f;
        StartPlayTimer();
    }

    public void SelectAnswer(UIAnswerModel answerModel)
    {
        foreach (var baseModel in playOSA.Data)
        {
            if (!(baseModel is AnswerItemModel answer))
            {
                continue;
            }
            if (answer.AnswerModel.ViewsHolder != null)
            {
                answer.AnswerModel.ViewsHolder.imageAnswerOutline.color = colorNonSelectAnswer;
            }
        }

        answerModel.ViewsHolder.imageAnswerOutline.color = colorSelectAnswer;

        currentAnswerSelect = answerModel;

        buttonCheckAnswer.interactable = true;
    }

    public void CheckAnswer()
    {
        if (currentAnswerSelect.IsCorrect)
        {
            uiCorrectNotice.gameObject.SetActive(true);
            correctAnswersCount++;
        }
        else
        {
            uiWrongNotice.gameObject.SetActive(true);
        }

        currentQuestion++;
        sliderProgress.value = currentQuestion;
    }

    public void LoadNextQuestion()
    {
        uiWrongNotice.gameObject.SetActive(false);
        uiCorrectNotice.gameObject.SetActive(false);
        buttonCheckAnswer.interactable = false;

        foreach (var baseModel in playOSA.Data)
        {
            if (!(baseModel is AnswerItemModel answer))
            {
                continue;
            }
            if (answer.AnswerModel.ViewsHolder != null)
            {
                answer.AnswerModel.ViewsHolder.imageAnswerOutline.color = colorNonSelectAnswer;
            }
        }

        if (currentQuestion == questionAmount)
        {
            StopPlayerTimer();

            uiComplete.gameObject.SetActive(true);
            textAccuracy.text = correctAnswersCount.ToString() + "/" + questionAmount;

            TimeSpan timeSpanInPlay = TimeSpan.FromSeconds((double)playTime);

            textPlayTime.text = timeSpanInPlay.Minutes.ToString("00") + ":" + timeSpanInPlay.Seconds.ToString("00");

            return;
        }

        List<BaseModel> answerItemModels = new List<BaseModel>();
        foreach (var answer in listOfCurrentQuestions[currentQuestion].answers)
        {
            answerItemModels.Add(new AnswerItemModel()
            {
                AnswerModel = new UIAnswerModel()
                {
                    Content = answer.content,
                    CreatedAt = answer.createdAt,
                    Id = answer.id,
                    QuestionId = answer.questionId,
                    IsCorrect = answer.isCorrect,
                }
            });
        }
        playOSA.Data.ResetItems(answerItemModels);

        playOSA.Data.InsertOneAtStart(new QuestionItemModel()
        {
            QuestionModel = new UIQuestionModel()
            {
                Content = listOfCurrentQuestions[currentQuestion].question.content,
                ClassroomId = listOfCurrentQuestions[currentQuestion].question.classroomId,
                CreatedAt = listOfCurrentQuestions[currentQuestion].question.createdAt,
                Id = listOfCurrentQuestions[0].question.id,
            }
        });
    }

    public void CompletePlay()
    {
        uiComplete.gameObject.SetActive(false);
        redirector.Pop();
    }

    public void StartPlayTimer()
    {
        isStartPlayTimer = true;
    }

    public void StopPlayerTimer()
    {
        isStartPlayTimer = false;
    }

    public void CheckAndGetOldClassrooms(UIClassroomModel classroomModel)
    {
        if (classroomModel.ViewsHolder.ItemIndex == (classroomOSA.Data.Count - 1))
        {
            StartCoroutine(GetOldClassroomsCoroutine(classroomModel));
        }
    }

    public IEnumerator GetOldClassroomsCoroutine(UIClassroomModel classroomModel)
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/classrooms/old" +
        "?per_page=" + perPage +
        "&date=" + classroomModel.CreatedAt);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log("time: " + classroomModel.CreatedAt + "\n" + res);

        GetOldClassrommResponse(res);
    }

    public void GetOldClassrommResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        var listClassroom = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"]["data"].Count; i++)
        {
            bool isConflict = false;
            foreach (var item in classroomOSA.Data)
            {
                if (item is ClassroomItemModel classroom)
                {
                    if (classroom.ClassroomModel.Id.Equals(resToValue["data"]["data"][i]["id"]))
                    {
                        isConflict = true;
                        break;
                    }
                }
            }

            if (!isConflict)
            {
                listClassroom.Add(new ClassroomItemModel()
                {
                    ClassroomModel = new UIClassroomModel()
                    {
                        CreatedAt = resToValue["data"]["data"][i]["created_at"],
                        Description = resToValue["data"]["data"][i]["description"],
                        Id = resToValue["data"]["data"][i]["id"],
                        Name = resToValue["data"]["data"][i]["name"],
                        AvatarPath = resToValue["data"]["data"][i]["avatar_path"],
                        ThemeColor = resToValue["data"]["data"][i]["theme_color"],
                    }
                });
            }

        }

        classroomOSA.Data.InsertItemsAtEnd(listClassroom);
    }
}
