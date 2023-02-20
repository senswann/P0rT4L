using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    //cooldown de desactivation du bouton (si set a 0 il n y a pas de desavtivation)
    [SerializeField] float cooldown = 3f;

    //variable stockant l'objet a activer
    [SerializeField] ObjectAction objAction;

    //boolean permettant de reguler l activation du bouton
    bool IsStart = false;
    
    //fonction activant le bouton
    public void Action()
    {
        if (!IsStart)
        {
            IsStart = true;
            objAction.Action();
            if(cooldown>0f)
                Invoke(nameof(Reset), cooldown);
        }
    }

    //fonction permettant le reset de l objet et du bouton
    private void Reset()
    {
        IsStart = false;
        objAction.ActionStop();
    }
}
