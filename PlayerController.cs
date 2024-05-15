using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        public float movingSpeed; // Hraði fyrir hreyfingu
        public float jumpForce; // Kraftur fyrir stökk
        private float moveInput; // Stýringar fyrir hreyfingu

        private bool facingRight = false; // Stýring á snúningi
        [HideInInspector]
        public bool deathState = false; // Stýring á dauðatilvist

        private bool isGrounded; // Stýring á því hvort leikmaðurinn sé á jörðinni
        public Transform groundCheck; // Staðsetning fyrir jörðina

        private Rigidbody2D rigidbody; // Tilvísun í Rigidbody2D
        private Animator animator; // Tilvísun í Animator
        private GameManager gameManager; // Tilvísun í GameManager

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>(); // Nær í Rigidbody2D hluti
            animator = GetComponent<Animator>(); // Nær í Animator hluti
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); // Nær í GameManager hlut
        }

        private void FixedUpdate()
        {
            CheckGround(); // Athugar hvort leikmaðurinn sé á jörðinni
        }

        void Update()
        {
            if (Input.GetButton("Horizontal")) // Ef er ýtt á takka fyrir hliðarhreyfingu
            {
                moveInput = Input.GetAxis("Horizontal"); // Nær í stýringu fyrir hreyfingu
                Vector3 direction = transform.right * moveInput; // Stýrir átt
                transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movingSpeed * Time.deltaTime); // Hreyfir leikmanninn
                animator.SetInteger("playerState", 1); // Kveikir á keyrslu-animasjón
            }
            else
            {
                if (isGrounded) animator.SetInteger("playerState", 0); // Kveikir á kyrrstöðu-animasjón
            }
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // Ef er ýtt á Space og leikmaðurinn er á jörðinni
            {
                rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse); // Bætir við krafti við stökk
            }
            if (!isGrounded) animator.SetInteger("playerState", 2); // Kveikir á stökk-animasjón

            if (facingRight == false && moveInput > 0) // Ef leikmaðurinn snýst til vinstri og er að fara til hægri
            {
                Flip(); // Snýr leikmanninum
            }
            else if (facingRight == true && moveInput < 0) // Ef leikmaðurinn snýst til hægri og er að fara til vinstri
            {
                Flip(); // Snýr leikmanninum
            }
        }

        private void Flip()
        {
            facingRight = !facingRight; // Snýr stefnunni
            Vector3 Scaler = transform.localScale; // Sækir stærðina
            Scaler.x *= -1; // Snýr um ás
            transform.localScale = Scaler; // Uppfærir stærðina
        }

        private void CheckGround()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.2f); // Athugar hvort jörð sé í kring
            isGrounded = colliders.Length > 1; // Segir hvort jörð sé til staðar
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Enemy") // Ef leikmaðurinn snertir óvin
            {
                gameManager.RemoveCoin(); // Dregur frá pening
                deathState = true; // Segir GameManager að leikmaður sé dauður
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Hleður næstu senu
            }
            else
            {
                deathState = false; // Segir GameManager að leikmaður sé ekki dauður
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Coin") // Ef leikmaðurinn nálgast pening
            {
                gameManager.AddCoin(); // Safnar pening
                Destroy(other.gameObject); // Eyðir peningnum í senunni
            }
        }
    }
}
