// See https://aka.ms/new-console-template for more information
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

//DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
//dapperExample.Create("DapperTitle", "DapperContent", "DapperAuthor");
//dapperExample.Edit(1015);
//dapperExample.Update();
//dapperExample.Update(11, "Blog Without Content", "AMMHEHE");
//dapperExample.Delete();

EFCoreExample eFCoreExample = new EFCoreExample();
//eFCoreExample.Read();
//eFCoreExample.Write("EfCoreExample", "AMM");
//eFCoreExample.Edit(8);
//eFCoreExample.Update();
eFCoreExample.Delete(9);

Console.ReadKey();