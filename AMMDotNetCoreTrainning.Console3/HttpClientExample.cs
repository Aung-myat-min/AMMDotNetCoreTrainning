using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AMMDotNetCoreTrainning.Console3;

public class HttpClientExample
{
    private readonly HttpClient _httpClient;
    private readonly string _todosEndPoint = "https://jsonplaceholder.typicode.com/todos";

    public HttpClientExample()
    {
        _httpClient = new HttpClient();
    }

    public async Task GetTodos()
    {
        var response = await _httpClient.GetAsync(_todosEndPoint);
        if (response.IsSuccessStatusCode)
        {
            string jsonString = await response.Content.ReadAsStringAsync();
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
        var response = await _httpClient.GetAsync($"{_todosEndPoint}/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine("Todo Not Found!\n");
            return;
        }
        if (response.IsSuccessStatusCode)
        {
            string jsonString = await response.Content.ReadAsStringAsync();
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

        StringContent todoSC = CreateJsonSC<TodoModel>(newTodo);
        var response = await _httpClient.PostAsync(_todosEndPoint, todoSC);
        if (response.IsSuccessStatusCode)
        {
            string jsonString = await response.Content.ReadAsStringAsync();
            var todo = JsonToObject<TodoModel>(jsonString);
            PrintTodo(todo);
        }
    }

    public async Task MarkCompletedTodo(int id)
    {
        var response = await _httpClient.GetAsync($"{_todosEndPoint}/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine("Todo Not Found!\n");
            return;
        }
        if (response.IsSuccessStatusCode)
        {
            string jsonString = await response.Content.ReadAsStringAsync();
            var todo = JsonToObject<TodoModel>(jsonString);

            if (todo.completed == true)
            {
                Console.WriteLine("Already Completed!");
                return;
            }
            todo.completed = true;

            var todoSC = CreateJsonSC(todo);
            var res = await _httpClient.PutAsync($"{_todosEndPoint}/{id}", todoSC);
            if (res.IsSuccessStatusCode)
            {
                string updatedJson = await res.Content.ReadAsStringAsync();
                var updatedTodo = JsonToObject<TodoModel>(updatedJson);
                PrintTodo(updatedTodo);
            }
        }
    }

    public async Task EditTodo(int id, string title)
    {
        var response = await _httpClient.GetAsync($"{_todosEndPoint}/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine("Todo Not Found!\n");
            return;
        }
        if (response.IsSuccessStatusCode)
        {
            string jsonString = await response.Content.ReadAsStringAsync();
            var todo = JsonToObject<TodoModel>(jsonString);

            var newTodo = new TodoModel
            {
                id = todo.id,
                title = title,
                completed = todo.completed,
                userId = todo.userId
            };

            var todoSC = CreateJsonSC(newTodo);
            var res = await _httpClient.PutAsync($"{_todosEndPoint}/{id}", todoSC);
            if (res.IsSuccessStatusCode)
            {
                string updatedJson = await res.Content.ReadAsStringAsync();
                var updatedTodo = JsonToObject<TodoModel>(updatedJson);
                PrintTodo(updatedTodo);
            }
        }
    }

    public async Task DeleteTodo(int id)
    {
        var response = await _httpClient.DeleteAsync($"{_todosEndPoint}/{id}");
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
