using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancerEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //GAME OVER
            GameManager.instance.gameSpeed = 0;
            PlayerController.instance.Die();
        }
    }

    private void OnEnable()
    {
        Invoke("Deactivate", 5f);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
