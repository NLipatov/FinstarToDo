namespace FinstarToDo.Services.HashCalculator
{
    public interface IHashCalculatorService
    {
        string CreateAnMD5Hash(string input);
    }
}