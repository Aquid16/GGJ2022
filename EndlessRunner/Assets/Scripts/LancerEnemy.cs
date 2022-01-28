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
        StartCoroutine(DeactivateAfterTime());
    }

    IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}
