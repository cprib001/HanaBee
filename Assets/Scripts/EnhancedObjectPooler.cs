using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancedObjectPooler : MonoBehaviour
{

    public List<GameObject> objectsToPool;
    public Canvas theCanvas;

    List<GameObject> pooledObjects = new List<GameObject>();
    Dictionary<string, GameObject> theDictionary = new Dictionary<string, GameObject>();

    private static EnhancedObjectPooler instance;
    public static EnhancedObjectPooler Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < objectsToPool.Count; i++)
        {
            theDictionary.Add(objectsToPool[i].name, objectsToPool[i]);
            GameObject obj = (GameObject)Instantiate(objectsToPool[i]);
            obj.SetActive(false);
            obj.transform.SetParent(theCanvas.transform);
            obj.transform.localScale = new Vector3(1, 1, 1);
            pooledObjects.Add(obj);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetPooledObject(string name)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects != null && pooledObjects[i].name == name + "(Clone)")
            {
                return pooledObjects[i];
            }
        }
        //Debug.Log(name);
        GameObject obj = (GameObject)Instantiate(theDictionary[name]);
        obj.SetActive(false);
        obj.transform.SetParent(theCanvas.transform);
        obj.transform.localScale = new Vector3(1, 1, 1);
        pooledObjects.Add(obj);
        return obj;
    }
}
