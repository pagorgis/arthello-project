using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates the green squares of the board dynamically to make it playable like Othello
public class CreateSquares : MonoBehaviour
{
    public GameObject squareObj;

    void Start()
    {
        InitSquares();
    }

    void Update()
    {
        
    }

    // Creates all 64 squares needed for Othello
    public void InitSquares()
    {
        GameObject board = GameObject.Find("Game_board");                               // Finds the base (black rectangular bottom)
        Transform boardTransform = board.GetComponent<Transform>();                     // Get the transform-part of board base (position and stuff)
        int countZ = 0;                                                                 // To get positions like 00, 01, ... , 76, 77 (row|column)
        int countX = 0;

        for (float z = 0.42f; z > -0.43f; z = z - 0.12f)                                // To add squares with even spaces between them
        {
            for (float x = -0.42f; x < 0.43f; x = x + 0.12f)
            {

                GameObject createdSquare = Instantiate(squareObj);                      // Create the square
                createdSquare.transform.parent = boardTransform;                        // Assign it to be the child of board base
                createdSquare.transform.localPosition = new Vector3(                    // Position should be loop x,z and the prefab's y-position
                    x, squareObj.transform.localPosition.y, z);
                createdSquare.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);   // No rotation
                createdSquare.transform.localScale = squareObj.transform.localScale;    // Scale of square should be as defined in the square prefab
                createdSquare.name = "square_" + countZ + countX;                       // To give name to the square, which row/col it concerns like square_06
                countX++;
            }
            countZ++;
            countX = 0;
        }
    }
}
