using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSquares : MonoBehaviour
{

    public Transform squareObj;

    // Start is called before the first frame update
    void Start()
    {
        for (float x = -0.24f; x < 0.24f; x = x + 0.06f)
        {
            for (float z = -0.24f; z < 0.24f; z = z + 0.06f)
            {
                Instantiate(squareObj, new Vector3(x, 0.2f, z), squareObj.rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
