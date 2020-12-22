using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPiece : MonoBehaviour
{
    public GameObject testObj;
    public GameObject square;

    // Start is called before the first frame update
    void Start()
    {
        Transform tempTransform = square.GetComponent<Transform>();
        Instantiate(testObj, new Vector3(0f, 0.7f, 0f), Quaternion.identity, tempTransform);
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

            //Create a new Material
            Material material = new Material(Shader.Find("Standard"));
            material.color = Color.red;

            //Switch to new material
            rend.material = material;
            */
        }
    }
}
