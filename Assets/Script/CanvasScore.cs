using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScore : MonoBehaviour
{
    public GameObject blackScore;
    public GameObject whiteScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject blackScore = GameObject.Find("Black_score");
        //GameObject whiteScore = GameObject.Find("White_score");
        Text blackTO = blackScore.GetComponent<Text>();
        Text whiteTO = whiteScore.GetComponent<Text>();
        blackTO.text = "" + OthelloGame.blackScore;
        whiteTO.text = "" + OthelloGame.whiteScore;
    }
}
