using UnityEngine;

//classe utiliser pour definir tous les objet qui auront besoin d etre activer / desactiver par un bouton ou un recepteur
public class ObjectAction : MonoBehaviour
{
    public virtual void Action(){}
    public virtual void ActionStop(){}
}
