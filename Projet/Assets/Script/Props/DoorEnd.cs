using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnd : MonoBehaviour
{
    //fonction permettant d'afficher la fin quand le joueur rentre dans le portail
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            GameManager.instance.End();
    }
}
