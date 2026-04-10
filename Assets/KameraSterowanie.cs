using UnityEngine;

public class KameraSterowanie : MonoBehaviour
{
    private Vector3 pointClick;
    private Camera cam;

    [Header("Ustawienia Zooma")]
    public float zoomSpeed = 5f;
    public float minSize = 2f;
    public float maxSize = 150f;

    [Header("Granice Mapy")]
    public float minX = -13f;
    public float maxX = 13f;
    public float minY = -7f;
    public float maxY = 7f;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        HandlePan();
        HandleZoom();

        // WYMUSZENIE GRANIC (To jest kluczowa zmiana)
        ConstrainCamera();
    }

    void HandlePan()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointClick = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = pointClick - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += direction;
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            float newSize = cam.orthographicSize - scroll * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(newSize, minSize, maxSize);
        }
    }

    // Nowa metoda trzymaj¹ca kamerê w ryzach
    void ConstrainCamera()
    {
        Vector3 pos = transform.position;

        // Ograniczamy pozycjê X i Y na podstawie wpisanych granic
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}

