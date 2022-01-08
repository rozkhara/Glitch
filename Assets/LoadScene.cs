using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadSceneA(string name)
    {
        SceneManager.LoadScene(name);
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
