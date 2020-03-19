using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController :  MonoBehaviour
{
    enum State
    {
        Idle,Run,Jump,Falling,Sliding,WallJumping
    }
    [SerializeField]
    Rigidbody2D player_rb;
    [SerializeField]
    State state;
    [SerializeField]
    Animator player_animator;
    [SerializeField]
    Vector2 velocity;
    float gravity;
    float min_vel_y;
    float max_vel_y;
    float min_height = 1;
    float max_height = 2;
    float time_duration = 1;
    float unit = 4;
    float move_x;
    [SerializeField]
    Transform feetPos;
    [SerializeField]
    bool isGrounded;
    [SerializeField]
    bool isWallJumping;
    public bool CanSlide;
    [SerializeField]
    LayerMask whatIsGround;
    [SerializeField]
    Transform wallJumpDirection;
    float Speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        gravity = 2 * max_height * unit / Mathf.Pow(time_duration, 2);
        max_vel_y = Mathf.Sqrt(2 * gravity * max_height * unit);
        min_vel_y = Mathf.Sqrt(2 * gravity * min_height * unit);
        print(gravity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetState();
        CheckGround();
        CheckJump();
        if(move_x > 0)  transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        else if (move_x < 0) transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        
        switch (state)
        { 
            case State.Idle:
                Idle();
                break;
            case State.Run:
                Run();
                break;
            case State.Jump:
                Jump();
                break;
            case State.Falling:
                Fall();
                break;
            case State.Sliding:
                Sliding();
                break;
            case State.WallJumping:
                WallJumping();
                break;
        }
        player_rb.velocity = velocity;
    }
    void Fall()
    {
        player_animator.Play("Falling");
        velocity.x = move_x * unit;
        velocity.y -= gravity * Time.fixedDeltaTime;
    }
    void Jump()
    {
        player_animator.Play("Jump");
        velocity.x = move_x * unit;
        velocity.y -= gravity * Time.fixedDeltaTime;
    }
    void Sliding()
    {
        player_animator.Play("Sliding");
        velocity.x = move_x;
        velocity.y = -gravity / 2;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 dir = wallJumpDirection.localPosition;
            dir.Normalize();
            velocity = dir * min_vel_y * 2;
            state = State.WallJumping;
            isWallJumping = true;
        }
    }
    void Run()
    {
        player_animator.Play("Run");
        velocity.x = move_x * unit;
    }
    void Idle()
    {
        player_animator.Play("Idle");
        velocity.x = move_x * unit;
    }
    void WallJumping()
    {
        player_animator.Play("WallJumping");
        velocity.x = Mathf.Lerp(velocity.x, move_x, 0.1f);
    }
    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, 0.1f, whatIsGround);
        if (isGrounded)
        {
            isWallJumping = false;
            velocity.y = 0;
        }
        else if (!isGrounded && !CanSlide)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
    }
    void CheckJump()
    {
        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = max_vel_y;
        }
        else if(Input.GetKeyUp(KeyCode.Space) && velocity.y > min_vel_y){
            velocity.y = min_vel_y;
        }
    }
    void SetState()
    {
        move_x = Input.GetAxisRaw("Horizontal") * Speed;
        if (velocity.y > 0)
        {
            state = State.Jump;
        }
        /*else if (CanSlide && !isGrounded && move_x != 0)
        {
            state = State.Sliding;
            isWallJumping = false;
        }*/
        else if (velocity.y < 0 && !isGrounded)
        {
            state = State.Falling;
        }
        else if (isWallJumping)
        {
            state = State.WallJumping;
            CanSlide = false;
        }
        else if (move_x != 0 && isGrounded)
        {
            state = State.Run;
        }
        else if(isGrounded && move_x == 0)
        {
            state = State.Idle;
        }
        
    }
    
}
