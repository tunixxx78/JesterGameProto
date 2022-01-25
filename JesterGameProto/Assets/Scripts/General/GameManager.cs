using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayerMovementGrid playerMovementGrid;
    public string totoScene;

    BattleState state;
    BattleSystem battleSystem;

    SFXManager sFXManager;

    [SerializeField] GameObject outFade;

    private void Awake()
    {
        battleSystem = FindObjectOfType<BattleSystem>();
        playerMovementGrid = FindObjectOfType<PlayerMovementGrid>();
        sFXManager = FindObjectOfType<SFXManager>();
    }

    public void EndOfTurn()
    {
        playerMovementGrid.IsActiveToFalse();
    }

    public void FromPTwoToPOne()
    {
        FindObjectOfType<BattleSystem>().PlayerOneTurn();
        
    }

    public void PlayerAttack()
    {
        //FindObjectOfType<PlayerMovementGrid>().Attack();
    }
    public void EnemyTurn()
    {
        FindObjectOfType<BattleSystem>().EnemyTurn();
    }

    public void TryAgain()
    {
        sFXManager.button.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToScene(string toScene)
    {
        totoScene = toScene;

        outFade.GetComponent<LevelChangeFade>().canMove = true;

        StartCoroutine(ChangeCanMoveToFalse());

    }

    IEnumerator ChangeCanMoveToFalse()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(totoScene);

    }

    
}
