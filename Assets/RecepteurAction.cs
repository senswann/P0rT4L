using UnityEngine;

public class RecepteurAction : MonoBehaviour
{
    [SerializeField] GameObject lightObj;

    private void Start()
    {
        lightObj.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Energy")
        {
            lightObj.SetActive(true);
            Destroy(collision.gameObject);
        }
    }
}
