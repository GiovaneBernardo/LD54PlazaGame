
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plaza;
using static Plaza.InternalCalls;
using static Plaza.Input;
using System.Runtime.Remoting.Messaging;

public class PlayerController : Entity
{
    public bool holdingBox = false;
    public int coins = 0;
    public int seconds = 60;
    public bool isControllingTruck = false;
    public float speed = 15.0f;
    public float sensitivity = 20.0f;
    private int ticks = 0;
    Transform playerTransform;
    Transform cameraTransform;
    Entity truck;
    public void OnStart()
    {
        playerTransform = FindEntityByName("Player").GetComponent<Transform>();
        cameraTransform = FindEntityByName("Camera").GetComponent<Transform>();
        truck = FindEntityByName("UN Truck 1");
        Cursor.Hide();
    }

    public void OnUpdate()
    {
        if (truck.GetScript<CarScript>().playerIsControlling == false)
        {
            if (Input.IsKeyDown(KeyCode.W))
            {
                float oldY = playerTransform.Translation.Y;
                playerTransform.MoveTowards(new Vector3(0.0f, 0.0f, 1.0f * speed * Time.deltaTime));
                playerTransform.Translation = new Vector3(playerTransform.Translation.X, oldY, playerTransform.Translation.Z);
            }
            if (Input.IsKeyDown(KeyCode.S))
            {
                playerTransform.MoveTowards(new Vector3(0.0f, 0.0f, -1.0f * speed * Time.deltaTime));
            }
            if (Input.IsKeyDown(KeyCode.A))
            {
                playerTransform.MoveTowards(new Vector3(-1.0f * speed * Time.deltaTime, 0.0f, 0.0f));
            }
            if (Input.IsKeyDown(KeyCode.D))
            {
                playerTransform.MoveTowards(new Vector3(1.0f * speed * Time.deltaTime, 0.0f, 0.0f));
            }
            if (Input.IsKeyDown(KeyCode.Space) && ticks > 300)
            {
                ticks = 0;
                GetComponent<RigidBody>().ApplyForce(new Vector3(0.0f, 30000.0f, 0.0f));
               // playerTransform.Translation += new Vector3(0.0f, 3.0f * speed * 15 * Time.deltaTime, 0.0f);
            }

            playerTransform.rotation += new Vector3(0.0f, -Input.MouseDeltaX() * sensitivity * Time.deltaTime, 0.0f);
            cameraTransform.rotation += new Vector3(0.0f, 0.0f, -Input.MouseDeltaY() * sensitivity * Time.deltaTime);

            if (Input.IsKeyDown(KeyCode.R))
            {
                GetComponent<Transform>().Translation = new Vector3(110.921f, -56.677f, 5.224f);
                GetComponent<Transform>().rotation = new Vector3(0.0f);
            }
        }
        ticks++;
    }

    public void OnRestart()
    {

    }
}
