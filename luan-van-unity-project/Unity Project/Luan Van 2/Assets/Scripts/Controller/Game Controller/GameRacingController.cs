using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRacingController : MonoBehaviour
{
    [Header("Stats: ")]
    [SerializeField] private float backgroundScrollTime;
    [Header("Scripts: ")]
    [SerializeField] private ClassroomController classroomController;
    [Header("Others: ")]
    [SerializeField] private Transform backgroundContainer;
    [SerializeField] private Transform obstaclesContainer;
    public void StartGame()
    {
        obstaclesContainer.LeanSetLocalPosY(10);
        backgroundContainer.LeanSetLocalPosY(0);
        LeanTween.cancel(backgroundContainer.gameObject);
        LeanTween.cancel(obstaclesContainer.gameObject);
        LeanTween.moveLocalY(backgroundContainer.gameObject, -20, backgroundScrollTime).setEase(LeanTweenType.linear).setLoopClamp();
    }

    public void StopGame()
    {
        if (classroomController.PlayerHp <= 0)
        {
            LeanTween.cancel(backgroundContainer.gameObject);
            LeanTween.cancel(obstaclesContainer.gameObject);
        }
    }
}
