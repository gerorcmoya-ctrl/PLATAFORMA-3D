using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Animator anim;
    private bool muerto = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Morir()
    {
        if (muerto) return;
        muerto = true;

        // Animación de muerte
        if (anim != null)
            anim.SetTrigger("death");

        // Avisar a la puerta
        if (Puerta.Instance != null)
            Puerta.Instance.EnemigoDerrotado();

        // Desactivar el script de patrulla
        EnemyPatrol patrol = GetComponent<EnemyPatrol>();
        if (patrol != null)
            patrol.enabled = false;

        // Destruir después de la animación
        Destroy(gameObject, 2f);
    }
}