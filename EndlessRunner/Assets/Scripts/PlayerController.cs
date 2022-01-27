using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] SpriteRenderer playerRenderer;
    [SerializeField] float jumpForce = 8f;

    public float flipDuration = 0.5f;

    int side = 1;
    Rigidbody2D playerRB;
    PlayerActions inputActions;
    CharacterState state;

    private void Awake()
    {
        instance = this;
        inputActions = new PlayerActions();

        inputActions.Gameplay.Swap.performed += ctx => FlipInput();
        inputActions.Gameplay.Jump.performed += ctx => JumpInput();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Start()
    {
        state = new RunningState();
        playerRB = GetComponent<Rigidbody2D>();
    }

    void FlipInput()
    {
        state = state.HandleInput(this, StateTransition.ToFlipping);
    }

    void JumpInput()
    {
        state = state.HandleInput(this, StateTransition.ToJumping);
    }

    public void FlipAction()
    {
        side *= -1;
        float scaleY = transform.localScale.y * side;
        Sequence flipSequence = DOTween.Sequence();
        flipSequence.Append(transform.DOScaleY(scaleY, flipDuration))
            .OnComplete(() => HandleFlipEnd());
        flipSequence.Play();
        playerRenderer.color = scaleY == 1 ? Color.black : Color.white;
        CameraController.instance.ChangePos();
    }

    private void HandleFlipEnd()
    {
        state = state.HandleInput(this, StateTransition.ToRunning);
        Vector2 prevGravity = Physics2D.gravity;
        prevGravity.y *= -1;
        Physics2D.gravity = prevGravity;
    }

    private void Update()
    {
        Debug.Log(state);
    }

    public void Jump()
    {
        playerRB.velocity = Vector2.up * jumpForce * side;
    }
}
