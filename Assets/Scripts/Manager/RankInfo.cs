using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;

public class RankInfo : MonoBehaviour
{
    //[StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RankData
    {
        //[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
        public int rank;
        //[MarshalAs(UnmanagedType.BStr, SizeConst = 6)]
        public string name;
        //[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
        public int score;
    }

    //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.Struct)]
    static private RankData[] rankData;
    static private RankData newRank;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    static public void LoadRank()
    {
        rankData = new RankData[5];

        for (int i = 0; i < rankData.Length; i++)
        {
            rankData[i].rank = PlayerPrefs.GetInt("RANK" + i.ToString(), 0);
            rankData[i].name = PlayerPrefs.GetString("NAME" + i.ToString(), "");
            rankData[i].score = PlayerPrefs.GetInt("SCORE" + i.ToString(), 0);
        }

        PlayerPrefs.Save();
    }

    static public RankData GetRankInfo(int idx)
    {
        return rankData[idx];
    }

    static public RankData LoadChampionInfo()
    {
        return rankData[0];
    }

    static public bool CheckIfRankIn()
    {
        if (newRank.score > rankData[rankData.Length - 1].score)
        {
            return true;
        }
        return false;
    }

    static public void SortRank()
    {
        RankData temp = newRank;

        bool hasGetRank = false;

        for (int i = 0; i < rankData.Length; i++)
        {
            if (rankData[i].score < temp.score)
            {
                RankData work = rankData[i];

                rankData[i].rank = i + 1;
                rankData[i].name = temp.name;
                rankData[i].score = temp.score;

                if(!hasGetRank)
                {
                    hasGetRank = true;
                    newRank.rank = rankData[i].rank;
                }

                temp = work;

                SaveRank(i);
            }
        }

        PlayerPrefs.Save();
    }

    static public void SaveRank(int idx)
    {
        PlayerPrefs.SetInt("RANK" + idx.ToString(), rankData[idx].rank);
        PlayerPrefs.SetString("NAME" + idx.ToString(), rankData[idx].name);
        PlayerPrefs.SetInt("SCORE" + idx.ToString(), rankData[idx].score);
    }

    static public void SetNewRankInfo(string name, int score)
    {
        newRank.name = name;
        newRank.score = score;
    }

    static public RankData GetNewRankInfo()
    {
        return newRank;
    }

    static public void InputRankName(string name, int idx)
    {
        rankData[idx].name = name;
    }

    static public void RankDebug()
    {
        for (int i = 0; i < rankData.Length; i++)
        {
            Debug.Log("Rank[" + i.ToString() + "]: " + rankData[i].rank);
            Debug.Log("Name[" + i.ToString() + "]: " + rankData[i].name);
            Debug.Log("Score[" + i.ToString() + "]: " + rankData[i].score);
        }
    }

    static public void SetOneScore()
    {
        SetNewRankInfo("WWWWWW", 99999);
        SortRank();
    }
}
