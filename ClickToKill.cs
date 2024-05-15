using UnityEngine;

public class ClickToKill2D : MonoBehaviour
{
    void Update()
    {
        // Athugar hvort vinstri m�sartakkinn s� smelltur
        if (Input.GetMouseButtonDown(0))
        {
            // B�r til geisl fr� myndav�linni a� m�sarst��inni
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Athugar hvort geisli snerti j�r�ina fyrst
            RaycastHit2D groundHit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Ground"));

            // Ef geislinn snertir ekki j�r�ina, halda �fram a� athuga fyrir �vinum e�a hlutum sem ekki hafa merki
            if (groundHit.collider == null)
            {
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                // Athugar hvort geislinn snertir eitthva�
                if (hit.collider != null)
                {
                    // Athugar hvort hluturinn sem geislinn snerti hafi merki� "Enemy" e�a enga merki (untagged)
                    if (hit.collider.tag == "Untagged")
                    {
                        // Ey�ir hlutnum sem geislinn snerti
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }
    }
}
