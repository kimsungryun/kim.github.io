using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject
{
    public int wallDamage = 1;
    public int pointsPerCrown_1 = 10;
    public int pointsPerCrown_2 = 20;
    public float RestartLevelDelay = 1f;

    private Animator animator;
    private int Crown;


    protected override void Start()
    {
        animator = GetComponent<Animator>();

        Crown = GameManager.instance.playerCrownPoints;

        base.Start();
    }

    private void OnDisable()
    {
        GameManager.instance.PlayerCrownPoints = Crown;
    }

    void Update()
    {
        if (!GameOver.instance.playersTurn) return;

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
            vertical = 0;

        if (horizontal != 0 || vertical != 0)
            AttemptMove<Wall>(horizontal, vertical);
    }
    protected override void AttemptMove<T> (int xDir, int yDir)
    {
        Crown--;

        base.AttemptMove<T>(xDir, yDir);

        RaycastHit2D hit;

        CheckIfGameOver();

        GameManager.instance.playersTurn = false;
    }
    protected override void OnCantMove<T>(T componenet)
    {
        Wall hitWall = componenet as Wall;
        hitWall.DamageWall(wallDamage);
        animator.SetTrigger("PlayerChop");
    }

    protected void CheckIfGameOver()
    {
        if (Crown <= 0)
            GameManager.instance.GameOver();
    }

    private void Restart ()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    public void LoseCrown (int loss)
    {
        animator.SetTrigger("playerHit");
        Crown -= loss;
        CheckIfGameOver();
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Exit")
        {
            invoke("Restart", RestartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "Crown_1")
        {
            Crown_1 += pointsPerCrown_1;
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Crown_2")
        {
            Crown_2 += pointsPerCrown_2;
            other.gameObject.SetActive(false);
        }
    }
}
