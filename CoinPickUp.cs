using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSoundEffect;
    [SerializeField] int coinsValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinPickUpSoundEffect, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddCoins(coinsValue);
        Destroy(gameObject);
    }
}
