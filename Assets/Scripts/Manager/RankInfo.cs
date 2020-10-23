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

    

    // 完成後削除
    bool isExist = false;

    private void Awake()
    {
        // 完成後削除
        if (!isExist)
        {
            DontDestroyOnLoad(gameObject);
            isExist = true;
        }

        // 完成後復元
        //DontDestroyOnLoad(gameObject);
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

    static public bool CheckIfRankIn(int score)
    {
        if (score > rankData[rankData.Length - 1].score)
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

    static public void SetNewRankName(string name, int idx)
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

//    public void LoadRankBinary()
//    {
//        string path;
//        string filename = "/rank.data";

//        rankInfo = new RankInfo[5];

//        if (Application.isEditor)
//        {
//            path = Application.dataPath + filename;
//        }
//        else
//        {
//#if UNITY_IOS

//#elif UNITY_ANDROID

//            path = Application.persistentDataPath + filename;
//#endif
//        }

//        if(File.Exists(path))
//        {
//            using (FileStream fs = new FileStream(path, FileMode.Create))
//            {
//                using (BinaryReader br = new BinaryReader(fs)) //true=追記 false=上書き
//                {
                    
//                    int size = Marshal.SizeOf(rankInfo);

//                    byte[] bytes = br.ReadBytes(size);

//                    System.IntPtr buffer = Marshal.AllocHGlobal(size);

//                    try
//                    {
//                        Marshal.Copy(bytes, 0, buffer, size);
//                        Marshal.PtrToStructure(buffer, rankInfo);
//                    }
//                    finally
//                    {
//                        Marshal.FreeHGlobal(buffer);
//                    }

//                    //for (int i = 0; i < rankInfo.Length; i++)
//                    //{
//                    //    rankInfo[i].rank = br.ReadByte();
//                    //    rankInfo[i].name = br.ReadBytes(6).ToString();
//                    //    rankInfo[i].score = br.ReadByte();
//                    //}
//                }
//            }
//        }
//        else
//        {
//            for (int i = 0; i < rankInfo.Length; i++)
//            {
//                rankInfo[i].rank = 0;
//                rankInfo[i].name = "      ";
//                rankInfo[i].score = 0;
//            }
//        }
//    }

    //public void SortRankBinary()
    //{
    //    RankInfo temp = newRank;

    //    bool hasGetRank = false;

    //    for (int i = 0; i < rankInfo.Length; i++)
    //    {
    //        if (rankInfo[i].score < temp.score)
    //        {
    //            RankInfo work = rankInfo[i];

    //            rankInfo[i].rank = i + 1;
    //            rankInfo[i].name = temp.name;
    //            rankInfo[i].score = temp.score;

    //            if (!hasGetRank)
    //            {
    //                hasGetRank = true;
    //                newRank.rank = rankInfo[i].rank;
    //            }

    //            temp = work;
    //        }
    //    }

    //    SaveRankBinary();
    //}

//    public void SaveRankBinary()
//    {
//        string path;
//        string filename = "/rank.data";

//        if (Application.isEditor)
//        {
//            path = Application.dataPath + filename;
//        }
//        else
//        {
//#if UNITY_IOS

//#elif UNITY_ANDROID

//            path = Application.persistentDataPath + filename;
//#endif
//        }

//        using (FileStream fs = new FileStream(path, FileMode.Create))
//        {
//            using (BinaryWriter bw = new BinaryWriter(fs)) //true=追記 false=上書き
//            {
//                int size = Marshal.SizeOf(rankInfo);
//                System.IntPtr buffer = Marshal.AllocHGlobal(size);

//                try
//                {
//                    Marshal.StructureToPtr(rankInfo, buffer, false);
//                    byte[] bytes = new byte[size];
//                    Marshal.Copy(buffer, bytes, 0, size);

//                    bw.Write(bytes);
//                }
//                finally
//                {
//                    Marshal.FreeHGlobal(buffer);
//                }

//                //for (int i = 0; i < rankInfo.Length; i++)
//                //{
//                //    bw.Write(rankInfo[i].rank);
//                //    bw.Write(rankInfo[i].name);
//                //    bw.Write(rankInfo[i].score);
//                //}
//            }
//        }
//    }
}
