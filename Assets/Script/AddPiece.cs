using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPiece : MonoBehaviour
{
    public GameObject testObj;
    private bool placed = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject square = GameObject.Find(gameObject.name);

        string[] square_num = gameObject.name.Split('_');
        if (square_num[1] == "44" || square_num[1] == "55")
        {
            Transform tempTransform = square.GetComponent<Transform>();
            GameObject testa = Instantiate(testObj, tempTransform);
            testa.name = "piece_" + square_num[1];
            placed = true;
        }
        else if (square_num[1] == "45" || square_num[1] == "54")
        {
            Transform tempTransform = square.GetComponent<Transform>();
            GameObject testa = Instantiate(testObj, tempTransform);
            testa.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
            testa.transform.localPosition = new Vector3(testa.transform.localPosition.x, 4, testa.transform.localPosition.z); // 4 constant, should be changed
            testa.name = "piece_" + square_num[1];
            placed = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Transform tempTransform = square.GetComponent<Transform>();
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {           
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit rayhit = new RaycastHit();
            if (Physics.Raycast(ray, out rayhit))
            {
                string hitObjectName = rayhit.transform.name;
                if (placed == false && hitObjectName == gameObject.name)
                {
                    GameObject hitObject = GameObject.Find(gameObject.name);
                    Transform hitObjectTransform = hitObject.GetComponent<Transform>();
                    string[] square_num = gameObject.name.Split('_');
                    if (OthelloGame.currentTurn == "black")
                    {
                        GameObject piece = Instantiate(testObj, hitObjectTransform);
                        piece.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
                        piece.transform.localPosition = new Vector3(piece.transform.localPosition.x, 4, piece.transform.localPosition.z); // 4 constant, should be changed
                        piece.name = "piece_" + square_num[1];
                        placed = true;
                        OthelloGame.currentTurn = "white";

                    } 
                    else
                    {
                        GameObject piece = Instantiate(testObj, hitObjectTransform);
                        piece.name = "piece_" + square_num[1];
                        placed = true;
                        OthelloGame.currentTurn = "black";
                    }                    
                }
            }
        }
    }
}
