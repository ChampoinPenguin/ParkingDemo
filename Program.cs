using Newtonsoft.Json;
using ParkingDemo;
internal class Program
{
    private static void Main(string[] args)
    {
        string uri = "https://hispark.hccg.gov.tw/OpenData/GetParkInfo";
        Console.WriteLine("新竹市剩餘停車位資訊");
        Console.WriteLine($"資料來源:{uri}");

        try
        {
            #region 送出查詢請求

            var client = new HttpClient();
            var query = client.GetAsync(uri);
            var timeout = DateTime.Now.AddSeconds(client.Timeout.Seconds);

            #endregion

            #region 選擇車位類型

            Console.WriteLine("車位類型代碼:");

            var name = new Dictionary<Vehicle,string>();
            foreach (Vehicle vehicle in Enum.GetValues(typeof(Vehicle)))
            {
                name.Add(vehicle, vehicle.GetDescriptionText());
                Console.WriteLine($"{(int)vehicle}.{name[vehicle]}");
            }   //列舉車輛類型

            var myVehicle = Vehicle.Unknown;
            while (myVehicle == Vehicle.Unknown)
            {
                Console.WriteLine("請輸入您的車位類型代碼:");
                var read = Console.ReadLine()?.Trim() ?? "";
                if(int.TryParse(read, out int number))
                {
                    if (Enum.IsDefined(typeof(Vehicle), number))
                        myVehicle = (Vehicle)number;
                }
            }

            #endregion

            while (!query.IsCompleted && DateTime.Now < timeout)
            {
                var countdown = Convert.ToInt32((timeout - DateTime.Now).TotalSeconds);
                Console.WriteLine($"查詢中...{countdown}");
                Task.Delay(1000).Wait();
            }   //等待查詢結果

            #region 顯示剩餘車位

            var response = query.Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var lots = JsonConvert.DeserializeObject<List<ParkingLot>>(json) ?? new List<ParkingLot>();
            var remaining = lots.FindAll(place => place.RemainingSpaceOf(myVehicle) > 0);
            foreach (var lot in remaining)
            {
                string text = $"{lot.PARKINGNAME}\t 剩餘{name[myVehicle]}車位 {lot.RemainingSpaceOf(myVehicle)}";
                Console.WriteLine(text);
            }

            if (remaining.Count == 0)
                Console.WriteLine($"目前所有場地皆無剩餘{name[myVehicle]}車位");

            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex}");
        }
        finally
        {
            Console.ReadKey();
        }

    }
}