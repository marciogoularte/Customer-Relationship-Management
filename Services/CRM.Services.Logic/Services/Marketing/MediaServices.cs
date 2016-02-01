using AutoMapper.QueryableExtensions;

namespace CRM.Services.Logic.Services.Marketing
{
    using System.Linq;
    using System.Collections.Generic;

    using CRM.Data;
    using CRM.Data.Models.Marketing;
    using Data.ViewModels.Marketing.Partners;
    using Contracts.Marketing;

    public class MediaServices : IMediaServices
    {
        private ICRMData Data { get; set; }

        public MediaServices(ICRMData data)
        {
            this.Data = data;
        }

        public List<string> Index()
        {
            var media = this.Data.Media
                .All()
                .Select(p => p.Name)
                .ToList();

            return media;
        }
        
        public MediaViewModel MediaInformation(int mediaId)
        {
            var media = this.Data.Media
                .All()
                .ProjectTo<MediaViewModel>()
                .FirstOrDefault(p => p.Id == mediaId);

            return media;
        }

        public MediaViewModel MediaDetails(int mediaId)
        {
            var Media = this.Data.Media
                .All()
                .ProjectTo<MediaViewModel>()
                .FirstOrDefault(t => t.Id == mediaId);

            return Media;
        }

        public List<MediaViewModel> ReadMedia(string searchboxMedia)
        {
            List<MediaViewModel> media;

            if (string.IsNullOrEmpty(searchboxMedia) || searchboxMedia == "")
            {
                media = this.Data.Media
                .All()
                .Where(m => m.IsVisible)
                .ProjectTo<MediaViewModel>()
                .ToList();
            }
            else
            {
                media = this.Data.Media
                .All()
                .ProjectTo<MediaViewModel>()
                .Where(p => p.Name.Contains(searchboxMedia))
                .ToList();
            }

            return media;
        }

        public MediaViewModel CreateMedia(MediaViewModel media)
        {
            if (media == null)
            {
                return null;
            }

            var newMedia = new Media
            {
                Name = media.Name,
                Address = media.Address,
                PhoneNumber = media.PhoneNumber,
                Email = media.Email,
                AllMedia = media.AllMedia,
                IsVisible = media.IsVisible
            };

            this.Data.Media.Add(newMedia);
            this.Data.SaveChanges();

            media.Id = newMedia.Id;

            return media;
        }

        public MediaViewModel UpdateMedia(MediaViewModel media)
        {
            var mediaFromDb = this.Data.Media.All()
                   .FirstOrDefault(p => p.Id == media.Id);

            if (media == null || mediaFromDb == null)
            {
                return media;
            }
            
            mediaFromDb.Name = media.Name;
            mediaFromDb.Address = media.Address;
            mediaFromDb.PhoneNumber = media.PhoneNumber;
            mediaFromDb.Email = media.Email;
            mediaFromDb.AllMedia = media.AllMedia;
            mediaFromDb.IsVisible = media.IsVisible;

            this.Data.SaveChanges();

            return media;
        }

        public MediaViewModel DestroyMedia(MediaViewModel media)
        {
            this.Data.Media.Delete(media.Id);
            this.Data.SaveChanges();

            return media;
        }

        public string GetMediaById(int mediaId)
        {
            var media = this.Data.Media
                .All()
                .Where(t => t.Id == mediaId)
                .Select(t => t.Name.ToString())
                .FirstOrDefault();

            return media;
        }
    }
}
