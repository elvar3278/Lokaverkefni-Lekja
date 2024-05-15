using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CameraController : MonoBehaviour
    {
        public float damping = 1.5f; // Hraði hreyfingar
        public Vector2 offset = new Vector2(0f, 0f); // Sérstakt áhrif ef þú vilt að leikmaðurinn sé ekki miðjuð á skjánum
        public bool faceLeft; // Speglun OFFSET eftir y-ásinn
        private Transform player; // Tilvísun í leikmann
        private int lastX; // Fyrri X staðsetning leikmanns

        void Start()
        {
            offset = new Vector2(Mathf.Abs(offset.x), offset.y); // Uppfærir offset
            FindPlayer(faceLeft); // Finna leikmann
        }

        public void FindPlayer(bool playerFaceLeft)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; // Finna leikmann með merkinu "Player"
            lastX = Mathf.RoundToInt(player.position.x); // Skráir núverandi X staðsetningu leikmanns
            if (playerFaceLeft) // Ef leikmaður snýr til vinstri
            {
                transform.position = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z); // Uppfærir staðsetningu myndavélar til að spegla hana til vinstri
            }
            else
            {
                transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z); // Uppfærir staðsetningu myndavélar til að spegla hana til hægri
            }
        }

        void Update()
        {
            if (player) // Ef leikmaður er til
            {
                int currentX = Mathf.RoundToInt(player.position.x); // Núverandi X staðsetning leikmanns
                if (currentX > lastX) faceLeft = false; else if (currentX < lastX) faceLeft = true; // Athugar í hvaða átt leikmaðurinn er að fara
                lastX = Mathf.RoundToInt(player.position.x); // Uppfærir fyrri X staðsetningu leikmanns

                Vector3 target;
                if (faceLeft) // Ef leikmaðurinn snýr til vinstri
                {
                    target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z); // Uppfærir staðsetningu markmiðs til að spegla hana til vinstri
                }
                else
                {
                    target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z); // Uppfærir staðsetningu markmiðs til að spegla hana til hægri
                }
                Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime); // Færir myndavélina að nýrri staðsetningu
                transform.position = currentPosition; // Uppfærir staðsetningu myndavélar
            }
        }
    }
}
