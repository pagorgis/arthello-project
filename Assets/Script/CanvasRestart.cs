using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasRestart : MonoBehaviour
{
    public GameObject restartScreen;
    public GameObject restartButton;
    public CreateSquares createSquaresScript;
    bool screenShown = false;
    // Start is called before the first frame update
    void Start()
    {
        //restartScreen.SetActive(false);
        Button button = restartButton.GetComponent<Button>();
        button.onClick.AddListener(buttonClicked);
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

    void buttonClicked()
    {
        Debug.Log("clicked button");
        OthelloGame.resetState();
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
