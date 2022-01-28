using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Sprite Management")]
    [SerializeField] SpriteRenderer playerRenderer;
    [SerializeField] Sprite angelSprite;
    [SerializeField] Sprite demonSprite;
    [Space]
    [Header("Jumping")]
    [SerializeField] float jumpForce = 8f;
    [SerializeField][Min(1)]  float fallMultiplier = 2.5f;
    [SerializeField][Min(1)] float lowJumpMultiplier = 2f;

    public float flipDuration = 0.5f;

    bool isJumpHeld;
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
        inputActions.Gameplay.Jump.canceled += ctx => isJumpHeld = false;

        inputActions.Gameplay.Pause.performed += ctx => GameManager.instance.TogglePause();
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
        if (GameManager.instance.firstInput)
        {
            GameManager.instance.StartGame();
        }
        isJumpHeld = true;
        state = state.HandleInput(this, StateTransition.ToJumping);
    }

    public void FlipAction()
    {
        playerRB.bodyType = RigidbodyType2D.Kinematic;
        PhysicsHandler.instance.TogglePlayerGroundCollision(false);
        side *= -1;
        Sequence flipSequence = DOTween.Sequence();
        flipSequence.Append(transform.DOScaleY(side, flipDuration))
            .OnComplete(() => HandleFlipEnd());
        flipSequence.Play();
        SFXPlayer.instance.PlaySFX(side == 1 ? SFXType.SwapToHeaven : SFXType.SwapToHell);
        playerRenderer.sprite = side == 1 ? angelSprite : demonSprite;
        CameraController.instance.ChangePos();
    }

    private void HandleFlipEnd()
    {
        state = state.HandleInput(this, StateTransition.ToRunning);
        PhysicsHandler.instance.FlipGravity();
        playerRB.bodyType = RigidbodyType2D.Dynamic;
        PhysicsHandler.instance.TogglePlayerGroundCollision(true);
    }

    private void Update()
    {
        HandleJumpGravity();
    }

    private void HandleJumpGravity()
    {
        if (playerRB.velocity.y * side < 0)
        {
            playerRB.velocity += Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (playerRB.velocity.y * side > 0 && !isJumpHeld)
        {
            playerRB.velocity += Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public void Jump()
    {
        playerRB.velocity = Vector2.up * jumpForce * side;
        SFXPlayer.instance.PlaySFX(SFXType.Jump);
        StartCoroutine(CheckForGround());
    }

    IEnumerator CheckForGround()
    {
        yield return new WaitForSeconds(0.1f);

        while (!Grounded())
        {
            yield return null;
        }

        state = state.HandleInput(this, StateTransition.ToRunning);
    }

    bool Grounded()
    {
        return Physics2D.Raycast(transform.position, -Vector2.up * side, 0.1f, PhysicsHandler.instance.groundLayer);
    }

    internal bool IsFalling()
    {
        return playerRB.velocity.y * side < 0;
    }

    public void Die()
    {
        SFXPlayer.instance.PlaySFX(SFXType.Death);
        inputActions.Disable();
        //animations......
        playerRB.bodyType = RigidbodyType2D.Kinematic;
        UIManager.instance.DisplayDeathScreen(1f);
        ObstacleGenerator.instance.StopSpawning();
    }

    public int GetSide()
    {
        return side;
    }
}
