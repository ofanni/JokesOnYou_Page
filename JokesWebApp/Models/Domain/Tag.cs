namespace JokesWebApp.Models.Domain;

public class Tag
{
    public Guid Id { get; set; }
    public string JokeQuestion { get; set; }
    public string JokeAnswer { get; set; }
    
    public ICollection<BlogPost> BlogPosts { get; set; }


}