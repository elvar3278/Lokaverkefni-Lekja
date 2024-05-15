using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class EnemyAI : MonoBehaviour
    {
        public float moveSpeed = 1f; // Hra�i hreyfingar
        public LayerMask ground; // L�g fyrir j�r�
        public LayerMask wall; // L�g fyrir vegg

        private Rigidbody2D rigidbody; // Tilv�sun � Rigidbody2D hlut
        public Collider2D triggerCollider; // Tilv�sun � Collider2D hlut

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>(); // N�r � Rigidbody2D hlut
        }

        void Update()
        {
            rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y); // Uppf�rir hra�i leikmanns
        }

        void FixedUpdate()
        {
            if (!triggerCollider.IsTouchingLayers(ground) || triggerCollider.IsTouchingLayers(wall)) // Ef triggerCollider snertir ekki j�r� e�a snertir vegg
            {
                Flip(); // Sn�r vi�
            }
        }

        private void Flip()
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y); // Sn�r vi� � �ttina � m�ti n�verandi stefnu
            moveSpeed *= -1; // Sn�r vi� hra�a
        }
    }
}
