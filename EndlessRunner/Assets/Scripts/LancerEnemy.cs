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
        StartCoroutine(CheckForPlayer());
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
        if (!GameManager.instance.passedTutorial)
        {
            GameManager.instance.IncrementTutorialCount();
        }
        else
        {
            ScoreHandler.instance.UpdateScore(1);
        }
    }
}
