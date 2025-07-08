using UnityEngine;
using System.Collections;

public class PageSpawner : MonoBehaviour
{
    public GameObject fallingPagePrefab; // Prefab de la pared
    //public float spawnInterval = 0.1f;      // Intervalo en segundos
    public Vector3 spawnPosition = new Vector3(0, 1, 4); // Posición inicial

    private GameObject currentPage; // 🌟 Guarda la pared actual

    void Start()
    {
        //InvokeRepeating(nameof(TrySpawnPage), 1f, spawnInterval);
        FindObjectOfType<PageSpawner>().TrySpawnPage();
    }

    void TrySpawnPage()
    {
        if (currentPage != null) return;

        // ✅ Forzar inclinación 2° más hacia adelante
        Quaternion tiltedRotation = Quaternion.Euler(87f, 0f, 0f);

        currentPage = Instantiate(fallingPagePrefab, spawnPosition, tiltedRotation);

        Rigidbody rb = currentPage.GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, 0, -0.5f); // Mueve centro de masa hacia adelante
    }


    IEnumerator EnablePhysics(Rigidbody rb)
    {
        yield return null; // espera 1 frame
        rb.isKinematic = false;
    }

    public void ClearCurrentPage()
    {
        currentPage = null;
        // 🚀 Spawnear inmediatamente
        FindObjectOfType<PageSpawner>().TrySpawnPage();
    }
}
