using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRestart : MonoBehaviour
{
    public GameObject restartScreen;
    public GameObject restartButton;
    bool screenShown = false;
    // Start is called before the first frame update
    void Start()
    {
        restartScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (screenShown == false && OthelloGame.gameOver == true)
        {
            restartScreen.SetActive(true);
            screenShown = true;
        }
        else if (screenShown == true && OthelloGame.gameOver == false)
        {
            restartScreen.SetActive(false);
            screenShown = false;
        }
    }
}
