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

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.y > 0)
        {
            Animator.Play("CharUp");

            _currentDirection = "up";
        }
        else if (movement.y < 0)
        {
            Animator.Play("CharDown");

            _currentDirection = "down";
        }
        
        if (movement.x < 0 && movement.y == 0) 
        {
            _renderer.flipX = false;
            Animator.Play("CharSide");

            _currentDirection = "left";
        }
        else if (movement.x > 0 && movement.y == 0)
        {
            _renderer.flipX = true;
            Animator.Play("CharSide");

            _currentDirection = "right";
        }

        if (movement.x == 0 && movement.y == 0)
        {
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
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        heroPosition = transform.position;
    }
}
