using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OthelloGame : MonoBehaviour
{
    public static string currentTurn = "black";             // Keep track of whose turn it is
    public static string[,] board = new string[8, 8];       // Store the board information (which color on which position)

    // Start is called before the first frame update
    void Start()
    {
        board[3, 3] = "white";
        board[3, 4] = "black";
        board[4, 3] = "black";
        board[4, 4] = "white";
        Debug.Log(board[3, 4]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
