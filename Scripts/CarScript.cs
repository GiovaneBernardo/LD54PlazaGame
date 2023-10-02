
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plaza;
using static Plaza.InternalCalls;
using static Plaza.Input;

public class Box {
    public string name = "Box";
    public int value = 1;
}



public class CarScript : Entity
{
    public List<string> boxesNames = new List<string>();
    public float GetRandomNumber(int min, int max)
    {
        Random random = new Random();
        float randomNumber = random.Next(min, max);
        return (float)random.NextDouble();
    }

    public void SpawnBoxes(int count)
    {
        Entity ent = FindEntityByName("Boxe");
        for(int i = 0; i < count; i++)
        {
            Instantiate(ent).GetComponent<Transform>().Translation = new Vector3(GetRandomNumber(-50, 50) * 40, GetRandomNumber(20, 30), GetRandomNumber(-50, 50) * 40);
        }
        foreach(string boxName in boxesNames)
        {
            Console.WriteLine(boxName);
            Entity newEnt = Instantiate(FindEntityByName(boxName));
            newEnt.parent = FindEntityByName("BoxesContainer");
            newEnt.GetComponent<Transform>().Translation = new Vector3(GetRandomNumber(-50, 50) * 40, GetRandomNumber(20, 30), GetRandomNumber(-50, 50) * 40);
            newEnt.RemoveComponent<Collider>();
            newEnt.RemoveComponent<RigidBody>();
            newEnt.AddComponent<Collider>().AddShape(ColliderShapeEnum.BOX);
            newEnt.AddComponent<RigidBody>();
            
        }
    }

    // Ticks to prevent player from entering and exiting the truck
    public List<Entity> boxes = new List<Entity>();
    public float speed = 50.3f;
    int ticks = 0;
    Transform transform;
    Entity player;
    Entity camera;
    Entity sellingPoint;
    public bool playerIsControlling = false;
    public void OnStart()
    {
        transform = GetComponent<Transform>();
        player = FindEntityByName("Player");
        camera = FindEntityByName("Camera");
        sellingPoint = FindEntityByName("sellingHouse");
        boxesNames.Add("bananaBox");
        boxesNames.Add("wineBox");
        boxesNames.Add("fertilizerBox");
        boxesNames.Add("gunsBox");
        boxesNames.Add("jewelryBox");
        boxesNames.Add("meatBox");
        boxesNames.Add("orangeBox");
        boxesNames.Add("potatoBox");
        boxesNames.Add("rubyBox");
        boxesNames.Add("wristwatchBox");
        boxesNames.Add("goldBox");
        boxesNames.Add("diamondBox");
        SpawnBoxes(12);
    }

    public void OnUpdate()
    {
        if (!playerIsControlling && Vector3.Distance(player.GetComponent<Transform>().Translation, transform.Translation) < 8.0f && Input.IsKeyDown(KeyCode.Q) && ticks > 60)
        {
            ticks = 0;
            playerIsControlling = true;
            player.parent = this;
            player.GetComponent<Transform>().Translation = new Vector3(0.0f, 5.0f, 0.0f);
            player.GetComponent<Transform>().rotation = new Vector3(90.0f, 0.0f, 90.0f);
            camera.GetComponent<Transform>().rotation = new Vector3(0.0f);
            camera.GetComponent<Transform>().Translation = new Vector3(-15.4f, 2.459f, 0.0f);
            player.GetScript<PlayerController>().isControllingTruck = true;
            player.RemoveComponent<Collider>();
            player.RemoveComponent<RigidBody>();
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
            player.GetScript<PlayerController>().isControllingTruck = false;
            player.AddComponent<Collider>();
            player.GetComponent<Collider>().AddShape(ColliderShapeEnum.BOX);
            player.AddComponent<RigidBody>();
            player.GetComponent<RigidBody>().LockAngular(Axis.X, true);
            player.GetComponent<RigidBody>().LockAngular(Axis.Y, true);
            player.GetComponent<RigidBody>().LockAngular(Axis.Z, true);
        }
        ticks++;

        // Control the car
        if (playerIsControlling)
        {
            if (Input.IsKeyDown(KeyCode.W) && transform.Translation.Y < 1.0f)
            {
                float oldY = GetComponent<Transform>().Translation.Y;
                GetComponent<Transform>().MoveTowards(new Vector3(0.0f, 1.0f * speed * Time.deltaTime, 0.0f));
                GetComponent<Transform>().Translation = new Vector3(GetComponent<Transform>().Translation.X, oldY, GetComponent<Transform>().Translation.Z);
            }
            if (Input.IsKeyDown(KeyCode.S) && transform.Translation.Y < 1.0f)
            {
                GetComponent<Transform>().MoveTowards(new Vector3(0.0f, -1.0f * speed * Time.deltaTime, 0.0f));
            }
            if (Input.IsKeyDown(KeyCode.A))
            {
                GetComponent<Transform>().rotation += new Vector3(0.0f, 1.1f * speed * Time.deltaTime, 0.0f);
            }
            if (Input.IsKeyDown(KeyCode.D))
            {
                GetComponent<Transform>().rotation += new Vector3(0.0f, -1.1f * speed * Time.deltaTime, 0.0f);
            }

            if (Input.IsKeyDown(KeyCode.F))
            {
                GetComponent<Transform>().rotation = new Vector3(-90.0f, 0.0f, 0.0f);
            }

            if(Vector3.Distance(transform.Translation, sellingPoint.GetComponent<Transform>().Translation) < 12.0f && boxes.Count > 0)
            {
                int boxesValue = 0;
                foreach(Entity box in boxes)
                {
                    boxesValue += box.GetScript<BoxScript>().box.value;
                    box.Delete();
                }
                player.GetScript<PlayerController>().coins += boxesValue;
                boxes.Clear();

                SpawnBoxes(3);
            }
        }
        if (Input.IsKeyDown(KeyCode.R))
        {
            GetComponent<Transform>().Translation = new Vector3(110.921f, -60.677f, 5.224f);
            GetComponent<Transform>().rotation = new Vector3(-90.0f, 0.0f, 0.0f);
        }
    }

    public void OnRestart()
    {

    }
}
