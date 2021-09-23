using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector2(-5, rb.velocity.y);
    }

    void OnCollisionEnter(Collision otherObj) {
        if (otherObj.gameObject.tag == "Bullet") {
            Destroy(gameObject,.5f);
        }
    }
}
