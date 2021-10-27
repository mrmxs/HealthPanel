namespace Stats.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }        
    }
}


// CREATE TABLE todoitems (
// 	id serial PRIMARY KEY,
// 	name VARCHAR NOT NULL,
// 	iscomplete bool 
// );