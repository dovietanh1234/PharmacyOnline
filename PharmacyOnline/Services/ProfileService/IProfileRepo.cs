using PharmacyOnline.DTO.ProfileDTO;
using PharmacyOnline.Entities;
using PharmacyOnline.Models.ProfileModel;

namespace PharmacyOnline.Services.ProfileService
{
    public interface IProfileRepo
    {
        Task<object> createProfile(ProfileModel model, int idCandidate, string fileThumbnail, string fileCV);

        Task<object> updateProfile(ProfileModel model, string idProfileDetail, string fileThumbnail, string fileCV);

        Task<object> cancelSubmitted( string idProfileDetail);

        Task<object> deleteSubmitted(string idProfileDetail);

        Task<object> Submit(string idProfileDetail);

        Task<object> detailProfile(string idProfileDetail);

        Task<List<ProfileDTO>> GetPLocal(int candidateId);

        Task<List<ProfileDTO>> GetHistory(int candidateId);

        // ADMIN
        Task<List<ProfileDTO>> GetHistoryProfileAdmin(int page, string? isQualified);
        Task<List<ProfileDTO>> GetListSubmittedAdmin(int page);

        Task<List<ProfileDTO>> searchSdtGmailAdmin(string search, int page);

        Task<object> ApprovingAdmin(string idProfileDetail, int isQualified, emailModel? body, PersonalDetail Element);


    }
}
