using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{

    public bool canWalkHere = false; //Checks if player is allowed to move onto tile
    public float health = 10;
    public int x;
    public int y;
    public bool damaged;
    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            canWalkHere = false;
            damaged = false;
            health = 0;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
        }
        if (damaged)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
            health -= Time.deltaTime;
        }
        else if (this.name != "Border" && health > 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }
}
