using UnityEngine;

public class TPWall : MonoBehaviour
{
    // target du portail
    [SerializeField] Transform target;
    // spawn du portail
    [SerializeField] Transform spawn;

    //getter de la target
    public Transform GetTarget() { return target;}
    public Transform GetSpawn() { return spawn; }
}
