using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script that is used to update the score on the UI on top of the screen
public class CanvasScore : MonoBehaviour
{
    public GameObject blackScore;
    public GameObject whiteScore;
    public GameObject blackTurn;
    public GameObject whiteTurn;

    void Start()
    {
        
    }

    // Updates the score that is observed from the state, and sets the "marker" to the color in turn
    void Update()
    {
        Text blackTO = blackScore.GetComponent<Text>();
        Text whiteTO = whiteScore.GetComponent<Text>();
        blackTO.text = "" + OthelloGame.blackScore;
        whiteTO.text = "" + OthelloGame.whiteScore;

        if (OthelloGame.currentTurn == "black")
        {
            blackTurn.SetActive(true);
            whiteTurn.SetActive(false);
        }
        else if (OthelloGame.currentTurn == "white")
        {
            blackTurn.SetActive(false);
            whiteTurn.SetActive(true);
        }
    }
}
