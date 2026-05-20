using UnityEngine;
using UnityEngine.SceneManagement;

public class BolaPeligro : MonoBehaviour
{
    public float intervaloRespawn = 5f;
    public Transform puntoInicio;

    private Rigidbody rb;
    private Vector3 posicionInicial;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        posicionInicial = transform.position;
        Empujar();
        InvokeRepeating("Resetear", intervaloRespawn, intervaloRespawn);
    }

    void Resetear()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = posicionInicial;
        Invoke("Empujar", 0.5f);
    }

    void Empujar()
    {
        // Empujar hacia adelante y abajo
        rb.AddForce(new Vector3(0, -2f, 5f), ForceMode.Impulse);
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}