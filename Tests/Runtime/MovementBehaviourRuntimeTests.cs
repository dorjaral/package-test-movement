using MiCompa.Movement;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class MovementBehaviourRuntimeTests : MonoBehaviour
{
    [UnityTest]
    public IEnumerator MoveInDirectionTest()
    {
        GameObject movementTester = new GameObject();
        MovementBehaviour movement = movementTester.AddComponent<MovementBehaviour>();
        Rigidbody movementTesterRigidbody = movementTester.GetComponent<Rigidbody>();
        const float acceleration = 100f;
        movement.Acceleration = acceleration;
        movement.MaxSpeed = 1000f;
        Vector3 movementDirection = new Vector3(0f, 0f, 1f);
        movement.MoveInDirection(movementDirection);
        movementTesterRigidbody.useGravity = false;
        yield return new WaitForFixedUpdate();

        var actualVelocity = movementTesterRigidbody.velocity;
        float fixedTimeStep = 0.02f;
        Vector3 expectedVelocity = acceleration * fixedTimeStep * movementDirection;
        Assert.AreEqual(expectedVelocity, actualVelocity);
    }
}
