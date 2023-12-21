using PharmacyOnline.DTO.ManageCandidatesDTO;

namespace PharmacyOnline.Services.ManageCandidates
{
    public interface IManageCanRepo
    {
        Task<List<ListCans>> getListCans(int page);

        Task<List<ListCans>> searchCans(string search ,int page);

        Task<object> toggleU(int id);

        Task<object> getProfileToken(int id);

        Task<object> updateCan(updateCan model, string url);
    }
}
