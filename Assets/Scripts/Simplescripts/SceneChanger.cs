using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
   
    // Start is called before the first frame update
    public void ChangeScene(string scenename)
    {
        Debug.Log("sceneName to load: " + scenename);
        UnityEngine.SceneManagement.SceneManager.LoadScene(scenename); ;
    }
}
