using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPiece : MonoBehaviour
{
    public GameObject testObj;

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
            //Instantiate(testObj, new Vector3(0f, 2f, 0f), Quaternion.identity);

            /*
            Renderer rend = square.GetComponent<Renderer>();
            //
            //Create a new Material
            Material material = new Material(Shader.Find("Standard"));
            material.color = Color.red;

            //Switch to new material
            rend.material = material;
            */
        }
    }
}
