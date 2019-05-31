using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;

namespace MS.Support
{
    //internal class Program
    //{
    //    public IConfiguration Configuration { get; set; }

        //    private static void Main(string[] args)
        //    {
        //        Console.WriteLine("Starting MS.Support");

        //        {
        //            Console.WriteLine("Starting Account Management Web Api");
        //            Process process0 = new Process();
        //            process0.StartInfo.CreateNoWindow = false;
        //            process0.StartInfo.UseShellExecute = false;
        //            process0.StartInfo.RedirectStandardOutput = false;
        //            process0.StartInfo.FileName = @"\\StartAccountManagementWebApi.bat";
        //            process0.StartInfo.Arguments = runningPath;
        //            process0.Start();
        //        }

        //        if (model.InventoryManagementWebApi == true)
        //        {
        //            Console.WriteLine("Starting Inventory Management Web Api");
        //            Process process1 = new Process();
        //            process1.StartInfo.CreateNoWindow = false;
        //            process1.StartInfo.UseShellExecute = false;
        //            process1.StartInfo.RedirectStandardOutput = false;
        //            process1.StartInfo.FileName = runningPath + @"MS.Support\StartInventoryManagementWebApi.bat";
        //            process1.StartInfo.Arguments = runningPath;
        //            process1.Start();
        //        }

        //        if (model.SalesOrderManagementWebApi == true)
        //        {
        //            Console.WriteLine("Starting Sales Order Management Web Api");
        //            Process process2 = new Process();
        //            process2.StartInfo.CreateNoWindow = false;
        //            process2.StartInfo.UseShellExecute = false;
        //            process2.StartInfo.RedirectStandardOutput = false;
        //            process2.StartInfo.FileName = runningPath + @"MS.Support\StartSalesOrderManagementWebApi.bat";
        //            process2.StartInfo.Arguments = runningPath;
        //            process2.Start();
        //        }

        //        if (model.PurchaseOrderManagementWebApi == true)
        //        {
        //            Console.WriteLine("Starting Purchase Order Management Web Api");
        //            Process process3 = new Process();
        //            process3.StartInfo.CreateNoWindow = false;
        //            process3.StartInfo.UseShellExecute = false;
        //            process3.StartInfo.RedirectStandardOutput = false;
        //            process3.StartInfo.FileName = runningPath + @"MS.Support\StartPurchaseOrderManagementWebApi.bat";
        //            process3.StartInfo.Arguments = runningPath;
        //            process3.Start();
        //        }

        //        if (model.InventoryManagementMessageQueue == true)
        //        {
        //            Console.WriteLine("Starting Inventory Management Message Queue");

        //            Process process4 = new Process();
        //            process4.StartInfo.CreateNoWindow = false;
        //            process4.StartInfo.UseShellExecute = false;
        //            process4.StartInfo.RedirectStandardOutput = false;
        //            process4.StartInfo.FileName = runningPath + @"MS.Support\StartInventoryManagementMessageQueue.bat";
        //            process4.StartInfo.Arguments = runningPath;
        //            process4.Start();
        //        }

        //        if (model.SalesOrderManagementMessageQueue == true)
        //        {
        //            Console.WriteLine("Starting Sales Order Management Message Queue");

        //            Process process5 = new Process();
        //            process5.StartInfo.CreateNoWindow = false;
        //            process5.StartInfo.UseShellExecute = false;
        //            process5.StartInfo.RedirectStandardOutput = false;
        //            process5.StartInfo.FileName = runningPath + @"MS.Support\StartSalesOrderManagementMessageQueue.bat";
        //            process5.StartInfo.Arguments = runningPath;
        //            process5.Start();
        //        }

        //        if (model.PurchaseOrderManagementMessageQueue == true)
        //        {
        //            Console.WriteLine("Starting Purchase Order Management Message Queue");

        //            Process process6 = new Process();
        //            process6.StartInfo.CreateNoWindow = false;
        //            process6.StartInfo.UseShellExecute = false;
        //            process6.StartInfo.RedirectStandardOutput = false;
        //            process6.StartInfo.FileName = runningPath + @"MS.Support\StartPurchaseOrderManagementMessageQueue.bat";
        //            process6.StartInfo.Arguments = runningPath;
        //            process6.Start();
        //        }

        //        if (model.PurchaseOrderManagementMessageQueue == true)
        //        {
        //            Console.WriteLine("Starting Purchase Order Management Message Queue");

        //            Process process7 = new Process();
        //            process7.StartInfo.CreateNoWindow = false;
        //            process7.StartInfo.UseShellExecute = false;
        //            process7.StartInfo.RedirectStandardOutput = false;
        //            process7.StartInfo.FileName = runningPath + @"MS.Support\StartPurchaseOrderManagementMessageQueue.bat";
        //            process7.StartInfo.Arguments = runningPath;
        //            process7.Start();
        //        }

        //        if (model.LoggingManagementMessageQueue == true)
        //        {
        //            Console.WriteLine("Starting Logging Management Message Queue");

        //            Process process8 = new Process();
        //            process8.StartInfo.CreateNoWindow = false;
        //            process8.StartInfo.UseShellExecute = false;
        //            process8.StartInfo.RedirectStandardOutput = false;
        //            process8.StartInfo.FileName = runningPath + @"MS.Support\StartLoggingManagementMessageQueue.bat";
        //            process8.StartInfo.Arguments = runningPath;
        //            process8.Start();
        //        }

        //        Console.ReadKey();
        //    }
        //}

        internal class Program
        {
            public IConfiguration Configuration { get; set; }

            private static void Main(string[] args)
            {
                string path = Directory.GetCurrentDirectory();

                Console.WriteLine("The current directory is {0}", path);

                char delimiter = '\\';

                string[] paths = path.Split(delimiter);

                int parts = paths.Length;
                string runningPath = string.Empty;

                foreach (string filePath in paths)
                {
                    if (filePath.ToLower() == "support")
                    {
                        break;
                    }

                    runningPath = runningPath + filePath + @"\";
                }

                Console.WriteLine(runningPath);

                SupportModel model = new SupportModel();
                model.AccountManagementWebApi = true;
                model.InventoryManagementMessageQueue = true;
                model.InventoryManagementWebApi = true;
                model.PurchaseOrderManagementMessageQueue = true;
                model.PurchaseOrderManagementWebApi = true;
                model.SalesOrderManagementMessageQueue = true;
                model.SalesOrderManagementWebApi = true;
                model.LoggingManagementMessageQueue = true;

                string jsonFile = $"appsettings.json";

                var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile(jsonFile, optional: true, reloadOnChange: true);

                IConfigurationRoot configuration = builder.Build();

                //configuration.GetSection("SupportModel").Bind(model);


                Console.WriteLine("Starting MS.Support");

                if (model.AccountManagementWebApi == true)
                {
                    Console.WriteLine("Starting Account Management Web Api");
                    Process process0 = new Process();
                    process0.StartInfo.CreateNoWindow = false;
                    process0.StartInfo.UseShellExecute = false;
                    process0.StartInfo.RedirectStandardOutput = false;
                    process0.StartInfo.FileName = runningPath + @"MS.Support\StartAccountManagementWebApi.bat";
                    process0.StartInfo.Arguments = runningPath;
                    process0.Start();
                }

                if (model.InventoryManagementWebApi == true)
                {
                    Console.WriteLine("Starting Inventory Management Web Api");
                    Process process1 = new Process();
                    process1.StartInfo.CreateNoWindow = false;
                    process1.StartInfo.UseShellExecute = false;
                    process1.StartInfo.RedirectStandardOutput = false;
                    process1.StartInfo.FileName = runningPath + @"MS.Support\StartInventoryManagementWebApi.bat";
                    process1.StartInfo.Arguments = runningPath;
                    process1.Start();
                }

                if (model.SalesOrderManagementWebApi == true)
                {
                    Console.WriteLine("Starting Sales Order Management Web Api");
                    Process process2 = new Process();
                    process2.StartInfo.CreateNoWindow = false;
                    process2.StartInfo.UseShellExecute = false;
                    process2.StartInfo.RedirectStandardOutput = false;
                    process2.StartInfo.FileName = runningPath + @"MS.Support\StartSalesOrderManagementWebApi.bat";
                    process2.StartInfo.Arguments = runningPath;
                    process2.Start();
                }

                if (model.PurchaseOrderManagementWebApi == true)
                {
                    Console.WriteLine("Starting Purchase Order Management Web Api");
                    Process process3 = new Process();
                    process3.StartInfo.CreateNoWindow = false;
                    process3.StartInfo.UseShellExecute = false;
                    process3.StartInfo.RedirectStandardOutput = false;
                    process3.StartInfo.FileName = runningPath + @"MS.Support\StartPurchaseOrderManagementWebApi.bat";
                    process3.StartInfo.Arguments = runningPath;
                    process3.Start();
                }

                if (model.InventoryManagementMessageQueue == true)
                {
                    Console.WriteLine("Starting Inventory Management Message Queue");

                    Process process4 = new Process();
                    process4.StartInfo.CreateNoWindow = false;
                    process4.StartInfo.UseShellExecute = false;
                    process4.StartInfo.RedirectStandardOutput = false;
                    process4.StartInfo.FileName = runningPath + @"MS.Support\StartInventoryManagementMessageQueue.bat";
                    process4.StartInfo.Arguments = runningPath;
                    process4.Start();
                }

                if (model.SalesOrderManagementMessageQueue == true)
                {
                    Console.WriteLine("Starting Sales Order Management Message Queue");

                    Process process5 = new Process();
                    process5.StartInfo.CreateNoWindow = false;
                    process5.StartInfo.UseShellExecute = false;
                    process5.StartInfo.RedirectStandardOutput = false;
                    process5.StartInfo.FileName = runningPath + @"MS.Support\StartSalesOrderManagementMessageQueue.bat";
                    process5.StartInfo.Arguments = runningPath;
                    process5.Start();
                }

                if (model.PurchaseOrderManagementMessageQueue == true)
                {
                    Console.WriteLine("Starting Purchase Order Management Message Queue");

                    Process process6 = new Process();
                    process6.StartInfo.CreateNoWindow = false;
                    process6.StartInfo.UseShellExecute = false;
                    process6.StartInfo.RedirectStandardOutput = false;
                    process6.StartInfo.FileName = runningPath + @"MS.Support\StartPurchaseOrderManagementMessageQueue.bat";
                    process6.StartInfo.Arguments = runningPath;
                    process6.Start();
                }

                if (model.PurchaseOrderManagementMessageQueue == true)
                {
                    Console.WriteLine("Starting Purchase Order Management Message Queue");

                    Process process7 = new Process();
                    process7.StartInfo.CreateNoWindow = false;
                    process7.StartInfo.UseShellExecute = false;
                    process7.StartInfo.RedirectStandardOutput = false;
                    process7.StartInfo.FileName = runningPath + @"MS.Support\StartPurchaseOrderManagementMessageQueue.bat";
                    process7.StartInfo.Arguments = runningPath;
                    process7.Start();
                }

                if (model.LoggingManagementMessageQueue == true)
                {
                    Console.WriteLine("Starting Logging Management Message Queue");

                    Process process8 = new Process();
                    process8.StartInfo.CreateNoWindow = false;
                    process8.StartInfo.UseShellExecute = false;
                    process8.StartInfo.RedirectStandardOutput = false;
                    process8.StartInfo.FileName = runningPath + @"MS.Support\StartLoggingManagementMessageQueue.bat";
                    process8.StartInfo.Arguments = runningPath;
                    process8.Start();
                }

                Console.ReadKey();
            }
        }
    }