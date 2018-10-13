using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorshopBase.Models;

namespace WorshopBase.Data
{
    public static class DbInitializer
    {
        public static void Initialize(WorkshopContext db)
        {
            db.Database.EnsureCreated();

            int breakdownsNumber = 40;
            int carsNumber = 150;
            int ordersNumber = 300;
            int ownersNumber = 80;
            int partsNumber = 50;
            int postsNumber = 10;
            int workersNumber = 50;

            InitializeOwners(db, ownersNumber);
            InitializePosts(db, postsNumber);
            InitializeParts(db, partsNumber);
            InitializeWorkers(db, workersNumber, postsNumber);
            InitializeCars(db, carsNumber, ownersNumber);
            InitializeOrders(db, ordersNumber, carsNumber, workersNumber);
            InitializeBreakdowns(db, breakdownsNumber, ordersNumber, partsNumber, workersNumber);            
        }

        private static void InitializeBreakdowns(WorkshopContext db, int breakdownsNumber, int ordersNumber, int partsNumber, int workersNumber)
        {
            db.Database.EnsureCreated();

            //проверка, занесены ли данные в Workers
            if (db.Breakdowns.Any())
            {
                return; //бд иницилизирована
            }

            int orderID;
            int partID;
            int workerID;

            Random randomObj = new Random(1);

            for(int breakdownID = 1; breakdownID <= breakdownsNumber; breakdownID++)
            {
                orderID = db.Orders.Skip(randomObj.Next(0, db.Orders.Count() - 2)).First().orderID;
                partID = db.Parts.Skip(randomObj.Next(0, db.Parts.Count() - 2)).First().partID;
                workerID = db.Workers.Skip(randomObj.Next(0, db.Workers.Count() - 2)).First().workerID;

                db.Breakdowns.Add(new Breakdown
                {
                    orderID = orderID,
                    partID = partID,
                    workerID = workerID
                });
            }

            db.SaveChanges();

        }

        private static void InitializeWorkers(WorkshopContext db, int workersNumber, int postsNumber)
        {
            db.Database.EnsureCreated();

            //проверка, занесены ли данные в Workers
            if (db.Workers.Any())
            {
                return; //бд иницилизирована
            }

            string fioWorker;
            int postID;
            DateTime dateOfEmployment;
            DateTime? dateOfDismissal;
            decimal salary;

            Random randomObj = new Random(1);

            //заполнение таблицы
            for(int workerID = 1; workerID <= workersNumber; workerID++)
            {
                var date = new DateTime(randomObj.Next(1990, 2018),
                    randomObj.Next(1, 12),
                    randomObj.Next(1, 28));
                fioWorker = MyRandom.RandomString(15);
                postID = db.Posts.Skip(randomObj.Next(0, db.Posts.Count() - 2)).First().postID;
                var twoDate = new DateTime(randomObj.Next(1, 10),
                    randomObj.Next(1, 12), 
                    randomObj.Next(1, 28));
                if (workerID % 5 == 0)
                {
                    dateOfDismissal = date.Add(new TimeSpan(twoDate.Ticks));
                }
                else
                {
                    dateOfDismissal = null;
                }
                dateOfEmployment = date;
                salary = randomObj.Next(100, 1000);

                db.Workers.Add(new Worker
                {
                    fioWorker = fioWorker,
                    postID = postID,
                    dateOfDismissal = dateOfDismissal,
                    dateOfEmployment = dateOfEmployment,
                    salary = salary,
                });

                db.SaveChanges();
            }
        }

        private static void InitializePosts(WorkshopContext db, int postsNumber)
        {
            db.Database.EnsureCreated();

            //проверка, занесены ли данные в Workers
            if (db.Posts.Any())
            {
                return; //бд иницилизирована
            }

            string postName;
            string descriptionPost;

            Random randomObj = new Random(1);

            //заполенение таблицы
            for(int postID = 1; postID <= postsNumber; postID++)
            {
                postName = MyRandom.RandomString(10);
                descriptionPost = MyRandom.RandomString(15);

                db.Posts.Add(new Post
                {
                    postName = postName,
                    descriptionPost = descriptionPost
                });
            }

            db.SaveChanges();
        }

        private static void InitializeOwners(WorkshopContext db, int ownersNumber)
        {
            db.Database.EnsureCreated();

            //проверка, занесены ли данные в Owners
            if(db.Owners.Any())
            {
                return; //бд иницилизирована
            }

            int driverLicense;
            string fioOwner;
            string adress;
            int phone;

            Random randomObj = new Random(1);

            //заполнение таблицы
            for(int ownerID = 1; ownerID <= ownersNumber; ownerID++)
            {
                driverLicense = randomObj.Next(1, 3000);
                fioOwner = MyRandom.RandomString(15);
                adress = MyRandom.RandomString(15);
                phone = randomObj.Next(1, 1000000);

                db.Owners.Add(new Owner
                {
                    driverLicense = driverLicense,
                    fioOwner = fioOwner,
                    adress = adress,
                    phone = phone
                });
            }

            //сохранение изменений в бд, связанную с объектом контекста
            db.SaveChanges();
        }

        private static void InitializeParts(WorkshopContext db, int partsNumber)
        {
            db.Database.EnsureCreated();

            //проверка, занесены ли данные в Owners
            if (db.Parts.Any())
            {
                return; //бд иницилизирована
            }

            string partName;
            decimal price;
            string descriptionPart;

            Random randomObj = new Random(1);

            for(int partID = 1; partID <= partsNumber; partID++)
            {
                partName = MyRandom.RandomString(15);
                price = randomObj.Next(5, 100);
                descriptionPart = MyRandom.RandomString(20);

                db.Parts.Add(new Part
                {
                    partName = partName,
                    price = price,
                    descriptionPart = descriptionPart
                });
            }

            //сохранение изменений в бд, связанную с объектом контекста
            db.SaveChanges();
        }

        private static void InitializeCars(WorkshopContext db, int carsNumber, int ownersNumber)
        {
            db.Database.EnsureCreated();

            //проверка, занесены ли данные в Owners
            if (db.Cars.Any())
            {
                return; //бд иницилизирована
            }

            int ownerID;
            string model;
            int vis;
            string colour;
            string stateNumber;
            int yearOfIssue;
            int bodyNumber;
            int engineNumber;

            Random randomObj = new Random(1);

            for(int carID = 1; carID <= carsNumber; carID++)
            {
                ownerID = db.Owners.Skip(randomObj.Next(0, db.Owners.Count() - 2)).First().ownerID;
                model = MyRandom.RandomString(10);
                vis = randomObj.Next(10, 100);
                colour = MyRandom.RandomString(10);
                stateNumber = MyRandom.RandomString(9);
                yearOfIssue = randomObj.Next(1, 4);
                bodyNumber = randomObj.Next(1, 10);
                engineNumber = randomObj.Next(1, 10);

                db.Cars.Add(new Car
                {
                    ownerID = ownerID,
                    model = model,
                    vis = vis,
                    colour = colour,
                    stateNumber = stateNumber,
                    yearOfIssue = yearOfIssue,
                    bodyNumber = bodyNumber,
                    engineNumber = engineNumber
                });
            }

            //сохранение изменений в бд, связанную с объектом контекста
            db.SaveChanges();
        }

        private static void InitializeOrders(WorkshopContext db, int ordersNumber, int carsNumber, int workersNumber)
        {
            db.Database.EnsureCreated();

            //проверка, занесены ли данные в Owners
            if (db.Orders.Any())
            {
                return; //бд иницилизирована
            }

            int carID;
            DateTime dateReceipt;
            DateTime? dateCompletion;
            int workerID;

            Random randomObj = new Random(DateTime.Now.Month * 30 + DateTime.Now.Year * 365 + DateTime.Now.Day 
                + DateTime.Now.Minute);

            for(int orderID = 1; orderID <= ordersNumber; orderID++)
            {
                var date = new DateTime(randomObj.Next(1990, 2008),
                    randomObj.Next(1, 12),
                    randomObj.Next(1, 28));
                carID = db.Cars.Skip(randomObj.Next(0, db.Cars.Count() - 2)).First().carID;
                dateReceipt = date;
                var twoDate = new DateTime(randomObj.Next(1, 10),
                    randomObj.Next(1, 12),
                    randomObj.Next(1, 28));
                if (orderID % 5 == 0)
                {
                    dateCompletion = date.Add(new TimeSpan(twoDate.Ticks));
                }
                else
                {
                    dateCompletion = null;
                }
                workerID = db.Workers.Skip(randomObj.Next(0, db.Workers.Count() - 2)).First().workerID;

                db.Orders.Add(new Order
                {
                    carID = carID,
                    dateReceipt = dateReceipt,
                    dateCompletion = dateCompletion,
                    workerID = workerID
                });
            }

            //сохранение изменений в бд, связанную с объектом контекста
            db.SaveChanges();
        }
    }
}
