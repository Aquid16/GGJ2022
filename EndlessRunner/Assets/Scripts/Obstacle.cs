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
            GameManager.instance.gameSpeed = 0;
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
}
