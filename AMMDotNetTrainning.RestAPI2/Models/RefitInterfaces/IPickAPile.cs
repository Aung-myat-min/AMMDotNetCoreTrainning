using Refit;

namespace AMMDotNetTrainning.RestAPI2.Models.RefitInterfaces
{
    public interface IPickAPile
    {
        [Get("/pick-a-pile")]
        Task<List<PickAPileModel>> GetPiles();

        [Get("/pick-a-pile/{id}")]
        Task<List<AnswerModel>> GetAPile(int id);
    }
}
