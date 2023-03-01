namespace FinstarToDo.Services.HashCalculator
{
    public interface IHashCalculatorService
    {
        string CalculateMD5Hash(string input);
    }
}