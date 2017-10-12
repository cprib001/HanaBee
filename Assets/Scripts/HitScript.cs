using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{

    public float timeToHit;
    public bool damage = false;
    public Color startColor = new Color(1, 1, 0, 1);
    public Color endColor = new Color(1, 0, 0, 1);
    public float colorTime = 0;

    private void OnEnable()
    {
        timeToHit = 1;
        colorTime = 0;
        damage = false;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeToHit -= Time.deltaTime;
        colorTime += Time.deltaTime;
        if (colorTime > 1)
        {
            colorTime = 1;
        }
        if (timeToHit < 0)
        {
            damage = true;
        }
        this.GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, endColor, colorTime);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (damage)
        {
            collision.gameObject.SendMessage("DamageMe", 1);
            this.gameObject.SetActive(false);
        }
    }
}
