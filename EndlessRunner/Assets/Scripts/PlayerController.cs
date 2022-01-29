using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Sprite Management")]
    [SerializeField] SpriteRenderer angelObject;
    [SerializeField] SpriteRenderer demonObject;
    public float flipDuration = 0.5f;
    [Space]
    [Header("Jumping")]
    [SerializeField] float jumpForce = 8f;
    [SerializeField][Min(1)]  float fallMultiplier = 2.5f;
    [SerializeField][Min(1)] float lowJumpMultiplier = 2f;
    [Space]

    [SerializeField] ParticleSystem deathParticles;

    bool isJumpHeld;
    int side = 1;
    Rigidbody2D playerRB;
    PlayerActions inputActions;
    CharacterState state;
    SpriteRenderer activePlayerSpriteObject;

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
        activePlayerSpriteObject = angelObject;
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
        flipSequence.Append(transform.DOScaleY(0, flipDuration / 2f))
            .OnComplete(() => FlipHalfWay());
        flipSequence.Play();
        SFXPlayer.instance.PlaySFX(side == 1 ? SFXType.SwapToHeaven : SFXType.SwapToHell);
        CameraController.instance.ChangePos();
    }

    void FlipHalfWay()
    {
        angelObject.gameObject.SetActive(side == 1);
        demonObject.gameObject.SetActive(side == -1);
        activePlayerSpriteObject = side == 1 ? angelObject : demonObject;
        Sequence flipHalfSequence = DOTween.Sequence();
        flipHalfSequence.Append(transform.DOScaleY(side, flipDuration / 2f))
            .OnComplete(() => HandleFlipEnd());
        flipHalfSequence.Play();
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

        ParticleSystem.MainModule mainModule = deathParticles.main;
        mainModule.startColor = side == 1 ? Color.black : Color.white;
        deathParticles.Play();

        float dissolveStep = 0;
        Sequence dissolve = DOTween.Sequence();
        dissolve.Append(DOTween.To(() => dissolveStep, x => dissolveStep = x, 1, 2))
            .OnUpdate(() => activePlayerSpriteObject.material.SetFloat("_StepAmount", dissolveStep));
        dissolve.Play();
    }

    public int GetSide()
    {
        return side;
    }
}
