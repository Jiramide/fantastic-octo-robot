using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player Constants")]
    public float WalkUnitsPerDeltaTime = 16.0f;
    public float SprintUnitsPerDeltaTime = 24.0f;
    public float ArmLength = 0.5f;
    public float MaxSprintUnits = 10.0f;
    public float SprintDegenerationRate = 2.0f;
    public float SprintRegenerationRate = 4.0f;

    [Header("Player State")]
    public float CurrentSprintUnits;
    public bool AllowedToSprint = true;

    private Rigidbody PlayerRigidbody;

    void Start()
    {
        CurrentSprintUnits = MaxSprintUnits;
        PlayerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(
            horizontal,
            0.0f,
            vertical
        );

        float speedModifer = WalkUnitsPerDeltaTime;

        if (Input.GetButton("Fire3") && AllowedToSprint)
        {
            if (CurrentSprintUnits > 0.0f)
            {
                CurrentSprintUnits -= SprintDegenerationRate * Time.deltaTime;
                speedModifer = SprintUnitsPerDeltaTime;
            }
            else
            {
                AllowedToSprint = false;
            }
        }
        else
        {
            CurrentSprintUnits = Mathf.Min(
                CurrentSprintUnits + SprintRegenerationRate * Time.deltaTime,
                MaxSprintUnits
            );

            if (CurrentSprintUnits == MaxSprintUnits)
            {
                AllowedToSprint = true;
            }
        }

        Debug.Log(speedModifer);

        PlayerRigidbody.MovePosition(PlayerRigidbody.position + moveDirection.normalized * speedModifer * Time.deltaTime);
    }

    void FixedUpdate()
    {

    }
}
