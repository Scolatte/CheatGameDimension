using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheatPanel : MonoBehaviour
{
    public TextMeshProUGUI cheatsLeftText;

    public int 
        cheatLeftCount = 2,
        levelCheatCount = 1;

    public Slider
        jumpForceSlider,
        jumpCountSlider,
        walkSpeedSlider,
        dashDistanceSlider,
        dashCooldownSlider;

    public Toggle unDamagable;

    private bool 
        canValid,
        isCheated;

    private bool[] cheats = new bool[6];

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        cheatsLeftText.text = levelCheatCount.ToString();
    }

    public void CheatActivated()
    {
        if (canValid)
        {
            player.GetComponent<PlayerController>().TakeCheatsStats(
            (int)jumpForceSlider.value,
            (int)jumpCountSlider.value,
            (int)walkSpeedSlider.value,
            (int)dashDistanceSlider.value,
            (int)dashCooldownSlider.value,
            unDamagable.isOn,
            isCheated);
            this.gameObject.SetActive(false);
            Debug.Log("Hileler Onaylandı");
        }
        else
        {
            jumpCountSlider.value = 1;
            jumpForceSlider.value = 1;
            walkSpeedSlider.value = 1;
            dashDistanceSlider.value = 1;
            dashCooldownSlider.value = 1;
            unDamagable.GetComponent<Toggle>().isOn = false;
        }
    }

    public void CheatLeftCheck()
    {
        int currentCheatsActivateCount = 0;

        if (jumpForceSlider.value > 1)
        {
            cheats[0] = true;
        }
        else
        {
            cheats[0] =false;
        }

        if (jumpCountSlider.value > 1)
        {
            cheats[1] = true;
        }
        else
        {
            cheats[1] = false;
        }

        if (walkSpeedSlider.value > 1)
        {
            cheats[2] = true;
        }
        else
        {
            cheats[2] = false;
        }

        if (dashDistanceSlider.value > 1)
        {
            cheats[3] = true;
        }
        else
        {
            cheats[3] = false;
        }

        if (dashCooldownSlider.value > 1)
        {
            cheats[4] = true;
        }
        else
        {
            cheats[4] = false;
        }

        if (unDamagable.isOn)
        {
            cheats[5] = true;
        }
        else
        {
            cheats[5] = false;
        }

        for (int i = 0; i < cheats.Length; i++)
        {
            if (cheats[i])
            {
                currentCheatsActivateCount += 1;
            }
        }

        if (currentCheatsActivateCount <= levelCheatCount)
        {
            canValid = true;

            if (currentCheatsActivateCount == 0)
            {
                isCheated = false;
            }
            else
            {
                isCheated = true;
            }
        }
        else
        {
            canValid = false;
        }
    }

    public void ResetCheatPanel()
    {
        jumpForceSlider.GetComponent<Slider>().value = 1;
        jumpCountSlider.GetComponent<Slider>().value = 1;
        walkSpeedSlider.GetComponent<Slider>().value = 1;
        dashDistanceSlider.GetComponent<Slider>().value = 1;
        dashCooldownSlider.GetComponent<Slider>().value = 1;
        unDamagable.GetComponent<Toggle>().isOn = false;
    }
}
