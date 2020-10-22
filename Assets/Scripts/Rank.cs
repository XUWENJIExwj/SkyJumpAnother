using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;

public class Rank : MonoBehaviour
{
    //[StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RankInfo
    {
        //[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
        public int rank;
        //[MarshalAs(UnmanagedType.BStr, SizeConst = 6)]
        public string name;
        //[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
        public int score;
    }

    //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.Struct)]
    public RankInfo[] rankInfo;
    public RankInfo newRank;

    public void LoadRank()
    {
        rankInfo = new RankInfo[5];

        for (int i = 0; i < rankInfo.Length; i++)
        {
            rankInfo[i].rank = PlayerPrefs.GetInt("RANK" + i.ToString(), 0);
            rankInfo[i].name = PlayerPrefs.GetString("NAME" + i.ToString(), "");
            rankInfo[i].score = PlayerPrefs.GetInt("SCORE" + i.ToString(), 0);
        }

        PlayerPrefs.Save();
    }

    public RankInfo GetRankInfo(int idx)
    {
        return rankInfo[idx];
    }

    public RankInfo LoadChampionInfo()
    {
        return rankInfo[0];
    }

    public bool CheckIfRankIn(int score)
    {
        if (score > rankInfo[rankInfo.Length - 1].score)
        {
            return true;
        }
        return false;
    }

    public void SortRank()
    {
        RankInfo temp = newRank;

        bool hasGetRank = false;

        for (int i = 0; i < rankInfo.Length; i++)
        {
            if (rankInfo[i].score < temp.score)
            {
                RankInfo work = rankInfo[i];

                rankInfo[i].rank = i + 1;
                rankInfo[i].name = temp.name;
                rankInfo[i].score = temp.score;

                if(!hasGetRank)
                {
                    hasGetRank = true;
                    newRank.rank = rankInfo[i].rank;
                }

                temp = work;

                SaveRank(i);
            }
        }
        PlayerPrefs.Save();
    }

    public void SaveRank(int idx)
    {
        PlayerPrefs.SetInt("RANK" + idx.ToString(), rankInfo[idx].rank);
        PlayerPrefs.SetString("NAME" + idx.ToString(), rankInfo[idx].name);
        PlayerPrefs.SetInt("SCORE" + idx.ToString(), rankInfo[idx].score);
    }

    public void SetNewRankInfo(string name, int score)
    {
        newRank.name = name;
        newRank.score = score;
    }

    public RankInfo GetNewRankInfo()
    {
        return newRank;
    }

    public void SetNewRankName(string name, int idx)
    {
        rankInfo[idx].name = name;
    }

    public void RankDebug()
    {
        for (int i = 0; i < rankInfo.Length; i++)
        {
            Debug.Log("Rank: " + rankInfo[i].rank);
            Debug.Log("Name: " + rankInfo[i].name);
            Debug.Log("Score:" + rankInfo[i].score);
        }
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
