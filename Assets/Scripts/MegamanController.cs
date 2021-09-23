using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegamanController : MonoBehaviour
{
    // public properties
    public float velocityX = 15;
    public float jumpForce = 5;

    public GameObject rightBullet;
    public GameObject leftBullet;

    

    // private components
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator animator;
    private GameController game;
    
    
    // private properties
    private bool isIntangible = false;
    private float intangibleTime = 0f;
    
    // constants
    private const int ANIMATION_IDLE = 0;
    private const int ANIMATION_RUN = 1;
    private const int ANIMATION_JUMP= 2;
    private const int ANIMATION_RUNSHOT= 3;

    private const int LAYER_GROUND = 10;

    private const string TAG_ENEMY = "Enemy";

    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Iniciando Game Object");
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        game = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        changeAnimation(ANIMATION_IDLE);
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocityX, rb.velocity.y); 
            sr.flipX = false;
            changeAnimation(ANIMATION_RUN);
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocityX, rb.velocity.y);
            sr.flipX = true;
            changeAnimation(ANIMATION_RUN);
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            var bullet = sr.flipX ? leftBullet : rightBullet;
            var position = new Vector2(transform.position.x, transform.position.y);
            var rotation = rightBullet.transform.rotation;
            Instantiate(bullet, position, rotation);
        }
        
        if (Input.GetKey(KeyCode.X))
        {
            changeAnimation(ANIMATION_RUNSHOT);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // salta
            changeAnimation(ANIMATION_JUMP); // saltar
        }

        if (isIntangible && intangibleTime < 2f)
        {
            intangibleTime += Time.deltaTime;
            sr.enabled = !sr.enabled;
        }
        else if(isIntangible && intangibleTime >= 2f)
        {
            isIntangible = false;
            sr.enabled = true;
            intangibleTime = 0f;
            // Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LAYER_GROUND && collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Collision: " + collision.gameObject.name);
        }

       if (collision.gameObject.CompareTag(TAG_ENEMY) && !isIntangible)
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 1);
            isIntangible = true;
//            game.LoseLife();
             Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
        }

        if (collision.gameObject.name == "Vida")
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 1);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
       Debug.Log("Trigger:" + this.name);
    }


    private void changeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }
}
