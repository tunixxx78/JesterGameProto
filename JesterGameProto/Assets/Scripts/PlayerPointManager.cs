using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPointManager : MonoBehaviour
{
    [SerializeField] TMP_Text PlayerPointsText, PlayerTwoPointsText;
    public static int playerPoints, playerTwoPoints;
    public int points,pointsTwo, wantedPoints, wantedPointsTwo;

    private void Start()
    {
        playerPoints = wantedPoints;
        playerTwoPoints = wantedPointsTwo;
    }

    private void Update()
    {
        points = playerPoints;
        pointsTwo = playerTwoPoints;

        if(playerPoints > PlayerPrefs.GetInt("PlayerPoints", 0))
        {
            PlayerPrefs.SetInt("PlayerPoints", playerPoints);
        }

        if(playerTwoPoints > PlayerPrefs.GetInt("PlayerTwoPoints", 0))
        {
            PlayerPrefs.SetInt("PlayerTwoPoints", playerTwoPoints);
        }

        PlayerPointsText.text = playerPoints.ToString();
        PlayerTwoPointsText.text = playerTwoPoints.ToString();
    }

    public void ResetPlayerPoints()
    {
        playerPoints = wantedPoints;
        playerTwoPoints = wantedPointsTwo;
    }
}
