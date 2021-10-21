using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playercontroller : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    private bool facingRight = true;
    private bool isJumping;

    public Text score;
    public Text winText;
    public Text livesText;

    private int scoreValue = 0;
    private int lives;
    

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    Animator anim;

    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        score.text = scoreValue.ToString();
        lives = 3;

        musicSource.clip = musicClipOne;
        musicSource.Play();

        winText.text = "";
        livesText.text = "";
    }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

         if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

         if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

         if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

        if (facingRight == false && hozMovement > 0)
         {
            Flip();
         }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if (scoreValue == 4)
            {
             transform.position = new Vector2(50.0f, 0.0f);
             lives = 3;
            }      
        }

        if (scoreValue == 9)
        {
            scoreValue += 1;
            musicSource.Stop();
            winText.text = "You Win! Game made by Angela Page";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }

        else if (collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
            Destroy(collision.collider.gameObject);
        }

        livesText.text = "Lives: " + lives.ToString();
        if (lives <=0)
        {
            winText.text = "You lose!";
        }

        if (lives == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {  
            if (isJumping)
            {
                isJumping = false;
                anim.SetBool("Jump", isJumping);
            }

            if (Input.GetKeyDown(KeyCode.W))
                {
                   rd2d.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
                   isJumping = true;
                   anim.SetBool("Jump", isJumping);
                }     
        }
    }

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
}