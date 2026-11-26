using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerVisualEffects : MonoBehaviour
{
    [Header("Spaceship Model")]
    public Transform spaceshipModel;

    [Header("Movement Rotation")]
    public float bankAngle = 25f;
    public float pitchAngle = 14f;
    public float rotationSmoothness = 6f;

    [Header("Direction")]
    public float turnSpeed = 10f;

    [Header("Shooting Recoil")]
    public float recoilAngle = 12f;
    public float recoilDuration = 0.2f;

    private Quaternion originalRotation;
    private Quaternion targetDirectionRotation;

    private float currentRecoil;

    private float lastHorizontal;
    private float lastVertical = 1f;

    private bool aimingAtMouse;

    private void Start()
    {
        if (spaceshipModel != null)
        {
            originalRotation = spaceshipModel.localRotation;
            targetDirectionRotation = originalRotation;
        }
    }

    private void Update()
    {
        if (spaceshipModel == null || Keyboard.current == null)
            return;

        float horizontal = 0f;
        float vertical = 0f;

        if (Keyboard.current.aKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed)
            horizontal = -1f;

        if (Keyboard.current.dKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed)
            horizontal = 1f;

        if (Keyboard.current.wKey.isPressed ||
            Keyboard.current.upArrowKey.isPressed)
            vertical = 1f;

        if (Keyboard.current.sKey.isPressed ||
            Keyboard.current.downArrowKey.isPressed)
            vertical = -1f;

        if (horizontal != 0f || vertical != 0f)
        {
            lastHorizontal = horizontal;
            lastVertical = vertical;
            aimingAtMouse = false;
        }

        if (!aimingAtMouse)
        {
            float directionRotation = 0f;

            if (lastVertical < 0f)
                directionRotation = 180f;
            else if (lastHorizontal < 0f)
                directionRotation = -90f;
            else if (lastHorizontal > 0f)
                directionRotation = 90f;

            float bank = -lastHorizontal * bankAngle;
            float pitch = lastVertical * pitchAngle;

            targetDirectionRotation =
                originalRotation *
                Quaternion.Euler(pitch, directionRotation, bank);
        }

        Quaternion recoilRotation =
            Quaternion.Euler(currentRecoil, 0f, 0f);

        Quaternion finalRotation =
            targetDirectionRotation * recoilRotation;

        spaceshipModel.localRotation = Quaternion.Slerp(
            spaceshipModel.localRotation,
            finalRotation,
            turnSpeed * Time.deltaTime
        );
    }

    public void AimAtDirection(Vector3 direction)
    {
        if (direction.sqrMagnitude < 0.001f)
            return;

        direction.y = 0f;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.x, direction.z)
                      * Mathf.Rad2Deg;

        aimingAtMouse = true;

        targetDirectionRotation =
            originalRotation *
            Quaternion.Euler(0f, angle, 0f);
    }

    public void PlayShootingRecoil()
    {
        StopCoroutine(nameof(RecoilRoutine));
        StartCoroutine(RecoilRoutine());
    }

    private IEnumerator RecoilRoutine()
    {
        currentRecoil = recoilAngle;

        yield return new WaitForSeconds(recoilDuration);

        currentRecoil = 0f;
    }
}