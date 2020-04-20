using UnityEngine;
using UnityEngine.UI;

public class GUIText : MonoBehaviour
{
    void Update()
    {
        this.GetComponent<Text>().text = "Coins: " + PlayerController.score;
    }
}