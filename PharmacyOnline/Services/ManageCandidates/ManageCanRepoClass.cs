using Microsoft.EntityFrameworkCore;
using PharmacyOnline.Common;
using PharmacyOnline.DTO.ManageCandidatesDTO;
using PharmacyOnline.Entities;
using PharmacyOnline.Models.Candidate;

namespace PharmacyOnline.Services.ManageCandidates
{
    public class ManageCanRepoClass : IManageCanRepo
    {
        private readonly PharmacyOnlineContext _context;
        
        public ManageCanRepoClass(PharmacyOnlineContext context)
        {
            _context = context;
        }

        public async Task<List<ListCans>> getListCans(int page)
        {
            List<ListCans> listCans = new List<ListCans>();
            try
            {

                var cans = _context.Candidates.AsQueryable();
                var results = PaginationList<Entities.Candidate>.Create(cans, page, 10);

                

                foreach (var candidate in results)
                {
                    listCans.Add( new ListCans()
                    {
                        id = candidate.Id,
                        username = candidate.Username,
                        thumbnail = candidate.Thumbnail,
                        role = candidate.Role,
                        email = candidate.Email,
                        isActive = candidate.IsAtive,
                    });
                }
                return listCans;
            }catch (Exception ex)
            {
                return listCans;
            }
        }



        public async Task<List<ListCans>> searchCans(string search, int page)
        {
            List<ListCans> listCans = new List<ListCans>();
            try
            {
                var cans = _context.Candidates.AsQueryable().Where( c => (c.Username.Contains(search) || c.Email.Contains(search)) );
                var results = PaginationList<Entities.Candidate>.Create(cans, page, 10);

                

                foreach (var candidate in results)
                {
                    listCans.Add(new ListCans()
                    {
                        id=candidate.Id,
                        username = candidate.Username,
                        thumbnail = candidate.Thumbnail,
                        role = candidate.Role,
                        email = candidate.Email,
                        isActive = candidate.IsAtive,
                    });
                }
                return listCans;
            }
            catch(Exception ex)
            {
                return listCans;
            }
        }

        public async Task<object> toggleU(int id)
        {
            try
            {
                var can = await _context.Candidates.FirstOrDefaultAsync( c => c.Id == id );

                if (can == null) return new result { status = 404, statusMessage = "candidate not found" };

                if ( can.IsAtive == true )
                {
                    can.IsAtive = false;
                    await _context.SaveChangesAsync();
                    return new result { status = 200, statusMessage = "User has been blocked done!" };
                }
                else
                {
                    can.IsAtive = true;
                    await _context.SaveChangesAsync();
                    return new result { status = 200, statusMessage = "UnBlock user successfully" };
                }

            }
            catch (Exception ex)
            {
                return new result { status = 401, statusMessage = $"Error! has error occured ${ex}" };
            }
        }

        public async Task<object> getProfileToken(int id)
        {
            try
            {
                var can = await _context.Candidates.FirstOrDefaultAsync(p => p.Id  == id);

                if (can == null) return new result { status = 400, statusMessage = "not found the product" };

                return new ListCans()
                {
                    id = can.Id,
                    username = can.Username,
                    thumbnail = can.Thumbnail,
                    role = can.Role,
                    email = can.Email,
                    isActive = can.IsAtive,
                };
            }
            catch (Exception ex)
            {
                return new result { status = 401, statusMessage = $"Error! has error occured ${ex}" };
            }
        }

        public async Task<object> updateCan(updateCan model, string url)
        {
            try
            {
                var can = await _context.Candidates.FirstOrDefaultAsync(p => p.Id == model.id);
                if (can == null) return new result { status = 400, statusMessage = "not found the product" };

                can.Username = model.username == "" ? can.Username : model.username;
                can.Thumbnail = model.thumbnail == null ? can.Thumbnail : url;

                await _context.SaveChangesAsync();  

                return new ListCans()
                {
                    id = can.Id,
                    username = can.Username,
                    thumbnail = can.Thumbnail,
                    role = can.Role,
                    email = can.Email,
                    isActive = can.IsAtive,
                };
            }
            catch (Exception ex)
            {
                return new result { status = 401, statusMessage = $"Error! has error occured ${ex}" };
            }
        }



    }
}
