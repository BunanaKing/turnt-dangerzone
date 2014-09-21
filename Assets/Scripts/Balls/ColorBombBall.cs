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
        public void Initialize(float _timeToLive)
        {
            timeToLive = _timeToLive;
        }
        public void CheckLifeTimer(float _timeCreated)
        {
            float timeLived = Time.time - _timeCreated;
            //Agregar visualmente el tiempo de vida de la bola
            //Debug.Log("TimeLived: " + timeLived);
            if (timeLived > timeToLive)
            {
                Destroy(this.gameObject);
            }
        }

        public void MakeSpecialty(Floor _floor, Color _realColor)
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
            _floor.CreateNewBalls(ballsDestroyed);
            _floor.AddExternalPoints(amountOfPointsToAdd);
        }
    }
}
