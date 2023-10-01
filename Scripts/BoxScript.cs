
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plaza;
using static Plaza.InternalCalls;
using static Plaza.Input;

public class BoxScript : Entity
{
    Entity player;
    public void OnStart()
    {
        player = FindEntityByName("Player");
    }

    public void OnUpdate()
    {
        if (player != null)
        {
            if (Vector3.Distance(player.GetComponent<Transform>().Translation, this.GetComponent<Transform>().Translation) < 10.0f && Input.IsKeyDown(KeyCode.F))
            {
                Console.WriteLine("Pressing F close to box");
            }
        }
    }

    public void OnRestart()
    {

    }
}
