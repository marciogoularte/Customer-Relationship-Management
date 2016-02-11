namespace CRM.Services.Logic.Contracts.Marketing
{
    using System.Collections.Generic;

    using Base;
    using Data.ViewModels.Marketing.Partners;

    public interface IMediaServices : IService
    {
        List<string> Index();

        MediaViewModel MediaInformation(int mediaId);

        MediaViewModel MediaDetails(int mediaId);

        List<MediaViewModel> ReadMedia(string searchboxMedia, bool showAll);

        MediaViewModel CreateMedia(MediaViewModel media);

        MediaViewModel UpdateMedia(MediaViewModel media);

        MediaViewModel DestroyMedia(MediaViewModel media);

        string GetMediaById(int mediaId);
    }
}