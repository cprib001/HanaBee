using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAttackManager : MonoBehaviour {

    public Player player1;
    public Player player2;

    public bool currentlySpawning1 = false;
    public bool currentlySpawning2 = false;

    private static NewAttackManager instance;
    public static NewAttackManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void spawnHitOn(int player)
    {
        if(player == 1 && !currentlySpawning1)
        {
            StartCoroutine(spawnHit(1));
        }
        else if (player == 2 && !currentlySpawning2)
        {
            StartCoroutine(spawnHit(2));
        }
    }

    public IEnumerator spawnHit(int player)
    {
        if(player == 1)
        {
            currentlySpawning1 = true;
            var hit = Physics2D.OverlapCircle(player1.transform.position, 0.3f, 1 << LayerMask.NameToLayer("Platform"));
            hit.gameObject.GetComponent<Hex>().damaged = true;
            yield return new WaitForSeconds(1);
            currentlySpawning1 = false;
        }
        if (player == 2)
        {
            currentlySpawning2 = true;
            var hit = Physics2D.OverlapCircle(player2.transform.position, 0.3f, 1 << LayerMask.NameToLayer("Platform"));
            hit.gameObject.GetComponent<Hex>().damaged = true;
            yield return new WaitForSeconds(1);
            currentlySpawning2 = false;
        }

    }
}
