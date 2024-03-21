using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel2 : Character
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] public float speed = 250;
    [SerializeField] public float jumpForce = 350;

    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;

    private bool isGrounded = true;
    private bool isAttack = false;

    private float horizontal;

    private int coin = 0;

    private Vector3 savePoint;

    private void Awake()
    {
        // coin = PlayerPrefs.GetInt("coin", 0);
    }

    void Update()
    {
        if (IsDead) return;

        isGrounded = CheckGrounded();

        // horizontal = Input.GetAxisRaw("Horizontal");

        if (isAttack)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (isGrounded)
        {
            // Run
            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim("run");
            }

            // Throw
            if (Input.GetKeyDown(KeyCode.V) && isGrounded)
            {
                Throw();
                return;
            }
        }

        // Moving
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            rb.velocity = new Vector2(horizontal * Time.deltaTime * speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        }
        else if (isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }
    }

    public override void OnInit()
    {
        base.OnInit();

        isAttack = false;

        ChangeAnim("idle");
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    public void SetMove(float horizontal)
    {
        this.horizontal = horizontal;
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        Invoke(nameof(OnInit), 2f);
    }

    private bool CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        return hit.collider != null;
    }

    private void Throw()
    {
        ChangeAnim("throw");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);

        Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation);
    }

    private void ResetAttack()
    {
        isAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            coin++;
            UIManager.Instance.SetCoin(coin);
            // PlayerPrefs.SetInt("coin", coin);
            Destroy(collision.gameObject);
        }

        if (collision.tag == "Heart")
        {
            this.hp = Math.Min(this.hp + 20, 100);
            this.healthBar.SetNewHp(this.hp);
            Destroy(collision.gameObject);
        }

        if (collision.tag == "DeathZone")
        {
            ChangeAnim("die");
            Invoke(nameof(OnInit), 1f);
        }
    }
}
