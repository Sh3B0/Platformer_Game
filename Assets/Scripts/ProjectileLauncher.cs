using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject fireball, projectile;
    public float power = 0.1f;
    int tmin = 0, tmx = 0, r = 0;
    bool fire;

    // Update is called once per frame
    void Update()
    {
        int time = (int)Time.time;
        if (time > r)
        {
            tmin = (int)Mathf.Ceil(Time.time);
            tmx = tmin + 5;
            r = Random.Range(tmin, tmx);
            fire = true;
        }
        if (time == r && fire == true)
        {
            Destroy(projectile);
            projectile = Instantiate(fireball);
            projectile.GetComponent<Rigidbody2D>().AddForce(power * new Vector2(-1, 1), ForceMode2D.Impulse);
            fire = false;
        }
    }
}
