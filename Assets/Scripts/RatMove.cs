using UnityEngine;

public class RatMove : MonoBehaviour
{
    int dir = -1;
    private const float step = 1f;
    void Update()
    {
        if (Mathf.Abs(GetComponent<Transform>().position.x) >= 3)
        {
            dir *= -1;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
        GetComponent<Transform>().position += new Vector3(dir * step * Time.deltaTime, 0, 0);       
    }
}
