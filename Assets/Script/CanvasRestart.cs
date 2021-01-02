using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasRestart : MonoBehaviour
{
    public GameObject restartScreen;
    public GameObject restartButton;
    public GameObject winnerText;
    public CreateSquares createSquaresScript;
    bool screenShown = false;

    // Start is called before the first frame update
    void Start()
    {
        restartScreen.SetActive(false);
        Button button = restartButton.GetComponent<Button>();
        button.onClick.AddListener(buttonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if (screenShown == false && OthelloGame.gameOver == true)
        {
            winnerText.GetComponent<Text>().text = "Winner: " + OthelloGame.GetWinner().ToUpper();
            restartScreen.SetActive(true);
            screenShown = true;
        }
    }

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
