using UnityEngine;
using System.Collections;

public class FreezeBall : MonoBehaviour, ISpecialBall
{
    private float timeToLive = 0;
    private float timeCreated = 0;
    private Floor floor;
    GameObject parent_go;

    public void Initialize(float _timeToLive, float _timeCreated, GameObject _parent_go)
    {
        timeToLive = _timeToLive;
        timeCreated = _timeCreated;
        parent_go = _parent_go;
        parent_go.name = "FreezeBall";

        GameObject floor_go = GameObject.FindGameObjectWithTag("Floor");
        if (floor_go != null)
            floor = floor_go.GetComponent<Floor>();

        if (floor == null)
            Debug.LogError("FreezeBall can't find Floor!");
    }

    public void CheckLifeTimer()
    {
        float timeLived = Time.time - timeCreated;
        //Agregar visualmente el tiempo de vida de la bola
        //Debug.Log("TimeLived: " + timeLived);
        if (timeLived > timeToLive)
        {
            Destroy(this.parent_go);
        }
    }

    public bool MakeSpeciality(Color _realColor)
    {
        GameObject timer_GO = GameObject.FindGameObjectWithTag("Timer");

        if (timer_GO != null)
        {
            Timer timerScript = timer_GO.GetComponent<Timer>();
            if (timerScript != null)
            {
                timerScript.Freeze();
            }
        }
        return true;
    }
}
