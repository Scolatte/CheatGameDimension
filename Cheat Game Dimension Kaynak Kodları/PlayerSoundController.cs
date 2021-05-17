using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeftFootSound()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("LeftFootSound");
    }

    public void RightFootSound()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("RightFootSound");
    }

    public void NormalJumpSFX()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("Jump");
        GameObject.FindObjectOfType<AudioManager>().Play("JumpVoice");
    }

    public void BoostJumpSFX()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("BoostJump");
        GameObject.FindObjectOfType<AudioManager>().Play("BoostJumpVoice");
    }

    public void DashSFX()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("Dash");
    }

    public void EndLevelSFX()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("EndLevel");
    }

    public void ReBornSFX()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("ReBorn");
    }

    public void DeathSFX()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("Death");
    }
}
