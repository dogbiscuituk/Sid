namespace ToyGraf.Models.Commands
{
    public class GraphTitleCommand : GraphPropertyCommand<string>
    {
        public GraphTitleCommand(string value) :
            base(value,
                g => g.Title,
                (g, v) => g.Title = v) { }

        protected override string Detail => "title";
    }
}
