using UnityEngine;
using TMPro;

public class FPSCount : MonoBehaviour
{
    //UI du compteur d FPS
    [SerializeField] TextMeshProUGUI counts;

    //variable permettant le calcul des fps
    private int frameRateIndex;
    private float[] frameDeltaTimeArray;

    // on initialise le frameDeltaTimeArray lors de l awake de la classe.
    private void Awake()
    {
        frameDeltaTimeArray = new float[50];
    }

    private void Update()
    {
        //on recupere des valeur de temps pour pouvoir calculer le fps le plus precis
        frameDeltaTimeArray[frameRateIndex] = Time.deltaTime;
        frameRateIndex = (frameRateIndex + 1) % frameDeltaTimeArray.Length;

        //on ecrit les FPS a l ecran
        counts.text = "FPS : " + Mathf.RoundToInt(CalculateFPS()).ToString();
    }

    //on effectue le calcul de FPS
    private float CalculateFPS()
    {
        float total = 0f;
        foreach (float deltaTime in frameDeltaTimeArray)
            total += deltaTime;
        return frameDeltaTimeArray.Length / total;
    }
}
