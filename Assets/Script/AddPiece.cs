using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that adds a piece to the board. Is inside each (green) Board_square object. 
public class AddPiece : MonoBehaviour
{
    public GameObject pieceObj;
    private bool placed = false; // True when a player has played a piece on this square

    // Start is called before the first frame update
    void Start()
    {
        GameObject square = GameObject.Find(gameObject.name);               // Finds the name of the square object, e.g. square_45
        string[] square_num = gameObject.name.Split('_');
        if (square_num[1] == "33" || square_num[1] == "44")                 // Create the two white pieces at the start of the game.
        {
            Transform squareTransform = square.GetComponent<Transform>();   // Get the transform-part of square (position and stuff)
            GameObject piece = Instantiate(pieceObj, squareTransform);      // Create piece in square; square becomes the piece's parent
            piece.name = "piece_" + square_num[1];                          // Give the piece a name
            placed = true;
        }
        else if (square_num[1] == "34" || square_num[1] == "43") // Create the two black pieces at the start of the game.
        {
            Transform squareTransform = square.GetComponent<Transform>();
            GameObject piece = Instantiate(pieceObj, squareTransform);
            piece.transform.localRotation = Quaternion.Euler(0f, 0f, 180f); // Flip it to show the black side of piece
            piece.transform.localPosition = new Vector3(
                piece.transform.localPosition.x, 4, piece.transform.localPosition.z); // Need to be pushed up after flip, constant 4 should be changed
            piece.name = "piece_" + square_num[1];
            placed = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        List<string> validMoves = OthelloGame.validMoves;
        string[] square_num = gameObject.name.Split('_');
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)            // If touch is detected by user
        {           
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit rayhit = new RaycastHit();
            if (Physics.Raycast(ray, out rayhit))                                           // If touch by user hit the square object
            {
                string hitObjectName = rayhit.transform.name;                               // Get the name of the square object
                if (placed == false && hitObjectName == gameObject.name && validMoves.Contains(square_num[1]))      // If it's this square object and no piece has been placed there
                {
                    GameObject hitObject = GameObject.Find(gameObject.name);                // Almost same code below as Start()
                    Transform hitObjectTransform = hitObject.GetComponent<Transform>();
                    if (OthelloGame.currentTurn == "black")
                    {
                        GameObject piece = Instantiate(pieceObj, hitObjectTransform);
                        piece.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
                        piece.transform.localPosition = new Vector3(piece.transform.localPosition.x, 4, piece.transform.localPosition.z);
                        piece.name = "piece_" + square_num[1];
                        placed = true;
                        OthelloGame.PlacePiece(square_num[1]);
                        //OthelloGame.currentTurn = "white";                                  // Changes game state to indicate opposing player's turn
                        //OthelloGame.changedValidMoves = true;                               // Tells Game State that valid moves need to be changed, update done in that class
                    } 
                    else
                    {
                        GameObject piece = Instantiate(pieceObj, hitObjectTransform);
                        piece.name = "piece_" + square_num[1];
                        placed = true;
                        OthelloGame.PlacePiece(square_num[1]);
                        //OthelloGame.currentTurn = "black";
                        //OthelloGame.changedValidMoves = true;
                    }                    
                }
            }
        }
        if (placed == true && OthelloGame.piecesToConvert.Contains(square_num[1]))
        {
            Debug.Log(gameObject.name + " YEEEEEEEEES " + OthelloGame.currentTurn);
            var children = new List<GameObject>();
            foreach (Transform child in transform) children.Add(child.gameObject);
            children.ForEach(child => Destroy(child));
            OthelloGame.ApplyConvertOfPiece(square_num[1]);

            GameObject squareObject = GameObject.Find(gameObject.name);
            Transform squareObjectTransform = squareObject.GetComponent<Transform>();
            if (OthelloGame.currentTurn == "white")
            {
                GameObject piece = Instantiate(pieceObj, squareObjectTransform);
                piece.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
                piece.transform.localPosition = new Vector3(piece.transform.localPosition.x, 4, piece.transform.localPosition.z);
                piece.name = "piece_" + square_num[1];
                placed = true;
            }
            else if (OthelloGame.currentTurn == "black")
            {
                GameObject piece = Instantiate(pieceObj, squareObjectTransform);
                piece.name = "piece_" + square_num[1];
            }
        }
    }
}
