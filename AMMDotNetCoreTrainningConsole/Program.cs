// See https://aka.ms/new-console-template for more information
using AMMDotNetCoreTrainningConsole;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");
//Console.ReadLine();

AdoDotNetExample adoDotNet = new AdoDotNetExample();
adoDotNet.Read();

Console.ReadKey();