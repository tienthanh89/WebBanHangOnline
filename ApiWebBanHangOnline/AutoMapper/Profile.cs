using Model.Domain;
using Model.Dto;

namespace ApiWebBanHangOnline.AutoMapper
{
    public class Profile:global::AutoMapper.Profile
    {
        public Profile()
        {
            CreateMap<TbProductCategory, TbProductCategoryDto>().ReverseMap();
        }
    }
}
