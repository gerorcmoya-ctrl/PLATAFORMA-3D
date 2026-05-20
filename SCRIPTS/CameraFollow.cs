using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform objetivo;
    public float distancia = 7f;
    public float altura = 4f;
    public float sensibilidadMouse = 3f;
    public float suavizado = 10f;

    private float anguloX = 0f;
    private float anguloY = 20f;

    void Update()
    {
        // Rotar con el mouse
        anguloX += Input.GetAxis("Mouse X") * sensibilidadMouse;
        anguloY -= Input.GetAxis("Mouse Y") * sensibilidadMouse;
        anguloY = Mathf.Clamp(anguloY, 5f, 60f);
    }

    void LateUpdate()
    {
        if (objetivo == null) return;

        // Calcular posición de la cámara alrededor del jugador
        Quaternion rotacion = Quaternion.Euler(anguloY, anguloX, 0);
        Vector3 offset = rotacion * new Vector3(0, 0, -distancia);
        Vector3 posicionDeseada = objetivo.position + offset + Vector3.up * 2f;

        transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavizado * Time.deltaTime);
        transform.LookAt(objetivo.position + Vector3.up * 1.5f);
    }
}