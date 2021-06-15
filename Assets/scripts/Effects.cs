using UnityEngine;

public class Effects : MonoBehaviour
{
    [SerializeField] ParticleSystem tapParticleSystemPrefab;
    Camera mainCamera;
    Transform Canvas;
    private void Start()
    {
        mainCamera = Camera.main;
        Canvas = FindObjectOfType<Canvas>().transform;
    }

    public void Tap()
    {
        ParticleSystem tapParticles = 
            Instantiate(tapParticleSystemPrefab, 
            Vector3.zero, Quaternion.identity) as ParticleSystem;
        tapParticles.transform.SetAsLastSibling();
        Vector3 mousePox = Input.mousePosition;
        tapParticles.transform.position = mainCamera.ScreenToWorldPoint(new Vector3(mousePox.x, mousePox.y, 1));
        tapParticles.Play();
    }
}
