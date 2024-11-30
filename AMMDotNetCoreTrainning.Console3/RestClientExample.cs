using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AMMDotNetCoreTrainning.Console3;

public class RestClientExample
{
    private readonly RestClient _restClient;
    private readonly string _todosEndPoint = "https://jsonplaceholder.typicode.com/todos";

    public RestClientExample()
    {
        _restClient = new RestClient();
    }

    public async Task GetTodos()
    {
        RestRequest request = new RestRequest(_todosEndPoint, Method.Get);
        var response = await _restClient.ExecuteAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string jsonString = response.Content!;
            List<TodoModel> todos = JsonConvert.DeserializeObject<List<TodoModel>>(jsonString)!;
            Console.WriteLine("Here are your todos:");
            foreach (TodoModel todo in todos)
            {
                PrintTodo(todo);
            }
        }
    }

    public async Task GetTodoById(int id)
    {
        RestRequest request = new RestRequest($"{_todosEndPoint}/{id}", Method.Get);
        var response = await _restClient.ExecuteAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine("Todo Not Found!\n");
            return;
        }
        if (response.IsSuccessStatusCode)
        {
            string jsonString = response.Content!;
            var todo = JsonToObject<TodoModel>(jsonString);
            Console.WriteLine("Here is the todo: ");
            PrintTodo(todo);
        }
    }

    public async Task CreateTodo(int userId, string title)
    {
        TodoModel newTodo = new TodoModel
        {
            userId = userId,
            title = title,
            completed = false
        };

        RestRequest request = new RestRequest(_todosEndPoint, Method.Get);
        request.AddBody(newTodo);
        var response = await _restClient.PostAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string jsonString = response.Content!;
            var todo = JsonToObject<TodoModel>(jsonString);
            PrintTodo(todo);
        }
    }

    public async Task MarkCompletedTodo(int id)
    {
        RestRequest request = new RestRequest($"{_todosEndPoint}/{id}", Method.Get);
        var response = await _restClient.GetAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine("Todo Not Found!\n");
            return;
        }
        if (response.IsSuccessStatusCode)
        {
            string jsonString = response.Content!;
            var todo = JsonToObject<TodoModel>(jsonString);

            if (todo.completed == true)
            {
                Console.WriteLine("Already Completed!");
                return;
            }
            todo.completed = true;

            RestRequest updateRequest = new RestRequest($"{_todosEndPoint}/{id}", Method.Put);
            request.AddBody(todo);
            var res = await _restClient.PutAsync(updateRequest);
            if (res.IsSuccessStatusCode)
            {
                string updatedJson = res.Content!;
                var updatedTodo = JsonToObject<TodoModel>(updatedJson);
                PrintTodo(updatedTodo);
            }
        }
    }

    public async Task EditTodo(int id, string title)
    {
        RestRequest request = new RestRequest($"{_todosEndPoint}/{id}", Method.Get);
        var response = await _restClient.GetAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine("Todo Not Found!\n");
            return;
        }
        if (response.IsSuccessStatusCode)
        {
            string jsonString = response.Content!;
            var todo = JsonToObject<TodoModel>(jsonString);

            var newTodo = new TodoModel
            {
                id = todo.id,
                title = title,
                completed = todo.completed,
                userId = todo.userId
            };

            RestRequest updateRequest = request.AddBody(newTodo);

            var res = await _restClient.PutAsync(updateRequest);
            if (res.IsSuccessStatusCode)
            {
                string updatedJson = res.Content!;
                var updatedTodo = JsonToObject<TodoModel>(updatedJson);
                PrintTodo(updatedTodo);
            }
        }
    }

    public async Task DeleteTodo(int id)
    {
        RestRequest request = new RestRequest($"{_todosEndPoint}/{id}", Method.Get);
        var response = await _restClient.DeleteAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine("Todo Not Found!\n");
            return;
        }
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Todo Deleted!");
        }
    }

    //model and private methods
    public class TodoModel
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }
    }

    private string SerialiseObj<T>(T obj)
    {
        return JsonConvert.SerializeObject(obj);
    }
    private StringContent CreateJsonSC<T>(T obj)
    {
        string jsonStr = SerialiseObj<T>(obj);
        StringContent sc = new StringContent(jsonStr, Encoding.UTF8, Application.Json);
        return sc;
    }
    private T JsonToObject<T>(string json)
    {
        T obj = JsonConvert.DeserializeObject<T>(json)!;
        return obj;
    }
    private void PrintTodo(TodoModel todo)
    {
        Console.WriteLine($"Todo Id: \t{todo.id}\nTodo Title: \t{todo.title}\nTodo Id: \t{todo.id}\nCompleted: \t{todo.completed}\nUser Id: \t{todo.userId}\n");
    }
}
