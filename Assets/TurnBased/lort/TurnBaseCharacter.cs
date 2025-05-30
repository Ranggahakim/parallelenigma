using UnityEngine;

public class TurnBaseCharacter : MonoBehaviour
{
    public string string_nama;
    public int int_hp;
    public int int_atkDmg;

    public CharacterScriptable myScriptable;
    public Animator myAnimator;

    void Start()
    {
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
            myAnimator.SetTrigger("isGetDamage");
    }

    public void AttackTarget(TurnBaseCharacter target)
    {
        target.SetHP(int_atkDmg);
        myAnimator.SetTrigger("isAttack");
    }
}
