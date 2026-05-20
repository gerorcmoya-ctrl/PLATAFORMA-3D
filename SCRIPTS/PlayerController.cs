using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float velocidadRotacion = 10f;

    [Header("Salto")]
    public float fuerzaSalto = 6f;
    public LayerMask capaSuelo;

    private Rigidbody rb;
    private Camera camara;
    private Animator anim;
    private bool estaEnElSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camara = Camera.main;
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Detectar suelo con el CapsuleCollider
        estaEnElSuelo = Physics.CheckCapsule(
            transform.position + Vector3.up * 0.1f,
            transform.position + Vector3.up * 0.3f,
            0.29f,
            capaSuelo
        );

        Saltar();
        ActualizarAnimaciones();
    }

    void FixedUpdate()
    {
        Mover();
    }

    void Mover()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 adelante = camara.transform.forward;
        Vector3 derecha = camara.transform.right;

        adelante.y = 0;
        derecha.y = 0;
        adelante.Normalize();
        derecha.Normalize();

        Vector3 direccion = adelante * vertical + derecha * horizontal;

        if (direccion.magnitude > 0.1f)
        {
            rb.MovePosition(rb.position + direccion * velocidad * Time.fixedDeltaTime);

            Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                rotacionDeseada,
                velocidadRotacion * Time.deltaTime
            );
        }
    }

    void ActualizarAnimaciones()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float magnitud = new Vector2(horizontal, vertical).magnitude;

        anim.SetFloat("SPEED", magnitud);
        anim.SetBool("IsGrounded", estaEnElSuelo);
    }

    void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space) && estaEnElSuelo)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            anim.SetTrigger("jump");
        }

        if (rb.linearVelocity.y < 0)
        {
            rb.AddForce(Vector3.down * 25f * Time.deltaTime, ForceMode.Impulse);
        }
    }
}