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

        void Start()
        {
            GameObject floor_go = GameObject.FindGameObjectWithTag("Floor");
            if (floor_go != null)
                floor = floor_go.GetComponent<Floor>();

            if (floor == null)
                Debug.LogError("ColorBombBall can't find Floor!");

        }

        public void Initialize(float _timeToLive, float _timeCreated)
        {
            timeToLive = _timeToLive;
            timeCreated = _timeCreated;
        }

        public void CheckLifeTimer()
        {
            float timeLived = Time.time - timeCreated;
            //Agregar visualmente el tiempo de vida de la bola
            //Debug.Log("TimeLived: " + timeLived);
            if (timeLived > timeToLive)
            {
                Destroy(this.gameObject);
            }
        }

        public void MakeSpecialty(Color _realColor)
        {
            GameObject[] ballsOfSameColor = GameObject.FindGameObjectsWithTag("Ball");

            int amountOfPointsToAdd = 0;
            int ballsDestroyed = 0;
            foreach (GameObject ball in ballsOfSameColor)
            {
                Ball ballScript = ball.GetComponent<Ball>();
                if (ballScript != null && ballScript.realColor == _realColor)
                {
                    amountOfPointsToAdd += ballScript.points;
                    ballsDestroyed++;
                    Destroy(ball);
                }

            }
            floor.CreateNewBalls(ballsDestroyed);
            floor.AddExternalPoints(amountOfPointsToAdd);
        }
    }
}
