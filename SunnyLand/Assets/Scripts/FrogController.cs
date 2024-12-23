using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D col;
    Animator ani;
    [SerializeField] LayerMask ground;


    float timeRest = 2.0f;//thời gian giữa các lần nhảy
    float m_timeRest;

    public float jumpForce = 5f; // Lực nhảy
    public float moveSpeed = 5f; // Tốc độ di chuyển

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        ani = GetComponent<Animator>();
        m_timeRest = timeRest;
    }

    void Update()
    {
        Movement();
    }
    void Movement()
    {

        if (col.IsTouchingLayers(ground))
        {
            m_timeRest -= Time.deltaTime;
            // Nhảy lên và di chuyển
            if (m_timeRest <= 0)
            {
                rb.velocity = new Vector2(moveSpeed, jumpForce);
                m_timeRest = timeRest;
                if(moveSpeed > 0)
                {
                    transform.rotation=Quaternion.Euler(0,180,0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                moveSpeed = -moveSpeed;
            }
            ani.SetInteger("state", 0);
        }
        else
        {
            ani.SetInteger("state", 1);
            if (rb.velocity.y < .1f)
            {
                ani.SetInteger("state", 2);
            }
        }
        //set vận tốc trục x=0 để frog ko di chuyển theo quán tính
        if (col.IsTouchingLayers(ground) && Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    //Phá hủy Object sau 1 khoảng thời gian
    public void Destroy()
    {
        StartCoroutine(DestroyAfterAnimation());
    }

    IEnumerator DestroyAfterAnimation()
    {
        ani.SetBool("death", true);

        yield return new WaitForSeconds(0.3f);

        Destroy(gameObject);
    }
}
