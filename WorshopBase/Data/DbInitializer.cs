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

            int breakdownsNumber = 400;
            int carsNumber = 1500;
            int ordersNumber = 3000;
            int ownersNumber = 800;
            int partsNumber = 500;
            int postsNumber = 100;
            int workersNumber = 500;

            InitializeBreakdowns(db, breakdownsNumber, ordersNumber, partsNumber, workersNumber);
            InitializeOwners(db, ownersNumber);
            InitializeWorkers(db, workersNumber, postsNumber);
            InitializePosts(db, postsNumber);
            InitializePart(db, partsNumber);
            InitializeCars(db, carsNumber, ownersNumber);
            InitializeOrders(db, ordersNumber, carsNumber, workersNumber);
        }

        private static void InitializeBreakdowns(WorkshopContext db, int breakdownsNumber, int ordersNumber, int partsNumber, int workersNumber)
        {
            db.Database.EnsureCreated();

            //проверка, занесены ли данные в Workers
            if (db.Workers.Any())
            {
                return; //бд иницилизирована
            }

            int orderID;
            int partID;
            int workerID;

            Random randomObj = new Random(1);

            for(int breakdownID = 1; breakdownID <= breakdownsNumber; breakdownID++)
            {
                orderID = randomObj.Next(1, ordersNumber - 1);
                partID = randomObj.Next(1, partsNumber - 1);
                workerID = randomObj.Next(1, workersNumber - 1);

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
            DateTime dateOfDismissal;
            decimal salary;

            Random randomObj = new Random(1);

            //заполнение таблицы
            for(int workerID = 1; workerID <= workersNumber; workerID++)
            {
                fioWorker = MyRandom.RandomString(15);
                postID = randomObj.Next(1, postsNumber - 1);
                dateOfDismissal = new DateTime(randomObj.Next(1990, 2018),
                    randomObj.Next(1, 12),
                    randomObj.Next(1, 31));
                dateOfEmployment = new DateTime(randomObj.Next(1990, 2018),
                    randomObj.Next(1, 12),
                    randomObj.Next(1, 31));
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

        private static void InitializePart(WorkshopContext db, int partsNumber)
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
                ownerID = randomObj.Next(1, ownersNumber - 1);
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
            DateTime dateCompletion;
            int workerID;

            Random randomObj = new Random(1);

            for(int orderID = 1; orderID <= ordersNumber; orderID++)
            {
                carID = randomObj.Next(1, carsNumber - 1);
                dateReceipt = new DateTime(randomObj.Next(1990, 2018),
                    randomObj.Next(1, 12),
                    randomObj.Next(1, 31));
                dateCompletion = new DateTime(randomObj.Next(1990, 2018),
                    randomObj.Next(1, 12),
                    randomObj.Next(1, 31));
                workerID = randomObj.Next(1, workersNumber - 1);
            }
        }
    }
}
