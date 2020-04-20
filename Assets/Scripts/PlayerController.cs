using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public const float runSpeed = 3, jumpSpeed = 13;
    public bool isGrounded = false, dir = true, doorOpen = false;
    public static int score = 0;

    public Animator anim;
    public GameObject Exit, Message;

    void Update()
    {
        // Getting Keyboard Input
        float h = Input.GetAxis("Horizontal");

        // If we are on the ground, are we running or not?
        if (isGrounded)
        {
            if (h == 0)
            {
                anim.SetBool("isRunning", false);
            }
            else
            {
                if ((h < 0 && dir) || (h > 0 && !dir))
                {
                    GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                    dir = !dir;
                }
                anim.SetBool("isRunning", true);
            }
        }

        // Run
        GetComponent<Transform>().position += new Vector3(h * runSpeed * Time.deltaTime, 0.0f, 0.0f);

        // Jump
        if (isGrounded && Input.GetButton("Jump"))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector3(0.0f, jumpSpeed, 0.0f), ForceMode2D.Impulse);
            isGrounded = false;
            anim.SetBool("isGrounded", false);
        }

        // To save the character from rotating over.
        if (GetComponent<Transform>().rotation.eulerAngles.z != 0)
        {
            GetComponent<Transform>().Rotate(new Vector3(0, 0, -GetComponent<Transform>().rotation.eulerAngles.z));
        }

        // Move the camera with the player.

        Camera.main.GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x + 4, 0, -10);
        
        // To avoid going off the boundaries.
        if (Camera.main.GetComponent<Transform>().position.x < -5)
            Camera.main.GetComponent<Transform>().position = new Vector3(-5, 0, -10);

        if (Camera.main.GetComponent<Transform>().position.x > 25)
            Camera.main.GetComponent<Transform>().position = new Vector3(25, 0, -10);
    }
    private void OnCollisionEnter2D(Collision2D Coll)
    {
        if (Coll.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isGrounded", true);
        }
        if (Coll.collider.CompareTag("Enemy"))
        {
            anim.Play("die", 0);
            Invoke(nameof(Respawn), 0.5f);
        }
    }
    private void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.gameObject.CompareTag("Coin"))
        {
            Destroy(Coll.gameObject);
            score++;
        }
        else if (Coll.gameObject.CompareTag("Key"))
        {
            Destroy(Coll.gameObject);
            doorOpen = true;
            Exit.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/doorOpen");
        }
        else if (Coll.gameObject.CompareTag("Exit"))
        {
            if (doorOpen)
            {
                Message.GetComponent<Text>().enabled = true;
                Application.Quit();
            }
        }
        else if (Coll.gameObject.CompareTag("MainCamera"))
        { // Dead-Z
            Respawn();
        }
    }
    private void Respawn()
    {
        GetComponent<Transform>().position = new Vector3(-8, 0, 0);
        anim.Rebind();
    }
}