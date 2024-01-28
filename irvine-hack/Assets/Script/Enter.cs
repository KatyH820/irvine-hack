using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enter : MonoBehaviour
{
    public bool enterAllowed;
    public string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {

    }   

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Wassup");
        if (collision.GetComponent<ScienceDoors>())
        {
            sceneToLoad = "ScienceLibraryInterior";
            enterAllowed = true;
        }
        {
            enterAllowed = false;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<ScienceDoors>())
        {
            enterAllowed = false;
        }
    }

    public void Update(){
        Debug.Log(enterAllowed);
        
        if (enterAllowed && Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
