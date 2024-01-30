using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingDemo
{
    internal class ParkingLot
    {
        #region 原始資料欄位

        /// <summary>停車場編號</summary>
        public string PARKNO = "";
        /// <summary>停車場名稱</summary>
        public string PARKINGNAME = "";
        /// <summary>停車場地址</summary>
        public string ADDRESS = "";
        /// <summary>營業時間</summary>
        public string BUSINESSHOURS = "";

        /// <summary>平日費率</summary>
        public string WEEKDAYS = "";
        /// <summary>假日費率</summary>
        public string HOLIDAY = "";

        /// <summary>大車車位剩餘</summary>
        public int FREESPACEBIG;
        /// <summary>大車車位總數</summary>
        public int TOTALSPACEBIG;
        /// <summary>一般車位剩餘</summary>
        public int FREESPACE;
        /// <summary>一般車位總數</summary>
        public int TOTALSPACE;
        /// <summary>機車車位剩餘</summary>
        public int FREESPACEMOT;
        /// <summary>機車車位總數</summary>
        public int TOTALSPACEMOT;
        /// <summary>身障車位剩餘</summary>
        public int FREESPACEDIS;
        /// <summary>身障車位總數</summary>
        public int TOTALSPACEDIS;
        /// <summary>婦幼車位剩餘</summary>
        public int FREESPACECW;
        /// <summary>婦幼車位總數</summary>
        public int TOTALSPACECW;
        /// <summary>電動車車位剩餘</summary>
        public int FREESPACEECAR;
        /// <summary>電動車車位總數</summary>
        public int TOTALSPACEECAR;

        /// <summary>座標經度</summary>
        public decimal X_COORDINATE;
        /// <summary>座標緯度</summary>
        public decimal Y_COORDINATE;
        /// <summary>更新時間</summary>
        public DateTime UPDATETIME;

        #endregion Raw Data Columns

        #region 車位查詢

        public int TotalSpaceOf(Vehicle vehicle)
        {
            switch (vehicle)
            {
                case Vehicle.Scooter:       return TOTALSPACEMOT;
                case Vehicle.Car:           return TOTALSPACE;
                case Vehicle.ElectricCar:   return TOTALSPACEECAR;
                case Vehicle.ForDisability: return TOTALSPACEDIS;
                case Vehicle.ForChindern:   return TOTALSPACECW;
                case Vehicle.Bus:           return TOTALSPACEBIG;
                default: return 0;
            }
        }
        public int RemainingSpaceOf(Vehicle vehicle)
        {
            switch (vehicle)
            {
                case Vehicle.Scooter:       return FREESPACEMOT;
                case Vehicle.Car:           return FREESPACE;
                case Vehicle.ElectricCar:   return FREESPACEECAR;
                case Vehicle.ForDisability: return FREESPACEDIS;
                case Vehicle.ForChindern:   return FREESPACECW;
                case Vehicle.Bus:           return FREESPACEBIG;
                default: return 0;
            }
        }
        public int this[Vehicle vehicle] => RemainingSpaceOf(vehicle);
        #endregion
    }

}
