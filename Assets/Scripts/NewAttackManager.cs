using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NewAttackManager : MonoBehaviour
{

    public Player player1;
    public Player player2;
    public GameObject Platform1;
    public GameObject Platform2;

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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnHitOn(int player)
    {
        if (player == 1 && !currentlySpawning1)
        {
            System.Random rnd = new System.Random();
            int temp = rnd.Next(0, 2);
            if(temp == 0)
            {
                StartCoroutine(spawnHit(1));
            }
            else
            {
                StartCoroutine(spawnHit(3));
            }
        }
        else if (player == 2 && !currentlySpawning2)
        {
            System.Random rnd = new System.Random();
            int temp = rnd.Next(0, 2);
            if (temp == 0)
            {
                StartCoroutine(spawnHit(2));
            }
            else
            {
                StartCoroutine(spawnHit(4));
            }
        }
    }

    public IEnumerator spawnHit(int player)
    {
        if (player == 1)
        {
            currentlySpawning1 = true;
            var hit = Physics2D.OverlapCircle(player1.transform.position, 0.3f, 1 << LayerMask.NameToLayer("Platform"));
            hit.gameObject.GetComponent<Hex>().damaged = true;
            yield return new WaitForSeconds(1);
            currentlySpawning1 = false;
        }
        if(player == 3)
        {
            currentlySpawning1 = true;
            System.Random rnd = new System.Random();
            var allChildren = Platform2.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
            var objectsToTarget = from o in allChildren where o.GetComponent<Hex>().x == rnd.Next(0, 7) select o;
            foreach (var o in objectsToTarget)
            {
                o.gameObject.GetComponent<Hex>().damaged = true;
                yield return new WaitForSeconds(3);
                o.gameObject.GetComponent<Hex>().damaged = false;
            }
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
        if (player == 4)
        {
            currentlySpawning2 = true;
            System.Random rnd = new System.Random();
            var allChildren = Platform1.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();
            var objectsToTarget = from o in allChildren where o.GetComponent<Hex>().x == rnd.Next(0, 6) select o;
            foreach (var o in objectsToTarget)
            {
                o.gameObject.GetComponent<Hex>().damaged = true;
                yield return new WaitForSeconds(3);
                o.gameObject.GetComponent<Hex>().damaged = false;
            }
            currentlySpawning2 = false;

        }

    }
}
