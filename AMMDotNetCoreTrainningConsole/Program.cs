﻿// See https://aka.ms/new-console-template for more information
using AMMDotNetCoreTrainningConsole;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");
//Console.ReadLine();

//AdoDotNetExample adoDotNet = new AdoDotNetExample();
////adoDotNet.Read();
////adoDotNet.Write();
////adoDotNet.ReadById();
////adoDotNet.Update();
//adoDotNet.Delete();

DapperExample dapperExample = new DapperExample();
dapperExample.Read();

Console.ReadKey();