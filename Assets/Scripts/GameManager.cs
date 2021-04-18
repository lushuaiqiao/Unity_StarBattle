using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

class global
{
    public struct PlayerItem
    {
        public int playerId;
        public GameObject prebPlayer;

    };
    public static GameObject g_mainPlayer;
    public static int g_weaponCount = 0;
    public static int g_playerCount = 0;
    public static Dictionary<int, PlayerItem> g_playerItems = new Dictionary<int, PlayerItem>();
    public static Camera g_mainCamera;
}
public class GameManager : MonoBehaviour
{
   
    //public float m_startDelay = 3.0f;
    //public float m_endDelay = 3.0f;

    //private WaitForSeconds m_startWait;
    //private WaitForSeconds m_endWait;
    //void Start()
    //{
    //    m_startWait = new WaitForSeconds(m_startDelay); 
    //    m_endWait = new WaitForSeconds(m_endDelay);

    //    CreatePlayers();
    //    SetCameraTarget();
    //    StartCoroutine(GameLoop());
    //}

    //private void CreatePlayers() {

    //}
    //private void SetCameraTarget()
    //{

    //}
    //private IEnumerator GameLoop() {
    //    yield return StartCoroutine(RoundStarting());   //等待一段时间后执行
    //    yield return StartCoroutine(RoundPlaying());
    //    yield return StartCoroutine(RoundEnding());

    //    if (m_GameWinner != null)
    //    {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //    }
    //    else
    //    {
    //        StartCoroutine(GameLoop());     //如果没有胜者，则继续循环
    //    }

    //}

    //private IEnumerator RoundStarting()
    //{
    //    ResetAllTanks();                   //重置坦克位置
    //    DisableTankControl();              //取消对坦克的控制
    //    m_CameraControl.SetStartPositionAndSize();  //摄像机聚焦位置重置

    //    m_RoundNumber++;                   //回合数增加
    //    m_MessageText.text = "ROUND" + m_RoundNumber; //更改UI的显示
    //    yield return m_startWait;
    //}

    //private IEnumerator RoundPlaying()
    //{
    //    EnableTankControl();    //激活对坦克的控制

    //    m_MessageText.text = string.Empty; //UI不显示

    //    //如果只剩下一个玩家，则跳出循环
    //    while (!OneTankLeft())
    //    {
    //        yield return null;
    //    }

    //}

    //private IEnumerator RoundEnding()
    //{
    //    //取消对坦克的控制
    //    DisableTankControl();

    //    m_RoundWinner = null;

    //    //判断当前回合获胜的玩家
    //    m_RoundWinner = GetRoundWinner();

    //    //累积胜利次数
    //    if (m_RoundWinner != null)
    //    {
    //        m_RoundWinner.m_Wins++;
    //    }

    //    //判断是否有玩家达到了游戏胜利的条件
    //    m_GameWinner = GetGameWinner();

    //    string message = EndMessage();
    //    m_MessageText.text = message;

    //    yield return m_EndWait;
    //}

    //private bool OneTankLeft()
    //{
    //    int numTanksLeft = 0;

    //    for (int i = 0; i < m_Tanks.Length; i++)
    //    {
    //        if (m_Tanks[i].m_Instance.activeSelf)
    //            numTanksLeft++;
    //    }

    //    return numTanksLeft <= 1;
    //}

    //private TankManager GetRoundWinner()
    //{
    //    for (int i = 0; i < m_Tanks.Length; i++)
    //    {
    //        if (m_Tanks[i].m_Instance.activeSelf)
    //            return m_Tanks[i];
    //    }

    //    return null;
    //}

    //private TankManager GetGameWinner()
    //{
    //    for (int i = 0; i < m_Tanks.Length; i++)
    //    {
    //        if (m_Tanks[i].m_Wins == m_NumRoundsToWin)
    //            return m_Tanks[i];
    //    }

    //    return null;
    //}

    //private string EndMessage()
    //{
    //    string message = "DRAW!";

    //    if (m_RoundWinner != null)
    //        message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

    //    message += "\n\n\n\n";

    //    for (int i = 0; i < m_Tanks.Length; i++)
    //    {
    //        message += m_Tanks[i].m_ColoredPlayerText + ": " + m_Tanks[i].m_Wins + " WINS\n";
    //    }

    //    if (m_GameWinner != null)
    //        message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

    //    return message;
    //}

    //private void ResetAllTanks()
    //{
    //    for (int i = 0; i < m_Tanks.Length; i++)
    //    {
    //        m_Tanks[i].Reset();     //调用TankManager的Reset()方法
    //    }
    //}


    //private void EnableTankControl()
    //{
    //    for (int i = 0; i < m_Tanks.Length; i++)
    //    {
    //        m_Tanks[i].EnableControl();   //调用TankManager的EnableControl()方法
    //    }
    //}


    //private void DisableTankControl()
    //{
    //    for (int i = 0; i < m_Tanks.Length; i++)
    //    {
    //        m_Tanks[i].DisableControl();   //调用TankManager的DisableControl()方法
    //    }
    //}
}
