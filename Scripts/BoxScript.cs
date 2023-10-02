
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
    public Box box = new Box();
    Entity truck;
    Entity player;
    bool beingHold = false;
    public void OnStart()
    {
        player = FindEntityByName("Player");
        truck = FindEntityByName("UN Truck 1");
    }

    public void OnUpdate()
    {
        if (player != null && this.Uuid != 0)
        {
            if (!beingHold && Vector3.Distance(player.GetComponent<Transform>().Translation, this.GetComponent<Transform>().Translation) < 2.0f && Input.IsKeyDown(KeyCode.F))
            {
                if (!player.GetScript<PlayerController>().holdingBox)
                {
                    player.GetScript<PlayerController>().holdingBox = true;
                    beingHold = true;
                    parent = player;
                    GetComponent<Transform>().Translation = new Vector3(1.0f, 0.5f, 0.3f);
                    RemoveComponent<Collider>();
                    RemoveComponent<RigidBody>();
                }
            }
            if (beingHold && Input.IsKeyDown(KeyCode.G) && player.GetScript<PlayerController>().holdingBox)
            {
                beingHold = false;
                player.GetScript<PlayerController>().holdingBox = false;
                if (Vector3.Distance(player.GetComponent<Transform>().Translation, truck.GetComponent<Transform>().Translation) < 7.0f && truck.GetScript<CarScript>().boxes.Count < 3)
                {
                    parent = truck;
                    GetComponent<Transform>().Translation = new Vector3(0.0f, -(truck.GetScript<CarScript>().boxes.Count + 0.3f * truck.GetScript<CarScript>().boxes.Count), 0.3f);
                    truck.GetScript<CarScript>().boxes.Add(this);
                } else
                {
                    parent = new Entity(0);
                    GetComponent<Transform>().Translation = player.GetComponent<Transform>().Translation + new Vector3(1.0f, 0.5f, 0.3f);
                    AddComponent<Collider>();
                    GetComponent<Collider>().AddShape(ColliderShapeEnum.BOX);
                    AddComponent<RigidBody>();
                }
            }
        }
    }

    public void OnRestart()
    {

    }
}
