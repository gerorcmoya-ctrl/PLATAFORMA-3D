using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float rangoAtaque = 2f;
    public KeyCode teclaAtaque = KeyCode.E;
    public AudioClip sonidoGolpe;

    private Animator anim;
    private AudioSource audioSource;
    private bool estaPegando = false;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(teclaAtaque) && !estaPegando)
        {
            Atacar();
        }

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("HumanArmature|Man_Punch"))
        {
            estaPegando = true;
            if (stateInfo.normalizedTime >= 0.9f)
            {
                estaPegando = false;
                anim.ResetTrigger("punch");
            }
        }
        else
        {
            estaPegando = false;
        }
    }

    void Atacar()
    {
        estaPegando = true;
        anim.ResetTrigger("punch");
        anim.SetTrigger("punch");

        // Reproducir sonido de golpe
        if (sonidoGolpe != null)
        {
            audioSource.PlayOneShot(sonidoGolpe);
        }

        Collider[] enemigos = Physics.OverlapSphere(
            transform.position,
            rangoAtaque,
            LayerMask.GetMask("Enemy")
        );

        foreach (Collider enemigo in enemigos)
        {
            EnemyHealth health = enemigo.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.Morir();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoAtaque);
    }
}