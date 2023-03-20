using Game.Data;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;

namespace Game
{
    public enum RoomType
    {
        Treat = 1,    //回血
        Unknown = 2,    //未知
        Coin = 3,    //金币
        
        Gain = 4,    //强化
        Melee = 5,    //近战
        Charge = 6,    //冲撞
        Evasion = 7,    //回避
        Wave = 8,    //冲击波
        Tornado = 9,    //龙卷风

        Boss = 100,    //Boss
    }
    public class MapMgr : Singleton<MapMgr>
    {
        private static readonly int mapHeight = 100;
        private static readonly int mapWidth = 4;

        private List<RoomData> roomList;

        //定义一个二维字典
        public Dictionary<int, Dictionary<int, RoomData>> mapDic = new Dictionary<int, Dictionary<int, RoomData>>();

        public void Init()
        {
            roomList = PlayerDataMgr.singleton.DB.roomDatas;

            foreach (RoomData roomData in roomList) 
            {
                Dictionary<int, RoomData> roomDic;
                if (!mapDic.TryGetValue(roomData.height, out roomDic))
                {
                    roomDic = new Dictionary<int, RoomData>();
                    mapDic[roomData.height] = roomDic;
                }
                roomDic.Add(roomData.width, roomData);
            }
        }

        public RoomData GetRoomData(int height, int roomID)
        {
            Dictionary<int, RoomData> roomDic;
            if (mapDic.TryGetValue(height, out roomDic))
            {
                foreach (var room in roomDic.Values)
                {
                    if (room.roomID == roomID)
                    {
                        return room;
                    }
                }
            }
            return null;
        }

        public void ClearMap()
        {
            mapDic.Clear();
            roomList.Clear();
        }

        public void CreateMap(int count)
        {
            mapDic.Clear();
            roomList.Clear();

            PlayerDataMgr.singleton.DB.curRoomID = 0;

            int roomID = 1;
            List<int> list = new List<int>();
            for (int i = 0; i < count; i++)
            {
                var num = 2;
                if (Random.Range(0, 100) < 40)
                {
                    num = 3;
                }
                list.Clear();
                Dictionary<int, RoomData> last_rooms;

                if (i == count - 1)
                {
                    list.Add(0);
                }
                else if (i == 0)
                {
                    for (int k = 0; k < mapWidth; k++)
                    {
                        list.Add(k);
                    }
                }
                else if (mapDic.TryGetValue(i - 1, out last_rooms))
                {
                    for (int k = 0; k < mapWidth; k++)
                    {

                        bool del = false;
                        foreach (var room in last_rooms.Values)
                        {
                            //if (!list.Contains(room.width))
                            //    list.Add(room.width);

                            //var idx = room.width - 1;
                            //if (idx >= 0 && !list.Contains(idx))
                            //    list.Add(idx);
                            //idx = room.width + 1;
                            //if (idx < mapWidth && !list.Contains(idx))
                            //    list.Add(idx);
                            if (k == 0 && room.width == 3)
                            {
                                del = true;
                                break;
                            }
                            else if (k == 3 && room.width == 0)
                            {
                                del = true;
                                break;
                            }
                        }

                        if (!del)
                        {
                            list.Add(k);
                            //num = 2;
                        }
                    }     
                    
                    //if (list.Count < mapWidth)
                    //{
                    //    num = list.Count;
                    //}
                }

                for (int j = 0; j < num; j++)
                {
                    var index = Random.Range(0, list.Count);                    

                    RoomData room = new RoomData();
                    room.roomID = roomID;
                    room.width = list[index];
                    room.height = i;
                    room.roomName = "room" + j;
                    room.roomLevel = 1;
                    room.roomType = 1;

                    Dictionary<int, RoomData> roomDic;
                    if (!mapDic.TryGetValue(room.height, out roomDic))
                    {
                        roomDic = new Dictionary<int, RoomData>();
                        mapDic[room.height] = roomDic;
                    }
                    roomDic.Add(room.width, room);
                    roomList.Add(room);
                    list.RemoveAt(index);
                    roomID++;

                    if (list.Count <= 0)
                        break;
                }
            }


            //遍历mapData[i,j]里面的元素，查找(i,j)元素,是否存在(i+1,j)元素
            for (int i = 0; i < mapDic.Count; ++i)
            {
                var cur_rooms = mapDic[i];
                Dictionary<int, RoomData> next_rooms;
                if (!mapDic.TryGetValue(i + 1, out next_rooms))
                    break;

                for (int j = 0; j < mapWidth; ++j)
                {
                    RoomData cur_room;
                    if (!cur_rooms.TryGetValue(j, out cur_room))
                        continue;

                    RoomData next_room;
                    if (next_rooms.TryGetValue(j, out next_room))
                    {
                        next_room.roomFrom.Add(cur_room.roomID);
                    }
                    else if (next_rooms.TryGetValue(j - 1, out next_room))
                    {
                        next_room.roomFrom.Add(cur_room.roomID);
                    }
                    else if (next_rooms.TryGetValue(j + 1, out next_room))
                    {
                        next_room.roomFrom.Add(cur_room.roomID);
                    }
                    //else if (i < mapDic.Count - 1)
                    //{
                    //    roomList.Remove(cur_rooms[j]);
                    //    cur_rooms.Remove(j);
                    //}
                }
            }

            for (int i = 1; i < mapDic.Count; ++i)
            {
                var cur_rooms = mapDic[i - 1];
                var next_rooms = mapDic[i];
                if (next_rooms == null)
                    break;

                for (int j = 0; j < mapWidth; ++j)
                {
                    RoomData next_room;
                    if (!next_rooms.TryGetValue(j, out next_room))
                        continue;

                    if (i == mapDic.Count - 1)
                    {
                        foreach (var cur_room in cur_rooms.Values)
                        {
                            next_room.roomFrom.Add(cur_room.roomID);
                        }
                        break;
                    }

                    if (next_room.roomFrom.Count <= 1)
                    {
                        RoomData cur_room;
                        if (cur_rooms.TryGetValue(j - 1, out cur_room))
                        {
                            next_room.roomFrom.Add(cur_room.roomID);
                            continue;
                        }

                        //if (next_room.roomFrom.Count <= 1 && Random.Range(0, 100) < 50)
                        //    continue;

                        if (cur_rooms.TryGetValue(j + 1, out cur_room))
                        {
                            next_room.roomFrom.Add(cur_room.roomID);
                            continue;
                        }

                        if (next_room.roomFrom.Count <= 0)
                        {
                            foreach (var room in roomList)
                            {
                                room.roomFrom.Remove(next_room.roomID);
                            }

                            roomList.Remove(next_rooms[j]);
                            next_rooms.Remove(j);
                        }
                    }
                }
            }

            for (int i = 0; i < count - 1; i++)
            {
                if (mapDic[i].Count <= 1)
                {
                    CreateMap(count);
                    return;
                }
            }

            for (int i = 0;i < roomList.Count;++i)
            {

            }

            CreateAllRoomType();
            PlayerDataMgr.singleton.NotifySaveData();
        }

        public void CreateAllRoomType()
        {
            List<int> idList = new List<int>();
            for (int i = 0;i < roomList.Count - 1;++i)
            {
                idList.Add(i);
            }

            roomList[roomList.Count - 1].roomType = (int)RoomType.Boss;

            for (RoomType type = RoomType.Unknown; type <= RoomType.Tornado; ++type)
            {
                var count = GetRoomTypeCount(type);
                for (int i = 0; i < count; i++)
                {
                    if (idList.Count <= 0)
                        break;
                    var index = idList[Random.Range(0, idList.Count)];
                    var room = roomList[index];
                    if (type == RoomType.Treat && room.height < 2)
                    {
                        i--;
                        continue;
                    }
                    room.roomType = (int)type;
                    idList.Remove(index);
                }
            }

            //通过GetRoomTypeCount接口获得每个RoomType的数量，然后把这个RoomType随机分配到roomList里面的元素的roomType字段
        }

        int GetRoomTypeCount(RoomType type)
        {
            var allCount = roomList.Count - 1;
            var count = 0f;
            switch (type)
            {
                case RoomType.Unknown:
                    {
                        count = allCount * 0.3f;
                    }
                    break;
                case RoomType.Coin:
                    {
                        count = allCount * 0.2f;
                    }
                    break;
                case RoomType.Treat:
                    {
                        count = allCount * 0.1f;
                    }
                    break;
                case RoomType.Gain:
                    {
                        count = allCount * 0.1f;
                    }
                    break;
                case RoomType.Melee:
                    {
                        count = allCount * 0.1f;
                    }
                    break;
                case RoomType.Charge:
                    {
                        count = allCount * 0.1f;
                    }
                    break;
                case RoomType.Evasion:
                    {
                        count = allCount * 0.1f;
                    }
                    break;
                case RoomType.Wave:
                    {
                        count = allCount * 0.1f;
                    }
                    break;
                case RoomType.Tornado:
                    {
                        count = allCount * 0.1f;
                    }
                    break;
            }

            return (int)Mathf.Round(count);
        }
    }
}