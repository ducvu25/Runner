using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("---------Setting information--------\n")]
    [SerializeField] float jumpFocre = 10f;
    [SerializeField] Transform pointCheckGround;
    [SerializeField] float pointCheckGroundRadius;
    [SerializeField] float timeJump = 1f;
    float m_timeJump;

    [Header("\n---------Color-----------")]
    [SerializeField] Color32 clNormalize;
    [SerializeField] Color32 clJump;
    [SerializeField] Color32 clSmall;

    bool isJumping;

    [SerializeField] float size;
    float m_size;

    Rigidbody2D rb;

    [Header("\n----------Layer Check---------\n")]
    [SerializeField] LayerMask lmGround;
    bool isTouchGround;

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.onPlay.AddListener(ActivatePlayer);
        m_size = transform.localScale.y;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.Instance.isPlaying) return;
        CheckCollison();
        CheckInput();
    }
    
    void CheckCollison()
    {
        isTouchGround = Physics2D.OverlapCircle(pointCheckGround.position, pointCheckGroundRadius, lmGround);
        if(isTouchGround)
            spriteRenderer.color = clNormalize;
        else
            spriteRenderer.color = clJump;
    }
    void CheckInput()
    {
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isTouchGround)
        {
            Jump();
        }

        if(isJumping && Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            if (m_timeJump > 0)
            {
                rb.AddForce(Vector2.up * jumpFocre*2 / (1 + m_timeJump / timeJump));
                //rb.AddForce(Vector2.up * jumpFocre/(1 + m_timeJump/timeJump));
                m_timeJump -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumping = false;
        }
        
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.GetChild(0).localScale = new Vector3(transform.localScale.x, size, transform.localScale.z);
            spriteRenderer.color = clSmall;
        }
        else
        {
            transform.GetChild(0).localScale = new Vector3(transform.localScale.x, m_size, transform.localScale.z);
        }
    }
    void Jump()
    {
        isJumping = true;
        m_timeJump = timeJump;
        rb.velocity += Vector2.up * jumpFocre;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointCheckGround.position, pointCheckGroundRadius);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            GameController.Instance.EndGame();
        }
    }
    void ActivatePlayer()
    {
        gameObject.SetActive(true);
    }
}
