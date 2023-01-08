namespace WebApp.Views;

using System.Text.Json.Serialization;

public class UserSearchView
{
  [JsonPropertyName("id")] public int Id { get; set; }

  [JsonPropertyName("username")] public string Username { get; set; }
}