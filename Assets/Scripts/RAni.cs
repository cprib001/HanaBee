using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAni : MonoBehaviour {
    Animator anim;
    private SpriteRenderer SR;
    public Color color;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.H))
        {
            anim.SetInteger("State", 1);
            SR.flipX = false;
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            anim.SetInteger("State 0", 3);
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetInteger("State", 1);
            SR.flipX = true;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            anim.SetInteger("State 0", 3);
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            anim.SetInteger("State", 2);
            SR.color = new Color(color.r, color.g, color.b, 0);
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            anim.SetInteger("State 0", 2);
            anim.SetInteger("State", 0);
            SR.color = new Color(255, 255, 255, 255);
        }
    }
}
