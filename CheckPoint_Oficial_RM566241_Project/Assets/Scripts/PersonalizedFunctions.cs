using Unity.VisualScripting;
using UnityEngine;

public class PersonalizedFunctions : MonoBehaviour
{
    public GameObject Character;
    public Animator animator;

  
    void Start()
    {
        
    }


    void Update()
    {
        if(Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);

        MakeCharacterBigger();



    }


    public void MakeCharacterBigger()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            Vector3 scaleCharacter =  Character.transform.localScale;
            Character.transform.localScale = scaleCharacter + new Vector3(+0.1f, +0.1f, +0.1f);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 scaleCharacter = Character.transform.localScale;
            Character.transform.localScale = scaleCharacter + new Vector3(-0.1f, -0.1f, -0.1f);
        }
    }

    [ContextMenu("Idle")]
    public void MakeCharacterIdle()
    {
        animator.SetBool("IsIdle", true);
        animator.SetBool("IsDancing", false);
        animator.SetBool("IsWalking", false);
    }

    [ContextMenu("Dance")]
    public void MakeCharacterDance()
    {
        animator.SetBool("IsDancing", true);
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsWalking", false);
    }

    [ContextMenu("Walk")]
    public void MakeCharacterWalk()
    {
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsDancing", false);
    }

    void SetObjectOff()
    {
        
        Character.SetActive(false);
    }

    void SetObjectOn()
    {
      
        Character.SetActive(true);
    }

}
