using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerBody;
    public LayerMask groundLayer;
    public Transform groundChecker;
    public GameObject HealthBar;
    private SliderController _healthBarController;

    public float playerSpeed;
    public float verticalPlayerSpeed = 5;
    public float jumpForce;
    public float fallMultiplier;
    public float lowJumpFallMultiplier;
    public float groundCheckRange;
    public int Health;
    public bool IsHurt;
    bool jumpPressed;
    public bool enableShortJump = true;
    float horizontalMovement;

    private Animator _animator;
    private SpriteRenderer _renderer;
    private bool groundedMemory = true;

    void Start () {
        fallMultiplier = 1.2f;
        lowJumpFallMultiplier = 1.1f;
        playerSpeed = 5f;
        verticalPlayerSpeed = 5f;
        groundCheckRange = 0.1f;

        Health = 100;
        IsHurt = false;

        _renderer = GetComponentInParent<SpriteRenderer>();
        _animator = GetComponentInParent<Animator>();

        _healthBarController = HealthBar.GetComponent<SliderController>();
        _healthBarController.Set(Health);
    }

    void Update () {
        CheckInput();
        BetterFall();
        MovePlayer();
    }

    void CheckInput () {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        jumpPressed = Input.GetKeyDown(KeyCode.Space);
        SetAnimation();
    }

    public void GotHit (int damage){
        Health -= Math.Abs(damage);
        IsHurt = true;
        _healthBarController.Set(Health);
        _animator.SetBool("hurt", true);
    }

    void MovePlayer () {
        var yMovement = playerBody.velocity.y;

        if (jumpPressed)  {
            // Debug.Log("jump pressed!");
            yMovement += jumpForce;
            _animator.SetBool("jumping", true);
            // Debug.Log("Setting jumping to true");
        }

        if (!groundedMemory && IsGrounded()) {
            _animator.SetBool("jumping", false);
            Debug.Log("Setting jumping to false");
        }

        _animator.SetBool("grounded", IsGrounded());
        _animator.SetFloat("vertical_speed", yMovement);
        _animator.SetFloat("speed", Math.Abs(horizontalMovement));

        var playerPosition = horizontalMovement * playerSpeed;
        playerBody.velocity = new Vector2(playerPosition, yMovement);

        groundedMemory = IsGrounded();
    }

    public void IsClimbing (bool isClimbing) {
        _animator.SetBool("climbing", isClimbing);
        Debug.Log($"setting climbing flag to {isClimbing}");
    }


    void BetterFall () {
        if (playerBody.velocity.y < 0 && enableShortJump) {
            playerBody.velocity += Vector2.up * Physics2D.gravity * fallMultiplier * Time.deltaTime;
        } else if (playerBody.velocity.y > 0 && !jumpPressed) {
            // player leci do góry, ale spacja nie jest wciśnięta
            playerBody.velocity += Vector2.up * Physics2D.gravity * lowJumpFallMultiplier * Time.deltaTime;
        }
    }
    void OnCollisionExit2D(Collision2D col) {
        if(col.gameObject.name == "Frog"){
            IsHurt = false;
            _animator.SetBool("hurt", false);
        }
    }
    private void SetAnimation () {
        _renderer.flipX = horizontalMovement < 0;
        _animator.SetFloat("speed", Math.Abs(horizontalMovement));
    }
    private bool IsGrounded() => Physics2D.OverlapCircle(groundChecker.position, groundCheckRange, groundLayer) != null;
}
