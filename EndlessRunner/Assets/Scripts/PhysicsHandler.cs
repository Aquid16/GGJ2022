using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsHandler : MonoBehaviour
{
    public static PhysicsHandler instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] LayerMask playerLayer;
    public LayerMask groundLayer;

    private void Start()
    {
        Physics2D.gravity = -9.81f * Vector2.up;
    }

    public void FlipGravity()
    {
        Vector2 prevGravity = Physics2D.gravity;
        prevGravity.y *= -1;
        Physics2D.gravity = prevGravity;
    }

    public void TogglePlayerGroundCollision(bool collide)
    {
        Physics2D.IgnoreLayerCollision((int)Mathf.Log(playerLayer.value, 2), (int)Mathf.Log(groundLayer.value, 2), !collide);
    }
}
