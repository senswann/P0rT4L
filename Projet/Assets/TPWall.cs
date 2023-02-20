using UnityEngine;

public class TPWall : MonoBehaviour
{
    // target du portail
    [SerializeField] Transform target;

    //getter de la target
    public Transform GetTarget() { return target;}
}
