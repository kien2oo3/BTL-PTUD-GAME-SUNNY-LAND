using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator ani;

    float hori;
    public float jump;

    Collider2D coll;
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask ground2;

    int cherry = 0;
    [SerializeField] Text cherryText;

    public AudioSource aus;
    public AudioClip collectCherry, playerJump, playerHurt, enemyDeath;


    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        ani= GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Nếu ko ở trạng thái đau sẽ đc di chuyển, ngược lại sẽ ko thể di chuyển cho đến
        //khi nhân vật bị đẩy ra dừng lại
        if (ani.GetInteger("status") != 4)
        {
            Movement();
        }
        else
        {
            if (rb.velocity.x < .1f&&rb.velocity.x>-.1f)
            {
                ani.SetInteger("status", 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //Kiểm tra va chạm với frog
        if (col.gameObject.CompareTag("frog") || col.gameObject.CompareTag("enemy"))
        {
            if (ani.GetInteger("status") == 3)
            {
                if (col.gameObject.transform.position.y < transform.position.y)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 5.0f);
                }
                // Lấy tham chiếu đến script EnemyController
                Enemy enemy1= col.gameObject.GetComponent<Enemy>();

                // Kiểm tra xem có tham chiếu không và gọi phương thức DestroyEnemy
                if (enemy1 != null)
                {
                    enemy1.Destroy();
                }

                FrogController enemy2=col.gameObject.GetComponent<FrogController>();
                if(enemy2 != null)
                {
                    enemy2.Destroy();
                }
                aus.PlayOneShot(enemyDeath);
            }
            else
            {
                aus.PlayOneShot(playerHurt);
                ani.SetInteger("status", 4);
                //Kiểm tra xem nhân vật ở bên nào của quái, lùi nhân vật 1 đoạn
                if (col.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-5.0f, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(5.0f, rb.velocity.y);
                }
            }
        }
        //Load lại màn hình khi nhân vật chết
        if (col.gameObject.CompareTag("death"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("cherry"))
        {
            cherry++;
            cherryText.text = "x" + cherry;
            aus.PlayOneShot(collectCherry);
            Destroy(col.gameObject);
        }
    }
    
    //Di chuyển
    void Movement()
    {
        //Di chuyển và animation di chuyển
        hori = Input.GetAxis("Horizontal");
        Vector2 pos = rb.position;
        pos.x += hori * 7.0f * Time.deltaTime;
        transform.position = pos;
        if (hori < 0)
        {
            //chỉnh hướng nhìn của nhân vật
            transform.rotation = Quaternion.Euler(0, 180, 0);
            if (rb.velocity.y == 0)
            {
                ani.SetInteger("status", 1);
            }
        }
        else if (hori > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (rb.velocity.y == 0)
            {
                ani.SetInteger("status", 1);
            }
        }
        else
        {
            ani.SetInteger("status", 0);
        }

        //Nhảy nếu nhân vật đang ở mặt đắt
        if (Input.GetButtonDown("Jump") && (coll.IsTouchingLayers(ground)||coll.IsTouchingLayers(ground2)))
        {
            aus.PlayOneShot(playerJump);
            rb.AddForce(Vector2.up * jump);
        }

        //Animation nhảy
        if (!coll.IsTouchingLayers(ground)&&!coll.IsTouchingLayers(ground2))
        {
            ani.SetInteger("status", 2);
            //Kiểm tra xem vận tốc trục y có xấp xỉ = 0 không, nếu có nhân vật đang rơi xuống, chỉnh trạng thái animation
            if (rb.velocity.y < .1f)
            {
                ani.SetInteger("status", 3);
            }
        }
    }
}
