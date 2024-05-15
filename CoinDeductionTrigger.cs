using UnityEngine;

namespace Platformer
{
    public class CoinDeductionTrigger : MonoBehaviour
    {
        private GameManager gameManager;

        private void Start()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();// finnur GameManager
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                gameManager.RemoveCoin(); // Tekur � burt pening
                Destroy(gameObject); // Ey�ileggur hlutinn eftir � 
            }
        }
    }
}
