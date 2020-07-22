using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IMemeGenerator
    {
        Task<string> GetWorkoutMeme();
    }
}