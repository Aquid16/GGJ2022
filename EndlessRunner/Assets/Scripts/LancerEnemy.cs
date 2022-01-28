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
            GameManager.instance.StopGame();
            PlayerController.instance.Die();
        }
    }

    private void OnEnable()
    {
        if (GameManager.instance.passedTutorial)
        {
            StartCoroutine(CheckForPlayer());
        }
        StartCoroutine(DeactivateAfterTime());
    }

    IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }

    IEnumerator CheckForPlayer()
    {
        while (PlayerController.instance.transform.position.x < transform.position.x)
        {
            yield return null;
        }
        int pointToGive = 1;
        Debug.Log($"Give player {pointToGive} points!");
    }
}
