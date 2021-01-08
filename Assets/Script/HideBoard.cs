using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class HideBoard : MonoBehaviour
{
    public GameObject gameBoard;
    bool shown = true;

    void Start()
    {

    }


    void Update()
    {
        if (shown == true && OthelloGame.gameOver == true)
        {
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
                r.enabled = false;
            gameObject.GetComponent<Renderer>().enabled = false;
            shown = false;
        }
        else if (shown == false && OthelloGame.gameOver == false)
        {
            /*
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
                r.enabled = false;
            gameObject.GetComponent<Renderer>().enabled = false;
            */
            var trackable = gameObject.GetComponent<TrackableBehaviour>();
            var status = trackable.CurrentStatus;
            if (status == TrackableBehaviour.Status.TRACKED)
            {
                foreach (Renderer r in GetComponentsInChildren<Renderer>())
                    r.enabled = true;
                gameObject.GetComponent<Renderer>().enabled = true;
                shown = true;
            }
        }
    }
}