
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plaza;
using static Plaza.InternalCalls;
using static Plaza.Input;

public class CarScript : Entity
{
    // Ticks to prevent player from entering and exiting the truck
    int ticks = 0;
    Transform transform;
    Entity player;
    Entity camera;
    bool playerIsControlling = false;
    public void OnStart()
    {
        transform = GetComponent<Transform>();
        player = FindEntityByName("Player");
        camera = FindEntityByName("Camera");
    }

    public void OnUpdate()
    {
        if (!playerIsControlling && Vector3.Distance(player.GetComponent<Transform>().Translation, transform.Translation) < 20.0f && Input.IsKeyDown(KeyCode.Q) && ticks > 60)
        {
            ticks = 0;
            playerIsControlling = true;
            player.parent = this;
            player.GetComponent<Transform>().Translation = new Vector3(0.0f, 5.0f, 0.0f);
            player.GetComponent<Transform>().rotation = new Vector3(90.0f, 0.0f, 90.0f);
            camera.GetComponent<Transform>().rotation = new Vector3(0.0f);
            camera.GetComponent<Transform>().Translation = new Vector3(-15.4f, 2.459f, 0.0f);
        }

        if (playerIsControlling && Input.IsKeyDown(KeyCode.Q) && ticks > 60)
        {
            ticks = 0;
            playerIsControlling = false;
            player.parent = new Entity(0);
            player.GetComponent<Transform>().Translation = transform.Translation + new Vector3(3.0f, 0.0f, 0.0f);
            player.GetComponent<Transform>().rotation = new Vector3(0.0f);
            camera.GetComponent<Transform>().rotation = new Vector3(0.0f);
            camera.GetComponent<Transform>().Translation = new Vector3(0.0f, 1.759f, 0.0f);
        }
        ticks++;
    }

    public void OnRestart()
    {

    }
}
