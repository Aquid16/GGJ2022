using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
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

    private void Update()
    {
        transform.localPosition -= Time.deltaTime * Vector3.right * GameManager.instance.gameSpeed;
        if (transform.localPosition.x <= -12.5f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (GameManager.instance.passedTutorial)
        {
            StartCoroutine(CheckForPlayer());
        }
    }

    IEnumerator CheckForPlayer()
    {
        while (PlayerController.instance.transform.position.x < transform.position.x)
        {
            yield return null;
        }
        int pointToGive = (gameObject.tag.Contains("Giant") || gameObject.tag.Contains("Wall")) ?
            2 : 1;
        Debug.Log($"Give player {pointToGive} points!");
    }
}
