using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class EnemyAI : MonoBehaviour
    {
        public float moveSpeed = 1f; // Hraði hreyfingar
        public LayerMask ground; // Lög fyrir jörð
        public LayerMask wall; // Lög fyrir vegg

        private Rigidbody2D rigidbody; // Tilvísun í Rigidbody2D hlut
        public Collider2D triggerCollider; // Tilvísun í Collider2D hlut

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>(); // Nær í Rigidbody2D hlut
        }

        void Update()
        {
            rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y); // Uppfærir hraði leikmanns
        }

        void FixedUpdate()
        {
            if (!triggerCollider.IsTouchingLayers(ground) || triggerCollider.IsTouchingLayers(wall)) // Ef triggerCollider snertir ekki jörð eða snertir vegg
            {
                Flip(); // Snýr við
            }
        }

        private void Flip()
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y); // Snýr við í áttina á móti núverandi stefnu
            moveSpeed *= -1; // Snýr við hraða
        }
    }
}
