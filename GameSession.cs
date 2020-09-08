using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameSession: MonoBehaviour
{
    [SerializeField] float numberOfLives = 3f;
    [SerializeField] int coins = 0;
    [SerializeField] Text numberOfLivesRemaining;
    [SerializeField] Text numberOfCoinsCurrentlyOwned;


    private void Awake()
    {
        //We check for the number of GameSessions that are created
        var numberOfGS = FindObjectsOfType<GameSession>().Length;

        //If there's already a session then I don't want another session to be created
        if(numberOfGS > 1)
        {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }

    }

    public void managePlayerDeath()
    {
        if(numberOfLives > 1)
        {
            TakeLife();
        } else {
            RestartSession();
        }
    }

    private void RestartSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLife()
    {
        numberOfLives -= 1;
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
        numberOfLivesRemaining.text = numberOfLives.ToString();
    }

    public void AddCoins(int coinsToAdd)
    {
        coins++;
        numberOfCoinsCurrentlyOwned.text = coins.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        numberOfLivesRemaining.text = numberOfLives.ToString();
        numberOfCoinsCurrentlyOwned.text = coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
