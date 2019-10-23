using UnityEngine;

public class SpawnRoadPart : MonoBehaviour
{
    public Transform Point;
    public GameObject Prefab;

    void OnTriggerEnter(Collider col) {
        //Debug.Log("enter");
        if(col.tag == "Player") {
            Instantiate (Prefab, Point.position, Quaternion.identity);
        }
    }

    void OnTriggerExit(Collider col) {
        //Debug.Log("exit");
        if(col.tag == "Player") {
            Destroy(transform.parent.gameObject, 0.8f);
            //Destroy(gameObject);
            //Destroy(transform.parent.gameObject);
        }
    }
}
