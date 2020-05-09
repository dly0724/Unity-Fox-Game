using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerControler : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jump_move;
    private Animator anim;
    public LayerMask ground;
    public Collider2D coll;
    public int cherry;
    public Text CherryNum;
    private bool isHurt;
    public AudioSource jumpAudio,cherryAudio;


     
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isHurt)
        {
            move();
        }
        
        Switch();
    }

    void move()
    {
        float h_move;
        float face_dir;
        h_move = Input.GetAxis("Horizontal");
        face_dir = Input.GetAxisRaw("Horizontal");
    
        if(h_move != 0)
        {
            rb.velocity = new Vector2(h_move * speed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(face_dir));
        }

        if (face_dir != 0)
        {
            transform.localScale = new Vector3(face_dir,1,1);
        }

        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jump_move * Time.deltaTime);
            jumpAudio.Play();
            anim.SetBool("jumping", true);
        }
     }

    void Switch()
    {
        anim.SetBool("stay", false);
        if (rb.velocity.y < 0.1 && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("down", true);
        }
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("down", true);
            }
        }
        if (coll.IsTouchingLayers(ground))
        {
            if (anim.GetBool("down"))
            {
                anim.SetBool("down", false);
                anim.SetBool("stay", true);
            }
        }
        if (isHurt)
        {   
            anim.SetBool("hurt", true);
            anim.SetFloat("running", 0);
            if (Mathf.Abs(rb.velocity.x) < 0.1)
            {
                anim.SetBool("hurt", false);
                anim.SetBool("stay", true);
                isHurt = false;
            }
        }
       
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Collection")
        {
            cherryAudio.Play();
            Destroy(collision.gameObject);
            cherry += 1;
            CherryNum.text = cherry.ToString();
        }
        if (collision.gameObject.tag == "Enemy")
        {
            if (anim.GetBool("down"))
            {
                Destroy(collision.gameObject);
                rb.velocity = new Vector2(rb.velocity.x, jump_move * Time.deltaTime);
                anim.SetBool("jumping", true);
            }
            if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                isHurt = true;
            }
            if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                isHurt = true;
            }
        }

        if (collision.gameObject.tag == "DeadLine")
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("restart",1f);
        }
       
    }

    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
  
}
