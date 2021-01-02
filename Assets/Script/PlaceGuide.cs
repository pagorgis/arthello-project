using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that handles the helper pieces that appear before a player makes a move
public class PlaceGuide : MonoBehaviour
{
    
    public GameObject whiteHelperObj;       // Stores the helper piece for color white
    public GameObject blackHelperObj;       // Stores the helper piece for color black
    private bool helperObjExists = false;   // Indicates if square has a helper piece active
    public Material blue;                   // The color/material blue to be applied to piece (white)
    public Material red;                    // The color/material red to be applied to piece (black)

    void Start()
    {

    }

    // Constantly updates the board with helper pieces if player can put piece on that square. Keeps track of
    // valid moves available at the time. Removes those that become invalid to put piece on.
    void Update()
    {
        GameObject square = GameObject.Find(gameObject.name);
        string[] square_num = gameObject.name.Split('_');
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in transform) children.Add(child.gameObject);
        foreach (GameObject child in children)
        {
            if (child.name.StartsWith("helper") && !OthelloGame.validMoves.Contains(square_num[1])) // If helper piece exists but position is no longer valid move => destroy hpiece
            {
                Destroy(child);
                helperObjExists = false;
            }
        }
        if (helperObjExists == false && OthelloGame.validMoves.Contains(square_num[1])) // If helper piece doesn't exist but position is now valid move => create hpiece
        {
            Transform squareTransform = square.GetComponent<Transform>();
            GameObject piece = Instantiate(OthelloGame.currentTurn == "white" ? whiteHelperObj : blackHelperObj, squareTransform);
            piece.name = "helper_" + square_num[1];
            piece.layer = LayerMask.NameToLayer("Ignore Raycast"); // To ignore helper piece when touching it, meaning detecting touch on the square below it
            helperObjExists = true;
        }
        else if (helperObjExists == true)   // If it exists, apply appropriate material (color). Without it, some helper pieces can get wrong colors which is bug.
        {
            foreach (GameObject child in children)
            {
                if (child.name.StartsWith("helper"))
                {
                    if (OthelloGame.currentTurn == "white")
                    {
                        child.GetComponent<Renderer>().material = blue;
                    }
                    else if (OthelloGame.currentTurn == "black")
                    {
                        child.GetComponent<Renderer>().material = red;
                    }
                }
            }
        }
    }
}
