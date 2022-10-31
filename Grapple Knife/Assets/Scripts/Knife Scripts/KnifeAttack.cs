using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeAttack : MonoBehaviour
{
    public LayerMask killableLayer;
    private bool cooldownDone = true;
    public Material knifeIndicatorMat;
    public Material knifeMat;

    [Header("Knife Vars")]
    public float attackWindow;
    public float attackCooldown;
    public float bonusTime;

    public GameManager gm;
    [SerializeField] CountdownTimer cdTimer;

    void Update()
    {
        if (gm.gameIsActive)
        {
            if ((Input.GetMouseButtonDown(1)) && cooldownDone == true)
            {
                StartAttack();
                AttackRecovery();
            }

        }
    }
    // detects if a collission happens with a certain tag, then destroys the knife box collission collided with
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Killable")
        {
            //  Debug.Log("Contacted Killable");
            //remove from knife, add to boxes
            cdTimer.AddTImeToTimer(bonusTime);
            Destroy(other.gameObject);
        }
    }

    void StartAttack()
    {
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<MeshRenderer>().material = knifeIndicatorMat;

        // starts both timers
        StartCoroutine(AttackWindow());
    }

    void AttackRecovery()
    {
        cooldownDone = false;
        StartCoroutine(AttackCooldown());
    }

    #region Enumerators

    // Starts timer for how long the hurtbox is open for
    IEnumerator AttackWindow()
    {
        yield return new WaitForSeconds(attackWindow);
        GetComponent<BoxCollider>().enabled = false;
    }

    // Starts cooldown to prevent spamming of attacks
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);

        GetComponent<MeshRenderer>().material = knifeMat;
        cooldownDone = true;
    }

    #endregion
}
