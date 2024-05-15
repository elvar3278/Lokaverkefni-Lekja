using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class NextSceneTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player") // Ef leikma�ur snertir triggerinn
            {
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // N�verandi senuindeks
                int nextSceneIndex = currentSceneIndex + 1; // Senuindeks n�stu senu
                if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) // Ef n�sta senu er til � byggingarskr�nni
                {
                    SceneManager.LoadScene(nextSceneIndex); // Hle�ur n�stu senu
                }
                else
                {
                    Debug.LogWarning("No next scene available. Make sure there is a scene after the current one in the build settings."); // Skilabo� um a� engin n�sta senu s� tilt�k. Athuga�u a� vera s� senu eftir n�verandi � byggingarskr�nni.
                }
            }
        }
    }
}
