using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerPlatformerController : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    public GameObject spawn_point;
    
    private bool hit_enemy = false;
    private bool hit_letter = false;
    private int letter_count = 0;
    private int MAX_PIECES = 7;

    public Transform hit_Check;
    public GameObject text_find_letter;

    private float hit_Radius = 0.2f;
    public LayerMask is_hitting_letter_layer;
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
        hit_letter = Physics2D.OverlapCircle(hit_Check.position, hit_Radius, is_hitting_letter_layer);
        
        if (hit_enemy)
		{
            Debug.Log("Morreu");
            this.transform.position = spawn_point.transform.position;
		}

        if (hit_letter)
        {            
            letter_count += 1;

            if (letter_count >= MAX_PIECES)
            {
                SceneManager.LoadScene(3); // game_win
            }

            Debug.Log("Achou pedaço! ");
            Debug.Log(letter_count);
        }

        text_find_letter.GetComponent<Text>().text = letter_count + " of 5";
    }
}