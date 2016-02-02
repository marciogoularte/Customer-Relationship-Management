namespace CRM.Web.Areas.Marketing.Controllers
{
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Data.Models;
    using Common.Base;
    using CRM.Data.Models.Marketing;
    using Services.Logic.Contracts.Marketing;
    using Services.Data.ViewModels.Marketing.Social;

    [Authorize]
    public class SocialPartnersController : Controller
    {
        private readonly ISocialPartnersServices socialPartners;

        public SocialPartnersController(ISocialPartnersServices socialPartners)
        {
            this.socialPartners = socialPartners;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AllFacebookSocialPartners()
        {
            return PartialView("_FacebookSocialPartners");
        }

        [HttpGet]
        public ActionResult AllTwitterSocialPartners()
        {
            return PartialView("_TwitterSocialPartners");
        }

        [HttpGet]
        public ActionResult AllGooglePlusSocialPartners()
        {
            return PartialView("_GooglePlusSocialPartners");
        }

        [HttpGet]
        public ActionResult AllLinkedInSocialPartners()
        {
            return PartialView("_LinkedInSocialPartners");
        }

        [HttpGet]
        public ActionResult FacebookSocialPartnerInformation(int socialPartnerId)
        {
            var socialPartnerInfo = socialPartners.SocialPartnerInformation(socialPartnerId, SocialSystemType.Facebook);

            return View("SocialPartnerInformation", socialPartnerInfo);
        }

        [HttpGet]
        public ActionResult TwitterSocialPartnersInformation(int socialPartnerId)
        {
            var socialPartnerInfo = socialPartners.SocialPartnerInformation(socialPartnerId, SocialSystemType.Twitter);

            return View("SocialPartnerInformation", socialPartnerInfo);
        }

        [HttpGet]
        public ActionResult GooglePlusSocialPartnersInformation(int socialPartnerId)
        {
            var socialPartnerInfo = socialPartners.SocialPartnerInformation(socialPartnerId, SocialSystemType.GooglePlus);

            return View("SocialPartnerInformation", socialPartnerInfo);
        }

        [HttpGet]
        public ActionResult LinkedInSocialPartnersInformation(int socialPartnerId)
        {
            var socialPartnerInfo = socialPartners.SocialPartnerInformation(socialPartnerId, SocialSystemType.LinkedIn);

            return View("SocialPartnerInformation", socialPartnerInfo);
        }

        public ActionResult FacebookSocialPartnersNames([DataSourceRequest]DataSourceRequest request)
        {
            var socialPartnersNames = socialPartners.Index(SocialSystemType.Facebook);

            return Json(socialPartnersNames, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TwitterSocialPartnersNames([DataSourceRequest]DataSourceRequest request)
        {
            var socialPartnersNames = socialPartners.Index(SocialSystemType.Twitter);

            return Json(socialPartnersNames, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GooglePlusSocialPartnersNames([DataSourceRequest]DataSourceRequest request)
        {
            var socialPartnersNames = socialPartners.Index(SocialSystemType.GooglePlus);

            return Json(socialPartnersNames, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LinkedInSocialPartnersNames([DataSourceRequest]DataSourceRequest request)
        {
            var socialPartnersNames = socialPartners.Index(SocialSystemType.LinkedIn);

            return Json(socialPartnersNames, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FacebookReadSocialPartners([DataSourceRequest] DataSourceRequest request, string searchTerm, bool showAll)
        {
            var readSocialPartners = socialPartners.ReadSocialPartners(searchTerm, SocialSystemType.Facebook, showAll);

            return Json(readSocialPartners.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult TwitterReadSocialPartners([DataSourceRequest] DataSourceRequest request, string searchTerm, bool showAll)
        {
            var readSocialPartners = socialPartners.ReadSocialPartners(searchTerm, SocialSystemType.Twitter, showAll);

            return Json(readSocialPartners.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GooglePlusReadSocialPartners([DataSourceRequest] DataSourceRequest request, string searchTerm, bool showAll)
        {
            var readSocialPartners = socialPartners.ReadSocialPartners(searchTerm, SocialSystemType.GooglePlus, showAll);

            return Json(readSocialPartners.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult LinkedInReadSocialPartners([DataSourceRequest] DataSourceRequest request, string searchTerm, bool showAll)
        {
            var readSocialPartners = socialPartners.ReadSocialPartners(searchTerm, SocialSystemType.LinkedIn, showAll);

            return Json(readSocialPartners.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult CreateFacebookSocialPartner([DataSourceRequest]  DataSourceRequest request, SocialPartnerViewModel socialPartnerModel)
        {
            if (socialPartnerModel == null || !ModelState.IsValid)
            {
                return Json(new[] { socialPartnerModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            socialPartnerModel.SocialSystem = SocialSystemType.Facebook;
            var createdSocialPartner = this.socialPartners.CreateSocialPartner(socialPartnerModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, createdSocialPartner.Id.ToString(), ActivityTargetType.SocialPartner, loggedUserId);

            socialPartnerModel.Id = createdSocialPartner.Id;

            return Json(new[] { socialPartnerModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult CreateTwitterSocialPartner([DataSourceRequest]  DataSourceRequest request, SocialPartnerViewModel socialPartnerModel)
        {
            if (socialPartnerModel == null || !ModelState.IsValid)
            {
                return Json(new[] { socialPartnerModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            socialPartnerModel.SocialSystem = SocialSystemType.Twitter;
            var createdSocialPartner = this.socialPartners.CreateSocialPartner(socialPartnerModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, createdSocialPartner.Id.ToString(), ActivityTargetType.SocialPartner, loggedUserId);

            socialPartnerModel.Id = createdSocialPartner.Id;

            return Json(new[] { socialPartnerModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult CreateGooglePlusSocialPartner([DataSourceRequest]  DataSourceRequest request, SocialPartnerViewModel socialPartnerModel)
        {
            if (socialPartnerModel == null || !ModelState.IsValid)
            {
                return Json(new[] { socialPartnerModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            socialPartnerModel.SocialSystem = SocialSystemType.GooglePlus;
            var createdSocialPartner = this.socialPartners.CreateSocialPartner(socialPartnerModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, createdSocialPartner.Id.ToString(), ActivityTargetType.SocialPartner, loggedUserId);

            socialPartnerModel.Id = createdSocialPartner.Id;

            return Json(new[] { socialPartnerModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult CreateLinkedInSocialPartner([DataSourceRequest]  DataSourceRequest request, SocialPartnerViewModel socialPartnerModel)
        {
            if (socialPartnerModel == null || !ModelState.IsValid)
            {
                return Json(new[] { socialPartnerModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            socialPartnerModel.SocialSystem = SocialSystemType.LinkedIn;
            var createdSocialPartner = this.socialPartners.CreateSocialPartner(socialPartnerModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, createdSocialPartner.Id.ToString(), ActivityTargetType.SocialPartner, loggedUserId);

            socialPartnerModel.Id = createdSocialPartner.Id;

            return Json(new[] { socialPartnerModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult UpdateFacebookSocialPartner([DataSourceRequest] DataSourceRequest request, SocialPartnerViewModel socialPartnerModel)
        {
            if (socialPartnerModel == null || !ModelState.IsValid)
            {
                return Json(new[] { socialPartnerModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            socialPartnerModel.SocialSystem = SocialSystemType.Facebook;
            var updatedSocialPartner = socialPartners.UpdateSocialPartner(socialPartnerModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedSocialPartner.Id.ToString(), ActivityTargetType.SocialPartner, loggedUserId);

            return Json((new[] { socialPartnerModel }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult UpdateTwitterSocialPartner([DataSourceRequest] DataSourceRequest request, SocialPartnerViewModel socialPartnerModel)
        {
            if (socialPartnerModel == null || !ModelState.IsValid)
            {
                return Json(new[] { socialPartnerModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            socialPartnerModel.SocialSystem = SocialSystemType.Twitter;
            var updatedSocialPartner = socialPartners.UpdateSocialPartner(socialPartnerModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedSocialPartner.Id.ToString(), ActivityTargetType.SocialPartner, loggedUserId);

            return Json((new[] { socialPartnerModel }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult UpdateGooglePlusSocialPartner([DataSourceRequest] DataSourceRequest request, SocialPartnerViewModel socialPartnerModel)
        {
            if (socialPartnerModel == null || !ModelState.IsValid)
            {
                return Json(new[] { socialPartnerModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            socialPartnerModel.SocialSystem = SocialSystemType.GooglePlus;
            var updatedSocialPartner = socialPartners.UpdateSocialPartner(socialPartnerModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedSocialPartner.Id.ToString(), ActivityTargetType.SocialPartner, loggedUserId);

            return Json((new[] { socialPartnerModel }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult UpdateLinkedInSocialPartner([DataSourceRequest] DataSourceRequest request, SocialPartnerViewModel socialPartnerModel)
        {
            if (socialPartnerModel == null || !ModelState.IsValid)
            {
                return Json(new[] { socialPartnerModel }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            socialPartnerModel.SocialSystem = SocialSystemType.LinkedIn;
            var updatedSocialPartner = socialPartners.UpdateSocialPartner(socialPartnerModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedSocialPartner.Id.ToString(), ActivityTargetType.SocialPartner, loggedUserId);

            return Json((new[] { socialPartnerModel }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult DestroyFacebookSocialPartner([DataSourceRequest] DataSourceRequest request, SocialPartnerViewModel socialPartnerModel)
        {
            socialPartnerModel.SocialSystem = SocialSystemType.Facebook;
            var deletedSocialPartner = socialPartners.DestroySocialPartner(socialPartnerModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, deletedSocialPartner.Id.ToString(), ActivityTargetType.SocialPartner, loggedUserId);

            return Json(new[] { socialPartnerModel }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult DestroyTwitterSocialPartner([DataSourceRequest] DataSourceRequest request, SocialPartnerViewModel socialPartnerModel)
        {
            socialPartnerModel.SocialSystem = SocialSystemType.Twitter;
            var deletedSocialPartner = socialPartners.DestroySocialPartner(socialPartnerModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, deletedSocialPartner.Id.ToString(), ActivityTargetType.SocialPartner, loggedUserId);

            return Json(new[] { socialPartnerModel }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult DestroyGooglePlusSocialPartner([DataSourceRequest] DataSourceRequest request, SocialPartnerViewModel socialPartnerModel)
        {
            socialPartnerModel.SocialSystem = SocialSystemType.GooglePlus;
            var deletedSocialPartner = socialPartners.DestroySocialPartner(socialPartnerModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, deletedSocialPartner.Id.ToString(), ActivityTargetType.SocialPartner, loggedUserId);

            return Json(new[] { socialPartnerModel }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Dealer")]
        public JsonResult DestroyLinkedInSocialPartner([DataSourceRequest] DataSourceRequest request, SocialPartnerViewModel socialPartnerModel)
        {
            socialPartnerModel.SocialSystem = SocialSystemType.LinkedIn;
            var deletedSocialPartner = socialPartners.DestroySocialPartner(socialPartnerModel);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, deletedSocialPartner.Id.ToString(), ActivityTargetType.SocialPartner, loggedUserId);

            return Json(new[] { socialPartnerModel }, JsonRequestBehavior.AllowGet);
        }
    }
}