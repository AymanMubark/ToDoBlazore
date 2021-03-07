using Blazore_Start_bwa_test.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;

namespace Blazore_Start_bwa_test.Pages
{
    public class IndexBase : ComponentBase
    {
        protected Item addItem = new Item();
        protected List<Item> items;
        protected string message = "";
        [Inject]
        protected HttpClient Http { get; set; }

        protected MessageType messageType { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await GetItems();
        }

        protected async Task AddToDo()
        {
            try
            {
                await Http.PostAsJsonAsync("https://localhost:5001/items", addItem);
                Item item = addItem;
                items.Add(item);
                messageType = MessageType.success;
                message = "Add Succefull";
            }
            catch
            {

            }
        }

        protected async Task SetFinsh(int id)
        {
            try
            {
                await Http.PutAsJsonAsync($"https://localhost:5001/items/{id}", new Item());
                await GetItems();
            }
            catch
            {

            }
        }

        protected async Task Delete(int id)
        {
            try
            {
                await Http.DeleteAsync($"https://localhost:5001/items/{id}");
                await GetItems();
                messageType = MessageType.success;
                message = "Delete Succefull";
            }
            catch
            {

            }
        }

        protected async Task GetItems()
        {
            items = await Http.GetFromJsonAsync<List<Item>>("https://localhost:5001/items");
        }
    }
}
