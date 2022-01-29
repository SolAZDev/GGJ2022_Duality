using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.InputSystem;

// [RequireComponent(typeof(CapsuleCollider))]
// [RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class Player : Creature
{

    CharacterController cc;
    public Animator LightChar, DarkChar;
    public Rigidbody LightBody;
    // Animator animator;
    Vector2 jDir = Vector2.zero;
    Vector3 mDir = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // animator.SetFloat("MoveRun", jDir.magnitude);
        Vector3 CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 joy = (jDir.y * CamForward + jDir.x * Camera.main.transform.right) * (8 * jDir.magnitude);
        mDir = new Vector3(joy.x, mDir.y, joy.z);
        mDir.y += Physics.gravity.y * 2 * Time.deltaTime; //Oops there goes Gravity
        if (jDir.magnitude > 0.2f) Tween.LocalRotation(transform, Quaternion.LookRotation(new Vector3(mDir.x, 0, mDir.z), Vector3.up).eulerAngles, .1f, 0);
        cc.Move(mDir * Time.deltaTime);
    }

    public void ThrowLight()
    {
        if (LightBody == null) return;
        LightBody.transform.parent = null;
        LightBody.isKinematic = false;
        LightBody.AddForce((transform.forward * 15) + (transform.up * 5), ForceMode.Impulse);
        print("Y E E T");
        LightBody = null;
    }

    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        print(other.transform.tag);
        if (other.transform.tag == "PlayerLightEmitter")
        {
            if (LightBody != null) return;
            LightBody = other.gameObject.GetComponent<Rigidbody>();
            LightBody.transform.parent = this.transform;
            LightBody.isKinematic = true;
            LightBody.transform.position = ((transform.position) + (transform.forward * 2));
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        print("Huh? What the flip, " + other.tag);
        if (other.tag == "PlayerLight") SwitchLayers(false);
        if (other.tag == "LightArea" && isDark) Hurt();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerLight") SwitchLayers(true);
    }


    public void SwitchLayers(bool enableDark)
    {
        print("Switching");
        isDark = enableDark;
        LightChar.gameObject.SetActive(!isDark);
        DarkChar.gameObject.SetActive(isDark);
        // animator = isDark ? DarkChar : LightChar;
        //Play Effect
        LevelManager.instance.ToggleWorld(isDark);
    }


    #region Input System
    public void OnMove(InputValue dir) => jDir = dir.Get<Vector2>();
    // public void OnJump() { if (cc.isGrounded) { mDir = new Vector3(mDir.x, mDir.y + 8, mDir.z); } }
    public void OnJump()
    {
        float up = 8;
        print("Jumping?" + cc.isGrounded + ":" + up);
        if (cc.isGrounded) { mDir.y = up; }
    }
    public void OnThrow() => ThrowLight();
    #endregion
}
