using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.Linq;

public class Player : MonoBehaviour {

    public float moveSpeed;
    public bool isMoving = false;
    public GameObject[] allChildren;
    public int Id;
    public float Health = 10;
    public float iFrames;
    bool Invulnerable = false;
    public bool damaged = false;
	// Use this for initialization
	void Start () {
        allChildren = transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
    }
	
	// Update is called once per frame
	void Update () {
		if (Health <= 0)
        {
            EditorSceneManager.LoadScene("Player" + Id + "Wins");
        }
        if(damaged)
        {
            Health -= Time.deltaTime;
        }
	}
    public void DamageMe(int value)
    {
        if (Invulnerable)
        {
            return;
        }
        else
        {
            Health -= value;
            StartCoroutine(InvFrames());
        }
        
    }
    IEnumerator InvFrames()
    {
        Invulnerable = true;
        yield return new WaitForSeconds(iFrames);
        Invulnerable = false;
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
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Hex>().damaged)
        {
            damaged = true;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        damaged = false;
    }
}
