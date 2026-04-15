using UnityEngine;

public class KameraSterowanie : MonoBehaviour
{
    private Camera cam;

    [Header("Ustawienia Ruchu")]
    public float moveSpeed = 15f;
    public float zoomSpeed = 5f;

    [Header("Limity Zooma")]
    public float minSize = 2f;
    public float maxSize = 12f;

    [Header("Granice Mapy")]
    public float minX = -30f;
    public float maxX = 30f;
    public float minY = -20f;
    public float maxY = 20f;

    void Start()
    {
        cam = GetComponent<Camera>();
        // Ustawiamy kamerê w bezpiecznym punkcie Z
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }

    void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    void HandleMovement()
    {
        // Pobieramy sygna³y z klawiatury (WSAD lub Strza³ki)
        float h = Input.GetAxisRaw("Horizontal"); // A i D
        float v = Input.GetAxisRaw("Vertical");   // W i S

        // Tworzymy wektor ruchu
        Vector3 move = new Vector3(h, v, 0).normalized * moveSpeed * Time.deltaTime;

        // Przesuwamy kamerê
        transform.Translate(move, Space.World);

        // OGRANICZENIA (Constrain)
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = -10f; // Zawsze trzymaj Z na -10
        transform.position = pos;
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scroll * zoomSpeed, minSize, maxSize);
        }
    }
}