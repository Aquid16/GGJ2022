using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] SpriteRenderer playerRenderer;

    PlayerActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerActions();

        inputActions.Gameplay.Swap.performed += ctx => Flip();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    void Flip()
    {
        Vector3 objScale = transform.localScale;
        objScale.y *= -1;
        transform.localScale = objScale;
        playerRenderer.color = objScale.y == 1 ? Color.black : Color.white;
        CameraController.instance.ChangePos();
    }

    void Jump()
    {

    }
}
