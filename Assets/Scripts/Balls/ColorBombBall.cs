using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Balls
{
    public class ColorBombBall : MonoBehaviour, ISpecialBall
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
            parent_go.name = "ColorBombBall";

            GameObject floor_go = GameObject.FindGameObjectWithTag("Floor");
            if (floor_go != null)
                floor = floor_go.GetComponent<Floor>();

            if (floor == null)
                Debug.LogError("ColorBombBall can't find Floor!");
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
            GameObject[] ballsOfSameColor = GameObject.FindGameObjectsWithTag("Ball");

            int amountOfPointsToAdd = 0;
            int ballsDestroyed = 0;
            foreach (GameObject ball in ballsOfSameColor)
            {
                Ball ballScript = ball.GetComponent<Ball>();
                if (ballScript != null && !ballScript.specialBall && ballScript.realColor == _realColor)
                {
                    amountOfPointsToAdd += ballScript.points;
                    ballsDestroyed++;
                    ballScript.DestroyYourself();                    
                }

            }
            floor.CreateNewBalls(ballsDestroyed);
            floor.AddExternalPoints(amountOfPointsToAdd);

            return true;
        }
    }
}
