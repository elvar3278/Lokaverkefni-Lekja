using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerDeathState : MonoBehaviour
    {
        public float jumpForce; // Kraftur fyrir st�kk

        private Rigidbody2D rigidbody; // Tilv�sun � Rigidbody2D
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>(); // N�r � Rigidbody2D hluti
            rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse); // B�tir krafti vi� st�kki� � upp�vi� stefnu
        }
    }
}
