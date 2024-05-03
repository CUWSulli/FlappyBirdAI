using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdScript : MonoBehaviour
{
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private PipeSpawner pipehandler = null;
    [SerializeField] private Transform bodyTransform = null;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxVelocity = 5f;
    [SerializeField] private float rewardPoints;
    private Vector3 startingPosition;

    //Initialize ML Agents
    public void Initialize()
    {
        startingPosition = transform.position;
    }

    //Start new generation of flappys by resetting position and pipes and beginning again
    public void OnEpisodeBegin()
    {
        transform.position = startingPosition;
        rb.velocity = Vector3.zero;

        pipehandler.ResetPipes();
    }

    private void EndEpisode()
    {
        OnEpisodeBegin();
    }
    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);   
    }

    private void OnActionRecieved(float[] vectorAction)
    {
        //Reward the bird every frame for surviving
        AddReward(0.1f);
        if (Mathf.FloorToInt(vectorAction[0]) != 1)
        {
            return;
        }
        Jump();
    }

    private void OnTriggerEnter(Collider other) 
    {
        //Punish the AI by subrtacting points when it dies
        AddReward(-1.0f);
        EndEpisode();
    }

    private void Update()
    {
        //Rotate bird to indicate when its falling or gaining height
        bodyTransform.rotation = Quaternion.LookRotation(rb.velocity + new Vector3(10f, 0f, 0f), transform.up);
    }

    private void AddReward(float reward)
    {
        rewardPoints = rewardPoints + reward;
    }

}
