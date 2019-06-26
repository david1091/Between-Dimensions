using UnityEngine;

public class CameraController : MonoBehaviour
{

    private GameObject player;
    private Vector3 offset;

    private float shakeTime = -1f;
    private float Magnitude;

    void Start()
    {
        player = GameObject.Find("Player");
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        if (Input.GetButtonDown("ChangeDimension"))
            ShakeCamera(0.15f, 0.3f);
    }

    
    void LateUpdate()
    {
        if (shakeTime >= 0)
        {
            Vector2 shakePosition = Random.insideUnitCircle * Magnitude;
            transform.position = new Vector3(player.transform.position.x + shakePosition.x, player.transform.position.y + shakePosition.y, -10);
            shakeTime -= Time.deltaTime;
        }
        else
            transform.position = player.transform.position + offset;
    }

    void ShakeCamera(float Magnitude_1, float shakeTime_1)
    {
        Magnitude = Magnitude_1;
        shakeTime = shakeTime_1;
    }
}
