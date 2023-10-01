
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plaza;
using static Plaza.InternalCalls;
using static Plaza.Input;

public class PlayerController : Entity
{
    public float speed = 0.03f;
    Transform playerTransform;
    Transform cameraTransform;
    public void OnStart()
    {
        playerTransform = FindEntityByName("Player").GetComponent<Transform>();
        cameraTransform = FindEntityByName("Camera").GetComponent<Transform>();

        Cursor.Hide();
    }

    public void OnUpdate()
    {
        if (Input.IsKeyDown(KeyCode.H))
        {
            Cursor.Hide();
        }
        if (Input.IsKeyDown(KeyCode.G))
        {
            Cursor.Show();
        }

        if (Input.IsKeyDown(KeyCode.W))
        {
            playerTransform.MoveTowards(new Vector3(0.0f, 0.0f, 1.0f * speed));
        }
        if (Input.IsKeyDown(KeyCode.S))
        {
            playerTransform.MoveTowards(new Vector3(0.0f, 0.0f, -1.0f * speed));
        }
        if (Input.IsKeyDown(KeyCode.A))
        {
            playerTransform.MoveTowards(new Vector3(-1.0f * speed, 0.0f, 0.0f));
        }
        if (Input.IsKeyDown(KeyCode.D))
        {
            playerTransform.MoveTowards(new Vector3(1.0f * speed, 0.0f, 0.0f));
        }
        if (Input.IsKeyDown(KeyCode.Space))
        {
            playerTransform.Translation += new Vector3(0.0f, 1.0f * speed * 15, 0.0f);
        }

        playerTransform.rotation += new Vector3(0.0f, -Input.MouseDeltaX() * 0.1f, 0.0f);
        cameraTransform.rotation += new Vector3(0.0f, 0.0f, -Input.MouseDeltaY() * 0.1f);
    }

    public void OnRestart()
    {

    }
}
