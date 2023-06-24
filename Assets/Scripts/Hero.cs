using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] float moveSpeed = 6f;
    public Animator Animator;
    [SerializeField] SpriteRenderer _renderer;
    
    Rigidbody2D rb;
    Vector2 movement;
    public Vector2 heroPosition;
    private string _currentDirection = "left";
    bool _isPlaying = false;

    public bool StopReceivingInput { get; set; } = false;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (StopReceivingInput)
        {
            return;
        }
        else
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.y > 0)
        {
            Animator.Play("CharUp");

            _currentDirection = "up";

            _isPlaying = true;
        }
        else if (movement.y < 0)
        {
            Animator.Play("CharDown");

            _currentDirection = "down";

            _isPlaying = true;
        }
        
        if (movement.x < 0 && movement.y == 0) 
        {
            _renderer.flipX = false;
            Animator.Play("CharSide");

            _currentDirection = "left";

            _isPlaying = true;
        }
        else if (movement.x > 0 && movement.y == 0)
        {
            _renderer.flipX = true;
            Animator.Play("CharSide");

            _currentDirection = "right";

            _isPlaying = true;
        }

        if (movement.x == 0 && movement.y == 0)
        {
            _isPlaying = false;
            
            switch (_currentDirection)
            {
                default: Animator.Play("CharSideIdle");
                break;
                case "up": Animator.Play("CharUpIdle");
                break;
                case "down": Animator.Play("CharDownIdle");
                break;
            }
        }

        if (_isPlaying)
        {
            SoundManager.Instance.PlayFootsteps(SoundManager.Instance.Effects[0]);
        }
        else
        {
            SoundManager.Instance.StopFootsteps();
        }
    }

    void FixedUpdate()
    {
        if (StopReceivingInput)
        {
            return;
        }
        
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        heroPosition = transform.position;
    }
}
