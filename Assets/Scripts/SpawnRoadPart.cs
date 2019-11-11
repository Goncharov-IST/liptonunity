using UnityEngine;

public class SpawnRoadPart : MonoBehaviour
{
  public Transform Point;
  public GameObject[] Prefabs;
  private GameObject currentPrefab;
  private int index;

  private void Start()
  {
    index = Random.Range(0, Prefabs.Length);
    currentPrefab = Prefabs[index];
  }

  void OnTriggerEnter(Collider col)
  {
    if (col.CompareTag("Player"))
    {
      Instantiate(currentPrefab, Point.position, Quaternion.identity);
    }
  }

  void OnTriggerExit(Collider col)
  {
    if (col.CompareTag("Player"))
    {
      Destroy(gameObject, .6f);
      Destroy(transform.parent.gameObject, .6f);
    }
  }
}
