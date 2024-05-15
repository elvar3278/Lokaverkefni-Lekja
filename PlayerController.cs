using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        public float movingSpeed; // Hra�i fyrir hreyfingu
        public float jumpForce; // Kraftur fyrir st�kk
        private float moveInput; // St�ringar fyrir hreyfingu

        private bool facingRight = false; // St�ring � sn�ningi
        [HideInInspector]
        public bool deathState = false; // St�ring � dau�atilvist

        private bool isGrounded; // St�ring � �v� hvort leikma�urinn s� � j�r�inni
        public Transform groundCheck; // Sta�setning fyrir j�r�ina

        private Rigidbody2D rigidbody; // Tilv�sun � Rigidbody2D
        private Animator animator; // Tilv�sun � Animator
        private GameManager gameManager; // Tilv�sun � GameManager

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>(); // N�r � Rigidbody2D hluti
            animator = GetComponent<Animator>(); // N�r � Animator hluti
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); // N�r � GameManager hlut
        }

        private void FixedUpdate()
        {
            CheckGround(); // Athugar hvort leikma�urinn s� � j�r�inni
        }

        void Update()
        {
            if (Input.GetButton("Horizontal")) // Ef er �tt � takka fyrir hli�arhreyfingu
            {
                moveInput = Input.GetAxis("Horizontal"); // N�r � st�ringu fyrir hreyfingu
                Vector3 direction = transform.right * moveInput; // St�rir �tt
                transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movingSpeed * Time.deltaTime); // Hreyfir leikmanninn
                animator.SetInteger("playerState", 1); // Kveikir � keyrslu-animasj�n
            }
            else
            {
                if (isGrounded) animator.SetInteger("playerState", 0); // Kveikir � kyrrst��u-animasj�n
            }
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // Ef er �tt � Space og leikma�urinn er � j�r�inni
            {
                rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse); // B�tir vi� krafti vi� st�kk
            }
            if (!isGrounded) animator.SetInteger("playerState", 2); // Kveikir � st�kk-animasj�n

            if (facingRight == false && moveInput > 0) // Ef leikma�urinn sn�st til vinstri og er a� fara til h�gri
            {
                Flip(); // Sn�r leikmanninum
            }
            else if (facingRight == true && moveInput < 0) // Ef leikma�urinn sn�st til h�gri og er a� fara til vinstri
            {
                Flip(); // Sn�r leikmanninum
            }
        }

        private void Flip()
        {
            facingRight = !facingRight; // Sn�r stefnunni
            Vector3 Scaler = transform.localScale; // S�kir st�r�ina
            Scaler.x *= -1; // Sn�r um �s
            transform.localScale = Scaler; // Uppf�rir st�r�ina
        }

        private void CheckGround()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.2f); // Athugar hvort j�r� s� � kring
            isGrounded = colliders.Length > 1; // Segir hvort j�r� s� til sta�ar
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Enemy") // Ef leikma�urinn snertir �vin
            {
                gameManager.RemoveCoin(); // Dregur fr� pening
                deathState = true; // Segir GameManager a� leikma�ur s� dau�ur
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Hle�ur n�stu senu
            }
            else
            {
                deathState = false; // Segir GameManager a� leikma�ur s� ekki dau�ur
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Coin") // Ef leikma�urinn n�lgast pening
            {
                gameManager.AddCoin(); // Safnar pening
                Destroy(other.gameObject); // Ey�ir peningnum � senunni
            }
        }
    }
}
