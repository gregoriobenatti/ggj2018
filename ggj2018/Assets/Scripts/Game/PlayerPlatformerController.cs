using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    public GameObject spawn_point;
    
    private bool hit_enemy = false;
    public Transform hit_Check;
    private float hit_Radius = 0.2f;
    public LayerMask is_hitting_enemy_layer;

    private SpriteRenderer spriteRenderer;
    // private Animator animator;

    // Use this for initialization
    void Awake () 
    {
        spriteRenderer = GetComponent<SpriteRenderer> (); 
        // animator = GetComponent<Animator> ();
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log(other);
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis ("Horizontal");

        if (Input.GetButtonDown ("Jump") && grounded) {
            velocity.y = jumpTakeOffSpeed;
        } else if (Input.GetButtonUp ("Jump")) 
        {
            if (velocity.y > 0) {
                velocity.y = velocity.y * 0.5f;
            }
        }

        if(move.x > 0.01f)
        {
            if(spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }
        } 
        else if (move.x < -0.01f)
        {
            if(spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
            }
        }

        // animator.SetBool ("grounded", grounded);
        // animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;

        hit_enemy = Physics2D.OverlapCircle(hit_Check.position, hit_Radius, is_hitting_enemy_layer);
        
        if(hit_enemy)
		{
            Debug.Log("--- DEAD ---------");
            this.transform.position = spawn_point.transform.position;
		}
    }
}