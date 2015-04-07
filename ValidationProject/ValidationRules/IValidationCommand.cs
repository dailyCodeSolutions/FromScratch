namespace ValidationProject.ValidationRules
{
    public interface IValidationCommand
    {
        void ExecuteCommand(ValidationCommandContext context);
        string ErrorMessage { get; set; }
    }
}