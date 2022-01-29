using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class Player : Creature
{

    Rigidbody rigid;
    public Animator LightChar, DarkChar;
    public Rigidbody LightBody;
    // Animator animator;
    Vector2 jDir = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // animator.SetFloat("MoveRun", jDir.magnitude);
        if (jDir.magnitude > .1f)
        {
            Vector3 CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            rigid.AddForce((jDir.y * CamForward + jDir.x * Camera.main.transform.right) * (8 * jDir.magnitude), ForceMode.Force);
        }
    }

    public void ThrowLight()
    {
        LightBody.transform.parent = null;
        LightBody.AddForce((transform.forward * hj 1) + (transform.up * 5), ForceMode.Impulse);
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "PlayerLightEmitter")
        {
            if (LightBody != null) return;
            LightBody = other.gameObject.GetComponent<Rigidbody>();
            LightBody.transform.parent = this.transform;
            LightBody.transform.position = ((transform.position) + (transform.forward * 2));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
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
    public void OnJump() => rigid.AddForce(Vector3.up * 8, ForceMode.Impulse);
    public void OnThrow() => ThrowLight();
    #endregion
}
