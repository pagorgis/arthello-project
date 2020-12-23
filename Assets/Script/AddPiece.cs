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
        if (square_num[1] == "44" || square_num[1] == "45" || square_num[1] == "54" || square_num[1] == "55")
        {
            Transform tempTransform = square.GetComponent<Transform>();
            GameObject testa = Instantiate(testObj, tempTransform);
            testa.name = "piece_" + square_num[1];
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
                    GameObject piece = Instantiate(testObj, hitObjectTransform);
                    piece.name = "piece_" + square_num[1];
                    placed = true;
                }
            }
        }
    }
}
