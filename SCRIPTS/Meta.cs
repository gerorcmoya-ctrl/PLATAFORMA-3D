using UnityEngine;
using UnityEngine.SceneManagement;

public class Meta : MonoBehaviour
{
    public string nombreSiguienteEscena = "Menu";
    public bool esUltimoNivel = false;
    public AudioClip sonidoMeta;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * 90f * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reproducir sonido
            if (sonidoMeta != null)
            {
                AudioSource.PlayClipAtPoint(sonidoMeta, transform.position);
            }

            if (esUltimoNivel)
            {
                SceneManager.LoadScene("Menu");
            }
            else
            {
                SceneManager.LoadScene(nombreSiguienteEscena);
            }
        }
    }
}