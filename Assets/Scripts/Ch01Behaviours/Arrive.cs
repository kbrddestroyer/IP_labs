using UnityEngine;
using System.Collections;

public class Arrive : AgentBehaviour
{
    public float targetRadius;              // минимальный радиус поимки
    public float slowRadius;                // максимальный радиус в котором фиксирована скорость
    public float timeToTarget = 0.1f;
 
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        Vector3 direction = target.transform.position - transform.position;
        float distance = direction.magnitude;
        float targetSpeed;
        
        if (distance < targetRadius)        // догнал
            return steering;
        if (distance > slowRadius)          // гонится но с маленькой скоростью
            targetSpeed = agent.maxSpeed;
        else
            targetSpeed = agent.maxSpeed * distance / slowRadius; // х*ярит на бензине
        
        Vector3 desiredVelocity = direction;                // применение скорости к объекту
        desiredVelocity.Normalize();
        desiredVelocity *= targetSpeed;
        steering.linear = desiredVelocity - agent.velocity;
        steering.linear /= timeToTarget;
        if (steering.linear.magnitude > agent.maxAccel)
        {
            steering.linear.Normalize();
            steering.linear *= agent.maxAccel;
        }
        return steering;
    }
}
