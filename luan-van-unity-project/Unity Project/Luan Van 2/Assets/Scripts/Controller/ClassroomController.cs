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
    [SerializeField] private UIMultiPrefabsOSA userClassroomOSA;
    [SerializeField] private UIMultiPrefabsOSA classroomInfoOSA;
    [SerializeField] private UIMultiPrefabsOSA topicCommentOSA;
    [SerializeField] private UIMultiPrefabsOSA classroomRanksOSA;
    [SerializeField] private UIMultiPrefabsOSA usersInClassroomOSA;

    [Header("Scripts: ")]
    [SerializeField] private Redirector redirector;
    [SerializeField] private MainGameplayController mainGameplayController;
    [SerializeField] private ResponseErrorChecker responseErrorChecker;

    [Header("Classrooms: ")]
    [SerializeField] private int page;
    [SerializeField] private int perPage;
    [SerializeField] UIClassroomModel currentClassroomModel;

    [Header("Topics: ")]
    [SerializeField] private int topicGetCount;
    [SerializeField] private int topicCommentGetCount;
    [SerializeField] private UITopicModel currentTopicSellect;
    [SerializeField] private TMP_InputField inputFieldTopicComment;

    [Header("Questions: ")]
    [SerializeField] private int questionAmount;

    [Header("Play Stats: ")]
    [SerializeField] private int playerHp;
    [SerializeField] private int playerHpMax;
    [SerializeField] private float playTime;
    [SerializeField] private bool isStartPlayTimer;
    [SerializeField] private int gameTypeId;
    [SerializeField] private int currentQuestion;
    [SerializeField] private int correctAnswersCount;
    [SerializeField] private UIAnswerModel currentAnswerSelect;
    [SerializeField] private List<QuestionWithAnswers> listOfCurrentQuestions = new List<QuestionWithAnswers>();

    [Header("UIs: ")]

    [Header("Ranks: ")]
    [SerializeField] private List<Image> imageButtonRanks;

    [Header("Global UIs:")]
    [SerializeField] private Slider sliderProgress;
    [SerializeField] private RectTransform uiCorrectNotice, uiWrongNotice;
    [SerializeField] private Color colorNonSelectAnswer;
    [SerializeField] private Color colorSelectAnswer;
    [SerializeField] private TextMeshProUGUI textPlayTime;
    [SerializeField] private TextMeshProUGUI textAccuracy;
    [SerializeField] private RectTransform uiComplete;
    [SerializeField] private Button buttonCheckAnswer;
    [SerializeField] private TextMeshProUGUI textDefeatContent;
    [SerializeField] private RectTransform uiCompleteDefeat;
    [SerializeField] private RectTransform uiTutorial;

    [Header("Fighting Monster UIs:")]
    [SerializeField] private RectTransform containerFightingMonster;
    [SerializeField] private RectTransform containerToturialsFightingMonster;
    [SerializeField] private Animator animatorPlayer;
    [SerializeField] private Animator animatorEnemy;
    [SerializeField] private Slider sliderPlayerHp;

    [Header("Car Racing UIs:")]
    [SerializeField] private GameRacingController gameRacingController;
    [SerializeField] private RectTransform containerCarRacing;
    [SerializeField] private RectTransform containerTutorialsCarRacing;
    [SerializeField] private Transform playerCar;
    [SerializeField] private Transform containerObstacles;
    [SerializeField] private List<Transform> obstaclesSprite = new List<Transform>();

    [Header("Ninja Fruit UIs: ")]
    [SerializeField] private RectTransform containerNinjaFruit;
    [SerializeField] private Transform playerNinja;
    [SerializeField] private Animator animatorNinja;
    [SerializeField] private Transform containerFruits;
    [SerializeField] private RectTransform containerTutorialsFruitNinja;
    [SerializeField] private TextMeshProUGUI textHpFruits;
    [SerializeField] private RectTransform timerBar;
    [SerializeField] private List<Transform> fruitSprites = new List<Transform>();

    [Header("Ninja Fruit UIs: ")]
    [SerializeField] private RectTransform containerDropFood;
    [SerializeField] private Transform playerBasket;
    [SerializeField] private TextMeshProUGUI textHpFoods;
    [SerializeField] private Transform containerFoods;
    [SerializeField] private List<Transform> foodSprites = new List<Transform>();
    [SerializeField] private RectTransform containerTutorialsDropFoods;
    [Header("Flappy Bird UIs: ")]
    [SerializeField] private RectTransform containerBird;
    [SerializeField] private Transform playerBird;
    [SerializeField] private Transform containerWalls;
    [SerializeField] private TextMeshProUGUI textBirdScores;
    [SerializeField] private RectTransform containerTutorialsFlappyBird;
    [SerializeField] private List<Transform> wallSprites = new List<Transform>();




    public UIMultiPrefabsOSA ClassroomOSA { get => classroomOSA; set => classroomOSA = value; }
    public int PlayerHp { get => playerHp; set => playerHp = value; }
    public UIClassroomModel CurrentClassroomModel { get => currentClassroomModel; set => currentClassroomModel = value; }
    public UITopicModel CurrentTopicSellect { get => currentTopicSellect; set => currentTopicSellect = value; }
    public UIMultiPrefabsOSA TopicCommentOSA { get => topicCommentOSA; set => topicCommentOSA = value; }
    public UITopicModel CurrentTopicSellect1 { get => currentTopicSellect; set => currentTopicSellect = value; }

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
        redirector.Push("classroom");
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
                    ContainerOSA = "classroom"
                }
            });
        }

        classroomOSA.Data.ResetItems(listClassroom);
    }

    public void GetQuestionsAndAnswers(string collectionId, int gameType)
    {
        gameTypeId = gameType;

        // disable all game container. After that, check and active.
        containerFightingMonster.gameObject.SetActive(false);
        containerCarRacing.gameObject.SetActive(false);
        containerNinjaFruit.gameObject.SetActive(false);
        containerDropFood.gameObject.SetActive(false);
        containerBird.gameObject.SetActive(false);

        containerToturialsFightingMonster.gameObject.SetActive(false);
        containerTutorialsCarRacing.gameObject.SetActive(false);
        containerTutorialsFruitNinja.gameObject.SetActive(false);
        containerTutorialsDropFoods.gameObject.SetActive(false);
        containerTutorialsFlappyBird.gameObject.SetActive(false);

        uiTutorial.gameObject.SetActive(true);

        uiWrongNotice.gameObject.SetActive(false);
        uiCorrectNotice.gameObject.SetActive(false);
        buttonCheckAnswer.interactable = false;

        if (gameTypeId == 1)
        {
            containerFightingMonster.gameObject.SetActive(true);
            containerToturialsFightingMonster.gameObject.SetActive(true);

            playerHpMax = 3;
            sliderPlayerHp.value = playerHpMax;
            sliderPlayerHp.maxValue = playerHpMax;
        }
        if (gameTypeId == 2)
        {
            gameRacingController.StartGame();
            containerCarRacing.gameObject.SetActive(true);
            containerTutorialsCarRacing.gameObject.SetActive(true);
            playerHpMax = 1;
        }

        if (gameTypeId == 3)
        {
            containerNinjaFruit.gameObject.SetActive(true);
            containerTutorialsFruitNinja.gameObject.SetActive(true);


            LeanTween.moveLocalX(playerNinja.gameObject, -4.5f, 0.5f).setOnComplete(() =>
            {
                animatorNinja.Play("idle");
            });

            playerHpMax = 3;
            textHpFruits.text = "Sức khỏe: <color=red>" + playerHpMax.ToString() + "</color>";
        }

        if (gameTypeId == 4)
        {
            containerDropFood.gameObject.SetActive(true);
            containerTutorialsDropFoods.gameObject.SetActive(true);
            playerHpMax = 3;
            textHpFoods.text = "<color=#ffaaaa>x " + playerHpMax.ToString() + "</color>";
        }
        if (gameTypeId == 5)
        {
            playerHpMax = 1;
            containerBird.gameObject.SetActive(true);
            containerTutorialsFlappyBird.gameObject.SetActive(true);

            LeanTween.cancel(containerWalls.gameObject);
            containerWalls.LeanSetLocalPosX(10);
        }


        redirector.Push("play");
        playOSA.Data.ResetItems(new List<BaseModel>());

        uiCompleteDefeat.gameObject.SetActive(false);

        currentQuestion = 0;
        correctAnswersCount = 0;

        textBirdScores.text = correctAnswersCount.ToString();

        StartCoroutine(GetQuestionsAndAnswersCoroutine(collectionId));
    }

    public IEnumerator GetQuestionsAndAnswersCoroutine(string collectionId)
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/questions" +
            "?question_collection_id=" + collectionId);

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
                    content = (j + 1).ToString() + ". " + resToValue["data"][i]["answers_in_random_order"][j]["content"],
                    createdAt = resToValue["data"][i]["answers_in_random_order"][j]["created_at"],
                    id = resToValue["data"][i]["answers_in_random_order"][j]["id"],
                    isCorrect = resToValue["data"][i]["answers_in_random_order"][j]["is_correct"],
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

        questionAmount = resToValue["data"].Count;

        sliderProgress.maxValue = questionAmount;
        sliderProgress.value = currentQuestion;

        playerHp = playerHpMax;

        mainGameplayController.AnsweredQuestionBody = new WWWForm();

        playTime = 0f;
    }

    public void SelectRandomAnswer()
    {
        var model = playOSA.Data[UnityEngine.Random.Range(1, playOSA.Data.Count)];
        var answer = model as AnswerItemModel;
        SelectAnswer(answer.AnswerModel);

        CheckAnswer();
    }


    public void SelectAnswer(UIAnswerModel answerModel)
    {

        if (gameTypeId == 2)
        {
            foreach (var sprite in obstaclesSprite)
            {
                sprite.gameObject.SetActive(true);
            }
        }

        if (gameTypeId == 3)
        {
            foreach (var sprite in fruitSprites)
            {
                sprite.gameObject.SetActive(true);
                sprite.GetComponent<Animator>().Play("base");
            }
        }
        if (gameTypeId == 4)
        {
            foreach (var sprite in foodSprites)
            {
                sprite.gameObject.SetActive(true);
                sprite.GetComponent<Animator>().Play("base");
            }
        }

        if (gameTypeId == 5)
        {
            foreach (var sprite in wallSprites)
            {
                sprite.gameObject.SetActive(false);
            }
        }

        int temp = 0;

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

            if (answer.AnswerModel.IsCorrect)
            {
                obstaclesSprite[temp].gameObject.SetActive(false);
                wallSprites[temp].gameObject.SetActive(true);
            }

            if (!answer.AnswerModel.IsCorrect)
            {
                fruitSprites[temp].gameObject.SetActive(false);
                foodSprites[temp].gameObject.SetActive(false);
            }

            temp++;
        }

        answerModel.ViewsHolder.imageAnswerOutline.color = colorSelectAnswer;

        if (gameTypeId == 2)
        {
            if (answerModel.ViewsHolder.ItemIndex == 1)
            {
                LeanTween.cancel(playerCar.gameObject);
                LeanTween.moveLocalX(playerCar.gameObject, -4.5f, 0.5f);
            }
            if (answerModel.ViewsHolder.ItemIndex == 2)
            {
                LeanTween.cancel(playerCar.gameObject);
                LeanTween.moveLocalX(playerCar.gameObject, -1.5f, 0.5f);
            }
            if (answerModel.ViewsHolder.ItemIndex == 3)
            {
                LeanTween.cancel(playerCar.gameObject);
                LeanTween.moveLocalX(playerCar.gameObject, 1.5f, 0.5f);
            }
            if (answerModel.ViewsHolder.ItemIndex == 4)
            {
                LeanTween.cancel(playerCar.gameObject);
                LeanTween.moveLocalX(playerCar.gameObject, 4.5f, 0.5f);
            }
        }

        if (gameTypeId == 3)
        {
            if (currentAnswerSelect.ViewsHolder != null)
            {
                if (answerModel.ViewsHolder.ItemIndex > currentAnswerSelect.ViewsHolder.ItemIndex)
                {
                    playerNinja.transform.localScale = new Vector3(1f, 1f, 1f);
                }
                else
                {
                    playerNinja.transform.localScale = new Vector3(-1f, 1f, 1f);
                }
            }

            if (answerModel.ViewsHolder.ItemIndex == 1)
            {
                LeanTween.cancel(playerNinja.gameObject);
                animatorNinja.Play("move");
                LeanTween.moveLocalX(playerNinja.gameObject, -4.5f, 0.5f).setOnComplete(() =>
                {
                    animatorNinja.Play("idle");
                });
            }
            if (answerModel.ViewsHolder.ItemIndex == 2)
            {
                LeanTween.cancel(playerNinja.gameObject);
                animatorNinja.Play("move");

                LeanTween.moveLocalX(playerNinja.gameObject, -1.5f, 0.5f).setOnComplete(() =>
                {
                    animatorNinja.Play("idle");

                });
            }
            if (answerModel.ViewsHolder.ItemIndex == 3)
            {
                animatorNinja.Play("move");
                LeanTween.cancel(playerNinja.gameObject);
                LeanTween.moveLocalX(playerNinja.gameObject, 1.5f, 0.5f).setOnComplete(() =>
                {

                    animatorNinja.Play("idle");
                });
            }
            if (answerModel.ViewsHolder.ItemIndex == 4)
            {
                animatorNinja.Play("move");
                LeanTween.cancel(playerNinja.gameObject);
                LeanTween.moveLocalX(playerNinja.gameObject, 4.5f, 0.5f).setOnComplete(() =>
                {
                    animatorNinja.Play("idle");

                });
            }
        }

        if (gameTypeId == 4)
        {
            if (answerModel.ViewsHolder.ItemIndex == 1)
            {
                LeanTween.cancel(playerBasket.gameObject);
                LeanTween.moveLocalX(playerBasket.gameObject, -4.5f, 0.5f).setOnComplete(() =>
                {

                });
            }
            if (answerModel.ViewsHolder.ItemIndex == 2)
            {
                LeanTween.cancel(playerBasket.gameObject);
                LeanTween.moveLocalX(playerBasket.gameObject, -1.5f, 0.5f).setOnComplete(() =>
                {

                });
            }
            if (answerModel.ViewsHolder.ItemIndex == 3)
            {
                LeanTween.cancel(playerBasket.gameObject);
                LeanTween.moveLocalX(playerBasket.gameObject, 1.5f, 0.5f).setOnComplete(() =>
                {

                });
            }
            if (answerModel.ViewsHolder.ItemIndex == 4)
            {
                LeanTween.cancel(playerBasket.gameObject);
                LeanTween.moveLocalX(playerBasket.gameObject, 4.5f, 0.5f).setOnComplete(() =>
                {

                });
            }
        }

        if (gameTypeId == 5)
        {
            if (answerModel.ViewsHolder.ItemIndex == 1)
            {
                LeanTween.cancel(playerBird.gameObject);
                LeanTween.moveLocalY(playerBird.gameObject, 3f, 0.5f).setOnComplete(() =>
                {

                });
            }

            if (answerModel.ViewsHolder.ItemIndex == 2)
            {
                LeanTween.cancel(playerBird.gameObject);
                LeanTween.moveLocalY(playerBird.gameObject, 1f, 0.5f).setOnComplete(() =>
                {

                });
            }
            if (answerModel.ViewsHolder.ItemIndex == 3)
            {
                LeanTween.cancel(playerBird.gameObject);
                LeanTween.moveLocalY(playerBird.gameObject, -1f, 0.5f).setOnComplete(() =>
                {

                });
            }

            if (answerModel.ViewsHolder.ItemIndex == 4)
            {
                LeanTween.cancel(playerBird.gameObject);
                LeanTween.moveLocalY(playerBird.gameObject, -3f, 0.5f).setOnComplete(() =>
                {

                });
            }
        }

        currentAnswerSelect = answerModel;

        buttonCheckAnswer.interactable = true;
    }

    public void CheckAnswer()
    {
        buttonCheckAnswer.interactable = false;

        double delayTimeShowAnswer = 2000;

        if (gameTypeId == 1)
        {
            delayTimeShowAnswer = 1000;
        }

        if (currentAnswerSelect.IsCorrect)
        {
            Timer.PerformWithDelay(delayTimeShowAnswer, (e) =>
            {
                uiCorrectNotice.gameObject.SetActive(true);
            });

            correctAnswersCount++;

            if (gameTypeId == 1)
            {
                animatorPlayer.Play("attack");
                animatorEnemy.Play("hurt");
            }

        }
        else
        {
            Timer.PerformWithDelay(delayTimeShowAnswer, (e) =>
            {
                uiWrongNotice.gameObject.SetActive(true);
            });

            playerHp--;

            if (gameTypeId == 1)
            {
                animatorPlayer.Play("hurt");
                animatorEnemy.Play("attack");
                sliderPlayerHp.value = playerHp;
            }
        }

        if (gameTypeId == 2)
        {
            LeanTween.cancel(containerObstacles.gameObject);
            containerObstacles.LeanSetLocalPosY(10);
            LeanTween.moveLocalY(containerObstacles.gameObject, -10f, 1.5f).setEase(LeanTweenType.linear);
        }

        if (gameTypeId == 3)
        {
            LeanTween.cancel(containerFruits.gameObject);
            containerFruits.LeanSetLocalPosY(10);
            LeanTween.moveLocalY(containerFruits.gameObject, -10f, 2f).setEase(LeanTweenType.linear);
        }
        if (gameTypeId == 4)
        {
            LeanTween.cancel(containerFoods.gameObject);
            containerFoods.LeanSetLocalPosY(10);
            LeanTween.moveLocalY(containerFoods.gameObject, -10f, 2f).setEase(LeanTweenType.linear);
        }
        if (gameTypeId == 5)
        {
            LeanTween.cancel(containerWalls.gameObject);
            containerWalls.LeanSetLocalPosX(10);
            LeanTween.moveLocalX(containerWalls.gameObject, -10f, 2f).setEase(LeanTweenType.linear);
        }

        if (playerHp <= 0)
        {
            if (gameTypeId == 1)
            {
                textDefeatContent.text = "Bạn đã kiệt sức!";
            }

            if (gameTypeId == 2)
            {
                textDefeatContent.text = "Bạn đã gây ra va chạm!";
            }


            uiCompleteDefeat.gameObject.SetActive(true);

            uiWrongNotice.gameObject.SetActive(false);
            uiCorrectNotice.gameObject.SetActive(false);
            buttonCheckAnswer.interactable = false;
            StopPlayerTimer();

        }

        mainGameplayController.AnsweredQuestionBody.AddField("data[]", currentAnswerSelect.Id);

        currentQuestion++;

        textHpFruits.text = "Sức khỏe: <color=red>" + playerHp.ToString() + "</color>";

        textHpFoods.text = "<color=#ffaaaa>x " + playerHp.ToString() + "</color>";

        sliderProgress.value = currentQuestion;

        LeanTween.cancel(timerBar.gameObject);
        LeanTween.scaleX(timerBar.gameObject, 1f, 0.5f);
    }

    public void LoadNextQuestion()
    {
        uiWrongNotice.gameObject.SetActive(false);
        uiCorrectNotice.gameObject.SetActive(false);

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

        if (gameTypeId == 3)
        {
            LeanTween.cancel(timerBar.gameObject);
            LeanTween.scaleX(timerBar.gameObject, 1f, 0.5f).setOnComplete(() =>
            {
                LeanTween.scaleX(timerBar.gameObject, 0.05f, 20f).setOnComplete(() =>
                {
                    SelectRandomAnswer();
                });
            });
        }

        if (gameTypeId == 5)
        {
            textBirdScores.text = correctAnswersCount.ToString();
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
        mainGameplayController.SendAnsweredQuestions();
        uiCompleteDefeat.gameObject.SetActive(false);
        uiComplete.gameObject.SetActive(false);
        gameRacingController.StopGame();
        redirector.Pop();
    }

    public void StartPlayTimer()
    {
        isStartPlayTimer = true;

        if (gameTypeId == 3)
        {
            LeanTween.cancel(timerBar.gameObject);
            LeanTween.scaleX(timerBar.gameObject, 1f, 0.5f).setOnComplete(() =>
            {
                LeanTween.scaleX(timerBar.gameObject, 0.05f, 20f).setOnComplete(() =>
                {
                    SelectRandomAnswer();
                });
            });
        }
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
                        ContainerOSA = "classroom"
                    }
                });
            }

        }

        classroomOSA.Data.InsertItemsAtEnd(listClassroom);
    }

    public void GetUserClassrooms()
    {
        StartCoroutine(GetUserClassroomsCoroutine());
    }

    private IEnumerator GetUserClassroomsCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/users/classrooms" +
            "?user_id=" + GlobalSetting.LoginUser.Id);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        GetUserClassroomsResponse(res);
    }
    public void GetUserClassroomsResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        var listClassroom = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"].Count; i++)
        {

            listClassroom.Add(new ClassroomItemModel()
            {
                ClassroomModel = new UIClassroomModel()
                {
                    CreatedAt = resToValue["data"][i]["classroom"]["created_at"],
                    Description = resToValue["data"][i]["classroom"]["description"],
                    Id = resToValue["data"][i]["classroom"]["id"],
                    Name = resToValue["data"][i]["classroom"]["name"],
                    AvatarPath = resToValue["data"][i]["classroom"]["avatar_path"],
                    ThemeColor = resToValue["data"][i]["classroom"]["theme_color"],
                    ContainerOSA = "user_classroom"
                }
            });
        }

        userClassroomOSA.Data.ResetItems(listClassroom);
    }

    public void GetClassroomInfo(UIClassroomModel classroomModel)
    {
        redirector.Push("classroom.info");

        currentClassroomModel = classroomModel;

        classroomInfoOSA.Data.ResetItems(new List<BaseModel>());

        StartCoroutine(GetClassroomInfoCoroutine(classroomModel.Id));
    }

    public IEnumerator GetClassroomInfoCoroutine(string classroomId)
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/classrooms/info" +
            "?user_id=" + GlobalSetting.LoginUser.Id +
            "&classroom_id=" + classroomId);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        GetClassroomInfoResponse(res);

        GetClassroomTopics(classroomId);
    }

    public void GetClassroomInfoResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        var classroomInfo = new ClassroomInfoItemModel()
        {
            ClassroomInfoModel = new UIClassroomInfoModel()
            {
                AvatarPath = resToValue["data"]["avatar_path"],
                CreatedAt = resToValue["data"]["created_at"],
                Description = resToValue["data"]["description"],
                Id = resToValue["data"]["id"],
                Name = resToValue["data"]["name"],
                StudyStatus = resToValue["data"]["study_status"]["study_status_id"] ?? "",
                ThemeColor = resToValue["data"]["theme_color"],
            }
        };

        mainGameplayController.GetQuestionCollectionsResponse(res);

        classroomInfoOSA.Data.InsertOneAtStart(classroomInfo);
    }

    public void GetClassroomTopics(string classroomId)
    {
        StartCoroutine(GetClassroomTopicsCoroutine(classroomId));
    }

    private IEnumerator GetClassroomTopicsCoroutine(string classroomId)
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/classrooms/topics" +
            "?user_id=" + GlobalSetting.LoginUser.Id +
            "&classroom_id=" + classroomId +
            "&per_page=" + topicGetCount);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);
        GetClassroomTopicsResponse(res);
    }

    private void GetClassroomTopicsResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);
        var topics = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"]["data"].Count; i++)
        {
            topics.Add(new TopicItemModel()
            {
                TopicModel = new UITopicModel()
                {
                    Id = resToValue["data"]["data"][i]["id"],
                    CommentCount = resToValue["data"]["data"][i]["comment_count"],
                    Content = resToValue["data"]["data"][i]["content"],
                    CreateAt = resToValue["data"]["data"][i]["created_at"],
                    LikeCount = (resToValue["data"]["data"][i]["like_up"] - resToValue["data"]["data"][i]["like_down"]),
                    LikeStatus = resToValue["data"]["data"][i]["like_status"]["like_status"] ?? "",
                    UserFullname = resToValue["data"]["data"][i]["user"]["name"],
                    UserId = resToValue["data"]["data"][i]["user"]["id"],
                    Username = resToValue["data"]["data"][i]["user"]["username"],
                    AvatarPath = resToValue["data"]["data"][i]["user"]["avatar_path"],
                    TopicStatus = resToValue["data"]["data"][i]["topic_status_id"],
                    ImagePath = resToValue["data"]["data"][i]["image_path"],
                    Title = resToValue["data"]["data"][i]["title"],
                }
            });
        }

        classroomInfoOSA.Data.InsertItems(1, topics);
    }

    public void ShowTopicComments(UITopicModel topicModel)
    {
        currentTopicSellect = topicModel;
        redirector.Push("classroom.topic.comment");

        topicCommentOSA.Data.ResetItems(new List<BaseModel>()
        {
            new TopicItemModel()
            {
                TopicModel = topicModel,
            }
        });

        GetTopicComments(currentTopicSellect.Id);
    }

    public void GetTopicComments(string topicId)
    {
        StartCoroutine(GetTopicCommentsCoroutine(topicId));
    }

    private IEnumerator GetTopicCommentsCoroutine(string topicId)
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/classrooms/topics/comments" +
            "?user_id=" + GlobalSetting.LoginUser.Id +
            "&classroom_topic_id=" + topicId +
            "&per_page=" + topicCommentGetCount);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        GetTopicCommentsRespose(res);
    }

    public void GetTopicCommentsRespose(string res)
    {
        var resToValue = JSONNode.Parse(res);

        var comments = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"]["data"].Count; i++)
        {
            comments.Add(new TopicCommentItemModel()
            {
                TopicCommentModel = new UITopicCommentModel()
                {
                    Content = resToValue["data"]["data"][i]["content"],
                    CreatedAt = resToValue["data"]["data"][i]["created_at"],
                    Id = resToValue["data"]["data"][i]["id"],
                    LikeCount = (resToValue["data"]["data"][i]["like_up"] - resToValue["data"]["data"][i]["like_down"]),
                    LikeStatus = resToValue["data"]["data"][i]["like_status"]["like_status"] ?? "",
                    TopicId = resToValue["data"]["data"][i]["classroom_topic_id"],
                    UserFullName = resToValue["data"]["data"][i]["user"]["name"],
                    UserId = resToValue["data"]["data"][i]["user"]["id"],
                    Username = resToValue["data"]["data"][i]["user"]["username"],
                    AvatarPath = resToValue["data"]["data"][i]["user"]["avatar_path"],
                }
            });
        }

        topicCommentOSA.Data.InsertItems(1, comments);
    }

    public void UpdateStudyClassroomStatus(string status, UIClassroomInfoModel classroomInfo)
    {
        StartCoroutine(UpdateStudyClassroomStatusCoroutine(status, classroomInfo));
    }

    private IEnumerator UpdateStudyClassroomStatusCoroutine(string status, UIClassroomInfoModel classroomInfo)
    {
        WWWForm body = new WWWForm();
        body.AddField("user_id", GlobalSetting.LoginUser.Id);
        body.AddField("classroom_id", classroomInfo.Id);
        body.AddField("status", status);

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/users/classrooms/edit", body);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        UpdateStudyClassroomStatusResponse(res, classroomInfo);
    }

    private void UpdateStudyClassroomStatusResponse(string res, UIClassroomInfoModel classroomInfo)
    {
        var resToValue = JSONNode.Parse(res);

        classroomInfo.StudyStatus = resToValue["data"]["study_status_id"];

        classroomInfoOSA.ForceUpdateViewsHolderIfVisible(0);
    }

    public void UpdateTopicLikeStatus(string topicId, string status)
    {
        //Debug.Log("Here");

        for (int i = 0; i < classroomInfoOSA.Data.Count; i++)
        {
            if (classroomInfoOSA.Data[i] is TopicItemModel topic)
            {
                if (topic.TopicModel.Id.Equals(topicId))
                {
                    classroomInfoOSA.ForceUpdateViewsHolderIfVisible(i);
                    break;
                }
            }
        }
        topicCommentOSA.ForceUpdateViewsHolderIfVisible(0);
        StartCoroutine(UpdateTopicLikeStatusCoroutine(topicId, status));
    }

    private IEnumerator UpdateTopicLikeStatusCoroutine(string topicId, string status)
    {
        WWWForm body = new WWWForm();
        body.AddField("user_id", GlobalSetting.LoginUser.Id);
        body.AddField("classroom_topic_id", topicId);
        body.AddField("status", status);

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/classrooms/topics/like", body);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);
        UpdateTopicLikeStatusResponse(res);
    }

    private void UpdateTopicLikeStatusResponse(string res)
    {

    }

    public void UpdateTopicCommentLikeStatus(string topicCommentId, string status, int index)
    {

        topicCommentOSA.ForceUpdateViewsHolderIfVisible(index);
        StartCoroutine(UpdateTopicCommentLikeStatusCoroutine(topicCommentId, status));
    }

    private IEnumerator UpdateTopicCommentLikeStatusCoroutine(string topicCommentId, string status)
    {
        WWWForm body = new WWWForm();
        body.AddField("user_id", GlobalSetting.LoginUser.Id);
        body.AddField("topic_comment_id", topicCommentId);
        body.AddField("status", status);

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/classrooms/topics/comments/like", body);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);
        UpdateTopicCommentLikeStatusRespose(res);

    }
    private void UpdateTopicCommentLikeStatusRespose(string res)
    {

    }

    public void CreateTopicComment()
    {
        StartCoroutine(CreateTopicCommentCoroutine());
    }

    private IEnumerator CreateTopicCommentCoroutine()
    {
        WWWForm body = new WWWForm();
        body.AddField("user_id", GlobalSetting.LoginUser.Id);
        body.AddField("classroom_topic_id", currentTopicSellect.Id);
        body.AddField("content", inputFieldTopicComment.text);

        inputFieldTopicComment.text = "";

        UnityWebRequest request = UnityWebRequest.Post(GlobalSetting.Endpoint + "api/classrooms/topics/comments", body);

        responseErrorChecker.SendRequest();
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);

        JSONNode resToValues = JSONNode.Parse(res);
        if (resToValues["message"] != null)
        {
            responseErrorChecker.GetResponse(resToValues["message"][0][0]);
            Debug.Log(resToValues["message"][0][0]);
            yield break;
        }
        responseErrorChecker.GetResponse("");

        currentTopicSellect.CommentCount++;

        for (int i = 0; i < classroomInfoOSA.Data.Count; i++)
        {
            if (classroomInfoOSA.Data[i] is TopicItemModel topic)
            {
                if (topic.TopicModel.Id.Equals(currentTopicSellect.Id))
                {
                    classroomInfoOSA.ForceUpdateViewsHolderIfVisible(i);
                    break;
                }
            }
        }
        topicCommentOSA.ForceUpdateViewsHolderIfVisible(0);

        CreateTopicCommentRespose(res);
    }

    private void CreateTopicCommentRespose(string res)
    {
        var resToValue = JSONNode.Parse(res);

        topicCommentOSA.Data.InsertOne(1, new TopicCommentItemModel()
        {
            TopicCommentModel = new UITopicCommentModel()
            {
                Content = resToValue["data"]["content"],
                CreatedAt = resToValue["data"]["created_at"],
                Id = resToValue["data"]["id"],
                LikeCount = 0,
                LikeStatus = "0",
                TopicId = resToValue["data"]["classroom_topic_id"],
                UserFullName = resToValue["data"]["user"]["name"],
                UserId = resToValue["data"]["user"]["id"],
                Username = resToValue["data"]["user"]["username"],
                AvatarPath = resToValue["data"]["user"]["avatar_path"],

            }
        });
    }

    public void GetClassroomRanks(string type)
    {
        redirector.Push("classroom.rank");

        var showText = new UIMultiModel()
        {
            Type = "center_text",
        };

        foreach (var image in imageButtonRanks)
        {
            image.color = Color.white;
        }

        if (type.Equals("day"))
        {
            imageButtonRanks[0].color = Color.cyan;
            showText.PassedVariable["content"] = "Bảng xếp hạng hằng ngày";
        }

        if (type.Equals("week"))
        {
            imageButtonRanks[1].color = Color.cyan;
            showText.PassedVariable["content"] = "Bảng xếp hạng hằng tuần";
        }

        if (type.Equals("month"))
        {
            imageButtonRanks[2].color = Color.cyan;
            showText.PassedVariable["content"] = "Bảng xếp hạng hằng tháng";
        }

        classroomRanksOSA.Data.ResetItems(new List<BaseModel>()
        {
            new MultiItemModel()
            {
                MultiModel = showText,
            }
        });

        StartCoroutine(GetClassroomRanksCoroutine(type));
    }

    public IEnumerator GetClassroomRanksCoroutine(string type)
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/rank/classrooms/" + type +
            "?classroom_id=" + currentClassroomModel.Id);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);
        GetClassroomRanksResponse(res);
    }

    private void GetClassroomRanksResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        var listOfRanks = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"].Count; i++)
        {
            listOfRanks.Add(new LatestOnlineUserItemModel()
            {
                LatestOnlineUserModel = new UILatestOnlineUserModel()
                {
                    AvatarPath = resToValue["data"][i]["user"]["avatar_path"],
                    ContainerOSA = "rank",
                    CreatedAt = resToValue["data"][i]["user"]["created_at"],
                    Id = resToValue["data"][i]["user"]["id"],
                    Name = "<size=-5><color=#fff700>Hạng #" + (i + 1).ToString() + "</color></size>\n" + resToValue["data"][i]["user"]["name"] + "\n" + "<size=-10>Tổng câu hỏi đã trả lời đúng: " + resToValue["data"][i]["total_answers"] + "</size>",
                    UpdatedAt = resToValue["data"][i]["user"]["updated_at"],
                    Username = resToValue["data"][i]["user"]["username"],
                }
            });
        }

        classroomRanksOSA.Data.InsertItemsAtEnd(listOfRanks);
    }

    public void GetUsersInClassroom()
    {
        redirector.Push("classroom.study.user");
        usersInClassroomOSA.Data.ResetItems(new List<BaseModel>());
        StartCoroutine(GetUsersInClassroomCoroutine());
    }

    private IEnumerator GetUsersInClassroomCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(GlobalSetting.Endpoint + "api/classrooms/users" +
            "?classroom_id=" + currentClassroomModel.Id);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        string res = request.downloadHandler.text;

        Debug.Log(res);
        GetUsersInClassroomResponse(res);
    }

    private void GetUsersInClassroomResponse(string res)
    {
        var resToValue = JSONNode.Parse(res);

        var users = new List<BaseModel>();

        for (int i = 0; i < resToValue["data"]["data"].Count; i++)
        {
            users.Add(new LatestOnlineUserItemModel()
            {
                LatestOnlineUserModel = new UILatestOnlineUserModel()
                {
                    CreatedAt = resToValue["data"]["data"][i]["created_at"],
                    Id = resToValue["data"]["data"][i]["id"],
                    Name = resToValue["data"]["data"][i]["name"],
                    UpdatedAt = resToValue["data"]["data"][i]["updated_at"],
                    Username = resToValue["data"]["data"][i]["username"],
                    AvatarPath = resToValue["data"]["data"][i]["avatar_path"] ?? "",
                    ContainerOSA = "friend",
                }
            });
        }

        usersInClassroomOSA.Data.InsertItemsAtStart(users);
    }

    public void BirdStopWallMove()
    {
        LeanTween.cancel(containerWalls.gameObject);
    }
}
