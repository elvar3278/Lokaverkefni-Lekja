using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance; // Static reference to GameManager instance

        public int coinsCounter = 0; // Tölva fyrir peninga
        public GameObject playerGameObject; // GameObject fyrir leikmann
        private PlayerController player; // Tilvísun í PlayerController
        public GameObject deathPlayerPrefab; // Prefab fyrir dauðan leikmann
        public Text coinText; // Texta hlutur fyrir peninga

        void Awake()
        {
            // Tryggir að aðeins ein tilvik af GameManager sé til
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            // Tryggir að GameManager sé ekki eytt milli sena
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>(); // Finnur leikmann
        }

        void Update()
        {
            coinText.text = coinsCounter.ToString(); // Uppfærir texta fyrir peninga
            if (player.deathState == true) // Ef leikmaðurinn er dauður
            {
                playerGameObject.SetActive(false); // Gerir leikmann óvirkann
                GameObject deathPlayer = Instantiate(deathPlayerPrefab, playerGameObject.transform.position, playerGameObject.transform.rotation); // Býr til dauðan leikmann
                deathPlayer.transform.localScale = new Vector3(playerGameObject.transform.localScale.x, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z); // Uppfærir stærð dauða leikmanns
                player.deathState = false; // Uppfærir dauðatilvist
                Invoke("LoadNextScene", 1f); // Hleður næstu senu eftir smá bið
            }
        }

        private void LoadNextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Núverandi senuindeks
            int nextSceneIndex = currentSceneIndex + 1; // Senuindeks næstu senu

            // Athugar hvort næsta senuindeks sé innan byggingarskrárinnar
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex); // Hleður næstu senu
            }
            else
            {
                Debug.LogWarning("No more scenes to load. The current scene is the last one."); // Skilaboð um að engar fleiri senur séu tiltækar
                // Eftirvirkni getur verið að byrja leikinn aftur eða fara á aðalskjá
                SceneManager.LoadScene(0); // Hleður fyrstu senu (tekið sem aðalskjá)
            }
        }

        // Aðferð til að bæta við peningum
        public void AddCoin()
        {
            coinsCounter++; // Bætir við peningum
            UpdateCoinText(); // Uppfærir texta fyrir peninga
        }

        // Aðferð til að fjarlægja peninga
        public void RemoveCoin()
        {
            if (coinsCounter > 0) // Ef það eru peningar til að fjarlægja
            {
                coinsCounter--; // Fjarlægir pening
                UpdateCoinText(); // Uppfærir texta fyrir peninga
            }
            else
            {
                Debug.LogWarning("Attempted to remove a coin when the coin count is already zero."); // Skilaboð um að reynt hafi verið að fjarlægja pening en talan er nú þegar núll
                KillPlayer(); // Drepir leikmann
            }
        }

        private void UpdateCoinText()
        {
            coinText.text = "Coins: " + coinsCounter; // Uppfærir texta fyrir peninga
        }

        private void KillPlayer()
        {
            player.deathState = true; // Segir PlayerController að leikmaður sé dauður
        }
    }
}
