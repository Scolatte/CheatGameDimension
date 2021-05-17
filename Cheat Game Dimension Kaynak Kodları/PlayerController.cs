using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject 
        jumpCloud,
        cooldownBar,
        playerDead,
        cloudHuge;

    [HideInInspector]
    public GameObject cheatPanel;

    public LayerMask ground, environment, endPortal;

    public GameObject groundCheck;

    public int jumpCount;

    private int jumpLeft;

    private float 
        inputDirection, 
        jumpForce,
        lastDashTime = Mathf.NegativeInfinity,
        fallStartTime = Mathf.Infinity;

    [HideInInspector]
    public bool 
        isInvunerable, 
        isCheated;

    public float
        defaultWalkSpeed,
        walkSpeed,
        defaultJumpForce,
        defaultDashForce,
        dashForce,
        dashCooldown,
        defaultDashCooldown;

    private bool
        isAlive,
        canJump,
        groundDetected,
        canDash,
        isRunning,
        facingRight,
        inAir,
        isDashing,
        canMove,
        isLevelEnd;

    [HideInInspector]
    public static bool isCheatPanelOn;

    public Vector2 groundCheckDistance;

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D coll;

    private void Awake()
    {
        isDashing = true;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = GameObject.Find("PlayerSpawnPoint").transform.position;
        lastDashTime = Time.time - dashCooldown;
        dashCooldown = defaultDashCooldown;
        isAlive = true;
        facingRight = true;
        jumpLeft = jumpCount;
        jumpForce = defaultJumpForce;
        dashForce = defaultDashForce;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            WriteMovement();
            WriteAnimation();
        } 
    }
    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isGamePaused)
        {
            CanMovement();
            if (!isDashing) { CheckMovement(); }
            CheckGround();
            CheckCollision();
        }

        CheatPanel();
    }

    private void CanMovement()
    {  
        if (!groundDetected && !inAir)
        {
            inAir = true;
            jumpLeft = jumpCount - 1;
        }

        if (groundDetected && coll.IsTouchingLayers(ground))
        {
            if (inAir)
            {
                GameObject.FindObjectOfType<AudioManager>().Play("Fall");
            }

            inAir = false;
            jumpLeft = jumpCount;
        }

        if (jumpLeft > 0)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

        if (Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (Time.time >= lastDashTime + dashCooldown)
        {
            canDash = true;
            isDashing = false;
        }
        else
        {
            canDash = false;
        }
    }

    private void CheckCollision()
    {
        if (coll.IsTouchingLayers(endPortal) && !isLevelEnd)
        {
            LoadNextLevel();
            Debug.Log("LEVEL GEÇTİ");
            anim.SetTrigger("endLevel");
            Instantiate(cloudHuge, new Vector2(transform.position.x, transform.position.y + 4), Quaternion.identity);
            GameObject.FindGameObjectWithTag("SettingsManager").GetComponent<SettingsManager>().levels[GameObject.FindGameObjectWithTag("LevelChecker").GetComponent<LevelChecker>().currentLevelIndex] = true;
            canMove = false;
            isDashing = true;
            isLevelEnd = true;
            rb.velocity = new Vector2(0,0);
        }

        if (coll.IsTouchingLayers(environment) && !isInvunerable)
        {
            Dead();
        }
    }

    private void CheckMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputDirection = -1;
            facingRight = false;
            Flip();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            inputDirection = 1;
            facingRight = true;
            Flip();
        }
        else
        {
            inputDirection = 0;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Dash();
        }
    }

    private void WriteMovement()
    {
        if (!isDashing)
        {
            
                rb.velocity = new Vector2(inputDirection * walkSpeed * Time.deltaTime * 100, rb.velocity.y);
            
        }
        else
        {
            if (facingRight)
            {
                rb.velocity = new Vector2(dashForce * Time.deltaTime * 100, 0);
            }
            else
            {
                rb.velocity = new Vector2(-1 * dashForce * Time.deltaTime * 100, 0);
            }
        }

        if (canDash)
        {
            cooldownBar.transform.localScale = new Vector2(0, cooldownBar.transform.localScale.y);
        }
        else if(!isDashing)
        {
            cooldownBar.transform.localScale = new Vector2(1, cooldownBar.transform.localScale.y);
            cooldownBar.GetComponent<SpriteRenderer>().size = new Vector2(4 * ( (Time.time - lastDashTime)/ dashCooldown), 4);
        }
    }
       
    private void WriteAnimation()
    {
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("groundDetected", groundDetected);
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetFloat("velocityX", rb.velocity.x);
        anim.SetBool("inAir",inAir);
    }

    private void CheatPanel()
    {
        if(!isCheatPanelOn && !PauseMenu.isGamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && !isCheated)
            {
                OpenCheatPanel();
            }
            else if (Input.GetKeyDown(KeyCode.Tab) && isCheated)
            {
                GameObject.FindGameObjectWithTag("LogPanel").GetComponent<LogPanel>().ShowMessage("Zaten Hile Kullandın");
                GameObject.FindObjectOfType<AudioManager>().Play("Notification2");
            }
        }
        else if(isCheatPanelOn)
        {
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
            {
                ClosecheatPanel();
                ResetCheats();
            }
        }
    }

    private void Dead()
    {
        Instantiate(playerDead, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void Flip()
    {
        transform.localScale = new Vector3(inputDirection * 1, 1, 1);
    }

    private void Dash()
    {
        if (canDash)
        {
            isDashing = true;
            anim.SetTrigger("dash");
            lastDashTime = Time.time;
        } 
    }

    public void StopDash()
    {
        isDashing = false;
    }

    public void CanMoveNow()
    {
        canMove = true;
    }

    private void Jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce / 10);
            jumpLeft -= 1;
            Debug.Log( jumpLeft + " zıplama hakkı kaldı");
            GetComponent<PlayerSoundController>().NormalJumpSFX();

            if (inAir)
            {
                anim.SetTrigger("doubleJump");
                Instantiate(jumpCloud, new Vector2(transform.position.x, transform.position.y - 4), Quaternion.identity);
            }
        } 
    }

    public void OpenCheatPanel()
    {
        cheatPanel.SetActive(true);
        GameObject.FindGameObjectWithTag("GlichedCanvas").GetComponent<PauseMenu>().Pause();
        isCheatPanelOn = true;
    }

    public void ClosecheatPanel()
    {
        cheatPanel.SetActive(false);
        GameObject.FindGameObjectWithTag("GlichedCanvas").GetComponent<PauseMenu>().Resume();
        isCheatPanelOn = false;
    }

    private void LoadNextLevel()
    {
        GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().NextLevel();
    }

    public void TakeCheatsStats(int jumpForce, int jumpCount, int walkSpeed, int dashDistance, int dashCooldown, bool unDamagable, bool isCheated)
    {
        this.jumpCount = jumpCount;
        this.jumpForce = defaultJumpForce + (125 * jumpForce - 125);
        this.walkSpeed = defaultWalkSpeed + (6 * walkSpeed - 6);
        this.dashForce = defaultDashForce + (20 * dashDistance - 20);
        this.dashCooldown = defaultDashCooldown - (dashCooldown / 5) * defaultDashCooldown + 0.5f;
        isInvunerable = unDamagable;
        this.isCheated = isCheated;

        if (isCheated)
        {
            GameObject.FindGameObjectWithTag("LogPanel").GetComponent<LogPanel>().ShowMessage("Hile Etkinleştirildi");
            GameObject.FindObjectOfType<AudioManager>().Play("Notification");
        }

        ClosecheatPanel();
    }

    private void CheckGround()
    {
        groundDetected = Physics2D.OverlapArea(
            new Vector2(groundCheck.transform.position.x - groundCheckDistance.x, groundCheck.transform.position.y + groundCheckDistance.y),
            new Vector2(groundCheck.transform.position.x + groundCheckDistance.x, groundCheck.transform.position.y - groundCheckDistance.y), ground);
    }

    public void ResetCheats()
    {
        jumpCount = 1;
        jumpForce = defaultJumpForce;
        walkSpeed = defaultWalkSpeed;
        dashForce = defaultDashForce;
        dashCooldown = defaultDashCooldown;
        isInvunerable = false;

        Debug.Log("Hileler Resetlendi");

        cheatPanel.GetComponent<CheatPanel>().ResetCheatPanel();
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(
            new Vector2(groundCheck.transform.position.x - groundCheckDistance.x, groundCheck.transform.position.y + groundCheckDistance.y),
            new Vector2(groundCheck.transform.position.x + groundCheckDistance.x, groundCheck.transform.position.y + groundCheckDistance.y), Color.green);
        Debug.DrawLine(
            new Vector2(groundCheck.transform.position.x - groundCheckDistance.x, groundCheck.transform.position.y - groundCheckDistance.y),
            new Vector2(groundCheck.transform.position.x + groundCheckDistance.x, groundCheck.transform.position.y - groundCheckDistance.y), Color.green);
        Debug.DrawLine(
            new Vector2(groundCheck.transform.position.x + groundCheckDistance.x, groundCheck.transform.position.y + groundCheckDistance.y),
            new Vector2(groundCheck.transform.position.x + groundCheckDistance.x, groundCheck.transform.position.y - groundCheckDistance.y), Color.green);
        Debug.DrawLine(
            new Vector2(groundCheck.transform.position.x - groundCheckDistance.x, groundCheck.transform.position.y + groundCheckDistance.y),
            new Vector2(groundCheck.transform.position.x - groundCheckDistance.x, groundCheck.transform.position.y - groundCheckDistance.y), Color.green);
    }
}
