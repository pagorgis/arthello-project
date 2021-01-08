using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that adds a piece to the board. Is inside each (green) square object. 
public class AddPiece : MonoBehaviour
{
    public GameObject pieceObj;
    private bool placed = false; // True when a player has played a piece on this square

   
    void Start()
    {
        InitialPieces();
    }


    // Detects touch input and also detects if this square object has been pressed. If yes, then adds the piece to the board
    // If piece is already on board but needs to change color, it is destroyed then recreated on the opposite side.
    // (THIS PART NEED TO BE ADJUSTED BOTH FOR ANIMATION AND CROSSHAIR STYLE OF PLAY)
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
                string hitObjectName = rayhit.transform.name;                               // Get the name of the square object hit
                if (placed == false && hitObjectName == gameObject.name && validMoves.Contains(square_num[1]))  // If this square, no piece placed there and move on square is valid
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
                        
                        OthelloGame.PlacePiece(square_num[1]);                              // Place piece in the board state (backend code)
                        

                    } 
                    else
                    {
                        GameObject piece = Instantiate(pieceObj, hitObjectTransform);
                        piece.name = "piece_" + square_num[1];
                        placed = true;
                        OthelloGame.PlacePiece(square_num[1]);
                        
                    }                    
                }
            }
        }
        if (placed == true && OthelloGame.piecesToConvert.Contains(square_num[1]))          // If player has won this piece from opponent, convert it
        {
            var children = new List<GameObject>();
            foreach (Transform child in transform) children.Add(child.gameObject);          // Add all children of square object to a temporary list

            //children.ForEach(child => Destroy(child));                                      // Destroy piece to create new (if animation later, shouldn't destroy)  
            if (OthelloGame.currentTurn == "white")
                children.ForEach(child => child.GetComponent<PieceAnimate>().PieceFlipWtoB());
            if (OthelloGame.currentTurn == "black")
                children.ForEach(child => child.GetComponent<PieceAnimate>().PieceFlipBtoW());
            
            

            OthelloGame.ApplyConvertOfPiece(square_num[1]);                                 // Remove piece from state so the view only update once with the new one

            /*
            GameObject squareObject = GameObject.Find(gameObject.name);
            Transform squareObjectTransform = squareObject.GetComponent<Transform>();
            if (OthelloGame.currentTurn == "white")
            {
                GameObject piece = Instantiate(pieceObj, squareObjectTransform);
                piece.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
                piece.transform.localPosition = new Vector3(piece.transform.localPosition.x, 4, piece.transform.localPosition.z);
                piece.GetComponent<PieceAnimate>().PieceDropWhite(); // init drop piece animation
                piece.name = "piece_" + square_num[1];
                placed = true;
            }
            else if (OthelloGame.currentTurn == "black")
            {
                GameObject piece = Instantiate(pieceObj, squareObjectTransform);
                piece.name = "piece_" + square_num[1];
                piece.GetComponent<PieceAnimate>().PieceDropBlack(); // init drop piece animation
            }
            */
        }
    }

    // Places the four pieces in the beginning of basic Othello
    public void InitialPieces()
    {
        string[] square_num = gameObject.name.Split('_');
        if (square_num[1] == "33" || square_num[1] == "44")                 // Create the two white pieces at the start of the game.
        {
            Debug.Log("RAN " + square_num[1]);
            Transform squareTransform = gameObject.GetComponent<Transform>();   // Get the transform-part of square (position and stuff)
            GameObject piece = Instantiate(pieceObj, squareTransform);      // Create piece in square; square becomes the piece's parent
            piece.name = "piece_" + square_num[1];                          // Name of piece, piece_06 indicates row 0 (z) column 6 (x) 
            placed = true;
        }
        else if (square_num[1] == "34" || square_num[1] == "43")            // Create the two black pieces at the start of the game.
        {
            Transform squareTransform = gameObject.GetComponent<Transform>();
            GameObject piece = Instantiate(pieceObj, squareTransform);
            piece.transform.localRotation = Quaternion.Euler(0f, 0f, 180f); // Flip it to show the black side of piece
            piece.transform.localPosition = new Vector3(
                piece.transform.localPosition.x, 4, piece.transform.localPosition.z); // Need to be pushed up after flip, constant 4 should be changed
            piece.name = "piece_" + square_num[1];
            placed = true;
        }
    }
}
