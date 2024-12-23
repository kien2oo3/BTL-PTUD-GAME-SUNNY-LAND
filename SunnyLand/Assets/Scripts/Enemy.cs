using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    Animator ani;

    PlayerController player;

    float left, right, top, bottom;
    public float x;
    public float y;

    public float moveSpeedX = 5f, moveSpeedY=5f;

    void Start()
    {
        player=FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        left = transform.position.x;
        right = left+ x;
        bottom = transform.position.y;
        top = bottom + y;
    }

    void Update()
    {
        if (check())
        {
            ani.enabled = true;
            Movement();
        }
        else
        {
            ani.enabled = false;
        }
    }

    void Movement()
    {
        Vector2 pos = rb.position;

        // Di chuyển theo trục X
        pos.x += moveSpeedX * Time.deltaTime;
        pos.y += moveSpeedY * Time.deltaTime;

        // Giới hạn giá trị trong khoảng left và right
        pos.x = Mathf.Clamp(pos.x, left, right);
        pos.y = Mathf.Clamp(pos.y, bottom, top);

        // Nếu vượt quá giới hạn trái, đảo ngược hướng moveSpeed
        if (x > 0)
        {
            if (pos.x <= left)
            {
                moveSpeedX = -moveSpeedX;
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, 0);
            }
            else if (pos.x >= right)
            {
                moveSpeedX = -moveSpeedX;
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, 0);
            }
        }
        if (y > 0)
        {
            if (pos.y <= bottom || pos.y >= top)
            {
                moveSpeedY = -moveSpeedY;
            }
        }
        transform.position = pos;
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
    //Kiểm tra vị trí của player so với quái
    bool check()
    {
        float x=player.transform.position.x-transform.position.x;
        float y=player.transform.position.y-transform.position.y;
        if (Mathf.Sqrt(x * x + y * y) <= 20)
        {
            return true;
        }
        return false;
    }
}
