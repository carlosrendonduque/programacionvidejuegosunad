using UnityEngine;

public class FallingPageController : MonoBehaviour
{
    public float destroyAngleThreshold = 85f; // Destruye si está muy inclinada
    public float checkDelay = 1f;             // Espera 1 segundo antes de comprobar
    private bool landed = false;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, 0, -0.5f); // Empuja el centro hacia el frente
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !landed)
        {
            landed = true;
            Invoke(nameof(CheckIfShouldDestroy), checkDelay);
        }
    }

    void CheckIfShouldDestroy()
    {
        float angle = Vector3.Angle(transform.up, Vector3.up);

        // Si la pared está suficientemente inclinada, destrúyela
        if (angle < destroyAngleThreshold)
        {
            Debug.Log($"👉 Destruyendo pared: ángulo {angle} > threshold {destroyAngleThreshold}");
            FindObjectOfType<PageSpawner>().ClearCurrentPage();
            Destroy(gameObject);
        }
        else
        {
            // Si no está inclinada, sigue comprobando cada medio segundo
            Debug.Log($"🔄 Aún no se destruye: ángulo {angle} ≤ threshold {destroyAngleThreshold}, volverá a comprobar en 5s");
            Invoke(nameof(CheckIfShouldDestroy), 0.5f);
        }
    }
}
