
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plaza;
using static Plaza.InternalCalls;
using static Plaza.Input;

public class BoxManager : Entity
{
    public float GetRandomNumber(int min, int max)
    {
        Random random = new Random();
        float randomNumber = random.Next(min, max);
        return (float)random.NextDouble();
    }
    public void SpawnBoxes()
    {
        Entity ent = FindEntityByName("Boxe");
        Instantiate(ent).GetComponent<Transform>().Translation = new Vector3(GetRandomNumber(-50, 50) * 40, GetRandomNumber(20, 30), GetRandomNumber(-50, 50) * 40);
        Instantiate(ent).GetComponent<Transform>().Translation = new Vector3(GetRandomNumber(-50, 50) * 40, GetRandomNumber(20, 30), GetRandomNumber(-50, 50) * 40);
        Instantiate(ent).GetComponent<Transform>().Translation = new Vector3(GetRandomNumber(-50, 50) * 40, GetRandomNumber(20, 30), GetRandomNumber(-50, 50) * 40);
    }

    public void OnStart()
    {

    }


    public void OnUpdate()
    {
    }

    public void OnRestart()
    {

    }
}
