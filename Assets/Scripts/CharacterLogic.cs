using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Lane
{
    LEFT,
    CENTRE,
    RIGHT,
}

public class CharacterLogic : MonoBehaviour
{
    public AudioClip runSound;
    public AudioClip slideSound;
    public AudioClip jumpSound;
    public AudioClip collectSound;
    public AudioClip collideSound;
    public Director director;
    AudioSource audioPlayer;
    public float speed = 6;
    public float nudgeAmount = 0.2f;
    Transform playerTransform;
    CapsuleCollider collider;
    float colliderY;
    float colliderHeight;
    int colliderHeightHash;
    int colliderYHash;
    int jumpTriggerHash;
    int slideTriggerHash;
    int jumpState;
    int runState;
    int slideState;
    bool jumping;
    bool sliding;
    Lane currentLane;
    // Start is called before the first frame update
    void Start()
    {
        currentLane = Lane.CENTRE;
        audioPlayer = this.gameObject.GetComponent<AudioSource>();
        collider = this.gameObject.GetComponent<CapsuleCollider>();
        colliderY = collider.center.y;
        colliderHeight = collider.height;
        playerTransform = this.gameObject.transform;
    }

    void OnEnable()
    {
        jumping = false;
        sliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        // fallen of the edge
        if (playerTransform.position.y < -1.5f)
        {
            director.ObstacleCollision(this.gameObject);
        }
    }

    // Jump is over can perform next jump
    void JumpOver()
    {
        collider.center.Set(collider.center.x, colliderY, collider.center.z);
        jumping = false;
    }

    void SlideOver()
    {
        collider.height = colliderHeight;
        collider.center.Set(collider.center.x, colliderY, collider.center.z);
        sliding = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            PlayOneShot(collectSound);
            other.gameObject.SetActive(false);
            director.CollectCoin();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            PlayOneShot(collideSound);
            director.ObstacleCollision(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Path End"))
        {
            director.ClearBlock();
        }
    }

    void PlayOneShot(AudioClip clip, float delay = 0f)
    {
        audioPlayer.Stop();
        audioPlayer.clip = clip;
        audioPlayer.loop = false;
        audioPlayer.PlayDelayed(delay);
    }
}
