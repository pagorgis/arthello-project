using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceAnimate : MonoBehaviour
{

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;

    }

    public void PieceFlipWtoB() // method for flipping  piece animation
    {
        anim.enabled = true;
        anim.Play("PieceFlipWtoB");
    }

    public void PieceDropBlack() // method for dropping piece animation
    {
        anim.enabled = true;
        anim.Play("PieceDropBlack");

    }

    public void PieceFlipBtoW() // method for flipping  piece animation
    {
        anim.enabled = true;
        anim.Play("PieceFlipBtoW");
        //anim.enabled = false;
    }

    public void PieceDropWhite() // method for dropping piece animation
    {
        anim.enabled = true;
        anim.Play("PieceDropWhite");
    }

    IEnumerator Timedelay(float time)
    {
        yield return new WaitForSeconds(time);
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update() // use 1 or 2 if you want to test animations
    {
        if (Input.GetKeyDown("1"))
        {
            PieceFlipWtoB();
            //StartCoroutine(Timedelay(4));
        }

        if (Input.GetKeyDown("2"))
        {
            PieceFlipBtoW();
            //StartCoroutine(Timedelay(4));
        }

        if (Input.GetKeyDown("3"))
        {
            PieceDropWhite();
            //StartCoroutine(Timedelay(2));
        }

        if (Input.GetKeyDown("4"))
        {
            PieceDropBlack();
            //StartCoroutine(Timedelay(2));
        }
    }
}
