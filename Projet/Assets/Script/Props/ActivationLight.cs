using UnityEngine;

public class ActivationLight : ObjectAction
{
    //variable stockant les lights
    [SerializeField] Light lightObject1;
    [SerializeField] Light lightObject2;

    [SerializeField] Renderer meshRender1;
    [SerializeField] Renderer meshRender2;

    //fonction pemrettant le changement de couleur des bouton lié
    public override void Action()
    {
        Color orange = Color.red + Color.yellow;
        meshRender1.material.color = orange;
        meshRender2.material.color = orange;
        lightObject1.intensity = 0.5f;
        lightObject1.color = orange;
        lightObject2.intensity = 0.5f;
        lightObject2.color = orange;
    }

    public override void ActionStop(){ }
}
