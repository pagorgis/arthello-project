using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script concerning the restart screen behaviour.
public class CanvasRestart : MonoBehaviour
{
    public GameObject restartScreen;
    public GameObject restartButton;
    public GameObject winnerText;
    public GameObject gameOverText;
    public GameObject backgroundCube;
    public Material blue;
    public Material red;
    public Material squareGreen;
    public CreateSquares createSquaresScript;
    bool screenShown = false;

    void Start()
    {
        restartScreen.SetActive(false);
        Button button = restartButton.GetComponent<Button>();
        button.onClick.AddListener(buttonClicked);
    }

    // If gameOver is true in state then game is over and display the winning text and appropriate colors
    void Update()
    {
        if (screenShown == false && OthelloGame.gameOver == true)
        {
            winnerText.GetComponent<Text>().text = "Winner: " + OthelloGame.GetWinner().ToUpper();
            if (OthelloGame.GetWinner() == "white")
            {
                winnerText.GetComponent<Text>().color = Color.white;
                gameOverText.GetComponent<Text>().color = Color.white;
                backgroundCube.GetComponent<Renderer>().material = blue;
            } else if (OthelloGame.GetWinner() == "black")
            {
                winnerText.GetComponent<Text>().color = Color.black;
                gameOverText.GetComponent<Text>().color = Color.black;
                backgroundCube.GetComponent<Renderer>().material = red;
            } else
            {
                winnerText.GetComponent<Text>().color = Color.white;
                gameOverText.GetComponent<Text>().color = Color.white;
                backgroundCube.GetComponent<Renderer>().material = squareGreen;
            }
            restartScreen.SetActive(true);
            screenShown = true;
        }
    }

    // When player clicks restart. Resets the game state, hides the restart screen and removes all squares + pieces
    // to recreate them immediately after
    void buttonClicked()
    {
        Debug.Log("clicked button");
        OthelloGame.ResetState();
        restartScreen.SetActive(false);
        screenShown = false;

        GameObject baseBoard = GameObject.Find("Game_board");
        Transform baseboardTransform = baseBoard.GetComponent<Transform>();
        var children = new List<GameObject>();
        foreach (Transform child in baseboardTransform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
        baseBoard.GetComponent<CreateSquares>().InitSquares();
    }
}
