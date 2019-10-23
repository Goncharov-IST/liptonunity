using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    private Vector3 moveVector;
    public float speed = 10f;

    void Update()
    {
        transform.Translate(1 * speed * Time.deltaTime, 0, 0);
    }
}
