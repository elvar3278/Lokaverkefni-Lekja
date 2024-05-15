using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class NextSceneTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player") // Ef leikmaður snertir triggerinn
            {
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Núverandi senuindeks
                int nextSceneIndex = currentSceneIndex + 1; // Senuindeks næstu senu
                if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) // Ef næsta senu er til í byggingarskránni
                {
                    SceneManager.LoadScene(nextSceneIndex); // Hleður næstu senu
                }
                else
                {
                    Debug.LogWarning("No next scene available. Make sure there is a scene after the current one in the build settings."); // Skilaboð um að engin næsta senu sé tiltæk. Athugaðu að vera sú senu eftir núverandi í byggingarskránni.
                }
            }
        }
    }
}
