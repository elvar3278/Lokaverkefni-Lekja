using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance; // Static reference to GameManager instance

        public int coinsCounter = 0; // T�lva fyrir peninga
        public GameObject playerGameObject; // GameObject fyrir leikmann
        private PlayerController player; // Tilv�sun � PlayerController
        public GameObject deathPlayerPrefab; // Prefab fyrir dau�an leikmann
        public Text coinText; // Texta hlutur fyrir peninga

        void Awake()
        {
            // Tryggir a� a�eins ein tilvik af GameManager s� til
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            // Tryggir a� GameManager s� ekki eytt milli sena
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>(); // Finnur leikmann
        }

        void Update()
        {
            coinText.text = coinsCounter.ToString(); // Uppf�rir texta fyrir peninga
            if (player.deathState == true) // Ef leikma�urinn er dau�ur
            {
                playerGameObject.SetActive(false); // Gerir leikmann �virkann
                GameObject deathPlayer = Instantiate(deathPlayerPrefab, playerGameObject.transform.position, playerGameObject.transform.rotation); // B�r til dau�an leikmann
                deathPlayer.transform.localScale = new Vector3(playerGameObject.transform.localScale.x, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z); // Uppf�rir st�r� dau�a leikmanns
                player.deathState = false; // Uppf�rir dau�atilvist
                Invoke("LoadNextScene", 1f); // Hle�ur n�stu senu eftir sm� bi�
            }
        }

        private void LoadNextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // N�verandi senuindeks
            int nextSceneIndex = currentSceneIndex + 1; // Senuindeks n�stu senu

            // Athugar hvort n�sta senuindeks s� innan byggingarskr�rinnar
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex); // Hle�ur n�stu senu
            }
            else
            {
                Debug.LogWarning("No more scenes to load. The current scene is the last one."); // Skilabo� um a� engar fleiri senur s�u tilt�kar
                // Eftirvirkni getur veri� a� byrja leikinn aftur e�a fara � a�alskj�
                SceneManager.LoadScene(0); // Hle�ur fyrstu senu (teki� sem a�alskj�)
            }
        }

        // A�fer� til a� b�ta vi� peningum
        public void AddCoin()
        {
            coinsCounter++; // B�tir vi� peningum
            UpdateCoinText(); // Uppf�rir texta fyrir peninga
        }

        // A�fer� til a� fjarl�gja peninga
        public void RemoveCoin()
        {
            if (coinsCounter > 0) // Ef �a� eru peningar til a� fjarl�gja
            {
                coinsCounter--; // Fjarl�gir pening
                UpdateCoinText(); // Uppf�rir texta fyrir peninga
            }
            else
            {
                Debug.LogWarning("Attempted to remove a coin when the coin count is already zero."); // Skilabo� um a� reynt hafi veri� a� fjarl�gja pening en talan er n� �egar n�ll
                KillPlayer(); // Drepir leikmann
            }
        }

        private void UpdateCoinText()
        {
            coinText.text = "Coins: " + coinsCounter; // Uppf�rir texta fyrir peninga
        }

        private void KillPlayer()
        {
            player.deathState = true; // Segir PlayerController a� leikma�ur s� dau�ur
        }
    }
}
