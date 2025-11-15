using UnityEngine;

public class testMove : MonoBehaviour
{
    public Rigidbody2D myBody;
    public BoxCollider2D myCollider;

    bool up;
    bool down;
    bool left;
    bool right;
    bool shift;

    Vector2 direction;
    public float speed;

    public float stamina;

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        up = Input.GetKey(KeyCode.W);
        down = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);
        shift = Input.GetKey(KeyCode.LeftShift);

        direction = Vector2.zero;

        if (up) direction += Vector2.up;
        if (down) direction += Vector2.down;
        if (left) { direction += Vector2.left; transform.localScale = new Vector3(-1, 1, 1); }
        if (right) { direction += Vector2.right; transform.localScale = new Vector3(1, 1, 1); }

        direction.Normalize();
        myBody.linearVelocity = speed * direction;

        if (shift && stamina > 0)   // conume stamina to move faster, stamina is limited
        {
            stamina -= Time.deltaTime;
            myBody.linearVelocity *= 2f;
        }


            // water gravity
            myBody.linearVelocity += new Vector2(0, -1);
    }
}
