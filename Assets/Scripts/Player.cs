using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public bool isMoving = false;
    public GameObject[] allChildren;
    public int Id;
    public float Health = 10;
    public bool damaged = false;
    Animator anim;
    public GameObject health;

    // Use this for initialization
    void Start()
    {
        allChildren = transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        health.GetComponent<Image>().fillAmount = Health/10;

        if (damaged == true)
        {
            anim.SetInteger("State", 2);
        }
        if (damaged == false && isMoving == false)
        {
            anim.SetInteger("State 0", 2);
            anim.SetInteger("State", 0);
        }
        var hit = Physics2D.OverlapCircle(this.transform.position, 0.3f, 1 << LayerMask.NameToLayer("Platform"));
        damaged = hit.gameObject.GetComponent<Hex>().damaged;
        if (Health <= 0)
        {
            SceneManager.LoadScene("Player"+Id+"Wins");
        }
        if (damaged)
        {
            Health -= Time.deltaTime;
        }
        if(!hit.gameObject.GetComponent<Hex>().canWalkHere)
        {
            Health -= Time.deltaTime;
        }
    }
    public void UpdateMovement()
    {
        foreach (var child in allChildren)
        {
            child.SetActive(true);
            var hit = Physics2D.OverlapCircle(child.transform.position, 0.1f, 1 << LayerMask.NameToLayer("Platform"));
            if (hit.gameObject.GetComponent<Hex>().canWalkHere == false)
            {
                child.SetActive(false);
            }
        }
    }
}
