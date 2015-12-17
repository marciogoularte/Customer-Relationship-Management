namespace CRM.Web.Areas.Marketing.Controllers
{
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Microsoft.AspNet.Identity;

    using Data.Models;
    using Common.Base;
    using Services.Logic.Contracts.Marketing;
    using Services.Data.ViewModels.Marketing.Partners;

    public class MediaController : Controller
    {
        private readonly IMediaServices medias;
        
        public MediaController(IMediaServices medias)
        {
            this.medias = medias;
        }

        [HttpGet]
        public ActionResult AllMedia()
        {
            return PartialView("_Media");
        }

        [HttpGet]
        public ActionResult MediaInformation(int mediaId)
        {
            var media = medias.MediaInformation(mediaId);

            return View("MediaInformation", media);
        }

        public ActionResult MediaNames([DataSourceRequest]DataSourceRequest request)
        {
            var mediaNames = medias.Index();

            return Json(mediaNames, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadMedia([DataSourceRequest] DataSourceRequest request, string searchTerm)
        {
            var readMedia = medias.ReadMedia(searchTerm);

            return Json(readMedia.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateMedia([DataSourceRequest]  DataSourceRequest request, MediaViewModel media)
        {
            if (media == null || !ModelState.IsValid)
            {
                return Json(new[] { media }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var createdMedia = medias.CreateMedia(media);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Create, createdMedia.Id.ToString(), ActivityTargetType.Media, loggedUserId);

            media.Id = createdMedia.Id;

            return Json(new[] { media }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateMedia([DataSourceRequest] DataSourceRequest request, MediaViewModel media)
        {
            if (media == null || !ModelState.IsValid)
            {
                return Json(new[] { media }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }

            var updatedMedia = medias.UpdateMedia(media);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Edit, updatedMedia.Id.ToString(), ActivityTargetType.Media, loggedUserId);

            return Json((new[] { media }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DestroyMedia([DataSourceRequest] DataSourceRequest request, MediaViewModel media)
        {
            var deletedMedia = medias.DestroyMedia(media);

            var loggedUserId = User.Identity.GetUserId();
            Base.CreateActivity(ActivityType.Delete, deletedMedia.Id.ToString(), ActivityTargetType.Media, loggedUserId);

            return Json(new[] { media }, JsonRequestBehavior.AllowGet);
        }
    }
}