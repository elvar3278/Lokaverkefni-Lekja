using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CameraController : MonoBehaviour
    {
        public float damping = 1.5f; // Hra�i hreyfingar
        public Vector2 offset = new Vector2(0f, 0f); // S�rstakt �hrif ef �� vilt a� leikma�urinn s� ekki mi�ju� � skj�num
        public bool faceLeft; // Speglun OFFSET eftir y-�sinn
        private Transform player; // Tilv�sun � leikmann
        private int lastX; // Fyrri X sta�setning leikmanns

        void Start()
        {
            offset = new Vector2(Mathf.Abs(offset.x), offset.y); // Uppf�rir offset
            FindPlayer(faceLeft); // Finna leikmann
        }

        public void FindPlayer(bool playerFaceLeft)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; // Finna leikmann me� merkinu "Player"
            lastX = Mathf.RoundToInt(player.position.x); // Skr�ir n�verandi X sta�setningu leikmanns
            if (playerFaceLeft) // Ef leikma�ur sn�r til vinstri
            {
                transform.position = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z); // Uppf�rir sta�setningu myndav�lar til a� spegla hana til vinstri
            }
            else
            {
                transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z); // Uppf�rir sta�setningu myndav�lar til a� spegla hana til h�gri
            }
        }

        void Update()
        {
            if (player) // Ef leikma�ur er til
            {
                int currentX = Mathf.RoundToInt(player.position.x); // N�verandi X sta�setning leikmanns
                if (currentX > lastX) faceLeft = false; else if (currentX < lastX) faceLeft = true; // Athugar � hva�a �tt leikma�urinn er a� fara
                lastX = Mathf.RoundToInt(player.position.x); // Uppf�rir fyrri X sta�setningu leikmanns

                Vector3 target;
                if (faceLeft) // Ef leikma�urinn sn�r til vinstri
                {
                    target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z); // Uppf�rir sta�setningu markmi�s til a� spegla hana til vinstri
                }
                else
                {
                    target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z); // Uppf�rir sta�setningu markmi�s til a� spegla hana til h�gri
                }
                Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime); // F�rir myndav�lina a� n�rri sta�setningu
                transform.position = currentPosition; // Uppf�rir sta�setningu myndav�lar
            }
        }
    }
}
