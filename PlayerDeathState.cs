using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerDeathState : MonoBehaviour
    {
        public float jumpForce; // Kraftur fyrir stökk

        private Rigidbody2D rigidbody; // Tilvísun í Rigidbody2D
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>(); // Nær í Rigidbody2D hluti
            rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse); // Bætir krafti við stökkið í uppávið stefnu
        }
    }
}
