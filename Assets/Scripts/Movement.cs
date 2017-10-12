using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Player thePlayer;
    public string direction;
    public string player;
    public float moveSpeed;
    public float t;
    public Vector3 startPosition;
    public Vector3 endPosition;

    Animator anim;
    private SpriteRenderer SR;

    public bool AnalogMovingUL = false;
    public bool AnalogMovingU = false;
    public bool AnalogMovingUR = false;
    public bool AnalogMovingR = false;
    public bool AnalogMovingDR = false;
    public bool AnalogMovingD = false;
    public bool AnalogMovingDL = false;
    public bool AnalogMovingL = false;
    public bool DpadMovingUL = false;
    public bool DpadMovingUR = false;
    public bool DpadMovingU = false;
    public bool DpadMovingR = false;
    public bool DpadMovingDR = false;
    public bool DpadMovingD = false;
    public bool DpadMovingDL = false;
    public bool DpadMovingL = false;

    // Use this for initialization
    void Start()
    {
        moveSpeed = thePlayer.moveSpeed;
        anim = thePlayer.GetComponent<Animator>();
        SR = thePlayer.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        AnalogMovingUL = Input.GetAxis(player + " PS4 Left Analog Horizontal") < -0.03 && Input.GetAxis(player + " PS4 Left Analog Vertical") > 0.03;
        AnalogMovingU = Input.GetAxis(player + " PS4 Left Analog Vertical") > 0.03 && !(Input.GetAxis(player + " PS4 Left Analog Horizontal") > 0.03) && !(Input.GetAxis(player + " PS4 Left Analog Horizontal") < -0.03);
        AnalogMovingUR = Input.GetAxis(player + " PS4 Left Analog Horizontal") > 0.03 && Input.GetAxis(player + " PS4 Left Analog Vertical") > 0.03;
        AnalogMovingR = Input.GetAxis(player + " PS4 Left Analog Horizontal") > 0.03 && !(Input.GetAxis(player + " PS4 Left Analog Vertical") > 0.03) && !(Input.GetAxis(player + " PS4 Left Analog Vertical") < -0.03);
        AnalogMovingDR = Input.GetAxis(player + " PS4 Left Analog Horizontal") > 0.03 && Input.GetAxis(player + " PS4 Left Analog Vertical") < -0.03;
        AnalogMovingD = Input.GetAxis(player + " PS4 Left Analog Vertical") < -0.03 && !(Input.GetAxis(player + " PS4 Left Analog Horizontal") > 0.03) && !(Input.GetAxis(player + " PS4 Left Analog Horizontal") < -0.03);
        AnalogMovingDL = Input.GetAxis(player + " PS4 Left Analog Horizontal") < -0.03 && Input.GetAxis(player + " PS4 Left Analog Vertical") < -0.03;
        AnalogMovingL = Input.GetAxis(player + " PS4 Left Analog Horizontal") < -0.03 && !(Input.GetAxis(player + " PS4 Left Analog Vertical") > 0.03) && !(Input.GetAxis(player + " PS4 Left Analog Vertical") < -0.03);

        DpadMovingUL = Input.GetAxis(player + " PS4 Dpad Horizontal") < -0.7 && Input.GetAxis("1 PS4 Dpad Vertical") > 0.7;
        DpadMovingU = Input.GetAxis(player + " PS4 Dpad Vertical") == 1;
        DpadMovingUR = Input.GetAxis(player + " PS4 Dpad Horizontal") > 0.7 && Input.GetAxis("1 PS4 Dpad Vertical") > 0.7;
        DpadMovingR = Input.GetAxis(player + " PS4 Dpad Horizontal") == 1;
        DpadMovingDR = Input.GetAxis(player + " PS4 Dpad Horizontal") > 0.7 && Input.GetAxis("1 PS4 Dpad Vertical") < -0.7;
        DpadMovingD = Input.GetAxis(player + " PS4 Dpad Vertical") == -1;
        DpadMovingDL = Input.GetAxis(player + " PS4 Dpad Horizontal") < -0.7 && Input.GetAxis("1 PS4 Dpad Vertical") < -0.7;
        DpadMovingL = Input.GetAxis(player + " PS4 Dpad Horizontal") == -1;


        if (!thePlayer.isMoving && (direction == "UL" && (AnalogMovingUL || DpadMovingUL || AnalogMovingU || DpadMovingU || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W)) ||
            direction == "UR" && (AnalogMovingUR || DpadMovingUR || AnalogMovingU || DpadMovingU) ||
            direction == "R" && (AnalogMovingR || DpadMovingR) ||
            direction == "DR" && (AnalogMovingDR || DpadMovingDR || AnalogMovingD || DpadMovingD) ||
            direction == "DL" && (AnalogMovingDL || DpadMovingDL || AnalogMovingD || DpadMovingD) ||
            direction == "L" && (AnalogMovingL || DpadMovingL )))
        {
            StartCoroutine(MoveToPosition());
        }
    }

    IEnumerator MoveToPosition()
    {
        thePlayer.isMoving = true;
        if (direction == "UL" || direction == "DL" || direction == "L")
        {
            anim.SetInteger("State", 1);
            SR.flipX = true;
        }
        if (direction == "UR" || direction == "DR" || direction == "R")
        {
            anim.SetInteger("State", 1);
            SR.flipX = false;
        }
        t = 0;

        startPosition = thePlayer.transform.position;
        endPosition = transform.position;

        while (t < 1f)
        {
            t += Mathf.Clamp(Time.deltaTime * (moveSpeed / Vector3.Distance(startPosition, endPosition)), 0f, 1f);
            thePlayer.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        thePlayer.transform.position = endPosition;
        thePlayer.UpdateMovement();
        thePlayer.isMoving = false;
        if(!thePlayer.isMoving && !thePlayer.damaged)
        {
            anim.SetInteger("State 0", 3);
            anim.SetInteger("State", 0);
        }
        
    }
}
