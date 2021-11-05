using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YOGBIS.Common.VModels;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DbModels;

namespace YOGBIS.UI.ViewComponents
{
    public class UserNameViewComponent: ViewComponent
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public UserNameViewComponent(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userFromDb = _uow.kullaniciRepository.GetFirstOrDefault(u => u.Id == claims.Value);

            var kullaniciToDb = _mapper.Map<Kullanici, KullaniciVM>(userFromDb);

            return View(kullaniciToDb);
        }
    }
}
