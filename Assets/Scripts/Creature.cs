using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
[RequireComponent(typeof(Collider))]
public class Creature : MonoBehaviour
{

    public int Health = 3, StartHealth = 3;
    public bool isDark = false, canBeHurt = false;
    public Renderer mesh, altMesh;

    public void Revive()
    {
        Health = StartHealth;
        QuickInvicibility();
    }

    public void Die() { gameObject.SetActive(false); }

    public void Hurt()
    {
        Health--;
        if (Health <= 0) Die();
        Tween.LocalPosition(transform, transform.position + (-transform.forward * Random.Range(1, 5)), .2f, 0);
        QuickInvicibility();
    }

    public void QuickInvicibility() => StartCoroutine(IQuickInvicibility());
    IEnumerator IQuickInvicibility()
    {
        canBeHurt = false;
        Material defMaterial = (isDark ? altMesh : mesh).material;
        for (int i = 0; i < 30; i++)
        {
            (isDark ? altMesh : mesh).material = LevelManager.instance.HurtMaterial;
            yield return new WaitForSeconds(.1f);
            (isDark ? altMesh : mesh).material = defMaterial;
        }
        canBeHurt = true;
    }



}
