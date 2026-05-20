using UnityEngine;

public class PlataformaDesaparece : MonoBehaviour
{
    public float tiempoAntesDeDesaparecer = 1f;
    public float tiempoParaVolver = 3f;

    private bool pisada = false;
    private MeshRenderer meshRenderer;
    private Collider col;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !pisada)
        {
            pisada = true;
            StartCoroutine(Desaparecer());
        }
    }

    System.Collections.IEnumerator Desaparecer()
    {
        yield return new WaitForSeconds(tiempoAntesDeDesaparecer);

        // Desactivar completamente el objeto
        gameObject.SetActive(false);

        yield return new WaitForSeconds(tiempoParaVolver);

        // Volver a aparecer
        gameObject.SetActive(true);
        pisada = false;
    }
}