﻿using AMMDotNetCoreTrainning.Console3;

//HttpClientExample httpClientExample = new HttpClientExample();

//await httpClientExample.GetTodos();
//await httpClientExample.CreateTodo(1, "Complete HttpClient PostMethod!");
//await httpClientExample.GetTodoById(201);
//await httpClientExample.GetTodoById(1);
//await httpClientExample.EditTodo(2, "Hello!");
//await httpClientExample.DeleteTodo(200);
//await httpClientExample.MarkCompletedTodo(4);
//await httpClientExample.MarkCompletedTodo(300);

//RestClientExample restClientExample = new RestClientExample();

//await restClientExample.GetTodos();
//await restClientExample.CreateTodo(1, "Complete HttpClient PostMethod!");
//await restClientExample.GetTodoById(201);
//await restClientExample.GetTodoById(1);
//await restClientExample.EditTodo(2, "Hello!");
//await restClientExample.DeleteTodo(200);
//await restClientExample.MarkCompletedTodo(4);
//await restClientExample.MarkCompletedTodo(300);


RefitExample refitExample = new RefitExample();

Console.Write("Waiting for the api....");
Console.ReadLine();

//refitExample.GetOneBlog(1);
//refitExample.CreateBlog(new BlogModel
//{
//    BlogTitle = "Refit Example",
//    BlogAuthor = "AMM",
//    BlogContent = "This is just an example!"
//});
//refitExample.UpdateBlog(3, new BlogModel
//{
//    BlogTitle = "Refit Example Updated!",
//    BlogAuthor = "AMM",
//    BlogContent = "This is just an example!"
//});
//refitExample.EditBlog(3, new BlogModel
//{
//    BlogTitle = "Refit Example Updated!",
//    BlogAuthor = "AMM",
//    BlogContent = "This is just an example! And it is updated using patch!"
//});
refitExample.DeleteBlog(3);
refitExample.GetBlogsAndPrint();


Console.ReadLine();