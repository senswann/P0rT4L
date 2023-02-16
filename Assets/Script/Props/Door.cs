using UnityEngine;

public class Door : ObjectAction
{
    [SerializeField] Animator anim;
    public override void Action()
    {
        anim.SetTrigger("Down");
    }
    public override void ActionStop()
    {
        anim.SetTrigger("Up");
    }
}
