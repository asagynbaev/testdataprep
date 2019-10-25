using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace test3
{
    public class Records
    {
        public DateTime date { get; set; }
        public int franchiseId { get; set; }
        public string franchiseName { get; set; }
        public int income { get; set; }
        public int wageCosts { get; set; }
        public int ingredientsCosts { get; set; }
        public int otherCosts { get; set; }
    }

    public class Bakeries
    {
        public int franchiseId { get; set; }
        public string franchiseName { get; set; }
        public DateTime date { get; set; }
        public int cake1Sold { get; set; }
        public string cake1Name { get; set; }
        public int cake2Sold { get; set; }
        public string cake2Name { get; set; }
        public int cake3Sold { get; set; }
        public string cake3Name { get; set; }
        public int cake4Sold { get; set; }
        public string cake4Name { get; set; }
        public int cake5Sold { get; set; }
        public string cake5Name { get; set; }
        public int cake6Sold { get; set; }
        public string cake6Name { get; set; }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Task2();
        }

        static void Task1()
        {
            using (StreamReader r = new StreamReader("E:/data4.json"))
            {
                string json = r.ReadToEnd();
                List<Records> items = JsonConvert.DeserializeObject<List<Records>>(json);
                var newItems = items.GroupBy(x => x.franchiseId).Select(x => x.FirstOrDefault());
                foreach (var item in newItems)
                {
                    item.income = items.Where(x => x.franchiseId == item.franchiseId).Sum(x => x.income) / items.Count;
                    item.wageCosts = items.Where(x => x.franchiseId == item.franchiseId).Sum(x => x.wageCosts) / items.Count;
                    item.otherCosts = items.Where(x => x.franchiseId == item.franchiseId).Sum(x => x.otherCosts) / items.Count;
                    item.ingredientsCosts = items.Where(x => x.franchiseId == item.franchiseId).Sum(x => x.ingredientsCosts) / items.Count;
                }

                string output = JsonConvert.SerializeObject(newItems);
                System.IO.File.WriteAllText(@"E:\myjson.json", output);
            }
        }

        static void Task2()
        {
            using (StreamReader r = new StreamReader("E:/data4.json"))
            {
                string json = r.ReadToEnd();
                List<Bakeries> items = JsonConvert.DeserializeObject<List<Bakeries>>(json);
                var itemsByDate = items.GroupBy(x => x.date.Date.Year).Select(x => x.FirstOrDefault());
                foreach (var item in itemsByDate)
                {
                    item.cake1Sold = items.Where(x => x.date.Date.Year == item.date.Date.Year).Sum(x => x.cake1Sold) / 100;
                    item.cake2Sold = items.Where(x => x.date.Date.Year == item.date.Date.Year).Sum(x => x.cake2Sold) / 100;
                    item.cake3Sold = items.Where(x => x.date.Date.Year == item.date.Date.Year).Sum(x => x.cake3Sold) / 100;
                    item.cake4Sold = items.Where(x => x.date.Date.Year == item.date.Date.Year).Sum(x => x.cake4Sold) / 100;
                    item.cake5Sold = items.Where(x => x.date.Date.Year == item.date.Date.Year).Sum(x => x.cake5Sold) / 100;
                    item.cake6Sold = items.Where(x => x.date.Date.Year == item.date.Date.Year).Sum(x => x.cake6Sold) / 100;
                }

                string output = JsonConvert.SerializeObject(itemsByDate);
                System.IO.File.WriteAllText(@"E:\myjson2.json", output);
            }
        }
    }
}
