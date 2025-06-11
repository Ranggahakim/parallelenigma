using System.Collections;
using UnityEngine;

public class TurnBaseCharacter : MonoBehaviour
{
    public string string_nama;
    public int int_hp;
    public int int_atkDmg;

    public CharacterScriptable myScriptable;
    public Animator myAnimator;

    public int uniqueCode;

    void Start()
    {
        if (uniqueCode == 0)
        {
            Debug.Log($"Enemy : {this.gameObject.name} has no uniqueCode");
        }

        if (myScriptable != null)
        {
            string_nama = myScriptable.string_nama;
            int_hp = myScriptable.int_hp;
            int_atkDmg = myScriptable.int_atkDmg;
        }
    }

    public void SetHP(int int_dmg)
    {
        int_hp -= int_dmg;

        if (myAnimator != null)
            myAnimator.SetBool("isGetDamage", true);
        StartCoroutine(ResetAnim());
    }

    public void AttackTarget(TurnBaseCharacter target)
    {
        target.SetHP(int_atkDmg);
        myAnimator.SetBool("isAttack", true);
        StartCoroutine(ResetAnim());
    }

    IEnumerator ResetAnim()
    {
        yield return new WaitForSeconds(1f);
        myAnimator.SetBool("isAttack", false);
        myAnimator.SetBool("isGetDamage", false);
    }
}
