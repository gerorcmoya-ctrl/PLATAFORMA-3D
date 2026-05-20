using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrulla")]
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad = 2f;

    [Header("Detección del jugador")]
    public Transform jugador;
    public float distanciaDeteccion = 8f;
    public float velocidadPersecucion = 4f;

    private Vector3 destino;
    private Animator anim;
    private bool persiguiendo = false;

    void Start()
    {
        destino = puntoB.position;
        anim = GetComponentInChildren<Animator>();

        // Buscar el jugador automáticamente
        if (jugador == null)
            jugador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (jugador == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia < distanciaDeteccion)
        {
            // Perseguir al jugador
            persiguiendo = true;
            PerseguirJugador();
        }
        else
        {
            // Volver a patrullar
            persiguiendo = false;
            Patrullar();
        }
    }

    void Patrullar()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            destino,
            velocidad * Time.deltaTime
        );

        if (destino != transform.position)
        {
            Vector3 direccion = (destino - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direccion);
            anim.SetBool("isWalking", true);
        }

        if (Vector3.Distance(transform.position, destino) < 0.1f)
        {
            destino = destino == puntoA.position ? puntoB.position : puntoA.position;
        }
    }

    void PerseguirJugador()
    {
        Debug.Log("isWalking: " + anim.GetBool("isWalking"));

        transform.position = Vector3.MoveTowards(
            transform.position,
            jugador.position,
            velocidadPersecucion * Time.deltaTime
        );

        Vector3 direccion = (jugador.position - transform.position).normalized;
        if (direccion != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direccion);

        anim.SetBool("isWalking", true);
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

    // Mostrar el radio de detección en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaDeteccion);
    }
}