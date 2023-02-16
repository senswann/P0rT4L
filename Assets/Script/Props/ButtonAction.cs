using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    [SerializeField] float cooldown = 3f;
    [SerializeField] ObjectAction objAction;
    bool IsStart = false;
    
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

    private void Reset()
    {
        IsStart = false;
        objAction.ActionStop();
    }
}
