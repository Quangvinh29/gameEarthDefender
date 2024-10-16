using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    public float HorizontalInput;
    public float VerticalInput;

    void Update()
    {
        DiChuyen();
    }

    public void DiChuyen()
    {
        // Di chuyen bang nut bam
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");

        Vector2 DiChuyen = new Vector2(HorizontalInput, VerticalInput);
        transform.Translate(DiChuyen * speed * Time.deltaTime);

        // gioi han nguoi cho trong map
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, (float)-2.6, (float)2.6), transform.position.y);
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, (float)-4.8, (float)4.8));
    }
}
