using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class global
{
    public static GameObject g_mainPlayer;
    public static int g_weaponCount = 0;
    public static int g_playerCount = 0;
    public static bool g_isWeaponCreate = false;
    public static List<int> g_leftPlayerId = new List<int>();
    public static Camera g_mainCamera;
}

public class GameManager : MonoBehaviour
{

    public float m_startDelay = 3.0f;
    public float m_beforeEndDelay = 3.0f;
    public float m_endDelay = 3.0f;

    public GameObject pool;
    public GameObject eventManager;
    public GameObject createPlayer;
    public GameObject createWeapon;
    public GameObject rigCamera;
    public GameObject fadeImage;


    private WaitForSeconds m_startWait;
    private WaitForSeconds m_beforeEndWait;
    private WaitForSeconds m_endWait;

    private Text m_textName;
    private Text m_textDetails;


    void Start()
    {
        m_startWait = new WaitForSeconds(m_startDelay);
        m_beforeEndWait = new WaitForSeconds(m_beforeEndDelay);
        m_endWait = new WaitForSeconds(m_endDelay);

        m_textName = GameObject.Find("Canvas/Weapon details/Name").GetComponent<Text>();
        m_textDetails = GameObject.Find("Canvas/Weapon details/Details").GetComponent<Text>();

        pool.SetActive(true);
        eventManager.SetActive(true);
        createPlayer.SetActive(true);
        createWeapon.SetActive(true);
        rigCamera.SetActive(true);
        fadeImage.SetActive(true);
        StartCoroutine(GameLoop());
    }
    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting()); 
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        StartCoroutine(GameLoop()); 

    }
    private IEnumerator RoundStarting()
    {
        EventManager.me.TriggerEvent("resetgame", null);
        global.g_weaponCount = 0;
        global.g_isWeaponCreate = true;

        yield return m_startWait;
    }
    private IEnumerator RoundPlaying() {

        while (global.g_playerCount > 1)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                global.g_leftPlayerId.Clear();
                global.g_isWeaponCreate = false;
                EventManager.me.TriggerEvent("endgame", null);
                SceneManager.LoadScene("Title");
            }
            yield return null;
        }

        if (global.g_playerCount <= 1 && global.g_leftPlayerId.Count <= 1)
        {
            int winnerId = 0;
            foreach (var item in global.g_leftPlayerId)
            {
                winnerId = item;
            }
            string strid = winnerId.ToString();
            m_textName.text = "游戏结束";
            m_textDetails.text = "获胜者是" + strid + "号星星！！";
            m_textName.transform.parent.GetComponent<WeaponDetails>().isrestart = true;
            EventManager.me.TriggerEvent("fadein", null);
            yield return m_beforeEndWait;
        }
        else
        {
            m_textName.text = "游戏结束";
            m_textDetails.text = "本局没有获胜者！！";
            m_textName.transform.parent.GetComponent<WeaponDetails>().isrestart = true;
            EventManager.me.TriggerEvent("fadein", null);
            yield return m_beforeEndWait;
        }

    }
    private IEnumerator RoundEnding()
    {
        global.g_leftPlayerId.Clear();
        global.g_isWeaponCreate = false;
        global.g_weaponCount = 0;
        EventManager.me.TriggerEvent("endgame", null);
        yield return m_endWait;
    }
}

