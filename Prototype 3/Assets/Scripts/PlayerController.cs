using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody body;
    public float jumpIntensity;
    public float gravityIntensity;
    public bool isOnGround = true;
    public bool gameOver = false;
    public int numberOfJumps;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public MoveLeft moveLeftScript;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        Physics.gravity *= gravityIntensity;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        body.useGravity = false;
        body.isKinematic = false;
    }

    private void Update()
    {
        if(transform.position.x < 5){
            transform.Translate(Vector3.forward * 10 * Time.deltaTime);    
        }
        else
        {
            body.useGravity = true;
        }
        if(Input.GetKeyDown(KeyCode.Space) && (isOnGround || numberOfJumps<2) && !gameOver){
            body.AddForce(Vector3.up * jumpIntensity, ForceMode.Impulse);
            isOnGround = false;
            numberOfJumps++;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound,1.0f);
        }
        if(Input.GetKeyDown(KeyCode.D) && !gameOver && isOnGround)
        {
            dash();
        }
        if(Input.GetKeyUp(KeyCode.D))
        {
            walk();
        }        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            numberOfJumps = 0;
            dirtParticle.Play();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b",true);
            playerAnim.SetInteger("DeathType_int",1);
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound,1.0f);
            dirtParticle.Stop();
        }
    }

    private void dash()
    {
        Debug.Log("I'm dashing");
        moveLeftScript.speed *= 2;
        playerAnim.SetFloat("Speed_f",1.5f);
    }

    private void walk()
    {
        Debug.Log("I'm walking");
        moveLeftScript.speed /= 2;
        playerAnim.SetFloat("Speed_f",1f);
    }
}
