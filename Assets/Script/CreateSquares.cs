using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSquares : MonoBehaviour
{

    public GameObject squareObj;

    // Start is called before the first frame update
    void Start()
    {
        GameObject board = GameObject.Find("Game_board");
        Transform boardTransform = board.GetComponent<Transform>();
        int countZ = 0;
        int countX = 0;

        for (float z = 0.42f; z > -0.43f; z = z - 0.12f)
        {
            countZ++;
            for (float x = -0.42f; x < 0.43f; x = x + 0.12f)
            {
                countX++;
                GameObject createdSquare = Instantiate(squareObj);
                createdSquare.transform.parent = boardTransform;
                createdSquare.transform.localPosition = new Vector3(x, squareObj.transform.localPosition.y, z);
                createdSquare.transform.rotation = Quaternion.identity;
                createdSquare.transform.localScale = squareObj.transform.localScale;
                createdSquare.name = "square_" + countZ + countX;
            }
            countX = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
