using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Entity
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Model1 db = new Model1())
            {
                Brunch b1 = new Brunch { BrunchID = 20, Name = "Tatata", City = "Kivertsi", Adress = "Tamtam", PhoneNumber = "chtoto tam" };
                db.Brunches.Add(b1);
                db.SaveChanges();

                foreach (Brunch br in db.Brunches)
                    Console.WriteLine("{0}  {1}  {2}  {3}  {4}", br.BrunchID, br.Name, br.City, br.Adress, br.PhoneNumber);
                Console.WriteLine("--------------------------------------------------------------------");
                

                //b1 = db.Brunches.FirstOrDefault();
                b1.City = "Varash";
                //db.Entry(b2).State = EntityState.Modified;
                db.SaveChanges();

                foreach (Brunch br in db.Brunches)
                    Console.WriteLine("{0}  {1}  {2}  {3}  {4}", br.BrunchID, br.Name, br.City, br.Adress, br.PhoneNumber);
                Console.WriteLine("--------------------------------------------------------------------");

                //Brunch b1 = db.Brunches.Find(20);
                db.Brunches.Remove(b1);
                db.SaveChanges();

                foreach (Brunch br in db.Brunches)
                    Console.WriteLine("{0}  {1}  {2}  {3}  {4}", br.BrunchID, br.Name, br.City, br.Adress, br.PhoneNumber);
                Console.WriteLine("--------------------------------------------------------------------");


                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                Console.WriteLine("Exercise 3");



                var t_brunch = db.Brunches.Take(3);
                foreach(Brunch br in t_brunch)
                    Console.WriteLine("{0}  {1}  {2}  {3}  {4}", br.BrunchID, br.Name, br.City, br.Adress, br.PhoneNumber);
                Console.WriteLine("--------------------------------------------------------------------");



                var c_brunch = db.Brunches.Count();
                Console.WriteLine(c_brunch);
                Console.WriteLine("--------------------------------------------------------------------");



                var con = db.Contracts.OrderByDescending(x => x.BranchID).Skip(1).Take(3);
                foreach (Contract c in con)
                    Console.WriteLine("{0}  {1}",c.ContractID, c.CreateDate);
                Console.WriteLine("--------------------------------------------------------------------");



                var cl = db.Clients
                    .Where(x => x.PhoneNumber.Contains("068/"))
                    .Where(y => y.Coefficient <= 0.01 && y.Coefficient >= 0.02);
                foreach (Client c in cl)
                    Console.WriteLine("{0}  {1}", c.PhoneNumber, c.FullName);



                var ut = db.Clients.Select(x => new {FullName = x.FullName })
                    .Union(db.Workers.Select(y =>  new {FullName = y.FullName }));
                foreach (var c in ut)
                    Console.WriteLine(c.FullName);
                Console.WriteLine("--------------------------------------------------------------------");



                var ext = db.Clients.Select(x => new { FullName = x.FullName })
                    .Except(db.Workers.Select(y => new { FullName = y.FullName }));
                foreach (var c in ext)
                    Console.WriteLine(c.FullName);
                Console.WriteLine("--------------------------------------------------------------------");





                var jt = from b in db.Brunches
                         join w in db.Workers
                         on b.BrunchID equals w.BrunchID
                         select new { Name = b.Name, Adress = b.Adress, FullName = w.FullName };
                foreach (var x in jt)
                    Console.WriteLine("{0}  {1}  {2}", x.Name, x.Adress, x.FullName);
                Console.WriteLine("--------------------------------------------------------------------");



                var jt3 = from cli in db.Clients
                          join co in db.Contracts
                          on cli.ClientID equals co.ClientID
                          join ie in db.InsuredEvents
                          on co.ContractID equals ie.ContractID
                          where ie.Sum > 7000
                          select new { FullName = cli.FullName };
                foreach (var x in jt3)
                    Console.WriteLine("{0}", x.FullName);
                Console.WriteLine("--------------------------------------------------------------------");



                var jto = from cli in db.Clients
                          join co in db.Contracts
                          on cli.ClientID equals co.ClientID
                          join ie in db.InsuredEvents
                          on co.ContractID equals ie.ContractID
                          where ie.Status == "O"
                          select new { FullName = cli.FullName };
                foreach (var x in jto)
                    Console.WriteLine("{0}", x.FullName);
                Console.WriteLine("--------------------------------------------------------------------");



                //var s_jt = from cli in db.Clients
                //           group cli by cli.FullName;

                //foreach (IGrouping<string, Client> x in s_jt)
                //{
                //    foreach (var t in x)
                //        Console.WriteLine("{0}", t.FullName);
                //            }
                //Console.WriteLine("--------------------------------------------------------------------");


                var s_jt = db.Clients.GroupBy(x => x.FullName);
                foreach (IGrouping<string, Client> x in s_jt)
                {
                    foreach (var t in x)
                        Console.WriteLine("{0}", t.FullName);
                }


                var sum1 = db.InsuredEvents.Sum(x => x.Sum);
                Console.WriteLine(sum1);

                var max1 = db.InsuredEvents.Max(x => x.Sum);
                Console.WriteLine(max1);

                var min1 = db.InsuredEvents.Min(x => x.Sum);
                Console.WriteLine(min1);




                Console.WriteLine("--------------------------------------------------------------------");
                Console.WriteLine("\n\n\tAdd Ex\n\n");



                var ex = from br in db.Brunches
                         join co in db.Contracts
                         on br.BrunchID equals co.BranchID
                         join ie in db.InsuredEvents
                         on co.ContractID equals ie.ContractID
                         where DbFunctions.DiffMonths(co.CreateDate, DateTime.Now) < 8
                         select new { bName = br.Name, Price = ie.Sum };

                var ex1 = from x in ex
                          group x by x.bName into g
                          select new { Name = g.Key, Price = g.Sum(x => x.Price) };

                var ex2 = ex1.OrderByDescending(x => x.Price)
                            .Take(1);

                foreach (var x in ex2)
                    Console.WriteLine($"{x.Name} : {x.Price}");




                System.Data.SqlClient.SqlParameter contr = new System.Data.SqlClient.SqlParameter("@minPrice", db.InsuredEvents.Max(x => x.Sum));
                System.Data.SqlClient.SqlParameter contr1 = new System.Data.SqlClient.SqlParameter("@maxPrice", db.InsuredEvents.Min(x => x.Sum));
                var InsuredEvents = db.Database.SqlQuery<InsuredEvent>("GetPriceStats @minPrice, @maxPrice", contr, contr1);
                Console.WriteLine($"{contr.Value} - {contr1.Value}");



                Console.Write("\n--ASYNC--\n");
                //MultithreadedRecording();

                Task task = PrintAllNames();
                task.Wait();

                Console.Write("\n--Time--\n");
                //Racing();



                Console.Read();
            }
        }
        static int Count = 0;
        static int LoopCounter = 0;
        static object locker = new object();
        public static async Task<List<String>> GetLastClientNames()
        {
            using (Model1 db = new Model1()) {
                List<string> names = new List<string>();
                var clients = await db.Clients.OrderByDescending(x => x.ClientID)
                                              .Take(5).ToListAsync();
                clients.ForEach(x => names.Add(x.FullName.ToString()));
                return names;
            }
        }

        public static async Task<List<String>> GetLastBrunchNames()
        {
            using (Model1 db = new Model1())
            {
                List<string> names = new List<string>();
                var brunches = await db.Brunches.OrderByDescending(x => x.BrunchID)
                                                .Take(5).ToListAsync();
                brunches.ForEach(x => names.Add(x.Name.ToString()));
                return names;
            }
        }

        public static async Task<List<String>> GetLastClientNamesAsync()
        {
            return await Task.Run(() => GetLastClientNames());
        }
        public static async Task<List<String>> GetLastBrunchNamesAsync()
        {
            return await Task.Run(() => GetLastBrunchNames());
        }

        public static async Task<List<String>> GetNames()
        {
            List<string> endnames = new List<string>();

            var result_1 = GetLastClientNamesAsync();
            var result_2 = GetLastBrunchNamesAsync();
            await Task.WhenAll(result_1, result_2);
            result_1.Result.ForEach(n => endnames.Add(n));
            result_2.Result.ForEach(n => endnames.Add(n));

            return endnames;
        }

        public static async Task PrintAllNames()
        {
            var names = await GetNames();
            names.ForEach(n => Console.WriteLine(n));
        }
        public static void MultithreadedRecording()
        {
            Console.WriteLine("Start !");

            var t1 = new Thread(LoopRecording);

            t1.Start();
            Console.WriteLine("Stop !");
        }

        public static void LoopRecording()
        {
            using (Model1 db = new Model1())
            {

                lock (locker)
                {
                    for (int i = 25; i <= 29; i++)
                    {
                        db.Clients.Add(new Client { ClientID = i, FullName = "Test" + i, PhoneNumber = "Nomer" + i, IdentificationCode = i, Coefficient = i });
                        db.Brunches.Add(new Brunch { BrunchID = i, Name = "Mesto" + i, City = "Taktak" + i, Adress = "XZXZ" + i, PhoneNumber = "Nomer"  + i });
                        Count++;
                        db.SaveChanges();
                    }
                }
            }
        }

        public static void Racing()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var result_1 = GetLastClientNamesAsync();
            var result_2 = GetLastBrunchNamesAsync();

            Task.WaitAll(result_1, result_2);
            stopwatch.Stop();
            Console.WriteLine("Async:" + stopwatch.ElapsedMilliseconds);


            Thread t1 = new Thread(TestLoopMethod);
            Thread t2 = new Thread(TestLoopMethod);

            stopwatch.Start();
            t1.Start();
            t2.Start();
            stopwatch.Stop();
            Console.WriteLine("Thread:" + stopwatch.ElapsedMilliseconds);
        }

        static void TestLoopMethod()
        {
            lock (locker)
            {
                if (LoopCounter == 0)
                {
                    ThreadMethodClientNames();
                    LoopCounter++;
                }
                if (LoopCounter == 1)
                {
                    ThreadMethodBrunchNames();
                }
            }
        }

        public static void ThreadMethodBrunchNames()
        {
            List<string> names = new List<string>();
            using (Model1 db = new Model1()) 
            {
                var menus = db.Brunches.Take(5).ToList();
                menus.ForEach(x => names.Add(x.Name.ToString()));
            }

        }

        public static void ThreadMethodClientNames()
        {
            List<string> names = new List<string>();
            using (Model1 db = new Model1()) 
            {
                var personals = db.Clients.Take(5).ToList();
                personals.ForEach(x => names.Add(x.FullName.ToString()));
            }

        }
    }
}
