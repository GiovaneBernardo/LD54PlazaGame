
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
    bool beingHold = false;
    public void OnStart()
    {
        player = FindEntityByName("Player");
    }

    public void OnUpdate()
    {
        if (player != null)
        {
            if (!beingHold && Vector3.Distance(player.GetComponent<Transform>().Translation, this.GetComponent<Transform>().Translation) < 10.0f && Input.IsKeyDown(KeyCode.F))
            {
                beingHold = true;
                parent = player;
                GetComponent<Transform>().Translation = new Vector3(1.0f, 0.5f, 0.3f);
            }

            if(beingHold && Input.IsKeyDown(KeyCode.G)){
                beingHold = false;
                parent = new Entity(0);
                GetComponent<Transform>().Translation = player.GetComponent<Transform>().Translation + new Vector3(1.0f, 0.5f, 0.3f);
            }
        }
    }

    public void OnRestart()
    {

    }
}
