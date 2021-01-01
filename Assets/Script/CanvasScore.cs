using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScore : MonoBehaviour
{
    public GameObject blackScore;
    public GameObject whiteScore;
    public GameObject blackTurn;
    public GameObject whiteTurn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
