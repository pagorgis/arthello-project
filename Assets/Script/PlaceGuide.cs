using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceGuide : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject whiteHelperObj;
    public GameObject blackHelperObj;
    private bool helperObjExists = false;

    void Start()
    {
        /*
        GameObject square = GameObject.Find(gameObject.name);               
        string[] square_num = gameObject.name.Split('_');
        if (square_num[1] == "23" || square_num[1] == "32")
        {
            //Transform squareTransform = square.GetComponent<Transform>();   
            //GameObject piece = Instantiate(blackHelperObj, squareTransform);      
            //piece.name = "helper_" + square_num[1];
            //Color colorPiece = piece.GetComponent<Renderer>().material.color;
            //colorPiece.a = 0.5f;
            //piece.GetComponent<Renderer>().material.color = colorPiece;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
        List<string> validMoves = OthelloGame.validMoves;
        GameObject square = GameObject.Find(gameObject.name);
        string[] square_num = gameObject.name.Split('_');
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in transform) children.Add(child.gameObject);
        foreach (GameObject child in children)
        {
            if (child.name.StartsWith("helper") && !OthelloGame.validMoves.Contains(square_num[1]))
            {
                Destroy(child);
                helperObjExists = false;
            }
        }
        if (helperObjExists == false && OthelloGame.validMoves.Contains(square_num[1]))
        {
            Transform squareTransform = square.GetComponent<Transform>();
            GameObject piece = Instantiate(OthelloGame.currentTurn == "white" ? whiteHelperObj : blackHelperObj, squareTransform);
            piece.name = "helper_" + square_num[1];
            piece.layer = LayerMask.NameToLayer("Ignore Raycast");
            helperObjExists = true;
        }
    }
}
