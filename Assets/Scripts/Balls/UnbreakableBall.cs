using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Balls
{
    public class UnbreakableBall : MonoBehaviour, ISpecialBall
    {
        private float timeToLive = 0;
        private float timeCreated = 0;
        private Floor floor;
        GameObject parent_go;
        int amountOfHitsLeft = 0;

        public void Initialize(float _timeToLive, float _timeCreated, GameObject _parent_go)
        {
            //Ignore time to live, this ball lives 20 seconds or 10 hits
            timeToLive = 20f;
            amountOfHitsLeft = 10;
            timeCreated = _timeCreated;
            parent_go = _parent_go;
            parent_go.name = "UnbreakableBall";

            GameObject floor_go = GameObject.FindGameObjectWithTag("Floor");
            if (floor_go != null)
                floor = floor_go.GetComponent<Floor>();

            if (floor == null)
                Debug.LogError("UnbreakableBall can't find Floor!");
        }

        public void CheckLifeTimer()
        {
            float timeLived = Time.time - timeCreated;
            //Agregar visualmente el tiempo de vida de la bola
            //Debug.Log("TimeLived: " + timeLived);
            if (timeLived > timeToLive)
                Destroy(this.parent_go);            
        }

        public bool MakeSpeciality(Color _realColor)
        {
            amountOfHitsLeft--;
            if (amountOfHitsLeft <= 0)
            {
                floor.CreateNewBalls(1);
                Ball ballScript = parent_go.GetComponent<Ball>();
                if (ballScript != null)
                    floor.AddExternalPoints(ballScript.points);
                return true;
            }
            else
                return false;
        }
    }
}
