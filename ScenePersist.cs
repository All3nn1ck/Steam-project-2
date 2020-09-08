using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    int sceneIndex;

    private void Awake()
    {
        int numberOfSP = FindObjectsOfType<ScenePersist>().Length;

        if(numberOfSP > 1)
        {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if (sceneIndex != SceneManager.GetActiveScene().buildIndex)
        {
            Destroy(gameObject);
        }
    }
}
