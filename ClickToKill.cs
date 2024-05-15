using UnityEngine;

public class ClickToKill2D : MonoBehaviour
{
    void Update()
    {
        // Athugar hvort vinstri músartakkinn sé smelltur
        if (Input.GetMouseButtonDown(0))
        {
            // Býr til geisl frá myndavélinni að músarstöðinni
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Athugar hvort geisli snerti jörðina fyrst
            RaycastHit2D groundHit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Ground"));

            // Ef geislinn snertir ekki jörðina, halda áfram að athuga fyrir óvinum eða hlutum sem ekki hafa merki
            if (groundHit.collider == null)
            {
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                // Athugar hvort geislinn snertir eitthvað
                if (hit.collider != null)
                {
                    // Athugar hvort hluturinn sem geislinn snerti hafi merkið "Enemy" eða enga merki (untagged)
                    if (hit.collider.tag == "Untagged")
                    {
                        // Eyðir hlutnum sem geislinn snerti
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }
    }
}
