using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class FrogController : MonoBehaviour {
    public float JumpMaxInterval;
    public float JumpMinInterval;
    public float JumpForce;
    public float JumpXSpeed;
    public float XThreshold;

    private float _currentInterval;


    private Transform _playerTransform;
    private Transform _frogTransform;
    private Rigidbody2D _frogRigidbody;
    private SpriteRenderer _frogRenderer;
    private Animator _frogAnimator;
    private bool _grounded;
    private int _direction;

    void Start () {
        XThreshold = 0.5f;
        _direction = 1;
        JumpForce = 4.5f;
        JumpXSpeed = 3.5f;
        JumpMaxInterval = 4;
        JumpMinInterval = 2.5f;
        _grounded = true;

        _currentInterval = UnityEngine.Random.Range(JumpMaxInterval, JumpMinInterval);

        var frogSpawner = this.transform.parent.GetComponent<FrogSpawner>();
        var player = frogSpawner.PlayerReference;

        _playerTransform = player.GetComponent<Transform>();
        _frogTransform = GetComponent<Transform>();
        _frogRenderer = GetComponent<SpriteRenderer>();
        _frogRigidbody = GetComponent<Rigidbody2D>();
        _frogAnimator = GetComponent<Animator>();
    }

    void Update () {
        DetermineDirection();
        SetAnimationDirection();

        if (_frogAnimator.GetBool("dead")) {
            _frogRigidbody.velocity = new Vector2(0, 0);
        }

        _currentInterval -= Time.deltaTime;
        if (_currentInterval <= 0.01f) {
            Debug.Log("Ready to jump!");
            Jump();
            _currentInterval = UnityEngine.Random.Range(JumpMinInterval, JumpMaxInterval);

        } else if (_frogRigidbody.velocity.y < 0) {
            _frogAnimator.SetBool("jump", false);
            _frogAnimator.SetBool("fall", true);
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name == "Tilemap") {
            _frogAnimator.SetBool("jump", false);
            _frogAnimator.SetBool("fall", false);
        } else if (col.gameObject.name == "Player") {
            if (IsAttackFromAbove(col.rigidbody)) {
                Debug.Log("I am attacked from above!");
                _frogAnimator.SetBool("dead", true);
            } else {
                var playerController = col.gameObject.GetComponent<PlayerController>();
                playerController.GotHit(15);
            }
        }
    }

    private bool IsAttackFromAbove (Rigidbody2D col) {
        // porównać środek żaby (pozycja Y) ze środkiem kolidera (też pozycja Y)
        var yIsOk = _frogRigidbody.position.y < col.position.y;
        var xDistance = Math.Abs(_frogRigidbody.position.x - col.position.x);
        var xIsOk = xDistance <= XThreshold;

        return yIsOk && xIsOk;
    }

    private void Jump () {
        _frogAnimator.SetBool("jump", true);
        _frogAnimator.SetBool("fall", false);

        var currentXSpeed = JumpXSpeed;
        currentXSpeed *= -_direction;

        _frogRigidbody.velocity = new Vector2(_frogRigidbody.velocity.x + currentXSpeed, _frogRigidbody.velocity.y + JumpForce);
    }

    private void DetermineDirection() {
        if (_frogTransform.position.x < _playerTransform.position.x) {
            _direction = -1;
        } else {
            _direction = 1;
        }
    }

    private void SetAnimationDirection () {_frogRenderer.flipX = _direction < 0;}
    public void Die () {Destroy(this.gameObject);}
}
