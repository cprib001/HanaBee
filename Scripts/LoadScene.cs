using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour {

    // Use this for initialization
    public bool ChangeAfterDelay = false;
    IEnumerator ChangeS()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(scene);
    }
    void Start()
    {
        if (ChangeAfterDelay == true)
        {
            StartCoroutine(ChangeS());
        }
    }

    public string scene;
    public void ChangeToScene(string scene)
    { //function to change scenes/levels.
        SceneManager.LoadScene(scene);         //
    }
    // Update is called once per frame
    public void Quit()
    {
        Application.Quit();
    }
    void Update()
    {

    }
}
