namespace TaskBoardApp.Data.ViewModels
{
    public class BoardViewModel
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public IEnumerable<TaskViewModel> Tasks { get; init; } = new List<TaskViewModel>();
    }
}
